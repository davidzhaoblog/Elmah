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
    public class ElmahHostRepository
        : IElmahHostRepository
    {
        private readonly ILogger<ElmahHostRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahHostRepository(EFDbContext dbcontext, ILogger<ElmahHostRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response> Delete(ElmahHostIdModel id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahHost.SingleOrDefault(t => t.Host == id.Host);

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ElmahHost.Remove(existing);
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

        public async Task<Response<ElmahHostModel>> Get(ElmahHostIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahHostModel>>.FromResult(new Response<ElmahHostModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahHost.SingleOrDefault(t => t.Host == id.Host);
                if (existing == null)
                    return await Task<Response<ElmahHostModel>>.FromResult(new Response<ElmahHostModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahHostModel>>.FromResult(
                    new Response<ElmahHostModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostModel
                        {
                            Host = existing.Host,
                            SpatialLocation = existing.SpatialLocation,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahHostModel>>.FromResult(new Response<ElmahHostModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahHostModel>> Create(ElmahHostModel input)
        {
            if (input == null)
                return await Task<Response<ElmahHostModel>>.FromResult(new Response<ElmahHostModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahHost
                {
                            Host = input.Host,
                            SpatialLocation = input.SpatialLocation,
                };
                await _dbcontext.ElmahHost.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahHostModel>>.FromResult(
                    new Response<ElmahHostModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostModel
                        {
                            Host = toInsert.Host,
                            SpatialLocation = toInsert.SpatialLocation,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahHostModel>>.FromResult(new Response<ElmahHostModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahHostModel>> Update(ElmahHostModel input)
        {
            if (input == null)
                return await Task<Response<ElmahHostModel>>.FromResult(new Response<ElmahHostModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahHost.SingleOrDefault(t => t.Host == input.Host);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahHostModel>>.FromResult(new Response<ElmahHostModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Host = input.Host;
                existing.SpatialLocation = input.SpatialLocation;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahHostModel>>.FromResult(
                    new Response<ElmahHostModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostModel
                        {
                            Host = existing.Host,
                            SpatialLocation = existing.SpatialLocation,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahHostModel>>.FromResult(new Response<ElmahHostModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<ElmahHostModel> SearchQuery(
            ElmahHostAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                Host = !string.IsNullOrEmpty(query.Host),
                SpatialLocation = query.SpatialLocation != null && !query.SpatialLocation.IsEmpty && query.SpatialLocationRadius.HasValue,
                SpatialLocationGeographyIntersects = query.SpatialLocationGeographyIntersects != null && !query.SpatialLocationGeographyIntersects.IsEmpty,

            };
            var geoQueryValue = new
            {
                SpatialLocation = query.SpatialLocation != null ? IGeometryHelper.FromGeoJson(GeoHelperSinglton.Instance.GeographyToGeoJSON(query.SpatialLocation)) : null,
                SpatialLocationGeographyIntersects = query.SpatialLocationGeographyIntersects != null ? IGeometryHelper.FromGeoJson(GeoHelperSinglton.Instance.GeographyToGeoJSON(query.SpatialLocationGeographyIntersects)) : null,
            };
            var queryable =
                from t in _dbcontext.ElmahHost

                where

                    (!toCompare.Host || t.Host.Contains(query.Host))
                    &&
                    (!toCompare.SpatialLocation || t.SpatialLocation____ != null && t.SpatialLocation____.IsWithinDistance(geoQueryValue.SpatialLocation, query.SpatialLocationRadius ?? 5.0))
                    &&
                    (!toCompare.SpatialLocationGeographyIntersects || t.SpatialLocation____ != null && geoQueryValue.SpatialLocationGeographyIntersects != null && geoQueryValue.SpatialLocationGeographyIntersects.Contains(t.SpatialLocation____))
                select new ElmahHostModel
                {

                        Host = t.Host,
                        SpatialLocation = t.SpatialLocation,
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

        public async Task<PagedResponse<ElmahHostModel[]>> Search(
            ElmahHostAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahHostModel>();
                return new PagedResponse<ElmahHostModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse { TotalCount = totalCount, Count = result.Count(), PageIndex = query.PageIndex, PageSize = query.PageSize },
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahHostModel[]>>.FromResult(new PagedResponse<ElmahHostModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<NameValuePair> GetCodeListQuery(
            ElmahHostAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                Host = !string.IsNullOrEmpty(query.Host),
                SpatialLocation = query.SpatialLocation != null && !query.SpatialLocation.IsEmpty && query.SpatialLocationRadius.HasValue,
                SpatialLocationGeographyIntersects = query.SpatialLocationGeographyIntersects != null && !query.SpatialLocationGeographyIntersects.IsEmpty,

            };
            var geoQueryValue = new
            {
                SpatialLocation = query.SpatialLocation != null ? IGeometryHelper.FromGeoJson(GeoHelperSinglton.Instance.GeographyToGeoJSON(query.SpatialLocation)) : null,
                SpatialLocationGeographyIntersects = query.SpatialLocationGeographyIntersects != null ? IGeometryHelper.FromGeoJson(GeoHelperSinglton.Instance.GeographyToGeoJSON(query.SpatialLocationGeographyIntersects)) : null,
            };
            var queryable =
                from t in _dbcontext.ElmahHost

                where

                    (!toCompare.Host || t.Host.Contains(query.Host))
                    &&
                    (!toCompare.SpatialLocation || t.SpatialLocation____ != null && t.SpatialLocation____.IsWithinDistance(geoQueryValue.SpatialLocation, query.SpatialLocationRadius ?? 5.0))
                    &&
                    (!toCompare.SpatialLocationGeographyIntersects || t.SpatialLocation____ != null && geoQueryValue.SpatialLocationGeographyIntersects != null && geoQueryValue.SpatialLocationGeographyIntersects.Contains(t.SpatialLocation____))
                select new NameValuePair
                {

                        Value = t.Host,
                        Name = t.Host,
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
            ElmahHostAdvancedQuery query)
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

