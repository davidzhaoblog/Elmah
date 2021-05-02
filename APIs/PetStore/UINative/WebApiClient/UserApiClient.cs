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

        // Get.1 LoginUser /user/login

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

        // Get.2 LogoutUser /user/logout

        public async Task<Framework.WebApi.Response> LogoutUserAsync()
        {
            string url = GetHttpRequestUrl($"/user/logout");
            try
            {
                await Get(url);
                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK};
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        // Get.3 GetUserByName /user/{username}

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

        // Post.1 CreateUser /user

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.User>> CreateUserAsync(Elmah.PetStore.Models.User item)
        {
            string url = GetHttpRequestUrl($"/user");
            try
            {
                var response = await PostCommon<Elmah.PetStore.Models.User, Elmah.PetStore.Models.User>(url, item);
                if (response == null)
                    return new Framework.WebApi.Response<Elmah.PetStore.Models.User> { Status = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource };

                return new Framework.WebApi.Response<Elmah.PetStore.Models.User> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response<Elmah.PetStore.Models.User> { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

        // Post.2 CreateUsersWithListInput /user/createWithList

        public async Task<Framework.WebApi.Response<Elmah.PetStore.Models.User>> CreateUsersWithListInputAsync(Elmah.PetStore.Models.User[] item)
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

        // Put.1 UpdateUser /user/{username}

        public async Task<Framework.WebApi.Response> UpdateUserAsync(string username, Elmah.PetStore.Models.User item)
        {
            string url = GetHttpRequestUrl($"/user/{username}");
            try
            {
                var response = await PutCommon<Elmah.PetStore.Models.User>(url, item);

                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK, Message = response };
            }
            catch (Exception ex)
            {
                return new Framework.WebApi.Response { Status = Framework.Services.BusinessLogicLayerResponseStatus.MessageErrorDetected, ErrorMessage = new Dictionary<string, string> { { ex.HResult.ToString(), ex.Message } } };
            }
        }

    }
}

