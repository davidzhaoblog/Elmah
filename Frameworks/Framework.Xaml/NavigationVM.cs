using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public partial class NavigationVM : Framework.Xaml._ViewModelBase
    {
        #region 1. Global VMs

        public static Framework.Xaml.PopupVM PopupVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.PopupVM>(DependencyFetchTarget.GlobalInstance);
            }
        }

        public static Framework.Xaml.AppShellVM AppShellVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.AppShellVM>(DependencyFetchTarget.GlobalInstance);
            }
        }

        #endregion 1. Global VMs

        #region 2. Commands

        public ICommand ShellBackButtonBehaviorCommand { get; set; } = new Command(async () => {
            var popupVM = DependencyService.Resolve<Framework.Xaml.PopupVM>(DependencyFetchTarget.GlobalInstance);
            popupVM.HidePopup();
            await Shell.Current.Navigation.PopAsync();
        });

        public ICommand NavigationCommand => new Command<Framework.Xaml.ActionForm.ActionParameter>(OnNavigationCommand);

        private async void OnNavigationCommand(Framework.Xaml.ActionForm.ActionParameter parameter)
        {
            await NavigationVM.NavigateAsync(parameter);
        }

        public ICommand HelpCommand => new Command<string>(async (url) => { await Launcher.OpenAsync(new Uri(url)); });
        public ICommand LogoutCommand => new Command(async () => await LogoutCommand_Clicked());

        public virtual async Task LogoutCommand_Clicked()
        {
            await Task.FromException(new NotImplementedException());
        }

        #endregion 2. Commands

        #region 3. ShellBackButtonBehaviorActionItemModel

        private Framework.Xaml.ActionForm.ActionItemModel m_ShellBackButtonBehaviorActionItemModel;
        public Framework.Xaml.ActionForm.ActionItemModel ShellBackButtonBehaviorActionItemModel
        {
            get { return m_ShellBackButtonBehaviorActionItemModel; }
            set
            {
                Set(nameof(ShellBackButtonBehaviorActionItemModel), ref m_ShellBackButtonBehaviorActionItemModel, value);
            }
        }

        #endregion 3. ShellBackButtonBehaviorActionItemModel

        #region 4. Routes

        protected Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }

        #endregion 4. Routes

        public NavigationVM()
        {
            RegisterRoutes();
            m_ShellBackButtonBehaviorActionItemModel = GetActionItemModel_ShellBackButtonBehavior();
        }

        public virtual void RegisterRoutes()
        {
            // throw new NotImplementedException();
        }

        public static async Task NavigateAsync(Framework.Xaml.ActionForm.ActionParameter actionParameter)
        {
            if (actionParameter.SendMessage != null)
                actionParameter.SendMessage();
            if (string.IsNullOrEmpty(actionParameter.Domain))
                await NavigateAsync(actionParameter.Page, actionParameter.Parameters);
            else if (string.IsNullOrEmpty(actionParameter.Page))
                await NavigateAsync(actionParameter.Domain, actionParameter.Parameters);
            else
                await NavigateAsync(actionParameter.Domain, actionParameter.Page, actionParameter.Parameters);
        }

        public static async Task NavigateAsync(string domain, string pageName, Dictionary<string, object> parameters = null)
        {
            var domainPageName = string.Format("{0}/{1}", domain, pageName);
            await NavigateAsync(domainPageName, parameters);
        }

        public static async Task NavigateAsync(string pageName, Dictionary<string, object> parameters = null)
        {
            AppShellVM.Cleanup();

            ShellNavigationState state = Shell.Current.CurrentState;
            if (parameters == null || parameters.Count == 0)
            {
                await Shell.Current.GoToAsync(string.Format("{0}", pageName));
            }
            else
            {
                var parametersQuery =
                    from t in parameters
                    select string.Format("{0}={1}", t.Key, t.Value);
                var parametersInString = string.Join("&", parametersQuery);
                await Shell.Current.GoToAsync(string.Format("{0}?{1}", pageName, parametersInString));
            }
            Shell.Current.FlyoutIsPresented = false;
        }

        private Framework.Xaml.ActionForm.ActionItemModel GetActionItemModel_ShellBackButtonBehavior()
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.NavigationItem,
                Title = Framework.Resx.UIStringResource.Back,
                FontIconSettings = new Framework.Xaml.FontIconSettings
                {
                    MasterFontIcon = Framework.Xaml.FontAwesomeIcons.ArrowCircleLeft
                ,
                    MasterFontIconFamily = "FontAwesomeSolid"
                },
                NavigationCommand = ShellBackButtonBehaviorCommand,
            };
        }
    }
}

