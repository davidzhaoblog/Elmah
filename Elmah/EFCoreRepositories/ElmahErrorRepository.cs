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

        public async Task<Response> Delete(ElmahErrorIdModel id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ELMAH_Error.SingleOrDefault(t => t.ErrorId == id.ErrorId);

                if (existing == null)
                    return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.NotFound });

                _dbcontext.ELMAH_Error.Remove(existing);
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

        public async Task<Response<ElmahErrorModel.DefaultView>> Get(ElmahErrorIdModel id)
        {
            if (id == null)
                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(new Response<ElmahErrorModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {

                var responseBody =
                    (
                    from t in _dbcontext.ELMAH_Error

                    join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application
                    join Host in _dbcontext.ElmahHost on t.Host equals Host.Host
                    join Source in _dbcontext.ElmahSource on t.Source equals Source.Source
                    join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode
                    join Type in _dbcontext.ElmahType on t.Type equals Type.Type
                    join User in _dbcontext.ElmahUser on t.User equals User.User
                    where t.ErrorId == id.ErrorId
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

                    }).First();
                if (responseBody == null)
                    return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(new Response<ElmahErrorModel.DefaultView> { Status = HttpStatusCode.NotFound });
                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(
                    new Response<ElmahErrorModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(new Response<ElmahErrorModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahErrorModel.DefaultView>> Create(ElmahErrorModel input)
        {
            if (input == null)
                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(new Response<ElmahErrorModel.DefaultView> { Status = HttpStatusCode.BadRequest });
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

                var responseBody =
                    (
                    from t in _dbcontext.ELMAH_Error

                    join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application
                    join Host in _dbcontext.ElmahHost on t.Host equals Host.Host
                    join Source in _dbcontext.ElmahSource on t.Source equals Source.Source
                    join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode
                    join Type in _dbcontext.ElmahType on t.Type equals Type.Type
                    join User in _dbcontext.ElmahUser on t.User equals User.User
                    where t.ErrorId == toInsert.ErrorId
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

                    }).First();

                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(
                    new Response<ElmahErrorModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(new Response<ElmahErrorModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        public async Task<Response<ElmahErrorModel.DefaultView>> Update(ElmahErrorModel input)
        {
            if (input == null)
                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(new Response<ElmahErrorModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing = _dbcontext.ELMAH_Error.SingleOrDefault(t => t.ErrorId == input.ErrorId);

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(new Response<ElmahErrorModel.DefaultView> { Status = HttpStatusCode.NotFound });

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

                var responseBody =
                    (
                    from t in _dbcontext.ELMAH_Error

                    join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application
                    join Host in _dbcontext.ElmahHost on t.Host equals Host.Host
                    join Source in _dbcontext.ElmahSource on t.Source equals Source.Source
                    join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode
                    join Type in _dbcontext.ElmahType on t.Type equals Type.Type
                    join User in _dbcontext.ElmahUser on t.User equals User.User
                    where t.ErrorId == existing.ErrorId
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

                    }).First();

                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(
                    new Response<ElmahErrorModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(new Response<ElmahErrorModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

        private IQueryable<ElmahErrorModel.DefaultView> SearchQuery(
            ElmahErrorAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ELMAH_Error

                join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application
                join Host in _dbcontext.ElmahHost on t.Host equals Host.Host
                join Source in _dbcontext.ElmahSource on t.Source equals Source.Source
                join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode
                join Type in _dbcontext.ElmahType on t.Type equals Type.Type
                join User in _dbcontext.ElmahUser on t.User equals User.User
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (t.Message!.Contains(query.TextSearch) || t.AllXml!.Contains(query.TextSearch)) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (t.Message!.StartsWith(query.TextSearch) || t.AllXml!.StartsWith(query.TextSearch)) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (t.Message!.EndsWith(query.TextSearch) || t.AllXml!.EndsWith(query.TextSearch)))
                    &&
                    (string.IsNullOrEmpty(query.Application) || Application.Application == query.Application)
                    &&
                    (string.IsNullOrEmpty(query.Host) || Host.Host == query.Host)
                    &&
                    (string.IsNullOrEmpty(query.Source) || Source.Source == query.Source)
                    &&
                    (!query.StatusCode.HasValue || StatusCode.StatusCode == query.StatusCode)
                    &&
                    (string.IsNullOrEmpty(query.Type) || Type.Type == query.Type)
                    &&
                    (string.IsNullOrEmpty(query.User) || User.User == query.User)
                    &&
                    (string.IsNullOrEmpty(query.Message) || query.MessageSearchType == TextSearchTypes.Contains && t.Message!.Contains(query.Message) || query.MessageSearchType == TextSearchTypes.StartsWith && t.Message!.StartsWith(query.Message) || query.MessageSearchType == TextSearchTypes.EndsWith && t.Message!.EndsWith(query.Message))
                    &&
                    (string.IsNullOrEmpty(query.AllXml) || query.AllXmlSearchType == TextSearchTypes.Contains && t.AllXml!.Contains(query.AllXml) || query.AllXmlSearchType == TextSearchTypes.StartsWith && t.AllXml!.StartsWith(query.AllXml) || query.AllXmlSearchType == TextSearchTypes.EndsWith && t.AllXml!.EndsWith(query.AllXml))
                    &&
                    (!query.TimeUtcRangeLower.HasValue && !query.TimeUtcRangeUpper.HasValue || (!query.TimeUtcRangeLower.HasValue || t.TimeUtc >= query.TimeUtcRangeLower) && (!query.TimeUtcRangeLower.HasValue || t.TimeUtc <= query.TimeUtcRangeUpper))
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
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
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

        private IQueryable<NameValuePair> GetCodeListQuery(
            ElmahErrorAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ELMAH_Error

                join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application
                join Host in _dbcontext.ElmahHost on t.Host equals Host.Host
                join Source in _dbcontext.ElmahSource on t.Source equals Source.Source
                join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode
                join Type in _dbcontext.ElmahType on t.Type equals Type.Type
                join User in _dbcontext.ElmahUser on t.User equals User.User
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (t.Message!.Contains(query.TextSearch) || t.AllXml!.Contains(query.TextSearch)) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (t.Message!.StartsWith(query.TextSearch) || t.AllXml!.StartsWith(query.TextSearch)) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (t.Message!.EndsWith(query.TextSearch) || t.AllXml!.EndsWith(query.TextSearch)))
                    &&
                    (string.IsNullOrEmpty(query.Application) || Application.Application == query.Application)
                    &&
                    (string.IsNullOrEmpty(query.Host) || Host.Host == query.Host)
                    &&
                    (string.IsNullOrEmpty(query.Source) || Source.Source == query.Source)
                    &&
                    (!query.StatusCode.HasValue || StatusCode.StatusCode == query.StatusCode)
                    &&
                    (string.IsNullOrEmpty(query.Type) || Type.Type == query.Type)
                    &&
                    (string.IsNullOrEmpty(query.User) || User.User == query.User)
                    &&
                    (string.IsNullOrEmpty(query.Message) || query.MessageSearchType == TextSearchTypes.Contains && t.Message!.Contains(query.Message) || query.MessageSearchType == TextSearchTypes.StartsWith && t.Message!.StartsWith(query.Message) || query.MessageSearchType == TextSearchTypes.EndsWith && t.Message!.EndsWith(query.Message))
                    &&
                    (string.IsNullOrEmpty(query.AllXml) || query.AllXmlSearchType == TextSearchTypes.Contains && t.AllXml!.Contains(query.AllXml) || query.AllXmlSearchType == TextSearchTypes.StartsWith && t.AllXml!.StartsWith(query.AllXml) || query.AllXmlSearchType == TextSearchTypes.EndsWith && t.AllXml!.EndsWith(query.AllXml))
                    &&
                    (!query.TimeUtcRangeLower.HasValue && !query.TimeUtcRangeUpper.HasValue || (!query.TimeUtcRangeLower.HasValue || t.TimeUtc >= query.TimeUtcRangeLower) && (!query.TimeUtcRangeLower.HasValue || t.TimeUtc <= query.TimeUtcRangeUpper))
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

