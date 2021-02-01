using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Elmah.XamarinForms.ViewModels
{
    public partial class NavigationVM : Elmah.MVVMLightViewModels.NavigationVM
    {
        #region 1. Global VMs

        public static Elmah.XamarinForms.ViewModels.AppVM AppVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppVM>();
            }
        }

        #endregion 1. Global VMs

        #region 2. Commands
        public override async Task LogoutCommand_Clicked()
        {
            try
            {
                var logInViewModel = DependencyService.Resolve<Elmah.XamarinForms.ViewModels.LogInViewModel>();
                var client = Elmah.MVVMLightViewModels.WebApiClientFactory.CreateAuthenticationApiClientWithToken();

                logInViewModel.LoginResponse = await client.LogoutAsync(AppVM.SignInData.UserName);

                //Framework.Xaml.ApplicationPropertiesHelper.ClearWelcomeWizardData();
                //await Framework.Xaml.ApplicationPropertiesHelper.ClearSignInData();
                await Framework.Xaml.ApplicationPropertiesHelper.ClearAll();

                AppVM.SignInData = new Framework.Xaml.SignInData();

                // TODO: should find a solution for this. The current problem is Shell flyout and original not hidden.
                ShellNavigationState state = Shell.Current.CurrentState;
                Shell.Current.FlyoutIsPresented = false;
                Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;

                //AppVM.Caching.DeleteDatabase();
                await Shell.Current.Navigation.PushModalAsync(new Elmah.XamarinForms.Pages.LogInPage());
            }
            catch (Exception ex)
            {

            }
        }

        #endregion 2. Commands

        public NavigationVM(): base()
        {
        }

        public override void RegisterRoutes()
        {
            routes.Add(string.Format("//{0}", StandardPages.Login), typeof(Elmah.XamarinForms.Pages.LogInPage));
            routes.Add(string.Format("//{0}", StandardPages.Register), typeof(Elmah.XamarinForms.Pages.RegisterPage));

            routes.Add(string.Format("//{0}", StandardPages.Dashboard), typeof(Elmah.XamarinForms.Pages.DashboardPage));

            //routes.Add(string.Format("//{0}", StandardPages.DailyCalendar), typeof(NTierOnTime.XamarinForms.Pages.Calendar.DailyCalendarPage));

            foreach (var domainRegistrationModel in AppVM.DomainRegistrationModels)
            {
                foreach (var route in domainRegistrationModel.Routes)
                {
                    routes.Add(route.Key, route.Value);
                }
            }

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        public static class InlineActionSheetHandler
        {
            static object previousInlineActionSheetTrigger = null;
            static Layout<View> wrapper = null;
            static Uri previousUri = null;

            public static void Reset()
            {
                Elmah.XamarinForms.ViewModels.NavigationVM.PopupVM.HidePopup();
                previousInlineActionSheetTrigger = null;
                previousUri = Shell.Current.CurrentState.Location;
            }

            public static void Show_InlineActionSheet<TItem>(object inlineActionSheetTrigger, EventArgs e, Framework.Xamariner.Controls.ActionForm.InlineActionSheet inlineActionSheet, Func<TItem, Framework.Xaml.ActionForm.ActionSheetVM> getInlineActionSheet)

            {
                if(previousUri != Shell.Current.CurrentState.Location || wrapper == null)
                    // 1. Navigated to another Uri, will clear all
                {
                    wrapper = (Layout<View>)inlineActionSheet.Parent;
                    Reset();
                }

                if (previousInlineActionSheetTrigger != inlineActionSheetTrigger && NavigationVM.PopupVM.PopupOption == Framework.Xaml.PopupOptions.InlineActionSheet)
                // 1. Clicked another trigger, close InlineActionSheet first, then Open again in step#3
                {
                    Elmah.XamarinForms.ViewModels.NavigationVM.PopupVM.HidePopup();

                    var previousParent = ((Layout<View>)inlineActionSheet.Parent);
                    previousParent.Children.Remove(inlineActionSheet);
                }
                else if (Elmah.XamarinForms.ViewModels.NavigationVM.PopupVM.PopupOption == Framework.Xaml.PopupOptions.InlineActionSheet)
                // 2. Clicked this trigger again to close InlineActionSheet
                {
                    Elmah.XamarinForms.ViewModels.NavigationVM.PopupVM.HidePopup();

                    var previousParent = ((Layout<View>)inlineActionSheet.Parent);
                    previousParent.Children.Remove(inlineActionSheet);

                    wrapper.Children.Add(inlineActionSheet);
                    return;
                }

                // 3. Show InlineActionSheet
                previousInlineActionSheetTrigger = inlineActionSheetTrigger;

                // Add to new Parent Layout
                var inlinPopupParent = (Grid)((View)inlineActionSheetTrigger).Parent; // Grid, may changed if use button
                inlinPopupParent.Children.Add(inlineActionSheet);

                var navigationVM = DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM>();
                var item = (TItem)((View)inlineActionSheetTrigger).BindingContext;
                Framework.Xaml.ActionForm.ActionSheetVM actionFormVM = getInlineActionSheet(item);
                Elmah.XamarinForms.ViewModels.NavigationVM.PopupVM.ShowInlineActionSheet(actionFormVM);
            }
        }
    }
}

