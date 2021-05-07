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
    public partial class OrderItemVM
        : Framework.Xaml.ViewModelBaseWithResultAndUIElement<Elmah.PetStore.Models.Order>
    {
        public override string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Order;

        public const string MessageTitle_LoadData_GetInventory = "Load_PetStore_Order_GetInventory";

        public const string MessageTitle_LoadData_GetOrderById = "Load_PetStore_Order_GetOrderById";

        #region 1. Properties

        protected NavigationVM.OrderActions m_CurrentGetAction;
        public NavigationVM.OrderActions CurrentGetAction
        {
            get { return m_CurrentGetAction; }
            set
            {
                Set(nameof(CurrentGetAction), ref m_CurrentGetAction, value);
            }
        }

        public NavigationVM.OrderContainer NavigationContainer
        {
            get
            {
                return DependencyService.Resolve<NavigationVM.OrderContainer>();
            }
        }

        // Store.Get.11 GetOrderById /store/order/{orderId}
        protected GetOrderByIdCriteria m_GetOrderByIdCriteria = new GetOrderByIdCriteria();
        public GetOrderByIdCriteria GetOrderByIdCriteria
        {
            get { return m_GetOrderByIdCriteria; }
            set
            {
                Set(nameof(GetOrderByIdCriteria), ref m_GetOrderByIdCriteria, value);
            }
        }

        #endregion 1. Properties

        #region 2. Commands

        // Store.Delete.01 DeleteOrder /store/order/{orderId}
        public ICommand DeleteOrderCommand { get; protected set; }

        // Store.Post.01 PlaceOrder /store/order
        public ICommand PlaceOrderCommand { get; protected set; }

        #endregion 2. Commands

        public OrderItemVM()
            : base()
        {

            // Store.Delete.01 DeleteOrder /store/order/{orderId}
            DeleteOrderCommand = new Command(OnDeleteOrder, CanDeleteOrder);

            // Store.Post.01 PlaceOrder /store/order
            PlaceOrderCommand = new Command(OnPlaceOrder, CanPlaceOrder);

        }

        #region Delete, Put and Post Command methods

        // Store.Delete.01 DeleteOrder /store/order/{orderId}
        public async void OnDeleteOrder()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreateStoreApiClient();

            // TODO: you may add more code here to get proper parameter values.
            var result = await client.DeleteOrderAsync(orderId);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                //if (Result.Any(t => t.Id == SelectedItem.Id))
                //{
                //    Result.Remove(SelectedItem);
                //}
                SelectedItem = new Elmah.PetStore.Models.Order();

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

        protected virtual bool CanDeleteOrder()
        {
            return this.SelectedItem != null;
        }

        // Store.Post.01 PlaceOrder /store/order
        public async void OnPlaceOrder()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);

            var client = WebApiClientFactory.CreateStoreApiClient();

            var result = await client.PlaceOrderAsync(SelectedItem);

            if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                SelectedItem = result.Message;
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
        protected virtual bool CanPlaceOrder()
        {
            return this.SelectedItem != null;
        }

        #endregion Delete, Put and Post Command methods
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

