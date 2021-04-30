using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Windows.Input;

//using Framework.Xaml.SQLite;

namespace Elmah.PetStore.ViewModels
{
    public partial class PetVM
        : Framework.Xaml.ViewModelBase2
    {

        public async void OnDFindPetsByStatus()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.FindPetsByStatusAsync("", Item.Id);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (Items.Any(t => t.Id == Item.Id))
                {
                    Items.Remove(Item);
                    Item = null;
                }
                // success, will close Item Popup and popup message box
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullydeleted, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.FailedToSave, GetThisItemDisplayString(), "!");
            }
        }

        protected virtual bool CanFindPetsByStatus()
        {
            return true;
        }
    }
}


