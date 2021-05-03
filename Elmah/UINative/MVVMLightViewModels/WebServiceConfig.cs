using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.MVVMLightViewModels
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
        public const string WebApiRootUrl_General = "http://localhost:7908/api/";
        public const string WebApiRootUrl_Android = "http://10.0.2.2:7908/api/";
        public const string WebApiRootUrl_IOS = "http://localhost:7908/api/";
#else
        // TODO: use production url.
        public const string WebApiRootUrl_General = "http://localhost:7908/api/";
        public const string WebApiRootUrl_Android = "http://10.0.2.2:7908/api/";
        public const string WebApiRootUrl_IOS = "http://localhost:7908/api/";
#endif
    }
}

