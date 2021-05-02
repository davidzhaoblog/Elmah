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
    public partial class OrderVM
        : Framework.Xaml.ViewModelBase2
    {
        public const string MessageTitle_LoadData = "Load_PetStore_Order_VM";
        public string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Order;

        //protected ObservableCollection<Elmah.PetStore.Models.Order> m_Items = new ObservableCollection<Elmah.PetStore.Models.Order>();
        //public ObservableCollection<Elmah.PetStore.Models.Order> Items
        //{
        //    get { return m_Items; }
        //    set
        //    {
        //        Set(nameof(Items), ref m_Items, value);
        //    }
        //}

        protected Elmah.PetStore.Models.Order m_Item;
        public Elmah.PetStore.Models.Order Item
        {
            get { return m_Item; }
            set
            {
                Set(nameof(Item), ref m_Item, value);
            }
        }

        // Store.Get.11 GetOrderById /store/order/{orderId}
        protected GetOrderByIdCriteria m_GetOrderByIdCriteria;
        public GetOrderByIdCriteria GetOrderByIdCriteria
        {
            get { return m_GetOrderByIdCriteria; }
            set
            {
                Set(nameof(GetOrderByIdCriteria), ref m_GetOrderByIdCriteria, value);
            }
        }

        // Store.Delete.01 DeleteOrder /store/order/{orderId}
        public ICommand DeleteOrderCommand { get; protected set; }

        // Store.Get.01 GetInventory /store/inventory
        public ICommand GetInventoryCommand { get; protected set; }

        // Store.Get.11 GetOrderById /store/order/{orderId}
        public ICommand GetOrderByIdCommand { get; protected set; }

        // Store.Post.01 PlaceOrder /store/order
        public ICommand PlaceOrderCommand { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the IndexVM class.
        /// </summary>
        public OrderVM()
            : base()
        {

            // Store.Delete.01 DeleteOrder /store/order/{orderId}
            DeleteOrderCommand = new Command(OnDeleteOrder, CanDeleteOrder);

            // Store.Get.01 GetInventory /store/inventory
            GetInventoryCommand = new Command(OnGetInventory, CanGetInventory);

            // Store.Get.11 GetOrderById /store/order/{orderId}
            GetOrderByIdCommand = new Command(OnGetOrderById, CanGetOrderById);

            // Store.Post.01 PlaceOrder /store/order
            PlaceOrderCommand = new Command(OnPlaceOrder, CanPlaceOrder);

        }

        // Store.Delete.01 DeleteOrder /store/order/{orderId}
        public async void OnDeleteOrder()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreateStoreApiClient();

            // TODO: Please assign proper parameters
            var result = await client.DeleteOrderAsync(Item.Id);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                //if (Items.Any(t => t.Id == Item.Id))
                //{
                //    Items.Remove(Item);
                //}
                Item = new Elmah.PetStore.Models.Order();

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

        protected virtual bool CanDeleteOrder()
        {
            return this.Item != null;
        }

        // Store.Get.01 GetInventory /store/inventory
        public async void OnGetInventory()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            var client = WebApiClientFactory.CreateStoreApiClient();

            var result = await client.GetInventoryAsync();

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (Items.Any(t => t.Id == Item.Id))
                {
                    Items.Add(Item);
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

        protected virtual bool CanGetInventory()
        {
            return true;
        }

        // Store.Get.11 GetOrderById /store/order/{orderId}
        public async void OnGetOrderById()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            var client = WebApiClientFactory.CreateStoreApiClient();

            var result = await client.GetOrderByIdAsync(GetOrderByIdCriteria.OrderId);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (Items.Any(t => t.Id == Item.Id))
                {
                    Items.Add(Item);
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

        protected virtual bool CanGetOrderById()
        {
            return true;
        }

        // Store.Post.01 PlaceOrder /store/order
        public async void OnPlaceOrder()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreateStoreApiClient();

            var result = await client.PlaceOrderAsync(Item);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                Item = result.Message;
                //if(!Items.Any(t=>t.Id == Item.Id))
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
        protected virtual bool CanPlaceOrder()
        {
            return this.Item != null;
        }

    }

    // Store.Get.11 GetOrderById /store/order/{orderId}
    public class GetOrderByIdCriteria: Framework.Models.PropertyChangedNotifier
    {

        private long m_OrderId;

        [Display(Name = "OrderId", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        public long OrderId
        {
            get
            {
                return m_OrderId;
            }
            set
            {
                Set(nameof(OrderId), ref m_OrderId, value);
            }
        }

    }

}

