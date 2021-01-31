using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Elmah.XamarinForms
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        void OnNavigating(object sender, ShellNavigatingEventArgs e)
        {
            // Cancel any back navigation
            //if (e.Source == ShellNavigationSource.Pop)
            //{
            //    e.Cancel();
            //}
        }

        void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
        }

        protected override bool OnBackButtonPressed()
        {
            var popupVM = DependencyService.Resolve<Framework.Xaml.PopupVM>(DependencyFetchTarget.GlobalInstance);
            popupVM.HidePopup();

            if(string.IsNullOrEmpty(popupVM.ItemControlKey))
                return base.OnBackButtonPressed();

            popupVM.HideItemControlPopup();
            return true;
        }
    }
}

