using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public static partial class WebApiClientFactory
    {
        // Step #1. Add ELMAH_ErrorApiController.cs to AspNetMvcCoreApiController project
        // Step #2. Add ELMAH_ErrorApiClient.cs to WebApiClient project

        public static Elmah.WebApiClient.ELMAH_ErrorApiClient CreateELMAH_ErrorApiClient()
        {
            return new Elmah.WebApiClient.ELMAH_ErrorApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }
    }
}

