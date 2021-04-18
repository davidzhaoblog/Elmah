using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Elmah.EntityFrameworkContext;
using Elmah.EntityFrameworkDAL;

namespace Elmah.AspNetMvcCoreApiController
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/[controller]/[action]")]
    public partial class ELMAH_ErrorApiController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private readonly ElmahModelEntities _linqContext;

        public ELMAH_ErrorApiController(Elmah.WcfContracts.IELMAH_ErrorService thisService, IServiceProvider serviceProvider, ILogger<ELMAH_ErrorApiController> logger, ElmahModelEntities linqContext)
        {
            this._serviceProvider = serviceProvider;
            this.ThisService = thisService;
            this._logger = logger;
            this._linqContext = linqContext;
        }

        Elmah.WcfContracts.IELMAH_ErrorService ThisService { get; set; }

        #region insert, update and delete in an entity

        /// <summary>
        /// Insert/Update an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// </summary>
        /// <param name="input">input entity</param>
        // [Authorize]
        [HttpPost, ActionName("UpsertEntity")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> UpsertEntity(
            [FromBody] Elmah.DataSourceEntities.ELMAH_Error input)
        {
            var request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn(){
                BusinessLogicLayerRequestTypes = Framework.Services.BusinessLogicLayerRequestTypes.CreateWithParent
            };

            request.Criteria.Add(input);
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn responseMessage = await this.ThisService.UpsertEntity(request);
            return await LoadDefaultItem(responseMessage);;
        }

/*
        /// <summary>
        /// Inserts an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// </summary>
        /// <param name="input">input entity</param>
        // [Authorize]
        [HttpPost, ActionName("InsertEntity")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> InsertEntity(
            [FromBody] Elmah.DataSourceEntities.ELMAH_Error input)
        {
            var request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn();
            request.Criteria.Add(input);
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn responseMessage = await this.ThisService.InsertEntity(request);
            return await LoadDefaultItem(responseMessage);;
        }

        /// <summary>
        /// Updates an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// </summary>
        /// <param name="input">input entity</param>
        // [Authorize]
        [HttpPut, ActionName("UpdateEntity")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> UpdateEntity(
            [FromBody] Elmah.DataSourceEntities.ELMAH_Error input)
        {
            var request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn();
            request.Criteria.Add(input);
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn responseMessage = await this.ThisService.UpdateEntity(request);
            return await LoadDefaultItem(responseMessage);;
        }
*/

        private async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> LoadDefaultItem(Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn responseMessage)
        {
            if (responseMessage.BusinessLogicLayerResponseStatus != Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                return new Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default { BusinessLogicLayerResponseStatus = responseMessage.BusinessLogicLayerResponseStatus, ServerErrorMessage = responseMessage.ServerErrorMessage };
            var request1 = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier();
            request1.Criteria.Identifier.ErrorId.NullableValueToCompare = responseMessage.Message[0].ErrorId;
            return await this.ThisService.GetCollectionOfDefaultByIdentifier(request1);
        }

/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/> by identifier.
        /// </summary>
        /// <param name="identifier">input identifier of an entity</param>
        // [Authorize]
        [HttpDelete, ActionName("DeleteByIdentifierEntity")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteByIdentifierEntity(
            [FromBody] Elmah.DataSourceEntities.ELMAH_ErrorIdentifier identifier)
        {
            var request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltInOfIdentifier();
            request.Criteria = identifier;

            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn responseMessage = await this.ThisService.DeleteByIdentifierEntity(request);

            return responseMessage;
        }
*/

/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/> using .net value types.
        /// Can't use HttpDelete, because body is ignored.
        /// </summary>
        /// <param name="input">input entity</param>
        // [Authorize]
        [HttpPost, ActionName("DeleteEntity")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteEntity(
            [FromBody] Elmah.DataSourceEntities.ELMAH_Error input)
        {
            var request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn(){
                BusinessLogicLayerRequestTypes = Framework.Services.BusinessLogicLayerRequestTypes.DeleteWithParent
            };

            request.Criteria.Add(input);
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn responseMessage = await this.ThisService.DeleteEntity(request);

            return responseMessage;
        }
*/
        #endregion insert, update and delete in an entity

        #region delete using .Net value types
/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/> by identifier using .net value types.
        /// </summary>
        /// <param name="errorId">property value of System.Guid</param>
        // [Authorize]
        [HttpDelete, ActionName("DeleteByIdentifier")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteByIdentifier(
            System.Guid errorId
            )
        {
            return await this.DeleteByIdentifierEntity(new Elmah.DataSourceEntities.ELMAH_ErrorIdentifier{
                ErrorId = errorId});
        }
*/

        #endregion delete using .Net value types

        #region batch insert, update and delete in an entity collection
/*
        /// <summary>
        /// batch update a collection of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// </summary>
        // [Authorize]
        [HttpPut, ActionName("BatchUpdate")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchUpdate([FromBody] Elmah.DataSourceEntities.ELMAH_ErrorCollection input)
        {
            var request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn();
            request.Criteria.AddRange(input);
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn responseMessage = await this.ThisService.BatchUpdate(request);

            return responseMessage;
        }

        /// <summary>
        /// batch insert a collection of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// </summary>
        /// <param name="input">The input collection.</param>
        // [Authorize]
        [HttpPost, ActionName("BatchInsert")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchInsert([FromBody] Elmah.DataSourceEntities.ELMAH_ErrorCollection input)
        {
            var request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn();
            request.Criteria.AddRange(input);
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn responseMessage = await this.ThisService.BatchInsert(request);

            return responseMessage;
        }

        /// <summary>
        /// batch delete a collection of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// </summary>
        /// <param name="input">The input collection.</param>
        // [Authorize]
        [HttpDelete, ActionName("BatchDelete")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchDelete([FromBody] Elmah.DataSourceEntities.ELMAH_ErrorCollection input)
        {
            var request = new Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn();
            request.Criteria.AddRange(input);
            Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn responseMessage = await this.ThisService.BatchDelete(request);

            return responseMessage;
        }
*/
        #endregion batch insert, update and delete in an entity collection

        // DataQueryPerQuerySettingCollection -- MethodDataQuery -- Start

        #region Query Methods Of DefaultByCommon

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="application">value to compare/filter with application property/field/column</param>
        /// <param name="host">value to compare/filter with host property/field/column</param>
        /// <param name="source">value to compare/filter with source property/field/column</param>
        /// <param name="statusCode">value to compare/filter with statusCode property/field/column</param>
        /// <param name="type">value to compare/filter with type property/field/column</param>
        /// <param name="user">value to compare/filter with user property/field/column</param>
        /// <param name="message" > value to compare/filter with message property/field/column</param>
        /// <param name="allXml" > value to compare/filter with allXml property/field/column</param>
        /// <param name="timeUtcRangeLow">value of lower bound</param>
        /// <param name="timeUtcRangeHigh">upper bound</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default"/></returns>
        [HttpGet, ActionName("GetMessageOfDefaultByCommon")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByCommon(
            string application = default(string)
            , string host = default(string)
            , string source = default(string)
            , int? statusCode = default(int?)
            , string type = default(string)
            , string user = default(string)
            , string message = default(string)
            , string allXml = default(string)
            , System.DateTime? timeUtcRangeLow = default(System.DateTime?), System.DateTime? timeUtcRangeHigh = default(System.DateTime?)
            , int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)

        {
            return await this.ThisService.GetMessageOfDefaultByCommon(
                !string.IsNullOrEmpty(application), application
                , !string.IsNullOrEmpty(host), host
                , !string.IsNullOrEmpty(source), source
                , statusCode.HasValue, statusCode
                , !string.IsNullOrEmpty(type), type
                , !string.IsNullOrEmpty(user), user
                , !string.IsNullOrEmpty(message), message
                , !string.IsNullOrEmpty(allXml), allXml
                , timeUtcRangeLow.HasValue || timeUtcRangeHigh.HasValue, timeUtcRangeLow.HasValue, timeUtcRangeLow, timeUtcRangeHigh.HasValue, timeUtcRangeHigh
                , currentIndex
                , pageSize
                , queryOrderByExpression);
        }

        #endregion Query Methods Of DefaultByCommon

        #region Query Methods Of DefaultByIdentifier

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="errorId">value to compare/filter with errorId property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default"/></returns>
        [HttpGet, ActionName("GetMessageOfDefaultByIdentifier")]
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByIdentifier(
            System.Guid? errorId = default(System.Guid?)
            , int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)

        {
            return await this.ThisService.GetMessageOfDefaultByIdentifier(
                errorId.HasValue, errorId
                , currentIndex
                , pageSize
                , queryOrderByExpression);
        }

        #endregion Query Methods Of DefaultByIdentifier

        // DataQueryPerQuerySettingCollection -- MethodDataQuery -- End

        /// <summary>
        /// Gets the Item View Model of ElmahModel.ELMAH_Error.
        /// http://[host]/api/ELMAH_ErrorApi/GetItemVM?
        /// </summary>
        /// <returns></returns>
        // [Authorize]
        [HttpGet]
        public async Task<Elmah.ViewModelData.ELMAH_Error.ItemVM> GetItemVM(
System.Guid? errorId = default(System.Guid?))
        {
            Elmah.AspNetMvcCoreViewModel.ELMAH_Error.ItemVM retval = new Elmah.AspNetMvcCoreViewModel.ELMAH_Error.ItemVM();
            retval.SetServiceProvider(this._serviceProvider);
            await retval.Load(errorId.HasValue, errorId, Framework.ViewModels.UIAction.ViewDetails);
            return retval;
        }

        /// <summary>
        /// Gets the Wrapper View Model of ElmahModel.ELMAH_Error: IndexVM.
        /// http://[host]/api/ELMAH_ErrorApi/GetIndexVM
        /// Content-Type: application/json; charset=utf-8
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <returns></returns>
        // [Authorize]
        // [HttpGet, ActionName("GetIndexVM")] //although JSON allowed in Http Delete request body, but not allowed in HttpClient
        [HttpPost, ActionName("GetIndexVM")]
        public async Task<Elmah.ViewModelData.ELMAH_Error.IndexVM> GetIndexVM(
            [FromBody]Elmah.ViewModelData.ELMAH_Error.IndexVM vm)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var retval = (Elmah.AspNetMvcCoreViewModel.ELMAH_Error.IndexVM)scope.ServiceProvider.GetRequiredService(typeof(Elmah.AspNetMvcCoreViewModel.ELMAH_Error.IndexVM));
                if (vm != null)
                {
                    retval.Criteria = vm.Criteria;
                    retval.QueryPagingSetting = vm.QueryPagingSetting;
                    retval.QueryOrderBySettingCollection = vm.QueryOrderBySettingCollection;
                }
                else
                {
                    retval.Criteria = new Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon();
                    //retval.QueryOrderBySettingCollecionInString = Elmah.ViewModelData.OrderByLists.IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
                }
                if(retval.QueryPagingSetting == null)
                    retval.QueryPagingSetting = Framework.Queries.QueryPagingSetting.GetDefault();

                retval.SetServiceProvider(this._serviceProvider);
                await retval.LoadData(false);
                return retval;
            }
        }

        #region FileRepository

        #endregion FileRepository

/*
        /// <summary>
        /// HearBeat.
        /// http://[host]/api/ELMAH_ErrorApi/HearBeat
        /// </summary>
        /// <returns></returns>
        // [Authorize]
        [HttpGet, ActionName("HeartBeat")]
        public bool HeartBeat()
        {
            return true;
        }
*/
    }
}

