using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages.ELMAH_Error
{

/*

    [QueryProperty("ErrorId", "errorid")]

    [QueryProperty("Sequence", "sequence")]

*/

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {

/*

        bool WaitForErrorId = true;
        private long m_ErrorId;
        // TODO: developer may need modify or delete, and parse value from string
        public string ErrorId
        {
            set
            {
                if (long.TryParse(value, out m_ErrorId))
                {
                    WaitForErrorId = false;
                    NotifyViewModelToLoadItem();
                }
            }
        }

        bool WaitForSequence = true;
        private long m_Sequence;
        // TODO: developer may need modify or delete, and parse value from string
        public string Sequence
        {
            set
            {
                if (long.TryParse(value, out m_Sequence))
                {
                    WaitForSequence = false;
                    NotifyViewModelToLoadItem();
                }
            }
        }

*/

/*
        protected Elmah.MVVMLightViewModels.ELMAH_Error.ItemVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.ELMAH_Error.ItemVM>();
            }
        }
*/
        public Edit()
        {
            InitializeComponent();
        }

/*

        private void NotifyViewModelToLoadItem()
        {
            if (!WaitForErrorId && !WaitForSequence)
            {
                var request = new Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ELMAH_ErrorIdentifier> {
                    Criteria = new Elmah.DataSourceEntities.ELMAH_ErrorIdentifier { ErrorId = m_ErrorId, Sequence = m_Sequence }
                , UIAction = Framework.ViewModels.UIAction.Update
                , EnablePopup = false
                , LoadDropDownContents = true
                , SetDropDownSelectedItems = true
                , LoadExtraDataIfNeeded = true
                , Refresh = false
                };
                // use ItemVM type full name to avoid debug optimization issue
                MessagingCenter.Send<Elmah.MVVMLightViewModels.ELMAH_Error.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ELMAH_ErrorIdentifier>>(
                    ViewModel, Elmah.MVVMLightViewModels.ELMAH_Error.ItemVM.MessageTitle_LoadItem, request);
                this.BindingContext = this.ViewModel;
                WaitForErrorId = true; WaitForSequence = true;
            }
        }

*/

    }
}

