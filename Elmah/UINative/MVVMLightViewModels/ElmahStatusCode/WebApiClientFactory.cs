using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public static partial class WebApiClientFactory
    {
        // Step #1. Add ElmahStatusCodeApiController.cs to AspNetMvcCoreApiController project
        // Step #2. Add ElmahStatusCodeApiClient.cs to WebApiClient project

        public static Elmah.WebApiClient.ElmahStatusCodeApiClient CreateElmahStatusCodeApiClient()
        {
            return new Elmah.WebApiClient.ElmahStatusCodeApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }
    }
}

