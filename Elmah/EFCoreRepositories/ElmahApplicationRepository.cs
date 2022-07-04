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

        public async Task<Response<ElmahApplicationModel>> Update(ElmahApplicationModel input)
        {
            if (input == null)
                return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahApplication.SingleOrDefault(t => t.Application == input.Application);

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

        public async Task<Response<ElmahApplicationModel>> Get(ElmahApplicationIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahApplicationModel>>.FromResult(new Response<ElmahApplicationModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahApplication.SingleOrDefault(t => t.Application == id.Application);
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

        public async Task<Response> Delete(ElmahApplicationIdModel id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahApplication.SingleOrDefault(t => t.Application == id.Application);

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

