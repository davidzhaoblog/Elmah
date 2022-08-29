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
    public class ElmahStatusCodeRepository
        : IElmahStatusCodeRepository
    {
        private readonly ILogger<ElmahStatusCodeRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahStatusCodeRepository(EFDbContext dbcontext, ILogger<ElmahStatusCodeRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        private IQueryable<ElmahStatusCodeDataModel> SearchQuery(
            ElmahStatusCodeAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahStatusCode

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Name!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Name!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Name!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Name) ||
                            query.NameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Name!, "%" + query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Name!, query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Name!, "%" + query.Name))

                select new ElmahStatusCodeDataModel
                {

                        StatusCode = t.StatusCode,
                        Name = t.Name,
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

        public async Task<ListResponse<ElmahStatusCodeDataModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahStatusCodeDataModel>();
                return new ListResponse<ElmahStatusCodeDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<ElmahStatusCodeDataModel[]>>.FromResult(new ListResponse<ElmahStatusCodeDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<ElmahStatusCodeDataModel>> Update(ElmahStatusCodeIdentifier id, ElmahStatusCodeDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(new Response<ElmahStatusCodeDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahStatusCode
                     where

                    t.StatusCode == id.StatusCode
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(new Response<ElmahStatusCodeDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.StatusCode = input.StatusCode;
                existing.Name = input.Name;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(
                    new Response<ElmahStatusCodeDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeDataModel
                        {
                            StatusCode = existing.StatusCode,
                            Name = existing.Name,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(new Response<ElmahStatusCodeDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahStatusCodeDataModel>> Get(ElmahStatusCodeIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(new Response<ElmahStatusCodeDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahStatusCode.SingleOrDefault(
                    t =>

                    t.StatusCode == id.StatusCode
                );

                if (existing == null)
                    return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(new Response<ElmahStatusCodeDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(
                    new Response<ElmahStatusCodeDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeDataModel
                        {
                            StatusCode = existing.StatusCode,
                            Name = existing.Name,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(new Response<ElmahStatusCodeDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahStatusCodeDataModel>> Create(ElmahStatusCodeDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(new Response<ElmahStatusCodeDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahStatusCode
                {
                            StatusCode = input.StatusCode,
                            Name = input.Name,
                };
                await _dbcontext.ElmahStatusCode.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(
                    new Response<ElmahStatusCodeDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeDataModel
                        {
                            StatusCode = toInsert.StatusCode,
                            Name = toInsert.Name,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahStatusCodeDataModel>>.FromResult(new Response<ElmahStatusCodeDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ElmahStatusCodeIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahStatusCode
                     where

                    t.StatusCode == id.StatusCode
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahStatusCode.Remove(existing);
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
            ElmahStatusCodeAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahStatusCode

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Name!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Name!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Name!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Name) ||
                            query.NameSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Name!, "%" + query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Name!, query.Name + "%") ||
                            query.NameSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Name!, "%" + query.Name))

                select new NameValuePair
                {

                        Name = t.Name,
                        Value = t.StatusCode.ToString(),
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

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = GetCodeListQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = GetCodeListQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<NameValuePair>();
                return new ListResponse<NameValuePair[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<NameValuePair[]>>.FromResult(new ListResponse<NameValuePair[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

    }
}

