using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages.ElmahApplication
{

/*

    [QueryProperty("Application", "application")]

*/

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {

/*

        // TODO: developer may need modify or delete, and parse value from string
        public string Application
        {
            set
            {
                var _Application = default(string);
                if (string.TryParse(value, out _Application))
                {
                    var request = new Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplicationIdentifier> {
                        Criteria = new Elmah.DataSourceEntities.ElmahApplicationIdentifier { Application = _Application }
                        , UIAction = Framework.ViewModels.UIAction.ViewDetails
                        , EnablePopup = false
                        , LoadDropDownContents = true
                        , SetDropDownSelectedItems = true
                        , LoadExtraDataIfNeeded = true
                        , Refresh = false
                    };
                    // use ItemVM type full name to avoid debug optimization issue
                    MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplicationIdentifier>>(
                        ViewModel, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_LoadItem, request);
                }
            }
        }

*/

/*
        protected Elmah.MVVMLightViewModels.ElmahApplication.ItemVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM>();
            }
        }
*/
        public Details()
        {
            InitializeComponent();
        }

    }
}

