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
    public partial class MapApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        public const string ControllerNameString = "MapApi";
        public override string ControllerName
        {
            get
            {
                return ControllerNameString;
            }
        }

        public MapApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        public const string ActionName_GetMapVMRange = "GetMapVMRange";
        /// <summary>
        /// Gets the build log item.
        /// http://[host]/api/MapApi/GetMapVMRange?
        /// </summary>
        /// <returns></returns>
        public async Task<Elmah.ViewModelData.MapVM> GetMapVMRange(double lat, double lon, string anyText = null, long? distance1 = null, long? distance2 = 5000)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("lat", lat.ToString());
            parameters.Add("lon", lon.ToString());
            parameters.Add("anyText", anyText.ToString());
            if(distance1.HasValue)
                parameters.Add("distance1", distance1.ToString());
            if (distance2.HasValue)
                parameters.Add("distance2", distance2.ToString());
            string url = GetHttpRequestUrl(ActionName_GetMapVMRange, parameters);

            return await Get<Elmah.ViewModelData.MapVM>(url);
        }

        public const string ActionName_GetMapVM = "GetMapVM";
        /// <summary>
        /// Gets the build log item.
        /// http://[host]/api/MapApi/GetMapVM?lat1=1&lon1=2&lat2=3&lon2=4&anytext=any
        /// </summary>
        /// <returns></returns>
        public async Task<Elmah.ViewModelData.MapVM> GetMapVM(double lat1, double lon1, double lat2, double lon2, string anyText = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("lat1", lat1.ToString());
            parameters.Add("lon1", lon1.ToString());
            parameters.Add("lat2", lat2.ToString());
            parameters.Add("lon2", lon2.ToString());
            parameters.Add("anyText", anyText);
            string url = GetHttpRequestUrl(ActionName_GetMapVM, parameters);

            return await Get<Elmah.ViewModelData.MapVM>(url);
        }

/*
        public const string ActionName_HeartBeat = "HeartBeat";
        /// <summary>
        /// Hearts the beat asynchronous.
        /// http://[host]/api/MapApi/HeartBeat
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

