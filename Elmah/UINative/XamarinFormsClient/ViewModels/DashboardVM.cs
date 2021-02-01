using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Framework.Models;

namespace Elmah.XamarinForms.ViewModels
{
    public partial class DashboardVM : Framework.Xaml.ViewModelBase
    {
        public const string MessageTitle_LoadData = "Load_DashboardVM_Data";

        #region 1. VMs

        public Elmah.XamarinForms.ViewModels.AppVM AppVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppVM>();
            }
        }

        public Elmah.MVVMLightViewModels.NavigationVM NavigationVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM>();
            }
        }

        #endregion 1. VMs

        #region 2. Dashboard Header, NickName, CoverImage, DateToday and Weather

        private string m_NickName;
        public string NickName
        {
            get { return m_NickName; }
            set
            {
                Set(nameof(NickName), ref m_NickName, value);
            }
        }

        private string _CoverImage;
        public string CoverImage
        {
            get { return _CoverImage; }
            set { Set(nameof(CoverImage), ref _CoverImage, value); }
        }

        private DateTime _DateToday;
        public DateTime DateToday
        {
            get { return _DateToday; }
            set { Set(nameof(DateToday), ref _DateToday, value); }
        }

        private bool _ShowWeatherForecast = false;
        public bool ShowWeatherForecast
        {
            get { return _ShowWeatherForecast; }
            set { Set(nameof(ShowWeatherForecast), ref _ShowWeatherForecast, value); }
        }

        private Framework.Xaml.WeatherVM _WeatherVM;
        public Framework.Xaml.WeatherVM WeatherVM
        {
            get { return _WeatherVM; }
            set { Set(nameof(WeatherVM), ref _WeatherVM, value); }
        }

        #endregion 2. Dashboard Header, NickName, CoverImage, DateToday and Weather

        #region 3. Dashboard Data
        /*
        private NTierOnTime.DataSourceEntities.DashboardFlags m_DashboardFlags;
        public NTierOnTime.DataSourceEntities.DashboardFlags DashboardFlags
        {
            get { return m_DashboardFlags; }
            set
            {
                Set(nameof(DashboardFlags), ref m_DashboardFlags, value);
            }
        }
        */
        #endregion 3. Dashboard Data

        #region 10. Commands

        //private bool _Display_NewBusinessWizard_LaunchButton;
        //public bool Display_NewBusinessWizard_LaunchButton
        //{
        //    get { return _Display_NewBusinessWizard_LaunchButton; }
        //    set { Set(nameof(Display_NewBusinessWizard_LaunchButton), ref _Display_NewBusinessWizard_LaunchButton, value); }
        //}

        private bool _DisplayRegisterAsCustomerButton;
        public bool DisplayRegisterAsCustomerButton
        {
            get { return _DisplayRegisterAsCustomerButton; }
            set { Set(nameof(DisplayRegisterAsCustomerButton), ref _DisplayRegisterAsCustomerButton, value); }
        }

        //public ICommand Command_NewBusinessWizard_LaunchButton { get; protected set; }
        public ICommand Command_RegisterAsCustomer { get; protected set; }

        private void InitCommands()
        {
            DisplayRegisterAsCustomerButton = true;

            Command_RegisterAsCustomer = new Command(
                execute: (t) =>
                {
                });
        }

        #endregion 10. Commands

        public DashboardVM()
        {
            InitCommands();

            MessagingCenter.Subscribe<DashboardVM, long>(this, MessageTitle_LoadData, async (sender, param) =>
            {
                await LoadData();

                //// TODO: test
                //PopupVM.ShowCountDownPopup(10, true);
            });
        }

        public async Task LoadData()
        {
            if (PopupVM.PopupOption == Framework.Xaml.PopupOptions.RegularPopup)
                return;

            PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            try
            {
                // TODO: how to convert local image to ImageSource
                // 1. Header
                CoverImage = "cover.png";
                DateToday = DateTime.Now;

                // 1.1. Person First Name
                var entityID = AppVM.SignInData.EntityID;

                //var vm = DependencyService.Resolve<NTierOnTime.MVVMLightViewModels.Employee.IndexVM>();
                //NavigationVM.Employee.SetCriteria_CommonResultView(entityID, null);
                //MessagingCenter.Send<NTierOnTime.MVVMLightViewModels.Employee.IndexVM, Framework.Xaml.LoadListDataRequest>(vm, NTierOnTime.MVVMLightViewModels.Employee.IndexVM.MessageTitle_LoadData,
                //    new Framework.Xaml.LoadListDataRequest { ListItemViewMode = ListItemViewModes.NavigationWhenClickItem });

                this.NickName = "Hey, You";
                /*
                var personItem = await AppVM.Caching.PersonRepository.GetItemFromTableAsync(t => t.EntityID == entityID);

                if (personItem == null)
                {
                    this.NickName = "Hey, You";
                }
                else
                {
                    this.NickName = personItem.NickName ?? personItem.FirstName ?? personItem.LastName ?? "Hey, You";
                }
                */

                //// 1.2. Weather
                //await GetWeather();

                // 2. Metadata
                //var client = new Elmah.WebApiClient.HomeApiClient(Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl, Elmah.MVVMLightViewModels.WebServiceConfig.UseToken, Elmah.MVVMLightViewModels.WebServiceConfig.Token);
                //var metaData = await client.GetDashboardVM(AppVM.SignInData.ShortGuid);
                //DashboardFlags = metaData.DashboardFlags;

                // 3. Contacts
                try
                {
                    var contacts = await Plugin.ContactService.CrossContactService.Current.GetContactListAsync();

                    System.Collections.ObjectModel.ObservableCollection<Plugin.ContactService.Shared.Contact> b = new System.Collections.ObjectModel.ObservableCollection<Plugin.ContactService.Shared.Contact>(contacts);
                }
                catch (Exception ex)
                {

                }

                //NavigationVM.Employee.SendMessage_Init_CommonResultView(entityID, null, Framework.Xaml.ListItemViewModes.NavigationWhenClickItem, true, nameof(NTierOnTime.DataSourceEntities.Employee.ModifiedDate), Framework.Queries.QueryOrderDirections.Descending);
            }
            catch(Exception ex)
            {

            }

            PopupVM.HidePopup();

            IsContentVisible = true;
        }

        async Task GetWeather()
        {
            // get GPS location
            //double longitude = 0.0d;
            //double latitude = 0.0d;
            const string key = "Weather";
            try
            {
                // TODO: should refresh every 15min
                if (Framework.Xaml.ApplicationPropertiesHelper.HasData(key))
                    WeatherVM = Framework.Xaml.ApplicationPropertiesHelper.GetData<Framework.Xaml.WeatherVM>(key);
                else
                {
                    var weather = await Framework.Weather.WeatherServiceSingleton.Instance.GetForecastToday(43.666667, -79.416667, null);
                    WeatherVM = weather.Clone<Framework.Xaml.WeatherVM>();
                    this.ShowWeatherForecast = weather.Status;
                    await Framework.Xaml.ApplicationPropertiesHelper.SaveData(key, weather);
                }
            }
            catch (Exception ex)
            {
                this.ShowWeatherForecast = false;
                this.WeatherVM.Message = "Unable to get weather information.";
                Debug.WriteLine("ERROR while refreshing weather ...\r\nERROR:" + ex.Message);
            }
        }
    }
}

