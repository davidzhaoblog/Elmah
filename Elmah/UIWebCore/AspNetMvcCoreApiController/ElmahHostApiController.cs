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
    public partial class ElmahHostApiController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private readonly ElmahModelEntities _linqContext;

        public ElmahHostApiController(Elmah.WcfContracts.IElmahHostService thisService, IServiceProvider serviceProvider, ILogger<ElmahHostApiController> logger, ElmahModelEntities linqContext)
        {
            this._serviceProvider = serviceProvider;
            this.ThisService = thisService;
            this._logger = logger;
            this._linqContext = linqContext;
        }

        Elmah.WcfContracts.IElmahHostService ThisService { get; set; }

        #region insert, update and delete in an entity

        /// <summary>
        /// Insert/Update an entity of <see cref=" Elmah.DataSourceEntities.ElmahHost"/>.
        /// </summary>
        /// <param name="input">input entity</param>
        // [Authorize]
        [HttpPost, ActionName("UpsertEntity")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> UpsertEntity(
            [FromBody] Elmah.DataSourceEntities.ElmahHost input)
        {
            var request = new Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn();
            request.Criteria.Add(input);
            Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn responseMessage = await this.ThisService.UpsertEntity(request);
            return responseMessage;;
        }

/*
        /// <summary>
        /// Inserts an entity of <see cref=" Elmah.DataSourceEntities.ElmahHost"/>.
        /// </summary>
        /// <param name="input">input entity</param>
        // [Authorize]
        [HttpPost, ActionName("InsertEntity")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> InsertEntity(
            [FromBody] Elmah.DataSourceEntities.ElmahHost input)
        {
            var request = new Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn();
            request.Criteria.Add(input);
            Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn responseMessage = await this.ThisService.InsertEntity(request);
            return responseMessage;;
        }

        /// <summary>
        /// Updates an entity of <see cref=" Elmah.DataSourceEntities.ElmahHost"/>.
        /// </summary>
        /// <param name="input">input entity</param>
        // [Authorize]
        [HttpPut, ActionName("UpdateEntity")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> UpdateEntity(
            [FromBody] Elmah.DataSourceEntities.ElmahHost input)
        {
            var request = new Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn();
            request.Criteria.Add(input);
            Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn responseMessage = await this.ThisService.UpdateEntity(request);
            return responseMessage;;
        }
*/

/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ElmahHost"/> by identifier.
        /// </summary>
        /// <param name="identifier">input identifier of an entity</param>
        // [Authorize]
        [HttpDelete, ActionName("DeleteByIdentifierEntity")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> DeleteByIdentifierEntity(
            [FromBody] Elmah.DataSourceEntities.ElmahHostIdentifier identifier)
        {
            var request = new Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltInOfIdentifier();
            request.Criteria = identifier;

            Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn responseMessage = await this.ThisService.DeleteByIdentifierEntity(request);

            return responseMessage;
        }
*/

/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ElmahHost"/> using .net value types.
        /// Can't use HttpDelete, because body is ignored.
        /// </summary>
        /// <param name="input">input entity</param>
        // [Authorize]
        [HttpPost, ActionName("DeleteEntity")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> DeleteEntity(
            [FromBody] Elmah.DataSourceEntities.ElmahHost input)
        {
            var request = new Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn();
            request.Criteria.Add(input);
            Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn responseMessage = await this.ThisService.DeleteEntity(request);

            return responseMessage;
        }
*/
        #endregion insert, update and delete in an entity

        #region delete using .Net value types
/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ElmahHost"/> by identifier using .net value types.
        /// </summary>
        /// <param name="host">property value of System.String</param>
        // [Authorize]
        [HttpDelete, ActionName("DeleteByIdentifier")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> DeleteByIdentifier(
            System.String host
            )
        {
            return await this.DeleteByIdentifierEntity(new Elmah.DataSourceEntities.ElmahHostIdentifier{
                Host = host});
        }
*/

        #endregion delete using .Net value types

        #region batch insert, update and delete in an entity collection
/*
        /// <summary>
        /// batch update a collection of <see cref=" Elmah.DataSourceEntities.ElmahHost"/>.
        /// </summary>
        // [Authorize]
        [HttpPut, ActionName("BatchUpdate")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> BatchUpdate([FromBody] Elmah.DataSourceEntities.ElmahHostCollection input)
        {
            var request = new Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn();
            request.Criteria.AddRange(input);
            Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn responseMessage = await this.ThisService.BatchUpdate(request);

            return responseMessage;
        }

        /// <summary>
        /// batch insert a collection of <see cref=" Elmah.DataSourceEntities.ElmahHost"/>.
        /// </summary>
        /// <param name="input">The input collection.</param>
        // [Authorize]
        [HttpPost, ActionName("BatchInsert")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> BatchInsert([FromBody] Elmah.DataSourceEntities.ElmahHostCollection input)
        {
            var request = new Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn();
            request.Criteria.AddRange(input);
            Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn responseMessage = await this.ThisService.BatchInsert(request);

            return responseMessage;
        }

        /// <summary>
        /// batch delete a collection of <see cref=" Elmah.DataSourceEntities.ElmahHost"/>.
        /// </summary>
        /// <param name="input">The input collection.</param>
        // [Authorize]
        [HttpDelete, ActionName("BatchDelete")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> BatchDelete([FromBody] Elmah.DataSourceEntities.ElmahHostCollection input)
        {
            var request = new Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn();
            request.Criteria.AddRange(input);
            Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn responseMessage = await this.ThisService.BatchDelete(request);

            return responseMessage;
        }
*/
        #endregion batch insert, update and delete in an entity collection

        // DataQueryPerQuerySettingCollection -- MethodDataQuery -- Start

        #region Query Methods Of EntityByCommon

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="host" > value to compare/filter with host property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn"/></returns>
        [HttpGet, ActionName("GetMessageOfEntityByCommon")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> GetMessageOfEntityByCommon(
            string host = default(string)
            , int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)

        {
            return await this.ThisService.GetMessageOfEntityByCommon(
                !string.IsNullOrEmpty(host), host
                , currentIndex
                , pageSize
                , queryOrderByExpression);
        }

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of EntityByIdentifier

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="host">value to compare/filter with host property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>business layer built-in message <see cref="Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn"/></returns>
        [HttpGet, ActionName("GetMessageOfEntityByIdentifier")]
        public async Task<Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn> GetMessageOfEntityByIdentifier(
            string host = default(string)
            , int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null)

        {
            return await this.ThisService.GetMessageOfEntityByIdentifier(
                !string.IsNullOrEmpty(host), host
                , currentIndex
                , pageSize
                , queryOrderByExpression);
        }

        #endregion Query Methods Of EntityByIdentifier

        // DataQueryPerQuerySettingCollection -- MethodDataQuery -- End

        /// <summary>
        /// Gets the Item View Model of ElmahModel.ElmahHost.
        /// http://[host]/api/ElmahHostApi/GetItemVM?
        /// </summary>
        /// <returns></returns>
        // [Authorize]
        [HttpGet]
        public async Task<Elmah.ViewModelData.ElmahHost.ItemVM> GetItemVM(
string host = default(string))
        {
            Elmah.AspNetMvcCoreViewModel.ElmahHost.ItemVM retval = new Elmah.AspNetMvcCoreViewModel.ElmahHost.ItemVM();
            retval.SetServiceProvider(this._serviceProvider);
            await retval.Load(!string.IsNullOrEmpty(host), host, Framework.ViewModels.UIAction.ViewDetails);
            return retval;
        }

        /// <summary>
        /// Gets the Wrapper View Model of ElmahModel.ElmahHost: IndexVM.
        /// http://[host]/api/ElmahHostApi/GetIndexVM
        /// Content-Type: application/json; charset=utf-8
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <returns></returns>
        // [Authorize]
        // [HttpGet, ActionName("GetIndexVM")] //although JSON allowed in Http Delete request body, but not allowed in HttpClient
        [HttpPost, ActionName("GetIndexVM")]
        public async Task<Elmah.ViewModelData.ElmahHost.IndexVM> GetIndexVM(
            [FromBody]Elmah.ViewModelData.ElmahHost.IndexVM vm)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var retval = (Elmah.AspNetMvcCoreViewModel.ElmahHost.IndexVM)scope.ServiceProvider.GetRequiredService(typeof(Elmah.AspNetMvcCoreViewModel.ElmahHost.IndexVM));
                if (vm != null)
                {
                    retval.Criteria = vm.Criteria;
                    retval.QueryPagingSetting = vm.QueryPagingSetting;
                    retval.QueryOrderBySettingCollecionInString = vm.QueryOrderBySettingCollecionInString;
                }
                else
                {
                    retval.Criteria = new Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaCommon();
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
        /// http://[host]/api/ElmahHostApi/HearBeat
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

