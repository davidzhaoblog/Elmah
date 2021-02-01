using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WebApiClient
{
    public class ListsApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        public override string ControllerName => "ListsApi";

        public ListsApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        public async Task<Dictionary<Elmah.EntityContracts.Enums.CacheLists, Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection>> GetAllCachedLists()
        {
            const string ActionName = "GetAllCachedLists";

            string url = GetListsApiHttpRequestUrl(ActionName, null);
            return await Get<Dictionary<Elmah.EntityContracts.Enums.CacheLists, Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection>> (url);
        }

        #region #1 List of ElmahModel.ELMAH_Error

        public async Task<List<Framework.Models.NameValuePair>> ELMAH_ErrorList(
            string application = default(string)
            , string host = default(string)
            , string source = default(string)
            , int? statusCode = default(int?)
            , string type = default(string)
            , string user = default(string)
            , int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null
            )
        {
            const string ActionName_ELMAH_ErrorList = "ELMAH_ErrorList";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("application", application);
            parameters.Add("host", host);
            parameters.Add("source", source);
            parameters.Add("statusCode", statusCode.ToString());
            parameters.Add("type", type);
            parameters.Add("user", user);
            parameters.Add("currentIndex", currentIndex.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("queryOrderByExpression", queryOrderByExpression);

            string url = GetListsApiHttpRequestUrl(ActionName_ELMAH_ErrorList, parameters);
            return await Get<List<Framework.Models.NameValuePair>>(url);
        }

        #endregion #1 List of ElmahModel.ELMAH_Error

        #region #2 List of ElmahModel.ElmahApplication

        public async Task<List<Framework.Models.NameValuePair>> ElmahApplicationList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null
            )
        {
            const string ActionName_ElmahApplicationList = "ElmahApplicationList";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("currentIndex", currentIndex.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("queryOrderByExpression", queryOrderByExpression);

            string url = GetListsApiHttpRequestUrl(ActionName_ElmahApplicationList, parameters);
            return await Get<List<Framework.Models.NameValuePair>>(url);
        }

        #endregion #2 List of ElmahModel.ElmahApplication

        #region #3 List of ElmahModel.ElmahHost

        public async Task<List<Framework.Models.NameValuePair>> ElmahHostList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null
            )
        {
            const string ActionName_ElmahHostList = "ElmahHostList";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("currentIndex", currentIndex.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("queryOrderByExpression", queryOrderByExpression);

            string url = GetListsApiHttpRequestUrl(ActionName_ElmahHostList, parameters);
            return await Get<List<Framework.Models.NameValuePair>>(url);
        }

        #endregion #3 List of ElmahModel.ElmahHost

        #region #4 List of ElmahModel.ElmahSource

        public async Task<List<Framework.Models.NameValuePair>> ElmahSourceList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null
            )
        {
            const string ActionName_ElmahSourceList = "ElmahSourceList";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("currentIndex", currentIndex.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("queryOrderByExpression", queryOrderByExpression);

            string url = GetListsApiHttpRequestUrl(ActionName_ElmahSourceList, parameters);
            return await Get<List<Framework.Models.NameValuePair>>(url);
        }

        #endregion #4 List of ElmahModel.ElmahSource

        #region #5 List of ElmahModel.ElmahStatusCode

        public async Task<List<Framework.Models.NameValuePair>> ElmahStatusCodeList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null
            )
        {
            const string ActionName_ElmahStatusCodeList = "ElmahStatusCodeList";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("currentIndex", currentIndex.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("queryOrderByExpression", queryOrderByExpression);

            string url = GetListsApiHttpRequestUrl(ActionName_ElmahStatusCodeList, parameters);
            return await Get<List<Framework.Models.NameValuePair>>(url);
        }

        #endregion #5 List of ElmahModel.ElmahStatusCode

        #region #6 List of ElmahModel.ElmahType

        public async Task<List<Framework.Models.NameValuePair>> ElmahTypeList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null
            )
        {
            const string ActionName_ElmahTypeList = "ElmahTypeList";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("currentIndex", currentIndex.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("queryOrderByExpression", queryOrderByExpression);

            string url = GetListsApiHttpRequestUrl(ActionName_ElmahTypeList, parameters);
            return await Get<List<Framework.Models.NameValuePair>>(url);
        }

        #endregion #6 List of ElmahModel.ElmahType

        #region #7 List of ElmahModel.ElmahUser

        public async Task<List<Framework.Models.NameValuePair>> ElmahUserList(
            int currentIndex = -1
            , int pageSize = -1
            , string queryOrderByExpression = null
            )
        {
            const string ActionName_ElmahUserList = "ElmahUserList";

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("currentIndex", currentIndex.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            parameters.Add("queryOrderByExpression", queryOrderByExpression);

            string url = GetListsApiHttpRequestUrl(ActionName_ElmahUserList, parameters);
            return await Get<List<Framework.Models.NameValuePair>>(url);
        }

        #endregion #7 List of ElmahModel.ElmahUser

    }
}

