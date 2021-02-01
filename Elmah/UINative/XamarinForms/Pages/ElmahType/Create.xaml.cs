using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages.ElmahType
{

/*

    [QueryProperty("Type", "type")]

*/

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create : ContentPage
    {

/*

        // TODO: developer may need modify or delete, and parse value from string
        public string Type
        {
            set
            {
                var _Type = default(string);
                if (string.TryParse(value, out _Type))
                {
                    var request = new Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahTypeIdentifier> {
                        Criteria = new Elmah.DataSourceEntities.ElmahTypeIdentifier { Type = _Type }
                        , UIAction = Framework.ViewModels.UIAction.Create
                        , EnablePopup = false
                        , LoadDropDownContents = true
                        , SetDropDownSelectedItems = true
                        , LoadExtraDataIfNeeded = true
                        , Refresh = false
                    };
                    // use ItemVM type full name to avoid debug optimization issue
                    MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahType.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahTypeIdentifier>>(
                        ViewModel, Elmah.MVVMLightViewModels.ElmahType.ItemVM.MessageTitle_LoadItem, request);
                }
            }
        }

*/

/*
        protected Elmah.MVVMLightViewModels.ElmahType.ItemVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahType.ItemVM>();
            }
        }
*/
        public Create()
        {
            InitializeComponent();
        }

    }
}

