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

        private IQueryable<ElmahTypeModel> SearchQuery(
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

                select new ElmahTypeModel
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

        public async Task<PagedResponse<ElmahTypeModel[]>> Search(
            ElmahTypeAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahTypeModel>();
                return new PagedResponse<ElmahTypeModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahTypeModel[]>>.FromResult(new PagedResponse<ElmahTypeModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<ElmahTypeModel>> Update(ElmahTypeIdentifier id, ElmahTypeModel input)
        {
            if (input == null)
                return await Task<Response<ElmahTypeModel>>.FromResult(new Response<ElmahTypeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahType

                     where

                    t.Type == id.Type
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahTypeModel>>.FromResult(new Response<ElmahTypeModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Type = input.Type;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahTypeModel>>.FromResult(
                    new Response<ElmahTypeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeModel
                        {
                            Type = existing.Type,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahTypeModel>>.FromResult(new Response<ElmahTypeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahTypeModel>> Get(ElmahTypeIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahTypeModel>>.FromResult(new Response<ElmahTypeModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahType.SingleOrDefault(
                    t =>

                    t.Type == id.Type
                );

                if (existing == null)
                    return await Task<Response<ElmahTypeModel>>.FromResult(new Response<ElmahTypeModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahTypeModel>>.FromResult(
                    new Response<ElmahTypeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeModel
                        {
                            Type = existing.Type,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahTypeModel>>.FromResult(new Response<ElmahTypeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahTypeModel>> Create(ElmahTypeModel input)
        {
            if (input == null)
                return await Task<Response<ElmahTypeModel>>.FromResult(new Response<ElmahTypeModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahType
                {
                            Type = input.Type,
                };
                await _dbcontext.ElmahType.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahTypeModel>>.FromResult(
                    new Response<ElmahTypeModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahTypeModel
                        {
                            Type = toInsert.Type,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahTypeModel>>.FromResult(new Response<ElmahTypeModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
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

