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
    public class ElmahErrorRepository
        : IElmahErrorRepository
    {
        private readonly ILogger<ElmahErrorRepository> _logger;
        private readonly EFDbContext _dbcontext;

        public ElmahErrorRepository(EFDbContext dbcontext, ILogger<ElmahErrorRepository> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public async Task<Response<ElmahErrorModel>> Delete(ElmahErrorIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ELMAH_Error.SingleOrDefault(t => t.ErrorId == id.ErrorId);

                if (existing == null)
                    return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.NotFound });

                _dbcontext.ELMAH_Error.Remove(existing);
                await _dbcontext.SaveChangesAsync();

                return await Task<Response<ElmahErrorModel>>.FromResult(
                    new Response<ElmahErrorModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahErrorModel
                        {
                            ErrorId = existing.ErrorId,
                            Application = existing.Application,
                            Host = existing.Host,
                            Type = existing.Type,
                            Source = existing.Source,
                            Message = existing.Message,
                            User = existing.User,
                            StatusCode = existing.StatusCode,
                            TimeUtc = existing.TimeUtc,
                            Sequence = existing.Sequence,
                            AllXml = existing.AllXml,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahErrorModel>> Get(ElmahErrorIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ELMAH_Error.SingleOrDefault(t => t.ErrorId == id.ErrorId);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.BadRequest });

                return await Task<Response<ElmahErrorModel>>.FromResult(
                    new Response<ElmahErrorModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahErrorModel
                        {
                            ErrorId = existing.ErrorId,
                            Application = existing.Application,
                            Host = existing.Host,
                            Type = existing.Type,
                            Source = existing.Source,
                            Message = existing.Message,
                            User = existing.User,
                            StatusCode = existing.StatusCode,
                            TimeUtc = existing.TimeUtc,
                            Sequence = existing.Sequence,
                            AllXml = existing.AllXml,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahErrorModel>> Create(ElmahErrorModel input)
        {
            if (input == null)
                return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahError
                {
                            ErrorId = input.ErrorId,
                            Application = input.Application,
                            Host = input.Host,
                            Type = input.Type,
                            Source = input.Source,
                            Message = input.Message,
                            User = input.User,
                            StatusCode = input.StatusCode,
                            TimeUtc = input.TimeUtc,
                            Sequence = input.Sequence,
                            AllXml = input.AllXml,
                };
                await _dbcontext.ELMAH_Error.AddAsync(toInsert);
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<ElmahErrorModel>>.FromResult(
                    new Response<ElmahErrorModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahErrorModel
                        {
                            ErrorId = toInsert.ErrorId,
                            Application = toInsert.Application,
                            Host = toInsert.Host,
                            Type = toInsert.Type,
                            Source = toInsert.Source,
                            Message = toInsert.Message,
                            User = toInsert.User,
                            StatusCode = toInsert.StatusCode,
                            TimeUtc = toInsert.TimeUtc,
                            Sequence = toInsert.Sequence,
                            AllXml = toInsert.AllXml,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahErrorModel>> Update(ElmahErrorModel input)
        {
            if (input == null)
                return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ELMAH_Error.SingleOrDefault(t => t.ErrorId == input.ErrorId);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.ErrorId = input.ErrorId;
                existing.Application = input.Application;
                existing.Host = input.Host;
                existing.Type = input.Type;
                existing.Source = input.Source;
                existing.Message = input.Message;
                existing.User = input.User;
                existing.StatusCode = input.StatusCode;
                existing.TimeUtc = input.TimeUtc;
                existing.Sequence = input.Sequence;
                existing.AllXml = input.AllXml;
                await _dbcontext.SaveChangesAsync();
                return await Task<Response<ElmahErrorModel>>.FromResult(
                    new Response<ElmahErrorModel>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = new ElmahErrorModel
                        {
                            ErrorId = existing.ErrorId,
                            Application = existing.Application,
                            Host = existing.Host,
                            Type = existing.Type,
                            Source = existing.Source,
                            Message = existing.Message,
                            User = existing.User,
                            StatusCode = existing.StatusCode,
                            TimeUtc = existing.TimeUtc,
                            Sequence = existing.Sequence,
                            AllXml = existing.AllXml,
                        }
                    });
            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahErrorModel>>.FromResult(new Response<ElmahErrorModel> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<ElmahErrorModel.DefaultView> GetSearchQuery(
            ElmahErrorAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                Application = !string.IsNullOrEmpty(query.Application),
                Host = !string.IsNullOrEmpty(query.Host),
                Source = !string.IsNullOrEmpty(query.Source),
                StatusCode = !query.StatusCode.HasValue,
                Type = !string.IsNullOrEmpty(query.Type),
                User = !string.IsNullOrEmpty(query.User),
                Message = !string.IsNullOrEmpty(query.Message),
                AllXml = !string.IsNullOrEmpty(query.AllXml),
                TimeUtcRangeLower = query.TimeUtcRangeLower.HasValue,
                TimeUtcRangeUpper = query.TimeUtcRangeUpper.HasValue,

            };

            var queryable =
                from t in _dbcontext.ELMAH_Error

                join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application
                join Host in _dbcontext.ElmahHost on t.Host equals Host.Host
                join Source in _dbcontext.ElmahSource on t.Source equals Source.Source
                join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode
                join Type in _dbcontext.ElmahType on t.Type equals Type.Type
                join User in _dbcontext.ElmahUser on t.User equals User.User
                where

                    (!toCompare.Application || Application.Application == query.Application)
&&
                    (!toCompare.Host || Host.Host == query.Host)
&&
                    (!toCompare.Source || Source.Source == query.Source)
&&
                    (!toCompare.StatusCode || StatusCode.StatusCode == query.StatusCode)
&&
                    (!toCompare.Type || Type.Type == query.Type)
&&
                    (!toCompare.User || User.User == query.User)
&&
                    (!toCompare.Message || t.Message.Contains(query.Message))
&&
                    (!toCompare.AllXml || t.AllXml.Contains(query.AllXml))
&&
                    (!toCompare.TimeUtcRangeLower && !toCompare.TimeUtcRangeUpper || (!toCompare.TimeUtcRangeLower || toCompare.TimeUtcRangeLower && t.TimeUtc >= query.TimeUtcRangeLower) && (!toCompare.TimeUtcRangeUpper || t.TimeUtc <= query.TimeUtcRangeUpper))
                select new ElmahErrorModel.DefaultView
                {

                        Application_Name = Application.Application,
                        ErrorId = t.ErrorId,
                        Host_Name = Host.Host,
                        Source_Name = Source.Source,
                        StatusCode_Name = StatusCode.Name,
                        Type_Name = Type.Type,
                        User_Name = User.User,
                        Application = t.Application,
                        Host = t.Host,
                        Type = t.Type,
                        Source = t.Source,
                        Message = t.Message,
                        User = t.User,
                        StatusCode = t.StatusCode,
                        TimeUtc = t.TimeUtc,
                        Sequence = t.Sequence,
                        AllXml = t.AllXml,
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

        public async Task<PagedResponse<ElmahErrorModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = GetSearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = GetSearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahErrorModel.DefaultView>();
                return new PagedResponse<ElmahErrorModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse { TotalCount = totalCount, Count = result.Count(), PageIndex = query.PageIndex, PageSize = query.PageSize },
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahErrorModel.DefaultView[]>>.FromResult(new PagedResponse<ElmahErrorModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<NameValuePair> GetGetCodeListQuery(
            ElmahErrorAdvancedQuery query, bool withPagingAndOrderBy)
        {
            var toCompare = new
            {
                Application = !string.IsNullOrEmpty(query.Application),
                Host = !string.IsNullOrEmpty(query.Host),
                Source = !string.IsNullOrEmpty(query.Source),
                StatusCode = !query.StatusCode.HasValue,
                Type = !string.IsNullOrEmpty(query.Type),
                User = !string.IsNullOrEmpty(query.User),
                Message = !string.IsNullOrEmpty(query.Message),
                AllXml = !string.IsNullOrEmpty(query.AllXml),
                TimeUtcRangeLower = query.TimeUtcRangeLower.HasValue,
                TimeUtcRangeUpper = query.TimeUtcRangeUpper.HasValue,

            };

            var queryable =
                from t in _dbcontext.ELMAH_Error

                join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application
                join Host in _dbcontext.ElmahHost on t.Host equals Host.Host
                join Source in _dbcontext.ElmahSource on t.Source equals Source.Source
                join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode
                join Type in _dbcontext.ElmahType on t.Type equals Type.Type
                join User in _dbcontext.ElmahUser on t.User equals User.User
                where

                    (!toCompare.Application || Application.Application == query.Application)
&&
                    (!toCompare.Host || Host.Host == query.Host)
&&
                    (!toCompare.Source || Source.Source == query.Source)
&&
                    (!toCompare.StatusCode || StatusCode.StatusCode == query.StatusCode)
&&
                    (!toCompare.Type || Type.Type == query.Type)
&&
                    (!toCompare.User || User.User == query.User)
&&
                    (!toCompare.Message || t.Message.Contains(query.Message))
&&
                    (!toCompare.AllXml || t.AllXml.Contains(query.AllXml))
&&
                    (!toCompare.TimeUtcRangeLower && !toCompare.TimeUtcRangeUpper || (!toCompare.TimeUtcRangeLower || toCompare.TimeUtcRangeLower && t.TimeUtc >= query.TimeUtcRangeLower) && (!toCompare.TimeUtcRangeUpper || t.TimeUtc <= query.TimeUtcRangeUpper))
                select new NameValuePair
                {

                        Value = t.ErrorId.ToString(),
                        Name = t.Application,
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
            ElmahErrorAdvancedQuery query)
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

