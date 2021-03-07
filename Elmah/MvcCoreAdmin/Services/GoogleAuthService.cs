using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Elmah.MvcCore.Services
{
    public class GoogleAuthService : IAuthService
    {
        private readonly UserManager<Elmah.MvcCore.Models.ApplicationUser> _userManager;

        public GoogleAuthService(UserManager<Elmah.MvcCore.Models.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Elmah.MvcCore.Models.ApplicationUser> Authenticate(Framework.WebApi.UserTokenIdModel userTokenModel)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(userTokenModel.JwtToken, new GoogleJsonWebSignature.ValidationSettings());
            return await CreateOrGetUser(payload, userTokenModel);
        }

        private async Task<Elmah.MvcCore.Models.ApplicationUser> CreateOrGetUser(Payload payload, Framework.WebApi.UserTokenIdModel userToken)
        {
            var user = await _userManager.FindByEmailAsync(payload.Email);

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
    }
}

