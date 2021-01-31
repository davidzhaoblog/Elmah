using System.Windows.Input;
using Xamarin.Essentials;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamForms.Controls;

namespace Elmah.XamarinForms.Pages
{
    public partial class CalendarPage : ContentPage
    {
        protected ViewModels.CalendarVM ViewModel
        {
            get
            {
                return DependencyService.Get<ViewModels.CalendarVM>();
            }
        }

        public CalendarPage()
        {
            InitializeComponent();
            this.BindingContext = this.ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (this.weelyCalendar.IsVisible)
            {
                await weelyCalendar.ScrollToHourAsync(8);
            }
            if (this.dailyCalendar.IsVisible)
            {
                await dailyCalendar.ScrollToHourAsync(8);
            }
            await this.ViewModel.LoadData();
        }
    }
}

