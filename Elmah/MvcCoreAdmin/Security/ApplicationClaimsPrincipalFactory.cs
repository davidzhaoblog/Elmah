using Elmah.MvcCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Elmah.MvcCore.Security
{
    public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;
        public ApplicationClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor
            , IServiceProvider serviceProvider
            , IOptions<AppSettings> appSettings)
        : base(userManager, roleManager, optionsAccessor)
        {
            this._serviceProvider = serviceProvider;
            this._appSettings = appSettings.Value;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            var claimsIdentity = ((ClaimsIdentity)principal.Identity);

            if (claimsIdentity.IsAuthenticated)
            {
                // 1. JwtToken
                string tokenInString = CustomizedClaimTypes.GetJwtSecurityTokenInString(user.Id.ToLower(), _appSettings.Secret);
                claimsIdentity.AddClaims(new[] { new Claim(CustomizedClaimTypes.JwtToken, tokenInString) });

                // TODO: load more Claims when logged in
                //claimsIdentity.AddClaims(new[] { new Claim(CustomizedClaimTypes.YourEntityId, user.EntityID.ToString()) });

                //if (user.EntityID.HasValue)
                //{
                //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                //        new Claim(ClaimTypes.GivenName, user.UserName)
                //    });
                //}
            }

            return principal;
        }
    }
}

