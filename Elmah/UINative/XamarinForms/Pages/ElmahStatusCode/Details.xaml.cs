using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages.ElmahStatusCode
{

/*

    [QueryProperty("StatusCode", "statuscode")]

*/

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {

/*

        // TODO: developer may need modify or delete, and parse value from string
        public string StatusCode
        {
            set
            {
                var _StatusCode = default(int);
                if (int.TryParse(value, out _StatusCode))
                {
                    var request = new Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahStatusCodeIdentifier> {
                        Criteria = new Elmah.DataSourceEntities.ElmahStatusCodeIdentifier { StatusCode = _StatusCode }
                        , UIAction = Framework.ViewModels.UIAction.ViewDetails
                        , EnablePopup = false
                        , LoadDropDownContents = true
                        , SetDropDownSelectedItems = true
                        , LoadExtraDataIfNeeded = true
                        , Refresh = false
                    };
                    // use ItemVM type full name to avoid debug optimization issue
                    MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahStatusCode.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahStatusCodeIdentifier>>(
                        ViewModel, Elmah.MVVMLightViewModels.ElmahStatusCode.ItemVM.MessageTitle_LoadItem, request);
                }
            }
        }

*/

/*
        protected Elmah.MVVMLightViewModels.ElmahStatusCode.ItemVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahStatusCode.ItemVM>();
            }
        }
*/
        public Details()
        {
            InitializeComponent();
        }

    }
}

