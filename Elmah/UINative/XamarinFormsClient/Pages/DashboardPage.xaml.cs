using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        protected Elmah.XamarinForms.ViewModels.DashboardVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.DashboardVM>();
            }
        }

        public DashboardPage()
        {
            InitializeComponent();
            this.BindingContext = this.ViewModel;
        }

        protected override void OnAppearing()
        {
            //// TODO: test
            //ViewModel.PopupVM.ShowCountDownPopup(10, true);
        }

        //async void OnAnimateLabelButtonClicked(object sender, EventArgs e)
        //{
        //    await Task.WhenAll(
        //        label.ColorTo(Color.Red, Color.Blue, c => label.TextColor = c, 5000),
        //        label.ColorTo(Color.Blue, Color.Red, c => label.BackgroundColor = c, 5000));

        //    label.BackgroundColor = Color.Default;
        //    label.TextColor = Color.Default;
        //}
    }
}

