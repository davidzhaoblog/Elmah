using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public static partial class WebApiClientFactory
    {
        static WebApiClientFactory()
        {
        }

        public static Elmah.PetStore.WebApiClient.PetApiClient CreatePetApiClient()
        {
            return new Elmah.PetStore.WebApiClient.PetApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }

        public static Elmah.PetStore.WebApiClient.StoreApiClient CreateStoreApiClient()
        {
            return new Elmah.PetStore.WebApiClient.StoreApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }

        public static Elmah.PetStore.WebApiClient.UserApiClient CreateUserApiClient()
        {
            return new Elmah.PetStore.WebApiClient.UserApiClient(WebServiceConfig.WebApiRootUrl, WebServiceConfig.UseToken, WebServiceConfig.Token);
        }
    }
}

