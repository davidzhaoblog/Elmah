using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Elmah.PetStore.WebApiClient
{
    /// <summary>

    /// </summary>
    public partial class StoreApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        // Set to string.Empty
        public override string ControllerName => string.Empty;

        public StoreApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        public async Task<Framework.WebApi.Response> DeleteOrderAsync(long orderId)
        {
            string url = GetHttpRequestUrl($"/store/order/{orderId}");
            try
            {
                var response = await DeleteCommon(url);
                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        ///// <summary>
        ///// TODO: $dynamic type, compile error, should use value type or a new classes$
        ///// </summary>
        //public async Task<Framework.WebApi.Response<dynamic>> GetInventoryAsync()
        //{
        //    string url = GetHttpRequestUrl($"/store/inventory");
        //    try
        //    {
        //        var response = await Get<dynamic>(url);
        //        if (response == null)
        //            return new Framework.WebApi.Response<dynamic> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

        //        return new Framework.WebApi.Response<dynamic> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Framework.WebApi.Response<dynamic> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
        //    }
        //}

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.Order>> GetOrderByIdAsync(long orderId)
        {
            string url = GetHttpRequestUrl($"/store/order/{orderId}");
            try
            {
                var response = await Get<Elmah.PetStore.Models.Order>(url);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.Order> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.Order> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.Order> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.Order>> PostAsync(Elmah.PetStore.Models.Order item)
        {
            string url = GetHttpRequestUrl($"/store/order");
            try
            {
                var response = await PostCommon<Elmah.PetStore.Models.Order, Elmah.PetStore.Models.Order>(url, item);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.Order> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.Order> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.Order> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

    }
}

