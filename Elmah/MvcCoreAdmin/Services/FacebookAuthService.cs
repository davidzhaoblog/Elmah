using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Elmah.MvcCore.Services
{
    public class FacebookAuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Elmah.MvcCore.Models.ApplicationUser> _userManager;

        public FacebookAuthService(IConfiguration configuration, UserManager<Elmah.MvcCore.Models.ApplicationUser> userManager)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Elmah.MvcCore.Models.ApplicationUser> Authenticate(Framework.WebApi.UserTokenIdModel token)
        {
            //NB: These config files are loaded from the user secrets.
            var appToken = await GenerateAppAccessToken(_configuration["Facebook:AppId"], _configuration["Facebook:AppSecret"]);
            var isValid = await DebugUserAccessToken(appToken, token.JwtToken);

            if (isValid)
            {
                var user = await CreateOrGetUser(token);
                return user;
            }

            throw new Exception("Invalid Token");
        }

        private async Task<Elmah.MvcCore.Models.ApplicationUser> CreateOrGetUser(Framework.WebApi.UserTokenIdModel userToken)
        {
            var user = await _userManager.FindByEmailAsync(userToken.Email);

            if (user == null)
            {
                var appUser = new Elmah.MvcCore.Models.ApplicationUser
                {
                    UserName = userToken.Email,
                    Email = userToken.Email,
                };
                var identityUser = await _userManager.CreateAsync(appUser);
                return appUser;
            }
            return user;
        }

        private async Task<string> GenerateAppAccessToken(string appId, string appSecret)
        {
            using var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($@"https://graph.facebook.com/oauth/access_token?client_id={appId}&client_secret={appSecret}&grant_type=client_credentials");
            var obj = JsonConvert.DeserializeAnonymousType(json, new { access_token = "", token_type = "" });
            return obj.access_token;
        }

        private async Task<bool> DebugUserAccessToken(string appAccessToken, string userAccessToken)
        {
            using var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($@"https://graph.facebook.com/debug_token?input_token={userAccessToken}&access_token={appAccessToken}");
            var obj = JsonConvert.DeserializeAnonymousType(json, new { data = new { app_id = "", is_valid = false } });
            return obj.data.is_valid;
        }
    }
}

