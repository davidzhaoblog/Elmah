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
    public partial class ApiResponseVM
        : Framework.Xaml.ViewModelBase2
    {
        public const string MessageTitle_LoadData = "Load_PetStore_ApiResponse_VM";
        public string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.ApiResponse;

        protected Elmah.PetStore.Models.ApiResponse m_Item;
        public Elmah.PetStore.Models.ApiResponse Item
        {
            get { return m_Item; }
            set
            {
                Set(nameof(Item), ref m_Item, value);
            }
        }

        // Pet.Get.01 FindPetsByStatus /pet/findByStatus
        protected FindPetsByStatusCriteria m_FindPetsByStatusCriteria;
        public FindPetsByStatusCriteria FindPetsByStatusCriteria
        {
            get { return m_FindPetsByStatusCriteria; }
            set
            {
                Set(nameof(FindPetsByStatusCriteria), ref m_FindPetsByStatusCriteria, value);
            }
        }

        // Pet.Get.11 FindPetsByTags /pet/findByTags
        protected FindPetsByTagsCriteria m_FindPetsByTagsCriteria;
        public FindPetsByTagsCriteria FindPetsByTagsCriteria
        {
            get { return m_FindPetsByTagsCriteria; }
            set
            {
                Set(nameof(FindPetsByTagsCriteria), ref m_FindPetsByTagsCriteria, value);
            }
        }

        // Pet.Get.21 GetPetById /pet/{petId}
        protected GetPetByIdCriteria m_GetPetByIdCriteria;
        public GetPetByIdCriteria GetPetByIdCriteria
        {
            get { return m_GetPetByIdCriteria; }
            set
            {
                Set(nameof(GetPetByIdCriteria), ref m_GetPetByIdCriteria, value);
            }
        }

        // Pet.Delete.01 DeletePet /pet/{petId}
        public ICommand DeletePetCommand { get; protected set; }

        // Pet.Get.01 FindPetsByStatus /pet/findByStatus
        public ICommand FindPetsByStatusCommand { get; protected set; }

        // Pet.Get.11 FindPetsByTags /pet/findByTags
        public ICommand FindPetsByTagsCommand { get; protected set; }

        // Pet.Get.21 GetPetById /pet/{petId}
        public ICommand GetPetByIdCommand { get; protected set; }

        // Pet.Post.01 AddPet /pet
        public ICommand AddPetCommand { get; protected set; }

        // Pet.Post.11 UpdatePetWithForm /pet/{petId}
        public ICommand UpdatePetWithFormCommand { get; protected set; }

        // Pet.Post.21 UploadFile /pet/{petId}/uploadImage
        public ICommand UploadFileCommand { get; protected set; }

        // Pet.Put.01 UpdatePet /pet
        public ICommand UpdatePetCommand { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the IndexVM class.
        /// </summary>
        public ApiResponseVM()
            : base()
        {

            // Pet.Delete.01 DeletePet /pet/{petId}
            DeletePetCommand = new Command(OnDeletePet, CanDeletePet);

            // Pet.Get.01 FindPetsByStatus /pet/findByStatus
            FindPetsByStatusCommand = new Command(OnFindPetsByStatus, CanFindPetsByStatus);

            // Pet.Get.11 FindPetsByTags /pet/findByTags
            FindPetsByTagsCommand = new Command(OnFindPetsByTags, CanFindPetsByTags);

            // Pet.Get.21 GetPetById /pet/{petId}
            GetPetByIdCommand = new Command(OnGetPetById, CanGetPetById);

            // Pet.Post.01 AddPet /pet
            AddPetCommand = new Command(OnAddPet, CanAddPet);

            // Pet.Post.11 UpdatePetWithForm /pet/{petId}
            UpdatePetWithFormCommand = new Command(OnUpdatePetWithForm, CanUpdatePetWithForm);

            // Pet.Post.21 UploadFile /pet/{petId}/uploadImage
            UploadFileCommand = new Command(OnUploadFile, CanUploadFile);

            // Pet.Put.01 UpdatePet /pet
            UpdatePetCommand = new Command(OnUpdatePet, CanUpdatePet);

        }

        // Pet.Delete.01 DeletePet /pet/{petId}
        public async void OnDeletePet()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.DeletePetAsync("", Item.Code);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                //if (Items.Any(t => t.Code == Item.Code))
                //{
                //    Items.Remove(Item);
                //}
                Item = new Elmah.PetStore.Models.ApiResponse();

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

        protected virtual bool CanDeletePet()
        {
            return this.Item != null;
        }

        public async void OnFindPetsByStatus()
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

        public async void OnFindPetsByTags()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.FindPetsByTagsAsync("", Item.Id);

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

        protected virtual bool CanFindPetsByTags()
        {
            return true;
        }

        public async void OnGetPetById()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.GetPetByIdAsync("", Item.Id);

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

        protected virtual bool CanGetPetById()
        {
            return true;
        }

        // Pet.Post.01 AddPet /pet
        public async void OnAddPet()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.AddPetAsync(Item);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                Item = result.Message;
                //if(!Items.Any(t=>t.Code == Item.Code))
                //{
                //    Items.Add(Item);
                //}
                // success, will close Item Popup and popup message box
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanAddPet()
        {
            return this.Item != null;
        }

        // Pet.Post.11 UpdatePetWithForm /pet/{petId}
        public async void OnUpdatePetWithForm()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.UpdatePetWithFormAsync(Item);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                Item = result.Message;
                //if(!Items.Any(t=>t.Code == Item.Code))
                //{
                //    Items.Add(Item);
                //}
                // success, will close Item Popup and popup message box
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanUpdatePetWithForm()
        {
            return this.Item != null;
        }

        // Pet.Post.21 UploadFile /pet/{petId}/uploadImage
        public async void OnUploadFile()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.UploadFileAsync(Item);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                Item = result.Message;
                //if(!Items.Any(t=>t.Code == Item.Code))
                //{
                //    Items.Add(Item);
                //}
                // success, will close Item Popup and popup message box
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanUploadFile()
        {
            return this.Item != null;
        }

        // Pet.Put.01 UpdatePet /pet
        public async void OnUpdatePet()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreatePetApiClient();

            var result = await client.UpdatePetAsync(Item);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                Item = result.Message;
                //if(!Items.Any(t=>t.Code == Item.Code))
                //{
                //    Items.Add(Item);
                //}
                // success, will close Item Popup and popup message box
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyupdated, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoupdate, GetThisItemDisplayString(), "!");
            }
        }
        protected virtual bool CanUpdatePet()
        {
            return this.Item != null;
        }

    }

    // Pet.Get.01 FindPetsByStatus /pet/findByStatus
    public class FindPetsByStatusCriteria : Framework.Models.PropertyChangedNotifier
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
    public class FindPetsByTagsCriteria : Framework.Models.PropertyChangedNotifier
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
    public class GetPetByIdCriteria : Framework.Models.PropertyChangedNotifier
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

