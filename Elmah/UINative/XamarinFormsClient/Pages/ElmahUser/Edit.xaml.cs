using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages.ElmahUser
{

/*

    [QueryProperty("User", "user")]

*/

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {

/*

        // TODO: developer may need modify or delete, and parse value from string
        public string User
        {
            set
            {
                var _User = default(string);
                if (string.TryParse(value, out _User))
                {
                    var request = new Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahUserIdentifier> {
                        Criteria = new Elmah.DataSourceEntities.ElmahUserIdentifier { User = _User }
                        , UIAction = Framework.ViewModels.UIAction.Update
                        , EnablePopup = false
                        , LoadDropDownContents = true
                        , SetDropDownSelectedItems = true
                        , LoadExtraDataIfNeeded = true
                        , Refresh = false
                    };
                    // use ItemVM type full name to avoid debug optimization issue
                    MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahUser.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahUserIdentifier>>(
                        ViewModel, Elmah.MVVMLightViewModels.ElmahUser.ItemVM.MessageTitle_LoadItem, request);
                }
            }
        }

*/

/*
        protected Elmah.MVVMLightViewModels.ElmahUser.ItemVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahUser.ItemVM>();
            }
        }
*/
        public Edit()
        {
            InitializeComponent();
        }

    }
}

