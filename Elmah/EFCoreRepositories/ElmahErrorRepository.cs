using Elmah.RepositoryContracts;
using Elmah.EFCoreContext;
using Elmah.Models;
using Framework.Helpers;
using Framework.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

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

        private IQueryable<ElmahErrorModel.DefaultView> SearchQuery(
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
            if (orderBys.Any())
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
                    Pagination = new PaginationResponse(totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
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

                    join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application// \Application
                    join Host in _dbcontext.ElmahHost on t.Host equals Host.Host// \Host
                    join Source in _dbcontext.ElmahSource on t.Source equals Source.Source// \Source
                    join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode// \StatusCode
                    join Type in _dbcontext.ElmahType on t.Type equals Type.Type// \Type
                    join User in _dbcontext.ElmahUser on t.User equals User.User// \User
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

        public async Task<Response<ElmahErrorModel.DefaultView>> Get(ElmahErrorIdentifier id)
        {
            if (id == null)
                return await Task<Response<ElmahErrorModel.DefaultView>>.FromResult(new Response<ElmahErrorModel.DefaultView> { Status = HttpStatusCode.BadRequest });

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
                    ErrorId = Guid.NewGuid(),
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

                    join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application// \Application
                    join Host in _dbcontext.ElmahHost on t.Host equals Host.Host// \Host
                    join Source in _dbcontext.ElmahSource on t.Source equals Source.Source// \Source
                    join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode// \StatusCode
                    join Type in _dbcontext.ElmahType on t.Type equals Type.Type// \Type
                    join User in _dbcontext.ElmahUser on t.User equals User.User// \User
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

        public async Task<Response> Delete(ElmahErrorIdentifier id)
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

        private IQueryable<Elmah.EFCoreContext.ElmahError> GetByPrimaryIdentifierQueryListQuery(
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
                select t;

            return queryable;
        }

        public async Task<Response> BatchDelete(List<ElmahErrorIdentifier> ids)
        {
            try
            {
                var querable = GetByPrimaryIdentifierQueryListQuery(ids);
                var result = await querable.BatchDeleteAsync();

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

        public async Task<Framework.Models.Response> BulkUpdate(Framework.Models.BatchActionViewModel<ElmahErrorIdentifier, Elmah.Models.ElmahErrorModel.DefaultView> data)
        {
            if (data.ActionData == null)
            {
                return await Task<Framework.Models.Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });
            }
            try
            {
                var querable = GetByPrimaryIdentifierQueryListQuery(data.Ids);
                if (data.ActionName == "Application")
                {
                    var result = await querable.BatchUpdateAsync(
                        new Elmah.EFCoreContext.ElmahError 
                        { 
                            Application = data.ActionData.Application 
                        });
                }
                return await Task<Framework.Models.Response>.FromResult(new Framework.Models.Response { Status = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return await Task<Framework.Models.Response>.FromResult(new Framework.Models.Response { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
            }
        }
    }
}
