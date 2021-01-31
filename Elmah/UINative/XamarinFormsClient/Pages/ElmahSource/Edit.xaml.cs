using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages.ElmahSource
{

/*

    [QueryProperty("Source", "source")]

*/

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {

/*

        // TODO: developer may need modify or delete, and parse value from string
        public string Source
        {
            set
            {
                var _Source = default(string);
                if (string.TryParse(value, out _Source))
                {
                    var request = new Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahSourceIdentifier> {
                        Criteria = new Elmah.DataSourceEntities.ElmahSourceIdentifier { Source = _Source }
                        , UIAction = Framework.ViewModels.UIAction.Update
                        , EnablePopup = false
                        , LoadDropDownContents = true
                        , SetDropDownSelectedItems = true
                        , LoadExtraDataIfNeeded = true
                        , Refresh = false
                    };
                    // use ItemVM type full name to avoid debug optimization issue
                    MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahSource.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahSourceIdentifier>>(
                        ViewModel, Elmah.MVVMLightViewModels.ElmahSource.ItemVM.MessageTitle_LoadItem, request);
                }
            }
        }

*/

/*
        protected Elmah.MVVMLightViewModels.ElmahSource.ItemVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahSource.ItemVM>();
            }
        }
*/
        public Edit()
        {
            InitializeComponent();
        }

    }
}

