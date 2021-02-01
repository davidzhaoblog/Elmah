using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public static partial class WebApiClientFactory
    {
        // Step #1. Add ElmahUserApiController.cs to AspNetMvcCoreApiController project
        // Step #2. Add ElmahUserApiClient.cs to WebApiClient project

        public static Elmah.WebApiClient.ElmahUserApiClient CreateElmahUserApiClient()
        {
            return new Elmah.WebApiClient.ElmahUserApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }
    }
}

