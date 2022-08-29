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

        private IQueryable<ElmahSourceDataModel> SearchQuery(
            ElmahSourceAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahSource

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Source!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Source!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Source!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Source) ||
                            query.SourceSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Source!, "%" + query.Source + "%") ||
                            query.SourceSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Source!, query.Source + "%") ||
                            query.SourceSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Source!, "%" + query.Source))

                select new ElmahSourceDataModel
                {

                        Source = t.Source,
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

        public async Task<ListResponse<ElmahSourceDataModel[]>> Search(
            ElmahSourceAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahSourceDataModel>();
                return new ListResponse<ElmahSourceDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<ElmahSourceDataModel[]>>.FromResult(new ListResponse<ElmahSourceDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        public async Task<Response<ElmahSourceDataModel>> Update(ElmahSourceIdentifier id, ElmahSourceDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahSource
                     where

                    t.Source == id.Source
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Source = input.Source;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahSourceDataModel>>.FromResult(
                    new Response<ElmahSourceDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceDataModel
                        {
                            Source = existing.Source,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahSourceDataModel>> Get(ElmahSourceIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahSource.SingleOrDefault(
                    t =>

                    t.Source == id.Source
                );

                if (existing == null)
                    return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahSourceDataModel>>.FromResult(
                    new Response<ElmahSourceDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceDataModel
                        {
                            Source = existing.Source,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahSourceDataModel>> Create(ElmahSourceDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahSource
                {
                            Source = input.Source,
                };
                await _dbcontext.ElmahSource.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahSourceDataModel>>.FromResult(
                    new Response<ElmahSourceDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahSourceDataModel
                        {
                            Source = toInsert.Source,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahSourceDataModel>>.FromResult(new Response<ElmahSourceDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ElmahSourceIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahSource
                     where

                    t.Source == id.Source
                     select t).SingleOrDefault();

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahSource.Remove(existing);
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
            ElmahSourceAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ElmahSource

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Source!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Source!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Source!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Source) ||
                            query.SourceSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Source!, "%" + query.Source + "%") ||
                            query.SourceSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Source!, query.Source + "%") ||
                            query.SourceSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Source!, "%" + query.Source))

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
            if (orderBys.Any())
            {
                queryable = queryable.OrderBy(QueryOrderBySetting.GetOrderByExpression(orderBys));
            }

            queryable = queryable.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize);

            return queryable;
        }

        public async Task<ListResponse<NameValuePair[]>> GetCodeList(
            ElmahSourceAdvancedQuery query)
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

