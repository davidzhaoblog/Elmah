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
    public class ElmahTypeRepository
        : IElmahTypeRepository
    {
        private readonly ILogger<ElmahTypeRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahTypeRepository(EFDbContext dbcontext, ILogger<ElmahTypeRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ElmahTypeDataModel> SearchQuery(
            ElmahTypeAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahType

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Type!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Type!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Type!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Type) ||
                            query.TypeSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Type!, "%" + query.Type + "%") ||
                            query.TypeSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Type!, query.Type + "%") ||
                            query.TypeSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Type!, "%" + query.Type))

                select new ElmahTypeDataModel
                {

                        Type = t.Type,
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

        public async Task<PagedResponse<ElmahTypeDataModel[]>> Search(
            ElmahTypeAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahTypeDataModel>();
                return new PagedResponse<ElmahTypeDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahTypeDataModel[]>>.FromResult(new PagedResponse<ElmahTypeDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<ElmahType> GetIQueryableByPrimaryIdentifierList(
            List<ElmahTypeIdentifier> ids)
        {
            var idList = ids.Select(t => t.Type).ToList();
            var queryable =
                from t in _dbcontext.ElmahType
                where idList.Contains(t.Type)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ElmahTypeIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ElmahType> newEFItems = new List<ElmahType>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ElmahType
                        {
                            Type = item.Type,
                        };
                        _dbcontext.ElmahType.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ElmahType
                             where

                             t.Type == item.Type
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Type = item.Type;
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
                        select t.Type);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.Type);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ElmahType
                    where identifierListToloadResponseItems.Contains(t.Type)

                    select new ElmahTypeDataModel
                    {

                        Type = t.Type,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.Type == t.Type)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.Type == t.Type)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDModel<ElmahTypeIdentifier, ElmahTypeDataModel>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ElmahTypeDataModel>> Update(ElmahTypeIdentifier id, ElmahTypeDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahTypeDataModel>>.FromResult(new Response<ElmahTypeDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahType
                     where

                    t.Type == id.Type
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahTypeDataModel>>.FromResult(new Response<ElmahTypeDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Type = input.Type;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahTypeDataModel>>.FromResult(
                    new Response<ElmahTypeDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeDataModel
                        {
                            Type = existing.Type,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahTypeDataModel>>.FromResult(new Response<ElmahTypeDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahTypeDataModel>> Get(ElmahTypeIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahTypeDataModel>>.FromResult(new Response<ElmahTypeDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahType.SingleOrDefault(
                    t =>

                    t.Type == id.Type
                );

                if (existing == null)
                    return await Task<Response<ElmahTypeDataModel>>.FromResult(new Response<ElmahTypeDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahTypeDataModel>>.FromResult(
                    new Response<ElmahTypeDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeDataModel
                        {
                            Type = existing.Type,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahTypeDataModel>>.FromResult(new Response<ElmahTypeDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahTypeDataModel>> Create(ElmahTypeDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahTypeDataModel>>.FromResult(new Response<ElmahTypeDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahType
                {
                            Type = input.Type,
                };
                await _dbcontext.ElmahType.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahTypeDataModel>>.FromResult(
                    new Response<ElmahTypeDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeDataModel
                        {
                            Type = toInsert.Type,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahTypeDataModel>>.FromResult(new Response<ElmahTypeDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ElmahTypeIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahType
                     where

                    t.Type == id.Type
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahType.Remove(existing);
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
            ElmahTypeAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahType

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Type!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Type!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Type!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Type) ||
                            query.TypeSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Type!, "%" + query.Type + "%") ||
                            query.TypeSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Type!, query.Type + "%") ||
                            query.TypeSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Type!, "%" + query.Type))

                select new NameValuePair
                {

                        Value = t.Type,
                        Name = t.Type,
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
            ElmahTypeAdvancedQuery query)
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

