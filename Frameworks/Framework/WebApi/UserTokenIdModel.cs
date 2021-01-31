using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.WebApi
{
    public class UserTokenIdModel
    {
        public Framework.Models.AuthenticationProvider AuthenticationProvider { get; set; }

        public string JwtToken { get; set; }

        public string Picture { get; set; }

        public string Gender { get; set; }

        public string Id { get; set; }

        public string Email { get; set; }

        public bool VerifiedEmail { get; set; }

        public string Name { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }
    }
}

