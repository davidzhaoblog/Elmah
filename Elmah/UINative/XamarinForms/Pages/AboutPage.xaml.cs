using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Elmah.XamarinForms.Pages
{
    public partial class AboutPage : ContentPage
    {
        public ICommand TapCommand => new Command<string>(async (url) => { await Launcher.OpenAsync(new Uri(url)); });

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}

