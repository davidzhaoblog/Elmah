using Google.Apis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Elmah.MvcCore.Services
{
    public interface IAuthService
    {
        Task<Elmah.MvcCore.Models.ApplicationUser> Authenticate(Framework.WebApi.UserTokenIdModel token);
    }
}

