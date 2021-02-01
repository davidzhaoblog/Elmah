using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public static partial class WebApiClientFactory
    {
        static WebApiClientFactory()
        {
        }

        public static Elmah.WebApiClient.HomeApiClient CreateHomeApiClient()
        {
            return new Elmah.WebApiClient.HomeApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }

        public static Elmah.WebApiClient.AuthenticationApiClient CreateAuthenticationApiClient()
        {
            return new Elmah.WebApiClient.AuthenticationApiClient(WebServiceConfig.WebApiRootUrl, false, null);
        }

        public static Elmah.WebApiClient.AuthenticationApiClient CreateAuthenticationApiClientWithToken()
        {
            return new Elmah.WebApiClient.AuthenticationApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }

        public static Elmah.WebApiClient.ListsApiClient CreateListsApiClient()
        {
            return new Elmah.WebApiClient.ListsApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }

        public static Elmah.WebApiClient.MapApiClient CreateMapApiClient()
        {
            return new Elmah.WebApiClient.MapApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }
    }
}

