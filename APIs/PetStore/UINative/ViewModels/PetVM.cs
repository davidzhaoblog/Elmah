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
        public const string MessageTitle_LoadData = "Load_PetStore_Pet_VM";
        public string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Pet;

        protected ObservableCollection<Elmah.PetStore.Models.Pet> m_Items = new ObservableCollection<Elmah.PetStore.Models.Pet>();
        public ObservableCollection<Elmah.PetStore.Models.Pet> Items
        {
            get { return m_Items; }
            set
            {
                Set(nameof(Items), ref m_Items, value);
            }
        }

        protected Elmah.PetStore.Models.Pet m_Item;
        public Elmah.PetStore.Models.Pet Item
        {
            get { return m_Item; }
            set
            {
                Set(nameof(Item), ref m_Item, value);
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
        public PetVM()
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

            var result = await client.DeletePetAsync("", Item.Id);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (Items.Any(t => t.Id == Item.Id))
                {
                    Items.Remove(Item);
                }
                Item = new Elmah.PetStore.Models.Pet();

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

        protected virtual bool CanFindPetsByStatus: /pet/findByStatus()
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

        protected virtual bool CanFindPetsByTags: /pet/findByTags()
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

        protected virtual bool CanGetPetById: /pet/{petId}()
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
                if(!Items.Any(t=>t.Id == Item.Id))
                {
                    Items.Add(Item);
                }
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
                if(!Items.Any(t=>t.Id == Item.Id))
                {
                    Items.Add(Item);
                }
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
                if(!Items.Any(t=>t.Id == Item.Id))
                {
                    Items.Add(Item);
                }
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
                if(!Items.Any(t=>t.Id == Item.Id))
                {
                    Items.Add(Item);
                }
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
}

    // Pet.Delete.01 DeletePet /pet/{petId}
    public class DeletePetCriteria
    {

        private string m_Api_key;

        [Display(Name = "Api_key", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public string Api_key
        {
            get
            {
                return m_Api_key;
            }
            set
            {
                Set(nameof(Api_key), ref m_Api_key, value);
            }
        }

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

    // Pet.Get.01 FindPetsByStatus /pet/findByStatus
    public class FindPetsByStatusCriteria
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
    public class FindPetsByTagsCriteria
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
    public class GetPetByIdCriteria
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

    public class UpdatePetWithFormCriteria
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

        private string m_Name;

        [Display(Name = "Name", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                Set(nameof(Name), ref m_Name, value);
            }
        }

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

