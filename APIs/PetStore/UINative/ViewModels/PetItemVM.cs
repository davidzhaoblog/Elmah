using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

//using Framework.Xaml.SQLite;

namespace Elmah.PetStore.ViewModels
{
    public partial class PetItemVM
        : Framework.Xaml.ViewModelBaseWithResultAndUIElement<Elmah.PetStore.Models.Pet>
    {
        public override string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Pet;

        public const string MessageTitle_LoadData_FindPetsByStatus = "Load_PetStore_Pet_FindPetsByStatus";

        public const string MessageTitle_LoadData_FindPetsByTags = "Load_PetStore_Pet_FindPetsByTags";

        public const string MessageTitle_LoadData_GetPetById = "Load_PetStore_Pet_GetPetById";

        #region 1. Properties

        protected NavigationVM.PetActions m_CurrentGetAction;
        public NavigationVM.PetActions CurrentGetAction
        {
            get { return m_CurrentGetAction; }
            set
            {
                Set(nameof(CurrentGetAction), ref m_CurrentGetAction, value);
            }
        }

        public NavigationVM.PetContainer NavigationContainer
        {
            get
            {
                return DependencyService.Resolve<NavigationVM.PetContainer>();
            }
        }

        // Pet.Get.01 FindPetsByStatus /pet/findByStatus
        protected FindPetsByStatusCriteria m_FindPetsByStatusCriteria = new FindPetsByStatusCriteria { Status = "available" };
        public FindPetsByStatusCriteria FindPetsByStatusCriteria
        {
            get { return m_FindPetsByStatusCriteria; }
            set
            {
                Set(nameof(FindPetsByStatusCriteria), ref m_FindPetsByStatusCriteria, value);
            }
        }

        // Pet.Get.11 FindPetsByTags /pet/findByTags
        protected FindPetsByTagsCriteria m_FindPetsByTagsCriteria = new FindPetsByTagsCriteria();
        public FindPetsByTagsCriteria FindPetsByTagsCriteria
        {
            get { return m_FindPetsByTagsCriteria; }
            set
            {
                Set(nameof(FindPetsByTagsCriteria), ref m_FindPetsByTagsCriteria, value);
            }
        }

        // Pet.Get.21 GetPetById /pet/{petId}
        protected GetPetByIdCriteria m_GetPetByIdCriteria = new GetPetByIdCriteria();
        public GetPetByIdCriteria GetPetByIdCriteria
        {
            get { return m_GetPetByIdCriteria; }
            set
            {
                Set(nameof(GetPetByIdCriteria), ref m_GetPetByIdCriteria, value);
            }
        }

        #endregion 1. Properties

        #region 2. Commands

        // Pet.Delete.01 DeletePet /pet/{petId}
        public ICommand DeletePetCommand { get; protected set; }

        // Pet.Post.01 AddPet /pet
        public ICommand AddPetCommand { get; protected set; }

        // Pet.Post.11 UpdatePetWithForm /pet/{petId}
        public ICommand UpdatePetWithFormCommand { get; protected set; }

        // Pet.Post.21 UploadFile /pet/{petId}/uploadImage
        public ICommand UploadFileCommand { get; protected set; }

        // Pet.Put.01 UpdatePet /pet
        public ICommand UpdatePetCommand { get; protected set; }

        #endregion 2. Commands

        public PetItemVM()
            : base()
        {

            // Pet.Delete.01 DeletePet /pet/{petId}
            DeletePetCommand = new Command(OnDeletePet, CanDeletePet);

            // Pet.Post.01 AddPet /pet
            AddPetCommand = new Command(OnAddPet, CanAddPet);

            // Pet.Post.11 UpdatePetWithForm /pet/{petId}
            UpdatePetWithFormCommand = new Command(OnUpdatePetWithForm, CanUpdatePetWithForm);

            // Pet.Post.21 UploadFile /pet/{petId}/uploadImage
            UploadFileCommand = new Command(OnUploadFile, CanUploadFile);

            // Pet.Put.01 UpdatePet /pet
            UpdatePetCommand = new Command(OnUpdatePet, CanUpdatePet);

        }

        #region Delete, Put and Post Command methods

        // Pet.Delete.01 DeletePet /pet/{petId}
        public async void OnDeletePet()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            // TODO: you may add more code here to get proper parameter values.
            string api_key = "";
            var result = await client.DeletePetAsync(api_key, SelectedItem.Id);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (Result.Any(t => t.Id == SelectedItem.Id))
                {
                    Result.Remove(SelectedItem);
                }
                SelectedItem = new Elmah.PetStore.Models.Pet();

                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullydeleted, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.FailedToSave, GetThisItemDisplayString(), "!");
            }
        }

        protected virtual bool CanDeletePet()
        {
            return this.SelectedItem != null;
        }

        // Pet.Post.01 AddPet /pet
        public async void OnAddPet()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.AddPetAsync(SelectedItem);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                SelectedItem = result.Message;
                if(!Result.Any(t=>t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanAddPet()
        {
            return this.SelectedItem != null;
        }

        // Pet.Post.11 UpdatePetWithForm /pet/{petId}
        public async void OnUpdatePetWithForm()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.UpdatePetWithFormAsync(SelectedItem.Id, SelectedItem.Name, SelectedItem.Status);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                // TODO: OnUpdatePetWithForm shouldn't used in Xamarin.Forms
                // SelectedItem = result.Message;
                //if(!Result.Any(t=>t.Id == SelectedItem.Id))
                //{
                //    Result.Add(SelectedItem);
                //}
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanUpdatePetWithForm()
        {
            return this.SelectedItem != null;
        }

        // Pet.Post.21 UploadFile /pet/{petId}/uploadImage
        public async void OnUploadFile()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            // TODO: what is additionalMetadata
            string additionalMetadata = "";
            var result = await client.UploadFileAsync(SelectedItem.Id, additionalMetadata);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                // TODO: OnUploadFile need more code.
                //SelectedItem = result.Message;
                //if(!Result.Any(t=>t.Id == SelectedItem.Id))
                //{
                //    Result.Add(SelectedItem);
                //}
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanUploadFile()
        {
            return this.SelectedItem != null;
        }

        // Pet.Put.01 UpdatePet /pet
        public async void OnUpdatePet()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.UpdatePetAsync(SelectedItem);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                SelectedItem = result.Message;
                if(!Result.Any(t=>t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
                // success, will close Item Popup and popup message box
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostItemAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanUpdatePet()
        {
            return this.SelectedItem != null;
        }

        #endregion Delete, Put and Post Command methods
    }

    // Pet.Get.01 FindPetsByStatus /pet/findByStatus
    public class FindPetsByStatusCriteria: Framework.Models.PropertyChangedNotifier
    {

        private string m_Status;

        [Display(Name = "Status", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public string Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                Set(nameof(Status), ref m_Status, value);
            }
        }

    }

    // Pet.Get.11 FindPetsByTags /pet/findByTags
    public class FindPetsByTagsCriteria: Framework.Models.PropertyChangedNotifier
    {

        private string[] m_Tags;

        [Display(Name = "Tags", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public string[] Tags
        {
            get
            {
                return m_Tags;
            }
            set
            {
                Set(nameof(Tags), ref m_Tags, value);
            }
        }

    }

    // Pet.Get.21 GetPetById /pet/{petId}
    public class GetPetByIdCriteria: Framework.Models.PropertyChangedNotifier
    {

        private long m_PetId;

        [Display(Name = "PetId", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public long PetId
        {
            get
            {
                return m_PetId;
            }
            set
            {
                Set(nameof(PetId), ref m_PetId, value);
            }
        }

    }

}

