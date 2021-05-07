using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.PetStore.ViewModels
{
    public class WebServiceConfig
    {
        public static string WebApiRootUrl { get; set; }
        public static bool UseToken { get; set; }
        /// <summary>
        /// Should use TOKEN when on all web services calls, only exception is login.
        /// </summary>
        public static string Token { get; set; }

#if DEBUG
        public const string WebApiRootUrl_General = "https://petstore3.swagger.io/api/v3";
        public const string WebApiRootUrl_Android = "https://petstore3.swagger.io/api/v3";
        public const string WebApiRootUrl_IOS = "https://petstore3.swagger.io/api/v3";
#else
        // TODO: use production url.
        public const string WebApiRootUrl_General = "https://petstore3.swagger.io/api/v3";
        public const string WebApiRootUrl_Android = "https://petstore3.swagger.io/api/v3";
        public const string WebApiRootUrl_IOS = "https://petstore3.swagger.io/api/v3";
#endif
    }
}

