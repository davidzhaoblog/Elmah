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
    public class ElmahApplicationRepository
        : IElmahApplicationRepository
    {
        private readonly ILogger<ElmahApplicationRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahApplicationRepository(EFDbContext dbcontext, ILogger<ElmahApplicationRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ElmahApplicationModel> SearchQuery(
            ElmahApplicationAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahApplication

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Application!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Application!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Application!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Application) ||
                            query.ApplicationSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Application!, "%" + query.Application + "%") ||
                            query.ApplicationSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Application!, query.Application + "%") ||
                            query.ApplicationSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Application!, "%" + query.Application))

                select new ElmahApplicationModel
                {

                        Application = t.Application,
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

        public async Task<PagedResponse<ElmahApplicationModel[]>> Search(
            ElmahApplicationAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahApplicationModel>();
                return new PagedResponse<ElmahApplicationModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahApplicationModel[]>>.FromResult(new PagedResponse<ElmahApplicationModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<ElmahApplication> GetIQueryableByPrimaryIdentifierList(
            List<ElmahApplicationIdentifier> ids)
        {
            var idList = ids.Select(t => t.Application).ToList();
            var queryable =
                from t in _dbcontext.ElmahApplication
                where idList.Contains(t.Application)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ElmahApplicationIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationModel> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationModel>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationModel>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ElmahApplication> newEFItems = new List<ElmahApplication>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ElmahApplication
                        {
                            Application = item.Application,
                        };
                        _dbcontext.ElmahApplication.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ElmahApplication
                             where

                             t.Application == item.Application
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Application = item.Application;
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
                        select t.Application);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.Application);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ElmahApplication
                    where identifierListToloadResponseItems.Contains(t.Application)

                    select new ElmahApplicationModel
                    {

                        Application = t.Application,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationModel>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationModel>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.Application == t.Application)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.Application == t.Application)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDModel<ElmahApplicationIdentifier, ElmahApplicationModel>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ElmahApplicationModel>> Update(ElmahApplicationIdentifier id, ElmahApplicationModel input)
        {
            if (input == null)
                return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahApplication
                     where

                    t.Application == id.Application
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Application = input.Application;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahApplicationModel>>.FromResult(
                    new Response<ElmahApplicationModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahApplicationModel
                        {
                            Application = existing.Application,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahApplicationModel>> Get(ElmahApplicationIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahApplication.SingleOrDefault(
                    t =>

                    t.Application == id.Application
                );

                if (existing == null)
                    return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahApplicationModel>>.FromResult(
                    new Response<ElmahApplicationModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahApplicationModel
                        {
                            Application = existing.Application,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahApplicationModel>> Create(ElmahApplicationModel input)
        {
            if (input == null)
                return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahApplication
                {
                            Application = input.Application,
                };
                await _dbcontext.ElmahApplication.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahApplicationModel>>.FromResult(
                    new Response<ElmahApplicationModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahApplicationModel
                        {
                            Application = toInsert.Application,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ElmahApplicationIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahApplication
                     where

                    t.Application == id.Application
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahApplication.Remove(existing);
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
            ElmahApplicationAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahApplication

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Application!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Application!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Application!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Application) ||
                            query.ApplicationSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Application!, "%" + query.Application + "%") ||
                            query.ApplicationSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Application!, query.Application + "%") ||
                            query.ApplicationSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Application!, "%" + query.Application))

                select new NameValuePair
                {

                        Value = t.Application,
                        Name = t.Application,
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
            ElmahApplicationAdvancedQuery query)
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

