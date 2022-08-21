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

        public async Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>> Search(
            ElmahErrorAdvancedQuery query)
        {
            try
            {
                var queryableOfTotalCount = SearchQuery(query, false);
                var totalCount = queryableOfTotalCount.Count();

                var queryable = SearchQuery(query, true);
                var result = await queryable.ToDynamicArrayAsync<ElmahErrorDataModel.DefaultView>();
                return new PagedResponse<ElmahErrorDataModel.DefaultView[]>
                {
                    Status = HttpStatusCode.OK,
                    Pagination = new PaginationResponse (totalCount, result?.Length ?? 0, query.PageIndex, query.PageSize, query.PaginationOption),
                    ResponseBody = result,
                };
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(new PagedResponse<ElmahErrorDataModel.DefaultView[]>
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

        public async Task<Response> BulkDelete(List<ElmahErrorIdentifier> ids)
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

        public async Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>> BulkUpdate(
            BatchActionViewModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> data)
        {
            if (data.ActionData == null)
            {
                return await Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(
                    new PagedResponse<ElmahErrorDataModel.DefaultView[]> { Status = HttpStatusCode.BadRequest });
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
                    return await Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(
                        new PagedResponse<ElmahErrorDataModel.DefaultView[]> {
                            Status = HttpStatusCode.OK,
                            ResponseBody = responseBody.ToArray(),
                        });
                }

                return await Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(
                    new PagedResponse<ElmahErrorDataModel.DefaultView[]> { Status = HttpStatusCode.BadRequest });
            }
            catch (Exception ex)
            {
                return await Task<PagedResponse<ElmahErrorDataModel.DefaultView[]>>.FromResult(
                    new PagedResponse<ElmahErrorDataModel.DefaultView[]> { Status = HttpStatusCode.InternalServerError, StatusMessage = ex.Message });
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

        public async Task<Response<MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>>> MultiItemsCUD(
            MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView> input)
        {
            // 1. DeleteItems, return if Failed
            if (input.DeleteItems != null)
            {
                var responseOfDeleteItems = await this.BulkDelete(input.DeleteItems);
                if (responseOfDeleteItems != null && responseOfDeleteItems.Status != HttpStatusCode.OK)
                {
                    return new Response<MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>> { Status = responseOfDeleteItems.Status, StatusMessage = "Deletion Failed. " + responseOfDeleteItems.StatusMessage };
                }
            }

            // 2. return OK, if no more NewItems and UpdateItems
            if (!(input.NewItems != null && input.NewItems.Count > 0 ||
                input.UpdateItems != null && input.UpdateItems.Count > 0))
            {
                return new Response<MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>> { Status = HttpStatusCode.OK };
            }

            // 3. NewItems and UpdateItems
            try
            {
                // 3.1.1. NewItems if any
                List<ElmahError> newEFItems = new List<ElmahError>();
                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    foreach (var item in input.NewItems)
                    {
                        var toInsert = new ElmahError
                        {
                            Application = item.Application,
                            Host = item.Host,
                            Type = item.Type,
                            Source = item.Source,
                            Message = item.Message,
                            User = item.User,
                            StatusCode = item.StatusCode,
                            TimeUtc = item.TimeUtc,
                            AllXml = item.AllXml,
                        };
                        _dbcontext.ELMAH_Error.Add(toInsert);
                        newEFItems.Add(toInsert);
                    }
                }

                // 3.1.2. UpdateItems if any
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    foreach (var item in input.UpdateItems)
                    {
                        var existing =
                            (from t in _dbcontext.ELMAH_Error
                             where

                             t.ErrorId == item.ErrorId
                             select t).SingleOrDefault();

                        if (existing != null)
                        {
                            // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Application = item.Application;
                existing.Host = item.Host;
                existing.Type = item.Type;
                existing.Source = item.Source;
                existing.Message = item.Message;
                existing.User = item.User;
                existing.StatusCode = item.StatusCode;
                existing.TimeUtc = item.TimeUtc;
                existing.AllXml = item.AllXml;
                        }
                    }
                }
                await _dbcontext.SaveChangesAsync();

                // 3.2 Load Response
                var identifierListToloadResponseItems = new List<System.Guid>();

                if (input.NewItems != null && input.NewItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in newEFItems
                        select t.ErrorId);
                }
                if (input.UpdateItems != null && input.UpdateItems.Count > 0)
                {
                    identifierListToloadResponseItems.AddRange(
                        from t in input.UpdateItems
                        select t.ErrorId);
                }

                var responseBodyWithNewAndUpdatedItems =
                    (from t in _dbcontext.ELMAH_Error
                    join Application in _dbcontext.ElmahApplication on t.Application equals Application.Application// \Application
                    join Host in _dbcontext.ElmahHost on t.Host equals Host.Host// \Host
                    join Source in _dbcontext.ElmahSource on t.Source equals Source.Source// \Source
                    join StatusCode in _dbcontext.ElmahStatusCode on t.StatusCode equals StatusCode.StatusCode// \StatusCode
                    join Type in _dbcontext.ElmahType on t.Type equals Type.Type// \Type
                    join User in _dbcontext.ElmahUser on t.User equals User.User// \User
                    where identifierListToloadResponseItems.Contains(t.ErrorId)

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

                    }).ToList();

                // 3.3. Final Response
                var response = new Response<MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.OK,
                    ResponseBody = new MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>
                    {
                        NewItems =
                            input.NewItems != null && input.NewItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => newEFItems.Any(t1 => t1.ErrorId == t.ErrorId)).ToList()
                                : null,
                        UpdateItems =
                            input.UpdateItems != null && input.UpdateItems.Count > 0
                                ? responseBodyWithNewAndUpdatedItems.Where(t => input.UpdateItems.Any(t1 => t1.ErrorId == t.ErrorId)).ToList()
                                : null,
                    }
                };
                return response;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Response<MultiItemsCUDModel<ElmahErrorIdentifier, ElmahErrorDataModel.DefaultView>>
                {
                    Status = HttpStatusCode.InternalServerError,
                    StatusMessage = "Create And/Or Update Failed. " + ex.Message
                });
            }
        }

        public async Task<Response<ElmahErrorDataModel.DefaultView>> Update(ElmahErrorIdentifier id, ElmahErrorDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahErrorDataModel.DefaultView>>.FromResult(new Response<ElmahErrorDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ELMAH_Error
                     where

                    t.ErrorId == id.ErrorId
                     select t).SingleOrDefault();

                // TODO: can create a new record here.
                if (existing == null)
                    return await Task<Response<ElmahErrorDataModel.DefaultView>>.FromResult(new Response<ElmahErrorDataModel.DefaultView> { Status = HttpStatusCode.NotFound });

                // TODO: the .CopyTo<> method may modified because some properties may should not be copied.
                existing.Application = input.Application;
                existing.Host = input.Host;
                existing.Type = input.Type;
                existing.Source = input.Source;
                existing.Message = input.Message;
                existing.User = input.User;
                existing.StatusCode = input.StatusCode;
                existing.TimeUtc = input.TimeUtc;
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

        public async Task<Response<ElmahErrorDataModel.DefaultView>> Create(ElmahErrorDataModel input)
        {
            if (input == null)
                return await Task<Response<ElmahErrorDataModel.DefaultView>>.FromResult(new Response<ElmahErrorDataModel.DefaultView> { Status = HttpStatusCode.BadRequest });
            try
            {
                var toInsert = new ElmahError
                {
                            Application = input.Application,
                            Host = input.Host,
                            Type = input.Type,
                            Source = input.Source,
                            Message = input.Message,
                            User = input.User,
                            StatusCode = input.StatusCode,
                            TimeUtc = input.TimeUtc,
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

        public async Task<Response> Delete(ElmahErrorIdentifier id)
        {
            if (id == null)
                return await Task<Response>.FromResult(new Response { Status = HttpStatusCode.BadRequest });

            try
            {
                var existing =
                    (from t in _dbcontext.ELMAH_Error
                     where

                    t.ErrorId == id.ErrorId
                     select t).SingleOrDefault();

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

        private IQueryable<NameValuePair> GetCodeListQuery(
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
            if (orderBys.Any())
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

