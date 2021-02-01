using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.WebApi
{
    public class AuthenticationResponse
    {
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public long? EntityID { get; set; }
        public IList<string> Roles { get; set; }

        public Framework.WebApi.LoggedInSource LoggedInSource { get; set; }
    }
}

