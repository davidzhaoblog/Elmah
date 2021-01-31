using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Elmah.WebApiClient
{
    /// <summary>

    /// </summary>
    public partial class AuthenticationApiClient : Framework.ViewModels.ApiControllerHttpClientBase
    {
        public override string ControllerName => "AuthenticationApi";

        public AuthenticationApiClient(string rootPath, bool useToken = false, string token = null) : base(rootPath, useToken, token)
        {
        }

        public const string ActionName_Login = "Login";

        public async Task<Framework.WebApi.AuthenticationResponse> LogInAsync(string userName, string password)
        {
            var model = new LoginModel
            {
                Email = userName,
                Password = password
            };

            string url = GetHttpRequestUrl(ActionName_Login);
            var response = await PostCommon<LoginModel, Framework.WebApi.AuthenticationResponse>(url, model);
            if (response == null)
            {
                response = new Framework.WebApi.AuthenticationResponse { Succeeded = false, IsLockedOut = false, IsNotAllowed = false, RequiresTwoFactor = false, };
            }

            return response;
        }

        public const string ActionName_Logout = "Logout";

        public async Task<Framework.WebApi.AuthenticationResponse> LogoutAsync(string userName)
        {
            var model = new LoginModel
            {
                Email = userName,
            };

            string url = GetHttpRequestUrl(ActionName_Logout);
            var response = await PostCommon<LoginModel, Framework.WebApi.AuthenticationResponse>(url, model);
            if (response == null)
            {
                response = new Framework.WebApi.AuthenticationResponse { Succeeded = false, IsLockedOut = false, IsNotAllowed = false, RequiresTwoFactor = false, };
            }
            return response;
        }
        public async Task<Framework.WebApi.AuthenticationResponse> RegisterUserAsync(string email, string password, string confirmPassword)
        {
            const string ActionName = "Register";
            string url = GetHttpRequestUrl(ActionName);

            var model = new RegisterModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
            return await PostCommon<RegisterModel, Framework.WebApi.AuthenticationResponse>(url, model);
        }

        public async Task<Framework.WebApi.AuthenticationResponse> SignInWithOAuth2(Framework.WebApi.UserTokenIdModel model, Framework.Models.AuthenticationProvider authProvider)
        {
            string url = GetHttpRequestUrl(authProvider.ToString());
            return await PostCommon<Framework.WebApi.UserTokenIdModel, Framework.WebApi.AuthenticationResponse>(url, model);
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}

