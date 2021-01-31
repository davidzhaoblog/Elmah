using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.WebApiClient
{
    public class ExtensionApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        public override string ControllerName => throw new NotImplementedException();

        public ExtensionApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        // TODO: Add More WebApi call methods

/*
        public async Task<TypeNameOfResponse> GetItemVM(TypeNameOfRequest input)
        {
            const string actionName = "actionName";
            string url = GetHttpRequestUrlWithoutController(actionName);
            var request = new TypeNameOfRequest
            {
                ProductNumber = productNumber,
                SensorDeviceSerialNumber = sensorDeviceSerialNumber,
                StoreShortGuid = storeShortGuid
            };
            return await Post<TypeNameOfResponse, TypeNameOfRequest>(url, request);
        }
*/
    }
}

