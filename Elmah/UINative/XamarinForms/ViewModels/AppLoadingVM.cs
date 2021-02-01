using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Elmah.XamarinForms.ViewModels
{
    public class AppLoadingVM: Framework.Xaml.ViewModelBase
    {
        public const string MessageTitle_LoadData = "Load_AppLoadingVM";

        public Elmah.XamarinForms.ViewModels.AppVM AppVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppVM>();
            }
        }

        public Elmah.MVVMLightViewModels.SQLiteFactory Caching
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.SQLiteFactory>();
            }
        }

        protected Elmah.XamarinForms.ViewModels.DashboardVM DashboardVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.DashboardVM>();
            }
        }

        public Elmah.MVVMLightViewModels.NavigationVM NavigationVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM>();
            }
        }

        public Framework.Xaml.ProgressBarVM ProgressBarVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.ProgressBarVM>(DependencyFetchTarget.GlobalInstance);
            }
        }

        public Framework.Xaml.Themes.ThemeSelectorVM ThemeSelectorVM
        {
            get { return DependencyService.Resolve<Framework.Xaml.Themes.ThemeSelectorVM>(); }
        }

        public AppLoadingVM()
        {
            MessagingCenter.Subscribe<AppLoadingVM, Framework.WebApi.AuthenticationResponse>(this, MessageTitle_LoadData, async (sender, request) =>
            {
                await LoggedInProcess(request);
            });
        }

        public void InitialNavigation(bool gotoWelcomeWizardPage, long? personID)
        {
            App.Current.MainPage = new Elmah.XamarinForms.AppShell();

            // TODO: You should goto wizard after registered a new account.
            /*
            if (gotoWelcomeWizardPage)
            {
                App.Current.MainPage = new NavigationPage(new NTierOnTime.XamarinForms.Pages.Settings.PersonWizardPage { PersonID = personID?.ToString() });
            }
            else
            {
                App.Current.MainPage = new Elmah.XamarinForms.AppShell();
            }
            */
        }

        /// <summary>
        /// Will navigate to Dashboard page after this LoggedInProcess
        /// </summary>
        /// <param name="loginResponse"></param>
        /// <returns></returns>
        public async Task LoggedInProcess(Framework.WebApi.AuthenticationResponse loginResponse)
        {
            ProgressBarVM.Initialization(Color.Orange, 4);

            Elmah.MVVMLightViewModels.WebServiceConfig.Token = loginResponse.Token;

            // 1.1. Theme
            var theme = Framework.Xaml.ApplicationPropertiesHelper.GetTheme();
            if (theme != Framework.Themes.Theme.Light)
            {
                ThemeSelectorVM.SwitchTheme(theme);
            }

            ProgressBarVM.Forward();

            await AppVM.GetCurrentLocation();

            ProgressBarVM.Forward();

            // 1.2. Token:
            Elmah.MVVMLightViewModels.WebServiceConfig.Token = loginResponse.Token;

            // 2.1. CacheLists
            {
                var allCacheDataKey = Caching.ListsHelper.BuildCacheDataKey(Elmah.EntityContracts.Enums.CacheLists.__ALL__.ToString());
                if (!Framework.Xaml.ApplicationPropertiesHelper.HasTableCachingData(allCacheDataKey))
                {
                    await Caching.ListsHelper.GetAndCacheLists();
                }
                else
                {
                    var cacheDataSetting = Framework.Xaml.ApplicationPropertiesHelper.GetTableCachingData(allCacheDataKey);
                    if (cacheDataSetting.ShouldRefresh)
                        await Caching.ListsHelper.GetAndCacheLists();
                }
            }

            ProgressBarVM.Forward();

            //// 2.2.GetAndCacheYourData
            //await NTierOnTime.MVVMLightViewModels.YourDataHelper.GetAndCacheYourData();

            ProgressBarVM.Forward();

            //await BusinessNavigationVM.InitializeActionItems();

            if (loginResponse.LoggedInSource == Framework.WebApi.LoggedInSource.LogInPage ||
                loginResponse.LoggedInSource == Framework.WebApi.LoggedInSource.AutoLogIn)
            {
                MessagingCenter.Send<Elmah.XamarinForms.ViewModels.DashboardVM, long>(DashboardVM, Elmah.XamarinForms.ViewModels.DashboardVM.MessageTitle_LoadData, loginResponse.EntityID.Value);
                App.Current.MainPage = new Elmah.XamarinForms.AppShell();
            }
            else if (loginResponse.LoggedInSource == Framework.WebApi.LoggedInSource.RegisterPage)
            {
                // TODO: you can goto Wizard page here, after registed a new account.
                //NavigationVM.Person.SendMessage_Init_TabDashboard(new NTierOnTime.DataSourceEntities.PersonIdentifier { EntityID = loginResponse.EntityID.Value });
                //App.Current.MainPage = new NavigationPage(new NTierOnTime.XamarinForms.Pages.Settings.PersonWizardPage { PersonID = loginResponse.EntityID?.ToString() });
            }

            // 3.1. CheckAllPermissions
            //await PermissionHelper.I.CheckAllPermissions();
        }
    }
}

