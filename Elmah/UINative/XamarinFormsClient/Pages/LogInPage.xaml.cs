using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages
{
    /// <summary>
    /// //https://forums.xamarin.com/discussion/117782/disable-device-back-button
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        protected Elmah.XamarinForms.ViewModels.LogInViewModel ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.LogInViewModel>();
            }
        }

        public LogInPage()
        {
            InitializeComponent();
            BindingContext = this.ViewModel;
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override void OnAppearing()
        {
            this.ViewModel.IsVisible = true;
            base.OnAppearing();
        }

        private async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }

        protected override bool OnBackButtonPressed()
        {
            // https://stackoverflow.com/questions/29257929/how-to-terminate-a-xamarin-application
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            return true;
        }
    }
}

