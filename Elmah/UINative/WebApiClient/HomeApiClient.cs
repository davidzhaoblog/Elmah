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
    public partial class HomeApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        public const string ControllerNameString = "HomeApi";
        public override string ControllerName
        {
            get
            {
                return ControllerNameString;
            }
        }

        public HomeApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        public const string ActionName_GetSystemDashboardVM = "GetSystemDashboard";
        /// <summary>
        /// Gets the build log item.
        /// http://[host]/api/HomeApi/GetSystemDashboardVM
        /// </summary>
        /// <returns></returns>
        public async Task<Elmah.ViewModelData.SystemDashboardVM> GetSystemDashboardVM()
        {
            string url = GetHttpRequestUrl(ActionName_GetSystemDashboardVM);
            return await Get<Elmah.ViewModelData.SystemDashboardVM>(url);
        }

        /// <summary>
        /// Gets the build log item.
        /// http://[host]/api/HomeApi/GetSystemDashboardVM
        /// </summary>
        /// <returns></returns>
        public async Task<Elmah.ViewModelData.DashboardVM> GetDashboardVM(string shortGuid)
        {
            const string ActionName = "GetDashboard";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("shortGuid", shortGuid);
            string url = GetHttpRequestUrl(ActionName, parameters);
            return await Get<Elmah.ViewModelData.DashboardVM>(url);
        }

/*
        public const string ActionName_HeartBeat = "HeartBeat";
        /// <summary>
        /// Hearts the beat asynchronous.
        /// http://[host]/api/HomeApi/HeartBeat
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> HeartBeatAsync()
        {
            string url = GetHttpRequestUrl(ActionName_HeartBeat);
            var response = await Client.GetAsync(url);

            return response;
        }
*/
    }
}

