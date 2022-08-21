using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Elmah.Models;
using Framework.Helpers;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Elmah.EFCoreRepositories
{
    public class ElmahSourceRepository
        : IElmahSourceRepository
    {
        private readonly ILogger<ElmahSourceRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahSourceRepository(EFDbContext dbcontext, ILogger<ElmahSourceRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ElmahSourceDataModel> SearchQuery(
            ElmahSourceAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahSource

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Source!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Source!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Source!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Source) ||
                            query.SourceSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Source!, "%" + query.Source + "%") ||
                            query.SourceSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Source!, query.Source + "%") ||
                            query.SourceSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Source!, "%" + query.Source))

                select new ElmahSourceDataModel
                {

                        Source = t.Source,
                };

            // 1. Without Paging And OrderBy
            if (!withPagingAndOrderBy)
                return queryable;

            // 2. With Paging And OrderBy
            var orderBys = QueryOrderBySetting.Parse(query.OrderBys);
            if (orderBys.Any())
            {
                queryable = queryable.OrderBy(QueryOrderBySetting.GetOrderByExpression(orderBys));
            }

            queryable = queryable.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

            return queryable;
        }

        public async Task<PagedResponse<ElmahSourceDataModel[]>> Search(
            ElmahSourceAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahSourceDataModel>();
                return new PagedResponse<ElmahSourceDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahSourceDataModel[]>>.FromResult(new PagedResponse<ElmahSourceDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<ElmahSource> GetIQueryableByPrimaryIdentifierList(
            List<ElmahSourceIdentifier> ids)
        {
            var idList = ids.Select(t => t.Source).ToList();
            var queryable =
                from t in _dbcontext.ElmahSource
                where idList.Contains(t.Source)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ElmahSourceIdentifier> ids)
        {
            try
            {
                var queryable = GetIQueryableByPrimaryIdentifierList(ids);
                var result = await queryable.BatchDeleteAsync();

                return await Task<Response>.FromResult(
                    new Response
                    {
                        Status = HttpStatusCode.OK,
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ElmahSource> newEFItems = new List<ElmahSource>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ElmahSource
                        {
                            Source = item.Source,
                        };
                        _dbcontext.ElmahSource.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ElmahSource
                             where

                             t.Source == item.Source
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Source = item.Source;
                        }
                    }
                }
                await _dbcontext.SaveChangesAsync();

                // 3.2 Load Response
                var identifierListToloadResponseItems = new List<string>();

                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in newEFItems
                        select t.Source);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.Source);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ElmahSource
                    where identifierListToloadResponseItems.Contains(t.Source)

                    select new ElmahSourceDataModel
                    {

                        Source = t.Source,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.Source == t.Source)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.Source == t.Source)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDModel<ElmahSourceIdentifier, ElmahSourceDataModel>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ElmahSourceDataModel>> Update(ElmahSourceIdentifier id, ElmahSourceDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahSource
                     where

                    t.Source == id.Source
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Source = input.Source;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahSourceDataModel>>.FromResult(
                    new Response<ElmahSourceDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceDataModel
                        {
                            Source = existing.Source,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahSourceDataModel>> Get(ElmahSourceIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahSource.SingleOrDefault(
                    t =>

                    t.Source == id.Source
                );

                if (existing == null)
                    return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahSourceDataModel>>.FromResult(
                    new Response<ElmahSourceDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceDataModel
                        {
                            Source = existing.Source,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahSourceDataModel>> Create(ElmahSourceDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahSource
                {
                            Source = input.Source,
                };
                await _dbcontext.ElmahSource.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahSourceDataModel>>.FromResult(
                    new Response<ElmahSourceDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceDataModel
                        {
                            Source = toInsert.Source,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ElmahSourceIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahSource
                     where

                    t.Source == id.Source
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahSource.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response>.FromResult(
                    new Response
                    {
                        Status = HttpStatusCode.OK,
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<NameValuePair> GetCodeListQuery(
            ElmahSourceAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahSource

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Source!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Source!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Source!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Source) ||
                            query.SourceSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Source!, "%" + query.Source + "%") ||
                            query.SourceSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Source!, query.Source + "%") ||
                            query.SourceSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Source!, "%" + query.Source))

                select new NameValuePair
                {

                        Value = t.Source,
                        Name = t.Source,
                };

            // 1. Without Paging And OrderBy
            if (!withPagingAndOrderBy)
                return queryable;

            // 2. With Paging And OrderBy
            var orderBys = QueryOrderBySetting.Parse(query.OrderBys);
            if (orderBys.Any())
            {
                queryable = queryable.OrderBy(QueryOrderBySetting.GetOrderByExpression(orderBys));
            }

            queryable = queryable.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

            return queryable;
        }

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = GetCodeListQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = GetCodeListQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<NameValuePair>();
                return new PagedResponse<NameValuePair[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<NameValuePair[]>>.FromResult(new PagedResponse<NameValuePair[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

    }
}

