using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

namespace Elmah.WebApiClient
{
    /// <summary>

    /// </summary>
    public partial class ElmahTypeApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        #region fields and properties

        public const string ControllerNameString = "ElmahTypeApi";
        public override string ControllerName
        {
            get
            {
                return ControllerNameString;
            }
        }

        #endregion fields and properties

        #region contructors

        public ElmahTypeApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        #endregion contructors

        #region insert, update and delete in an entity

        /// <summary>
        /// Insert/Update an entity of <see cref=" Elmah.DataSourceEntities.ElmahType"/>.
        /// http://[host]/api/ElmahTypeApi/UpsertEntity
        /// </summary>
        /// <param name="input">input entity</param>
        public async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> UpsertEntityAsync(
            Elmah.DataSourceEntities.ElmahType input)
        {
            const string ActionName_UpsertEntity = "UpsertEntity";
            string url = GetHttpRequestUrl(ActionName_UpsertEntity);

            return await Post<Elmah.DataSourceEntities.ElmahType, Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn>(url, input);
        }

/*
        /// <summary>
        /// Inserts an entity of <see cref=" Elmah.DataSourceEntities.ElmahType"/>.
        /// http://[host]/api/ElmahTypeApi/InsertEntity
        /// </summary>
        /// <param name="input">input entity</param>
        public async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> InsertEntityAsync(
            Elmah.DataSourceEntities.ElmahType input)
        {
            const string ActionName_InsertEntity = "InsertEntity";
            string url = GetHttpRequestUrl(ActionName_InsertEntity);

            return await Post<Elmah.DataSourceEntities.ElmahType, Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn>(url, input);
        }

        /// <summary>
        /// Updates an entity of <see cref=" Elmah.DataSourceEntities.ElmahType"/>.
        /// http://[host]/api/ElmahTypeApi/UpdateEntity
        /// </summary>
        /// <param name="input">input entity</param>
        public async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> UpdateEntityAsync(
            Elmah.DataSourceEntities.ElmahType input)
        {
            const string ActionName_UpdateEntity = "UpdateEntity";
            string url = GetHttpRequestUrl(ActionName_UpdateEntity);

            return await Put<Elmah.DataSourceEntities.ElmahType, Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn>(url, input);
        }

/*

        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ElmahType"/> by identifier.
        /// http://[host]/api/ElmahTypeApi/DeleteByIdentifierEntity
        /// </summary>
        /// <param name="identifier">input identifier of an entity</param>
        public async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> DeleteByIdentifierEntityAsync(
            Elmah.DataSourceEntities.ElmahTypeIdentifier identifier)
        {
            const string ActionName_DeleteByIdentifierEntity = "DeleteByIdentifierEntity";
            string url = GetHttpRequestUrl(ActionName_DeleteByIdentifierEntity);

            return await Post<Elmah.DataSourceEntities.ElmahTypeIdentifier, Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn>(url, identifier);
        }
*/

/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ElmahType"/> using .net value types.
        /// http://[host]/api/ElmahTypeApi/DeleteEntity
        /// </summary>
        /// <param name="input">input entity</param>
        public async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> DeleteEntityAsync(
            Elmah.DataSourceEntities.ElmahType input)
        {
            const string ActionName_DeleteEntity = "DeleteEntity";
            string url = GetHttpRequestUrl(ActionName_DeleteEntity);

            return await Post<Elmah.DataSourceEntities.ElmahType, Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn>(url, input);
        }
*/
        #endregion insert, update and delete in an entity

        #region delete using .Net value types
/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ElmahType"/> by identifier using .net value types.
        /// http://[host]/api/ElmahTypeApi/DeleteByIdentifier
        /// </summary>
        /// <param name="id">property value of System.Int64</param>
        public async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> DeleteByIdentifierAsync(
            System.Int64 id
            )
        {
            const string ActionName_DeleteByIdentifier = "DeleteByIdentifier";
            string url = GetHttpRequestUrl(ActionName_DeleteByIdentifier);

            return await Delete<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn>(url);
        }
*/
        #endregion delete using .Net value types

        #region batch insert, update and delete in an entity collection

/*
        /// <summary>
        /// batch insert a collection of <see cref=" Elmah.DataSourceEntities.ElmahType"/>.
        /// http://[host]/api/ElmahTypeApi/BatchInsert
        /// </summary>
        /// <param name="input">The input collection.</param>
        public async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> BatchInsertAsync(List<Elmah.DataSourceEntities.ElmahType> input)
        {
            const string ActionName_BatchInsert = "BatchInsert";
            string url = GetHttpRequestUrl(ActionName_BatchInsert);

            return await Post<List<Elmah.DataSourceEntities.ElmahType>, Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn>(url, input);
        }

        /// <summary>
        /// batch update a collection of <see cref=" Elmah.DataSourceEntities.ElmahType"/>.
        /// http://[host]/api/ElmahTypeApi/BatchUpdate
        /// </summary>
        public async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> BatchUpdateAsync(List<Elmah.DataSourceEntities.ElmahType> input)
        {
            const string ActionName_BatchUpdate = "BatchUpdate";
            string url = GetHttpRequestUrl(ActionName_BatchUpdate);

            return await Put<List<Elmah.DataSourceEntities.ElmahType>, Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn>(url, input);
        }

        /// <summary>
        /// batch delete a collection of <see cref=" Elmah.DataSourceEntities.ElmahType"/>.
        /// http://[host]/api/ElmahTypeApi/BatchDelete
        /// </summary>
        /// <param name="input">The input collection.</param>
        // [HttpDelete, ActionName("BatchDelete")] //although JSON allowed in Http Delete request body, but not allowed in HttpClient
        public async Task<Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn> BatchDeleteAsync(List<Elmah.DataSourceEntities.ElmahType> input)
        {
            const string ActionName_BatchDelete = "BatchDelete";
            string url = GetHttpRequestUrl(ActionName_BatchDelete);

            return await Post<List<Elmah.DataSourceEntities.ElmahType>, Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn>(url, input);
        }
*/

        #endregion batch insert, update and delete in an entity collection

        // DataQueryPerQuerySettingCollection -- MethodDataQuery -- Start

        // DataQueryPerQuerySettingCollection -- MethodDataQuery -- End

        /// <summary>
        /// Gets the build log item.
        /// http://[host]/api/ElmahTypeApi/GetItemVM?id=1
        /// </summary>
        /// <returns></returns>
        public async Task<Elmah.ViewModelData.ElmahType.ItemVM> GetItemVMAsync(
            System.String type)
        {
            const string ActionName_GetItemVM = "GetItemVM";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("type", type.ToString());
            string url = GetHttpRequestUrl(ActionName_GetItemVM, parameters);

            return await GetItemViewModel<Elmah.ViewModelData.ElmahType.ItemVM>(url);
        }

        /// <summary>
        /// Gets the wp common of build log vm.
        /// http://[host]/api/ElmahTypeApi/GetIndexVM
        /// Content-Type: application/json; charset=utf-8
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <returns></returns>
        public async Task<Elmah.ViewModelData.ElmahType.IndexVM> GetIndexVMAsync(
            Elmah.ViewModelData.ElmahType.IndexVM vm)
        {
            const string ActionName_GetIndexVM = "GetIndexVM";
            string url = GetHttpRequestUrl(ActionName_GetIndexVM);

            return await Post<Elmah.ViewModelData.ElmahType.IndexVM>(url, vm);
        }

/*
        /// <summary>
        /// Hearts the beat asynchronous.
        /// http://[host]/api/ElmahTypeApi/HeartBeat
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> HeartBeatAsync()
        {
            const string ActionName_HeartBeat = "HeartBeat";
            string url = GetHttpRequestUrl(ActionName_HeartBeat);
            var response = await Client.GetAsync(url);

            return response;
        }
*/
    }
}

