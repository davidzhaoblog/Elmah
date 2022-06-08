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
    public class ElmahUserRepository
        : IElmahUserRepository
    {
        private readonly ILogger<ElmahUserRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahUserRepository(EFDbContext dbcontext, ILogger<ElmahUserRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response> Delete(ElmahUserIdModel id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahUser.SingleOrDefault(t => t.User == id.User);

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahUser.Remove(existing);
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

        public async Task<Response<ElmahUserModel>> Get(ElmahUserIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahUser.SingleOrDefault(t => t.User == id.User);
                if (existing == null)
                    return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahUserModel>>.FromResult(
                    new Response<ElmahUserModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahUserModel
                        {
                            User = existing.User,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahUserModel>> Create(ElmahUserModel input)
        {
            if (input == null)
                return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahUser
                {
                            User = input.User,
                };
                await _dbcontext.ElmahUser.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahUserModel>>.FromResult(
                    new Response<ElmahUserModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahUserModel
                        {
                            User = toInsert.User,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahUserModel>> Update(ElmahUserModel input)
        {
            if (input == null)
                return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahUser.SingleOrDefault(t => t.User == input.User);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.User = input.User;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahUserModel>>.FromResult(
                    new Response<ElmahUserModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahUserModel
                        {
                            User = existing.User,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<ElmahUserModel> SearchQuery(
            ElmahUserAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                User = !string.IsNullOrEmpty(query.User),

            };

            var queryable =
                from t in _dbcontext.ElmahUser

                where

                    (!toCompare.User || t.User.Contains(query.User))
                select new ElmahUserModel
                {

                        User = t.User,
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

        public async Task<PagedResponse<ElmahUserModel[]>> Search(
            ElmahUserAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahUserModel>();
                return new PagedResponse<ElmahUserModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse { TotalCount = totalCount, Count = result.Count(), PageIndex = query.PageIndex, PageSize = query.PageSize },
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahUserModel[]>>.FromResult(new PagedResponse<ElmahUserModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<NameValuePair> GetCodeListQuery(
            ElmahUserAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                User = !string.IsNullOrEmpty(query.User),

            };

            var queryable =
                from t in _dbcontext.ElmahUser

                where

                    (!toCompare.User || t.User.Contains(query.User))
                select new NameValuePair
                {

                        Value = t.User,
                        Name = t.User,
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
            ElmahUserAdvancedQuery query)
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

