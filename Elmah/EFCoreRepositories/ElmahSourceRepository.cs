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

        public async Task<Response<ElmahSourceModel>> Delete(ElmahSourceIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahSource.SingleOrDefault(t => t.Source == id.Source);

                if (existing == null)
                    return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahSource.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahSourceModel>>.FromResult(
                    new Response<ElmahSourceModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceModel
                        {
                            Source = existing.Source,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahSourceModel>> Get(ElmahSourceIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahSource.SingleOrDefault(t => t.Source == id.Source);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.BadRequest });

                return await Task<Response<ElmahSourceModel>>.FromResult(
                    new Response<ElmahSourceModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceModel
                        {
                            Source = existing.Source,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahSourceModel>> Create(ElmahSourceModel input)
        {
            if (input == null)
                return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahSource
                {
                            Source = input.Source,
                };
                await _dbcontext.ElmahSource.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<ElmahSourceModel>>.FromResult(
                    new Response<ElmahSourceModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceModel
                        {
                            Source = toInsert.Source,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahSourceModel>> Update(ElmahSourceModel input)
        {
            if (input == null)
                return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahSource.SingleOrDefault(t => t.Source == input.Source);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Source = input.Source;
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<ElmahSourceModel>>.FromResult(
                    new Response<ElmahSourceModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceModel
                        {
                            Source = existing.Source,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceModel>>.FromResult(new Response<ElmahSourceModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<ElmahSourceModel> GetSearchQuery(
            ElmahSourceAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                Source = !string.IsNullOrEmpty(query.Source),

            };

            var queryable =
                from t in _dbcontext.ElmahSource

                where

                    (!toCompare.Source || t.Source.Contains(query.Source))
                select new ElmahSourceModel
                {

                        Source = t.Source,
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

        public async Task<PagedResponse<ElmahSourceModel[]>> Search(
            ElmahSourceAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = GetSearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = GetSearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahSourceModel>();
                return new PagedResponse<ElmahSourceModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse { TotalCount = totalCount, Count = result.Count(), PageIndex = query.PageIndex, PageSize = query.PageSize },
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahSourceModel[]>>.FromResult(new PagedResponse<ElmahSourceModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<NameValuePair> GetGetCodeListQuery(
            ElmahSourceAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                Source = !string.IsNullOrEmpty(query.Source),

            };

            var queryable =
                from t in _dbcontext.ElmahSource

                where

                    (!toCompare.Source || t.Source.Contains(query.Source))
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
            if (orderBys.Count() > 0)
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

