using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public static partial class WebApiClientFactory
    {
        // Step #1. Add ElmahTypeApiController.cs to AspNetMvcCoreApiController project
        // Step #2. Add ElmahTypeApiClient.cs to WebApiClient project

        public static Elmah.WebApiClient.ElmahTypeApiClient CreateElmahTypeApiClient()
        {
            return new Elmah.WebApiClient.ElmahTypeApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }
    }
}

