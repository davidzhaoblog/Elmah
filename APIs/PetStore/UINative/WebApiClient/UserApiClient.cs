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
    public partial class UserApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        // Set to string.Empty
        public override string ControllerName => string.Empty;

        public UserApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        public async Task<Framework.WebApi.Response> DeleteUserAsync(string username)
        {
            string url = GetHttpRequestUrl($"/user/{username}");
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

        public async Task<Framework.WebApi.Response<string>> LoginUserAsync(string username, string password)
        {
            string url = GetHttpRequestUrl($"/user/login?username={username}&password={password}");
            try
            {
                var response = await Get<string>(url);
                if (response == null)
                    return new Framework.WebApi.Response<string> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<string> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<string> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        ///// <summary>
        ///// TODO: $unknown 200 response$
        ///// </summary>
        //public async Task<Framework.WebApi.Response<$unknown 200 response$>> LogoutUserAsync()
        //{
        //    string url = GetHttpRequestUrl($"/user/logout");
        //    try
        //    {
        //        var response = await Get<$unknown 200 response$>(url);
        //        if (response == null)
        //            return new Framework.WebApi.Response<$unknown 200 response$> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

        //        return new Framework.WebApi.Response<$unknown 200 response$> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Framework.WebApi.Response<$unknown 200 response$> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
        //    }
        //}

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.User>> GetUserByNameAsync(string username)
        {
            string url = GetHttpRequestUrl($"/user/{username}");
            try
            {
                var response = await Get<Elmah.PetStore.Models.User>(url);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.User> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.User> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.User> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        ///// <summary>
        ///// TODO: $unknown 200 response$
        ///// </summary>
        //public async Task<Framework.WebApi.Response<$unknown 200 response$>> PostAsync(Elmah.PetStore.Models.User item)
        //{
        //    string url = GetHttpRequestUrl($"/user");
        //    try
        //    {
        //        var response = await PostCommon<Elmah.PetStore.Models.User, $unknown 200 response$>(url, item);
        //        if (response == null)
        //            return new Framework.WebApi.Response<$unknown 200 response$> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

        //        return new Framework.WebApi.Response<$unknown 200 response$> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Framework.WebApi.Response<$unknown 200 response$> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
        //    }
        //}

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.User>> PostAsync(Elmah.PetStore.Models.User[] item)
        {
            string url = GetHttpRequestUrl($"/user/createWithList");
            try
            {
                var response = await PostCommon<Elmah.PetStore.Models.User[], Elmah.PetStore.Models.User>(url, item);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.User> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.User> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.User> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        ///// <summary>
        ///// TODO: $unknown 200 response$
        ///// </summary>
        //public async Task<Framework.WebApi.Response<$unknown 200 response$>> PutAsync(Elmah.PetStore.Models.User item)
        //{
        //    string url = GetHttpRequestUrl($"/user/{username}");
        //    try
        //    {
        //        var response = await PutCommon<Elmah.PetStore.Models.User, $unknown 200 response$>(url, item);
        //        if (response == null)
        //            return new Framework.WebApi.Response<$unknown 200 response$> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

        //        return new Framework.WebApi.Response<$unknown 200 response$> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Framework.WebApi.Response<$unknown 200 response$> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
        //    }
        //}

    }
}

