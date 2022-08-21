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

        private IQueryable<ElmahHostDataModel> SearchQuery(
            ElmahHostAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var geoQueryValue = new
            {
                SpatialLocation = query.SpatialLocation != null ? IGeometryHelper.FromGeoJson(GeoHelperSinglton.Instance.GeographyToGeoJSON(query.SpatialLocation)) : null,
                SpatialLocationGeographyIntersects = query.SpatialLocationGeographyIntersects != null ? IGeometryHelper.FromGeoJson(GeoHelperSinglton.Instance.GeographyToGeoJSON(query.SpatialLocationGeographyIntersects)) : null,
            };
            var queryable =
                from t in _dbcontext.ElmahHost

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Host!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Host!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Host!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Host) ||
                            query.HostSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Host!, "%" + query.Host + "%") ||
                            query.HostSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Host!, query.Host + "%") ||
                            query.HostSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Host!, "%" + query.Host))
                    &&

                    (query.SpatialLocation == null || query.SpatialLocation.IsEmpty || !query.SpatialLocationRadius.HasValue || t.SpatialLocation____ != null && t.SpatialLocation____!.IsWithinDistance(geoQueryValue.SpatialLocation, query.SpatialLocationRadius ?? 5.0))
                    &&
                    (query.SpatialLocationGeographyIntersects == null || query.SpatialLocationGeographyIntersects.IsEmpty || t.SpatialLocation____ != null && geoQueryValue.SpatialLocationGeographyIntersects != null && geoQueryValue.SpatialLocationGeographyIntersects.Contains(t.SpatialLocation____))

                select new ElmahHostDataModel
                {

                        Host = t.Host,
                        SpatialLocation = t.SpatialLocation,
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

        public async Task<PagedResponse<ElmahHostDataModel[]>> Search(
            ElmahHostAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahHostDataModel>();
                return new PagedResponse<ElmahHostDataModel[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahHostDataModel[]>>.FromResult(new PagedResponse<ElmahHostDataModel[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<ElmahHost> GetIQueryableByPrimaryIdentifierList(
            List<ElmahHostIdentifier> ids)
        {
            var idList = ids.Select(t => t.Host).ToList();
            var queryable =
                from t in _dbcontext.ElmahHost
                where idList.Contains(t.Host)
                select t;

            return queryable;
        }

        public async Task<Response> BulkDelete(List<ElmahHostIdentifier> ids)
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

        public async Task<Response<MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ElmahHost> newEFItems = new List<ElmahHost>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ElmahHost
                        {
                            Host = item.Host,
                            SpatialLocation = item.SpatialLocation,
                        };
                        _dbcontext.ElmahHost.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ElmahHost
                             where

                             t.Host == item.Host
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Host = item.Host;
                existing.SpatialLocation = item.SpatialLocation;
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
                        select t.Host);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.Host);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ElmahHost
                    where identifierListToloadResponseItems.Contains(t.Host)

                    select new ElmahHostDataModel
                    {

                        Host = t.Host,
                        SpatialLocation = t.SpatialLocation,

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.Host == t.Host)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.Host == t.Host)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDModel<ElmahHostIdentifier, ElmahHostDataModel>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ElmahHostDataModel>> Update(ElmahHostIdentifier id, ElmahHostDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahHostDataModel>>.FromResult(new Response<ElmahHostDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahHost
                     where

                    t.Host == id.Host
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahHostDataModel>>.FromResult(new Response<ElmahHostDataModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Host = input.Host;
                existing.SpatialLocation = input.SpatialLocation;
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahHostDataModel>>.FromResult(
                    new Response<ElmahHostDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostDataModel
                        {
                            Host = existing.Host,
                            SpatialLocation = existing.SpatialLocation,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahHostDataModel>>.FromResult(new Response<ElmahHostDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahHostDataModel>> Get(ElmahHostIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahHostDataModel>>.FromResult(new Response<ElmahHostDataModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ElmahHost.SingleOrDefault(
                    t =>

                    t.Host == id.Host
                );

                if (existing == null)
                    return await Task<Response<ElmahHostDataModel>>.FromResult(new Response<ElmahHostDataModel> { Status = HttpStatusCode.NotFound });

                return await Task<Response<ElmahHostDataModel>>.FromResult(
                    new Response<ElmahHostDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostDataModel
                        {
                            Host = existing.Host,
                            SpatialLocation = existing.SpatialLocation,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahHostDataModel>>.FromResult(new Response<ElmahHostDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahHostDataModel>> Create(ElmahHostDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahHostDataModel>>.FromResult(new Response<ElmahHostDataModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahHost
                {
                            Host = input.Host,
                            SpatialLocation = input.SpatialLocation,
                };
                await _dbcontext.ElmahHost.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahHostDataModel>>.FromResult(
                    new Response<ElmahHostDataModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahHostDataModel
                        {
                            Host = toInsert.Host,
                            SpatialLocation = toInsert.SpatialLocation,
                        }
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahHostDataModel>>.FromResult(new Response<ElmahHostDataModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response> Delete(ElmahHostIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ElmahHost
                     where

                    t.Host == id.Host
                     select t).SingleOrDefault();

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

        private IQueryable<NameValuePair> GetCodeListQuery(
            ElmahHostAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var geoQueryValue = new
            {
                SpatialLocation = query.SpatialLocation != null ? IGeometryHelper.FromGeoJson(GeoHelperSinglton.Instance.GeographyToGeoJSON(query.SpatialLocation)) : null,
                SpatialLocationGeographyIntersects = query.SpatialLocationGeographyIntersects != null ? IGeometryHelper.FromGeoJson(GeoHelperSinglton.Instance.GeographyToGeoJSON(query.SpatialLocationGeographyIntersects)) : null,
            };
            var queryable =
                from t in _dbcontext.ElmahHost

                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Host!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Host!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Host!, "%" + query.TextSearch)))
                    &&

                    (string.IsNullOrEmpty(query.Host) ||
                            query.HostSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Host!, "%" + query.Host + "%") ||
                            query.HostSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Host!, query.Host + "%") ||
                            query.HostSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Host!, "%" + query.Host))
                    &&

                    (query.SpatialLocation == null || query.SpatialLocation.IsEmpty || !query.SpatialLocationRadius.HasValue || t.SpatialLocation____ != null && t.SpatialLocation____!.IsWithinDistance(geoQueryValue.SpatialLocation, query.SpatialLocationRadius ?? 5.0))
                    &&
                    (query.SpatialLocationGeographyIntersects == null || query.SpatialLocationGeographyIntersects.IsEmpty || t.SpatialLocation____ != null && geoQueryValue.SpatialLocationGeographyIntersects != null && geoQueryValue.SpatialLocationGeographyIntersects.Contains(t.SpatialLocation____))

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
            if (orderBys.Any())
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

