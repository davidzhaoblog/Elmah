using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages.ElmahHost
{

/*

    [QueryProperty("Host", "host")]

*/

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Delete : ContentPage
    {

/*

        // TODO: developer may need modify or delete, and parse value from string
        public string Host
        {
            set
            {
                var _Host = default(string);
                if (string.TryParse(value, out _Host))
                {
                    var request = new Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahHostIdentifier> {
                        Criteria = new Elmah.DataSourceEntities.ElmahHostIdentifier { Host = _Host }
                        , UIAction = Framework.ViewModels.UIAction.Delete
                        , EnablePopup = false
                        , LoadDropDownContents = true
                        , SetDropDownSelectedItems = true
                        , LoadExtraDataIfNeeded = true
                        , Refresh = false
                    };
                    // use ItemVM type full name to avoid debug optimization issue
                    MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahHost.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahHostIdentifier>>(
                        ViewModel, Elmah.MVVMLightViewModels.ElmahHost.ItemVM.MessageTitle_LoadItem, request);
                }
            }
        }

*/

/*
        protected Elmah.MVVMLightViewModels.ElmahHost.ItemVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahHost.ItemVM>();
            }
        }
*/
        public Delete()
        {
            InitializeComponent();
        }

    }
}

