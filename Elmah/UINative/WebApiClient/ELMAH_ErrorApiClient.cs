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
    public partial class ELMAH_ErrorApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        #region fields and properties

        public const string ControllerNameString = "ELMAH_ErrorApi";
        public override string ControllerName
        {
            get
            {
                return ControllerNameString;
            }
        }

        #endregion fields and properties

        #region contructors

        public ELMAH_ErrorApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        #endregion contructors

        #region insert, update and delete in an entity

        /// <summary>
        /// Insert/Update an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// http://[host]/api/ELMAH_ErrorApi/UpsertEntity
        /// </summary>
        /// <param name="input">input entity</param>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> UpsertEntityAsync(
            Elmah.DataSourceEntities.ELMAH_Error input)
        {
            const string ActionName_UpsertEntity = "UpsertEntity";
            string url = GetHttpRequestUrl(ActionName_UpsertEntity);

            return await Post<Elmah.DataSourceEntities.ELMAH_Error, Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default>(url, input);
        }

/*
        /// <summary>
        /// Inserts an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// http://[host]/api/ELMAH_ErrorApi/InsertEntity
        /// </summary>
        /// <param name="input">input entity</param>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> InsertEntityAsync(
            Elmah.DataSourceEntities.ELMAH_Error input)
        {
            const string ActionName_InsertEntity = "InsertEntity";
            string url = GetHttpRequestUrl(ActionName_InsertEntity);

            return await Post<Elmah.DataSourceEntities.ELMAH_Error, Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default>(url, input);
        }

        /// <summary>
        /// Updates an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// http://[host]/api/ELMAH_ErrorApi/UpdateEntity
        /// </summary>
        /// <param name="input">input entity</param>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> UpdateEntityAsync(
            Elmah.DataSourceEntities.ELMAH_Error input)
        {
            const string ActionName_UpdateEntity = "UpdateEntity";
            string url = GetHttpRequestUrl(ActionName_UpdateEntity);

            return await Put<Elmah.DataSourceEntities.ELMAH_Error, Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default>(url, input);
        }

/*

        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/> by identifier.
        /// http://[host]/api/ELMAH_ErrorApi/DeleteByIdentifierEntity
        /// </summary>
        /// <param name="identifier">input identifier of an entity</param>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteByIdentifierEntityAsync(
            Elmah.DataSourceEntities.ELMAH_ErrorIdentifier identifier)
        {
            const string ActionName_DeleteByIdentifierEntity = "DeleteByIdentifierEntity";
            string url = GetHttpRequestUrl(ActionName_DeleteByIdentifierEntity);

            return await Post<Elmah.DataSourceEntities.ELMAH_ErrorIdentifier, Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn>(url, identifier);
        }
*/

/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/> using .net value types.
        /// http://[host]/api/ELMAH_ErrorApi/DeleteEntity
        /// </summary>
        /// <param name="input">input entity</param>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteEntityAsync(
            Elmah.DataSourceEntities.ELMAH_Error input)
        {
            const string ActionName_DeleteEntity = "DeleteEntity";
            string url = GetHttpRequestUrl(ActionName_DeleteEntity);

            return await Post<Elmah.DataSourceEntities.ELMAH_Error, Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn>(url, input);
        }
*/
        #endregion insert, update and delete in an entity

        #region delete using .Net value types
/*
        /// <summary>
        /// delete an entity of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/> by identifier using .net value types.
        /// http://[host]/api/ELMAH_ErrorApi/DeleteByIdentifier
        /// </summary>
        /// <param name="id">property value of System.Int64</param>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> DeleteByIdentifierAsync(
            System.Int64 id
            )
        {
            const string ActionName_DeleteByIdentifier = "DeleteByIdentifier";
            string url = GetHttpRequestUrl(ActionName_DeleteByIdentifier);

            return await Delete<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn>(url);
        }
*/
        #endregion delete using .Net value types

        #region batch insert, update and delete in an entity collection

/*
        /// <summary>
        /// batch insert a collection of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// http://[host]/api/ELMAH_ErrorApi/BatchInsert
        /// </summary>
        /// <param name="input">The input collection.</param>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchInsertAsync(List<Elmah.DataSourceEntities.ELMAH_Error> input)
        {
            const string ActionName_BatchInsert = "BatchInsert";
            string url = GetHttpRequestUrl(ActionName_BatchInsert);

            return await Post<List<Elmah.DataSourceEntities.ELMAH_Error>, Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn>(url, input);
        }

        /// <summary>
        /// batch update a collection of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// http://[host]/api/ELMAH_ErrorApi/BatchUpdate
        /// </summary>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchUpdateAsync(List<Elmah.DataSourceEntities.ELMAH_Error> input)
        {
            const string ActionName_BatchUpdate = "BatchUpdate";
            string url = GetHttpRequestUrl(ActionName_BatchUpdate);

            return await Put<List<Elmah.DataSourceEntities.ELMAH_Error>, Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn>(url, input);
        }

        /// <summary>
        /// batch delete a collection of <see cref=" Elmah.DataSourceEntities.ELMAH_Error"/>.
        /// http://[host]/api/ELMAH_ErrorApi/BatchDelete
        /// </summary>
        /// <param name="input">The input collection.</param>
        // [HttpDelete, ActionName("BatchDelete")] //although JSON allowed in Http Delete request body, but not allowed in HttpClient
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn> BatchDeleteAsync(List<Elmah.DataSourceEntities.ELMAH_Error> input)
        {
            const string ActionName_BatchDelete = "BatchDelete";
            string url = GetHttpRequestUrl(ActionName_BatchDelete);

            return await Post<List<Elmah.DataSourceEntities.ELMAH_Error>, Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn>(url, input);
        }
*/

        #endregion batch insert, update and delete in an entity collection

        // DataQueryPerQuerySettingCollection -- MethodDataQuery -- Start

        #region Query Methods Of DefaultByIdentifier

/*
        public const string ActionName_GetCollectionOfDefaultByIdentifier = "GetCollectionOfDefaultByIdentifier";
        /// <summary>
        /// Gets the collection of entity of common.
        /// http://[host]/api/ELMAH_ErrorApi/GetCollectionOfDefaultByIdentifier
        /// </summary>
        /// <param name="errorId">value to compare/filter with errorId property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>the collection of type <see cref="List<Elmah.DataSourceEntities.ELMAH_Error.Default>"/></returns>
        public async Task<List<Elmah.DataSourceEntities.ELMAH_Error.Default>> GetCollectionOfDefaultByIdentifierAsync(
            System.Guid? errorId = default(System.Guid?)
            , int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null
            )
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("errorId", errorId.ToString());
            parameters.Add("currentIndex", currentIndex.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("queryOrderByExpression", queryOrderByExpression);

            string url = GetHttpRequestUrl(ActionName_GetCollectionOfDefaultByIdentifier, parameters);
            return await Get<List<Elmah.DataSourceEntities.ELMAH_Error.Default>>(url);
        }
*/
        public const string ActionName_GetMessageOfDefaultByIdentifier = "GetMessageOfDefaultByIdentifier";
        /// <summary>
        /// Gets the collection of entity of common.
        /// http://[host]/api/ELMAH_ErrorApi/GetMessageOfDefaultByIdentifier
        /// </summary>
        /// <param name="errorId">value to compare/filter with errorId property/field/column</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        /// <returns>the collection of type <see cref="List<Elmah.DataSourceEntities.ELMAH_Error.Default>"/></returns>
        public async Task<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default> GetMessageOfDefaultByIdentifierAsync(
            System.Guid? errorId = default(System.Guid?)
            , int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null
            )
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("errorId", errorId.ToString());
            parameters.Add("currentIndex", currentIndex.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("queryOrderByExpression", queryOrderByExpression);

            string url = GetHttpRequestUrl(ActionName_GetMessageOfDefaultByIdentifier, parameters);
            return await Get<Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default>(url);
        }

        #endregion Query Methods Of DefaultByIdentifier

        // DataQueryPerQuerySettingCollection -- MethodDataQuery -- End

        /// <summary>
        /// Gets the build log item.
        /// http://[host]/api/ELMAH_ErrorApi/GetItemVM?id=1
        /// </summary>
        /// <returns></returns>
        public async Task<Elmah.ViewModelData.ELMAH_Error.ItemVM> GetItemVMAsync(
            System.Guid errorId)
        {
            const string ActionName_GetItemVM = "GetItemVM";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("errorId", errorId.ToString());
            string url = GetHttpRequestUrl(ActionName_GetItemVM, parameters);

            return await GetItemViewModel<Elmah.ViewModelData.ELMAH_Error.ItemVM>(url);
        }

        /// <summary>
        /// Gets the wp common of build log vm.
        /// http://[host]/api/ELMAH_ErrorApi/GetIndexVM
        /// Content-Type: application/json; charset=utf-8
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <returns></returns>
        public async Task<Elmah.ViewModelData.ELMAH_Error.IndexVM> GetIndexVMAsync(
            Elmah.ViewModelData.ELMAH_Error.IndexVM vm)
        {
            const string ActionName_GetIndexVM = "GetIndexVM";
            string url = GetHttpRequestUrl(ActionName_GetIndexVM);

            return await Post<Elmah.ViewModelData.ELMAH_Error.IndexVM>(url, vm);
        }

/*
        /// <summary>
        /// Hearts the beat asynchronous.
        /// http://[host]/api/ELMAH_ErrorApi/HeartBeat
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

