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

        private IQueryable<ElmahStatusCodeModel> SearchQuery(
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

                select new ElmahStatusCodeModel
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

        public async Task<PagedResponse<ElmahStatusCodeModel[]>> Search(
            ElmahStatusCodeAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahStatusCodeModel>();
                return new PagedResponse<ElmahStatusCodeModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahStatusCodeModel[]>>.FromResult(new PagedResponse<ElmahStatusCodeModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<ElmahStatusCodeModel>> Update(ElmahStatusCodeIdentifier id, ElmahStatusCodeModel input)
        {
            if (input == null)
                return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahStatusCode.SingleOrDefault(t => t.StatusCode == input.StatusCode);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.StatusCode = input.StatusCode;
                existing.Name = input.Name;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahStatusCodeModel>>.FromResult(
                    new Response<ElmahStatusCodeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeModel
                        {
                            StatusCode = existing.StatusCode,
                            Name = existing.Name,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahStatusCodeModel>> Get(ElmahStatusCodeIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahStatusCode.SingleOrDefault(
                    t =>

                    id.StatusCode.HasValue && t.StatusCode == id.StatusCode
                );

                if (existing == null)
                    return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahStatusCodeModel>>.FromResult(
                    new Response<ElmahStatusCodeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeModel
                        {
                            StatusCode = existing.StatusCode,
                            Name = existing.Name,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahStatusCodeModel>> Create(ElmahStatusCodeModel input)
        {
            if (input == null)
                return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahStatusCode
                {
                            StatusCode = input.StatusCode,
                            Name = input.Name,
                };
                await _dbcontext.ElmahStatusCode.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahStatusCodeModel>>.FromResult(
                    new Response<ElmahStatusCodeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahStatusCodeModel
                        {
                            StatusCode = toInsert.StatusCode,
                            Name = toInsert.Name,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ElmahStatusCodeIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahStatusCode.SingleOrDefault(
                    t =>

                    id.StatusCode.HasValue && t.StatusCode == id.StatusCode
                );

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

                        Value = t.StatusCode.ToString(),
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

        public async Task<PagedResponse<NameValuePair[]>> GetCodeList(
            ElmahStatusCodeAdvancedQuery query)
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

