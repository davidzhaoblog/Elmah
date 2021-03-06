using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.MvcCore.Security
{
    public class CustomizedClaimTypes
    {
        //TODO: add more ClaimName here
        //public const string YourSalesCompanyId = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/YourSalesCompanyId";
        //public const string YourSalesCompanyShortGuid = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/YourSalesCompanyShortGuid";
        //public const string YourStoreId = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/YourStoreId";
        //public const string YourStoreShortGuid = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/YourStoreShortGuid";
        //public const string YourEntityId = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/YourEntityId";
        //public const string CurrentSalesCompanyId = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/CurrentSalesCompanyId";
        //public const string CurrentStoreId = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/CurrentStoreId";
        //public const string Email = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/Email";
        //public const string UserID = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/UserID";
        //public const string ShortGuid = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/ShortGuid";
        //public const string DisplayName = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/DisplayName";
        //public const string Age = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/Age";
        //public const string City = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/City";
        //public const string Role = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/Role";
        //public const string Permission = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/Permission";
        //public const string RememberMe = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/RememberMe";
        //public const string LoginType = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/LoginType";
        //public const string IdentityProvider = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/IdentityProvider";

        public const string JwtToken = "http://schemas.ntierontime.com/objects/2015/12/authentication/identity/claims/web/JwtToken";

        public static string GetJwtSecurityTokenInString(string userId, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenInString = tokenHandler.WriteToken(token);
            return tokenInString;
        }

        public static Tuple<bool, string> GetJwtToken(ClaimsPrincipal user)
        {
            var input = user.FindFirst(CustomizedClaimTypes.JwtToken);
            var retval = new Tuple<bool, string>(input != null, input?.Value);

            return retval;
        }

        //TODO: add get value of ClaimName
/*
        public static Tuple<bool, long?> GetYourSalesCompanyId(ClaimsPrincipal user)
        {
            Tuple<bool, long?> retval = new Tuple<bool, long?>(false, null);

            //long? entityID = null;
            if (user.IsInRole(Security.Roles.CompanyAdmin.ToString()) || user.IsInRole(Security.Roles.StoreAdmin.ToString()) || user.IsInRole(Security.Roles.Employee.ToString()))
            {
                long salesCompanyID;
                var yourSalesCompanyID = user.FindFirst(Security.CustomizedClaimTypes.YourSalesCompanyId);
                if (yourSalesCompanyID != null && long.TryParse(yourSalesCompanyID.Value, out salesCompanyID))
                {
                    retval = new Tuple<bool, long?>(true, salesCompanyID);
                }
                else
                {
                    // should redirect to an error page.
                }
            }

            return retval;
        }

        public static Tuple<bool, string> GetYourSalesCompanyShortGuid(ClaimsPrincipal user)
        {
            Tuple<bool, string> retval = new Tuple<bool, string>(false, null);

            //long? entityID = null;
            if (user.IsInRole(Security.Roles.CompanyAdmin.ToString()) || user.IsInRole(Security.Roles.StoreAdmin.ToString()) || user.IsInRole(Security.Roles.Employee.ToString()))
            {
                string result = string.Empty;
                var input = user.FindFirst(Security.CustomizedClaimTypes.YourSalesCompanyShortGuid);
                retval = new Tuple<bool, string>(true, result);
            }

            return retval;
        }

        public static Tuple<bool, long?> GetYourStoreId(ClaimsPrincipal user)
        {
            Tuple<bool, long?> retval = new Tuple<bool, long?>(false, null);

            //long? entityID = null;
            if (user.IsInRole(Security.Roles.StoreAdmin.ToString()) || user.IsInRole(Security.Roles.Employee.ToString()))
            {
                long storeId;
                var yourStoreId = user.FindFirst(Security.CustomizedClaimTypes.YourStoreId);
                if (yourStoreId != null && long.TryParse(yourStoreId.Value, out storeId))
                {
                    retval = new Tuple<bool, long?>(true, storeId);
                }
                else
                {
                    // should redirect to an error page.
                }
            }

            return retval;
        }

        public static Tuple<bool, string> GetYourStoreShortGuid(ClaimsPrincipal user)
        {
            Tuple<bool, string> retval = new Tuple<bool, string>(false, null);

            //long? entityID = null;
            if (user.IsInRole(Security.Roles.StoreAdmin.ToString()) || user.IsInRole(Security.Roles.Employee.ToString()))
            {
                string result = string.Empty;
                var input = user.FindFirst(Security.CustomizedClaimTypes.YourStoreShortGuid);
                retval = new Tuple<bool, string>(true, result);
            }

            return retval;
        }
*/
    }
}

