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

        private IQueryable<ElmahUserModel> SearchQuery(
            ElmahUserAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahUser

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.User!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.User!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.User!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.User) ||
                            query.UserSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.User!, "%" + query.User + "%") ||
                            query.UserSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.User!, query.User + "%") ||
                            query.UserSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.User!, "%" + query.User))

                select new ElmahUserModel
                {

                        User = t.User,
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
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
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

        private IQueryable<ElmahUser> GetIQueryableByPrimaryIdentifierList(
            List<ElmahUserIdentifier> ids)
        {
            var idList = ids.Select(t => t.User).ToList();
            var queryable =
                from t in _dbcontext.ElmahUser
                where idList.Contains(t.User)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ElmahUserIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserModel> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserModel>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserModel>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ElmahUser> newEFItems = new List<ElmahUser>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ElmahUser
                        {
                            User = item.User,
                        };
                        _dbcontext.ElmahUser.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ElmahUser
                             where

                             t.User == item.User
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.User = item.User;
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
                        select t.User);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.User);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ElmahUser
                    where identifierListToloadResponseItems.Contains(t.User)

                    select new ElmahUserModel
                    {

                        User = t.User,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserModel>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserModel>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.User == t.User)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.User == t.User)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDModel<ElmahUserIdentifier, ElmahUserModel>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ElmahUserModel>> Update(ElmahUserIdentifier id, ElmahUserModel input)
        {
            if (input == null)
                return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahUser
                     where

                    t.User == id.User
                     select t).SingleOrDefault();

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

        public async Task<Response<ElmahUserModel>> Get(ElmahUserIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahUserModel>>.FromResult(new Response<ElmahUserModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahUser.SingleOrDefault(
                    t =>

                    t.User == id.User
                );

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

        public async Task<Response> Delete(ElmahUserIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahUser
                     where

                    t.User == id.User
                     select t).SingleOrDefault();

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

        private IQueryable<NameValuePair> GetCodeListQuery(
            ElmahUserAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahUser

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.User!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.User!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.User!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.User) ||
                            query.UserSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.User!, "%" + query.User + "%") ||
                            query.UserSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.User!, query.User + "%") ||
                            query.UserSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.User!, "%" + query.User))

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
            if (orderBys.Any())
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

