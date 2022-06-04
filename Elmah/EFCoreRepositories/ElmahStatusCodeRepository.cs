using Elmah.Models;
using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Framework.Helpers;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Linq.Dynamic.Core;

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

        public async Task<Response<ElmahStatusCodeModel>> Delete(ElmahStatusCodeIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahStatusCode.SingleOrDefault(t => t.StatusCode == id.StatusCode);

                if (existing == null)
                    return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahStatusCode.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahStatusCodeModel>>.FromResult(
                    new Response<ElmahStatusCodeModel>
                    {
                        Status = HttpStatusCode.OK,
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahStatusCodeModel>> Get(ElmahStatusCodeIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahStatusCode.SingleOrDefault(t => t.StatusCode == id.StatusCode);
                if (existing == null)
                    return await Task<Response<ElmahStatusCodeModel>>.FromResult(new Response<ElmahStatusCodeModel> { Status = HttpStatusCode.BadRequest });

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

        public async Task<Response<ElmahStatusCodeModel>> Update(ElmahStatusCodeModel input)
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

        private IQueryable<ElmahStatusCodeModel> GetSearchQuery(
            ElmahStatusCodeAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                Name = !string.IsNullOrEmpty(query.Name),

            };

            var queryable =
                from t in _dbcontext.ElmahStatusCode

                where

                    (!toCompare.Name || t.Name.Contains(query.Name))
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
            if (orderBys.Count() > 0)
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
                var queryableOfTotalCount = GetSearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = GetSearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahStatusCodeModel>();
                return new PagedResponse<ElmahStatusCodeModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse { TotalCount = totalCount, Count = result.Count(), PageIndex = query.PageIndex, PageSize = query.PageSize },
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

        private IQueryable<NameValuePair> GetGetCodeListQuery(
            ElmahStatusCodeAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                Name = !string.IsNullOrEmpty(query.Name),

            };

            var queryable =
                from t in _dbcontext.ElmahStatusCode

                where

                    (!toCompare.Name || t.Name.Contains(query.Name))
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
            if (orderBys.Count() > 0)
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
                var queryableOfTotalCount = GetGetCodeListQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = GetGetCodeListQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<NameValuePair>();
                return new PagedResponse<NameValuePair[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse { TotalCount = totalCount, Count = result.Count(), PageIndex = query.PageIndex, PageSize = query.PageSize },
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

