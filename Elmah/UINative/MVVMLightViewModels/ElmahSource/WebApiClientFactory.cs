using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public static partial class WebApiClientFactory
    {
        // Step #1. Add ElmahSourceApiController.cs to AspNetMvcCoreApiController project
        // Step #2. Add ElmahSourceApiClient.cs to WebApiClient project

        public static Elmah.WebApiClient.ElmahSourceApiClient CreateElmahSourceApiClient()
        {
            return new Elmah.WebApiClient.ElmahSourceApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }
    }
}

