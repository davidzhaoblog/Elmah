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

        private IQueryable<ElmahErrorDataModel.DefaultView> SearchQuery(
            ElmahErrorAdvancedQuery query, bool withPagingAndOrderBy)
        {

            var queryable =
                from t in _dbcontext.ELMAH_Error

                    join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application// \Application
                    join Host in _dbcontext.ElmahHost on t.Host equals Host.Host// \Host
                    join Source in _dbcontext.ElmahSource on t.Source equals Source.Source// \Source
                    join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode// \StatusCode
                    join Type in _dbcontext.ElmahType on t.Type equals Type.Type// \Type
                    join User in _dbcontext.ElmahUser on t.User equals User.User// \User
                where

                    (string.IsNullOrEmpty(query.TextSearch) ||
                        query.TextSearchType == TextSearchTypes.Contains && (EF.Functions.Like(t.Message!, "%" + query.TextSearch + "%") || EF.Functions.Like(t.AllXml!, "%" + query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.StartsWith && (EF.Functions.Like(t.Message!, query.TextSearch + "%") || EF.Functions.Like(t.AllXml!, query.TextSearch + "%")) ||
                        query.TextSearchType == TextSearchTypes.EndsWith && (EF.Functions.Like(t.Message!, "%" + query.TextSearch) || EF.Functions.Like(t.AllXml!, "%" + query.TextSearch)))
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

                    (!query.TimeUtcRangeLower.HasValue && !query.TimeUtcRangeUpper.HasValue || (!query.TimeUtcRangeLower.HasValue || t.TimeUtc >= query.TimeUtcRangeLower) && (!query.TimeUtcRangeLower.HasValue || t.TimeUtc <= query.TimeUtcRangeUpper))
                    &&

                    (string.IsNullOrEmpty(query.Message) ||
                            query.MessageSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.Message!, "%" + query.Message + "%") ||
                            query.MessageSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.Message!, query.Message + "%") ||
                            query.MessageSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.Message!, "%" + query.Message))
                    &&
                    (string.IsNullOrEmpty(query.AllXml) ||
                            query.AllXmlSearchType == TextSearchTypes.Contains && EF.Functions.Like(t.AllXml!, "%" + query.AllXml + "%") ||
                            query.AllXmlSearchType == TextSearchTypes.StartsWith && EF.Functions.Like(t.AllXml!, query.AllXml + "%") ||
                            query.AllXmlSearchType == TextSearchTypes.EndsWith && EF.Functions.Like(t.AllXml!, "%" + query.AllXml))

                select new ElmahErrorDataModel.DefaultView
                {

                        ErrorId = t.ErrorId,
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
                        Application_Name = Application.Application,
                        Host_Name = Host.Host,
                        Source_Name = Source.Source,
                        StatusCode_Name = StatusCode.Name,
                        Type_Name = Type.Type,
                        User_Name = User.User,
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

        public async Task<ListResponse<ElmahErrorDataModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahErrorDataModel.DefaultView>();
                return new ListResponse<ElmahErrorDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(new ListResponse<ElmahErrorDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = ex.Message
                });
            }
        }

        private IQueryable<ElmahError> GetIQueryableByPrimaryIdentifierList(
            List<ElmahErrorIdentifier> ids)
        {
            var idList = ids.Select(t => t.ErrorId).ToList();
            var queryable =
                from t in _dbcontext.ELMAH_Error
                where idList.Contains(t.ErrorId)
                select t;

            return queryable;
        }

        public async Task<ListResponse<ElmahErrorDataModel.DefaultView[]>> BulkUpdate(
            BatchActionRequest<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> data)
        {
            if (data.ActionData == null)
            {
                return await Task<ListResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(
                    new ListResponse<ElmahErrorDataModel.DefaultView[]> { Status = HttpStatusCode.BadRequest });
            }
            try
            {
                var querable = GetIQueryableByPrimaryIdentifierList(data.Ids);

                if (data.ActionName == "StatusCode")
                {
                    var result = await querable.BatchUpdateAsync(t =>
                        new ElmahError
                        {
                            StatusCode = data.ActionData.StatusCode,
                        });
                    var responseBody = GetIQueryableAsBulkUpdateResponse(data.Ids);
                    return await Task<ListResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(
                        new ListResponse<ElmahErrorDataModel.DefaultView[]> {
                            Status = HttpStatusCode.OK,
                            ResponseBody = responseBody.ToArray(),
                        });
                }

                return await Task<ListResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(
                    new ListResponse<ElmahErrorDataModel.DefaultView[]> { Status = HttpStatusCode.BadRequest });
            }
            catch (Exception ex)
            {
                return await Task<ListResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(
                    new ListResponse<ElmahErrorDataModel.DefaultView[]> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

private IQueryable<ElmahErrorDataModel.DefaultView> GetIQueryableAsBulkUpdateResponse(
            List<ElmahErrorIdentifier> ids)
        {
            var idList = ids.Select(t => t.ErrorId).ToList();
            var queryable =
                from t in _dbcontext.ELMAH_Error
                    join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application// \Application
                    join Host in _dbcontext.ElmahHost on t.Host equals Host.Host// \Host
                    join Source in _dbcontext.ElmahSource on t.Source equals Source.Source// \Source
                    join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode// \StatusCode
                    join Type in _dbcontext.ElmahType on t.Type equals Type.Type// \Type
                    join User in _dbcontext.ElmahUser on t.User equals User.User// \User
                where idList.Contains(t.ErrorId)

                select new ElmahErrorDataModel.DefaultView
                {
                        ErrorId = t.ErrorId,
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
                        Application_Name = Application.Application,
                        Host_Name = Host.Host,
                        Source_Name = Source.Source,
                        StatusCode_Name = StatusCode.Name,
                        Type_Name = Type.Type,
                        User_Name = User.User,
                };

            return queryable;
        }

        public async Task<Response<ElmahErrorDataModel.DefaultView>> Get(ElmahErrorIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahErrorDataModel.DefaultView>>.FromResult(new Response<ElmahErrorDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {

                var responseBody =
                    (
                    from t in _dbcontext.ELMAH_Error

                    join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application// \Application
                    join Host in _dbcontext.ElmahHost on t.Host equals Host.Host// \Host
                    join Source in _dbcontext.ElmahSource on t.Source equals Source.Source// \Source
                    join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode// \StatusCode
                    join Type in _dbcontext.ElmahType on t.Type equals Type.Type// \Type
                    join User in _dbcontext.ElmahUser on t.User equals User.User// \User
                    where

                    t.ErrorId == id.ErrorId

                    select new ElmahErrorDataModel.DefaultView
                    {

                        ErrorId = t.ErrorId,
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
                        Application_Name = Application.Application,
                        Host_Name = Host.Host,
                        Source_Name = Source.Source,
                        StatusCode_Name = StatusCode.Name,
                        Type_Name = Type.Type,
                        User_Name = User.User,

                    }).First();
                if (responseBody == null)
                    return await Task<Response<ElmahErrorDataModel.DefaultView>>.FromResult(new Response<ElmahErrorDataModel.DefaultView> { Status = HttpStatusCode.NotFound });
                return await Task<Response<ElmahErrorDataModel.DefaultView>>.FromResult(
                    new Response<ElmahErrorDataModel.DefaultView>
                    {
                        Status = HttpStatusCode.OK,
                        ResponseBody = responseBody
                    });

            }
            catch (Exception ex)
            {
                return await Task<Response<ElmahErrorDataModel.DefaultView>>.FromResult(new Response<ElmahErrorDataModel.DefaultView> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }

    }
}

