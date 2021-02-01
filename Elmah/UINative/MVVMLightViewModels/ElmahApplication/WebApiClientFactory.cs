using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public static partial class WebApiClientFactory
    {
        // Step #1. Add ElmahApplicationApiController.cs to AspNetMvcCoreApiController project
        // Step #2. Add ElmahApplicationApiClient.cs to WebApiClient project

        public static Elmah.WebApiClient.ElmahApplicationApiClient CreateElmahApplicationApiClient()
        {
            return new Elmah.WebApiClient.ElmahApplicationApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }
    }
}

