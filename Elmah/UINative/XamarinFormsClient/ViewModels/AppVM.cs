//using NTierOnTime.XamarinForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Elmah.XamarinForms.ViewModels
{
    public class AppVM : Framework.Xaml.ViewModelBase
    {
        public bool HasAuthentication { get; set; }

        public Elmah.XamarinForms.ViewModels.AppLoadingVM AppLoadingVM
        {
            get { return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppLoadingVM>(DependencyFetchTarget.GlobalInstance); }
        }

        public Framework.Xaml.AppShellVM AppShellVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.AppShellVM>(DependencyFetchTarget.GlobalInstance);
            }
        }

        public Elmah.MVVMLightViewModels.NavigationVM NavigationVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM>();
            }
        }

        protected Elmah.XamarinForms.ViewModels.DashboardVM DashboardVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.DashboardVM>();
            }
        }

        private Microsoft.Spatial.GeographyPoint m_CurrentLocation;
        public Microsoft.Spatial.GeographyPoint CurrentLocation
        {
            get { return m_CurrentLocation; }
            set
            {
                Set(nameof(CurrentLocation), ref m_CurrentLocation, value);
            }
        }

        private Framework.Xaml.SignInData m_SignInData;
        public Framework.Xaml.SignInData SignInData
        {
            get { return m_SignInData; }
            set
            {
                Set(nameof(SignInData), ref m_SignInData, value);
            }
        }

        public Elmah.MVVMLightViewModels.SQLiteFactory Caching
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.SQLiteFactory>();
            }
        }

        private List<Framework.Xaml.DomainModel> m_DomainRegistrationModels = new List<Framework.Xaml.DomainModel>();
        public List<Framework.Xaml.DomainModel> DomainRegistrationModels
        {
            get { return m_DomainRegistrationModels; }
            protected set
            {
                Set(nameof(DomainRegistrationModels), ref m_DomainRegistrationModels, value);
            }
        }

        private List<Framework.Xamariner.Interfaces.IDomainManager> m_DomainManagers = new List<Framework.Xamariner.Interfaces.IDomainManager>();
        public List<Framework.Xamariner.Interfaces.IDomainManager> DomainManagers
        {
            get { return m_DomainManagers; }
            protected set
            {
                Set(nameof(DomainManagers), ref m_DomainManagers, value);
            }
        }

        public void Initialize(bool hasAuthentication = true)
        {
            HasAuthentication = hasAuthentication;

            // 1. initialize

            Framework.Xamariner.TranslateExtension.RegisterResourceManager(typeof(Framework.Resx.UIStringResource));
            Framework.Xamariner.TranslateExtension.RegisterResourceManager(typeof(Elmah.Resx.UIStringResourcePerApp));
            Framework.Xamariner.TranslateExtension.RegisterResourceManager(typeof(Elmah.Resx.UIStringResourcePerEntity));
            Framework.Xamariner.TranslateExtension.RegisterResourceManager(typeof(Elmah.Resx.UIStringResourceReport));

            Framework.Models.PropertyChangedNotifierHelper.Initialize(true);
            Framework.Weather.WeatherServiceSingleton.Instance.InitWeatherServiceProvider(new Framework.Weather.OpenWeatherMap.OpenWeatherMapProvider());
            Elmah.MVVMLightViewModels.SQLiteFactory.Initialize();
            Framework.Helpers.GeoHelperSinglton.Instance.Init("3");

            // 2. Web Service Url and Toke
            if (Device.RuntimePlatform.ToLower() == Framework.Xamariner.Platforms.Android.ToString().ToLower())
            {
                Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl = Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl_Android;
            }
            else if (Device.RuntimePlatform.ToLower() == Framework.Xamariner.Platforms.iOS.ToString().ToLower())
            {
                Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl = Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl_IOS;
            }
            else
            {
                Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl = Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl_General;
            }

#if DEBUG
            // TODO: change to false if want to test/enable/disable XXX.MVVMLightViewModels.WebServiceConfig.UseToken
            Elmah.MVVMLightViewModels.WebServiceConfig.UseToken = true;
#else
            Elmah.MVVMLightViewModels.WebServiceConfig.UseToken = true;
#endif
            // 3. Initialize Localization
            if (Device.RuntimePlatform.ToLower() == Framework.Xamariner.Platforms.iOS.ToString().ToLower() || Device.RuntimePlatform.ToLower() == Framework.Xamariner.Platforms.Android.ToString().ToLower())
            {
                // determine the correct, supported .NET culture
                var ci = DependencyService.Get<Framework.Xamariner.ILocalize>().GetCurrentCultureInfo();
                Framework.Resx.UIStringResource.Culture = ci; // set the RESX for resource localization
                Elmah.Resx.UIStringResourcePerApp.Culture = ci; // set the RESX for resource localization

                Elmah.Resx.UIStringResourcePerEntity.Culture = ci; // set the RESX for resource localization
                Elmah.Resx.UIStringResourceReport.Culture = ci; // set the RESX for resource localization

                DependencyService.Get<Framework.Xamariner.ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            // 5. Register ViewModels
            // 5.1. Register Swagger ViewModels
            Elmah.PetStore.ViewModels.ViewModelLocator._RegisterViewModels();

            // 5.2. Register ViewModels
            Elmah.MVVMLightViewModels.ViewModelLocator._RegisterViewModels();
            Elmah.XamarinForms.ViewModels.ViewModelLocator.RegisterViewModels();

            // 6. Register Domains
            RegisterDomains();
            foreach (var domainManager in DomainManagers)
            {
                // 6.1. Register domain specific ViewModels
                domainManager.RegisterViewModels();
                // 6.2. Get DomainRegistrationModel, DomainRegistrationModel.Routers will be called in NavigationVM.RegisterRoutes()
                this.DomainRegistrationModels.Add(domainManager.CreateDomainModel());
            }

            // 7.
            AppShellVM.Cleanup();
        }

        private void RegisterDomains()
        {
            // Add instances of all Framework.Xamariner.Interfaces.IDomainManager types to register domains and routes.
            var typeOfIDomainManager = typeof(Framework.Xamariner.Interfaces.IDomainManager);

            // Currently using Assemblies root namespace as filter, may add more.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(t=>t.FullName.StartsWith("Elmah."));
            var concreteTypesOfIDomainManager = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => typeOfIDomainManager.IsAssignableFrom(p) && (p.IsClass && !p.IsAbstract));

            foreach(var concreteTypeOfIDomainManager in concreteTypesOfIDomainManager)
            {
                var instance = (Framework.Xamariner.Interfaces.IDomainManager)Activator.CreateInstance(concreteTypeOfIDomainManager);
                this.DomainManagers.Add(instance);
            }
        }

        public async Task OnStart()
        {
            if (!HasAuthentication)
            {
                MessagingCenter.Send<Elmah.XamarinForms.ViewModels.DashboardVM, long>(DashboardVM, Elmah.XamarinForms.ViewModels.DashboardVM.MessageTitle_LoadData, -1);
                return;
            }

            try
            {
                App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.AppLoadingPage());

                // Load SignInData
                SignInData = Framework.Xaml.ApplicationPropertiesHelper.GetSignInData();

                if (SignInData.RememberMe && SignInData.AutoSignIn)
                {
                    var client = Elmah.MVVMLightViewModels.WebApiClientFactory.CreateAuthenticationApiClient();

                    var LoginResponse = await client.LogInAsync(SignInData.UserName, SignInData.Password);
                    if (LoginResponse.Succeeded)
                    {
                        LoginResponse.LoggedInSource = Framework.WebApi.LoggedInSource.AutoLogIn;

                        await Framework.Xaml.ApplicationPropertiesHelper.SetSignInData(
                            SignInData.UserName
                            , SignInData.Password
                            , SignInData.RememberMe, SignInData.AutoSignIn, LoginResponse.Token, LoginResponse.EntityID ?? 0, !LoginResponse.EntityID.HasValue, null);

                        SignInData = Framework.Xaml.ApplicationPropertiesHelper.GetSignInData();

                        App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.AppLoadingPage());
                        MessagingCenter.Send<Elmah.XamarinForms.ViewModels.AppLoadingVM, Framework.WebApi.AuthenticationResponse>(AppLoadingVM, Elmah.XamarinForms.ViewModels.AppLoadingVM.MessageTitle_LoadData, LoginResponse);
                    }
                    else
                    {
                        await Framework.Xaml.ApplicationPropertiesHelper.ClearSignInData();
                        App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.LogInPage());
                    }
                }
                else
                {
                    await Framework.Xaml.ApplicationPropertiesHelper.ClearSignInData();
                    App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.LogInPage());
                }
                // TODO: if you want to track current GPS location
                //Framework.Xamariner.CrossGeolocatorHelper.locationChangedHandler += OnLocationChanged;
                //CurrentLocation = await Framework.Xamariner.CrossGeolocatorHelper.GetCurrentLocationAsync(null);
            }
            catch (System.Exception ex)
            {
                await Framework.Xaml.ApplicationPropertiesHelper.ClearSignInData();
                App.Current.MainPage = new NavigationPage(new Elmah.XamarinForms.Pages.LogInPage());
            }
        }

        public async Task GetCurrentLocation()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if (status != Xamarin.Essentials.PermissionStatus.Granted)
                        return;
                }
                Location location = null;
                for (var i = 0; i < 10; i++)
                {
                    if (CurrentLocation == null)
                    {
                        location = await Geolocation.GetLastKnownLocationAsync();
                    }
                    else
                    {
                        location = await Geolocation.GetLocationAsync();
                    }

                    if (location == null)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        break;
                    }
                }
                if (location != null)
                {
                    CurrentLocation = Microsoft.Spatial.GeographyPoint.Create(location.Latitude, location.Longitude);
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}

