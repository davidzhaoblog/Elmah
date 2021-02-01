using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        protected Elmah.XamarinForms.ViewModels.RegisterViewModel ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.RegisterViewModel>();
            }
        }

        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = this.ViewModel;
        }

        private async void GotoLoginButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}

