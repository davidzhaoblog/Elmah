using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public static partial class WebApiClientFactory
    {
        // Step #1. Add ElmahHostApiController.cs to AspNetMvcCoreApiController project
        // Step #2. Add ElmahHostApiClient.cs to WebApiClient project

        public static Elmah.WebApiClient.ElmahHostApiClient CreateElmahHostApiClient()
        {
            return new Elmah.WebApiClient.ElmahHostApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }
    }
}

