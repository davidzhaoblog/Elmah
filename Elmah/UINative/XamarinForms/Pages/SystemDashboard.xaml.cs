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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SystemDashboard : ContentPage
    {
        protected Elmah.MVVMLightViewModels.SystemDashboardVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.SystemDashboardVM>();
            }
        }
        public object Parameter { get; set; }

        public SystemDashboard()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await this.ViewModel.Refresh();
            base.OnAppearing();
        }
    }
}

