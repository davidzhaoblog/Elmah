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
        : Framework.Xaml.ViewModelBaseWithResultAndUIElement<Elmah.PetStore.Models.Order>
    {
        public override string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Order;

        public const string MessageTitle_LoadData_GetInventory = "Load_PetStore_Order_GetInventory";

        public const string MessageTitle_LoadData_GetOrderById = "Load_PetStore_Order_GetOrderById";

        #region 1. Properties

        protected NavigationVM.OrderActions m_CurrentGetAction;
        public NavigationVM.Order CurrentGetAction
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
        protected GetOrderByIdCriteria m_GetOrderByIdCriteria;
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

        // Store.Get.01 GetInventory /store/inventory
        public ICommand GetInventoryCommand { get; protected set; }

        // Store.Get.11 GetOrderById /store/order/{orderId}
        public ICommand GetOrderByIdCommand { get; protected set; }

        // Store.Post.01 PlaceOrder /store/order
        public ICommand PlaceOrderCommand { get; protected set; }

        #endregion 2. Commands

        public OrderVM()
            : base()
        {

            MessagingCenter.Subscribe<OrderVM, Framework.Xaml.LoadListDataRequest>(this, MessageTitle_LoadData_GetInventory, async (sender, request) =>
            {
                CurrentGetAction = NavigationVM.OrderAction.GetInventory;
                ListItemViewMode = request.ListItemViewMode;
                if(request.BindToGroupedResults.HasValue)
                {
                    if (!request.BindToGroupedResults.Value)
                        BindToGroupedResults = request.BindToGroupedResults.Value;
                    else
                        SetBindToGroupedResults(request.OrderByPropertyName, request.OrderByDirection);
                }
                // Set Critieria
                if(request.Parameters != null)
                {
                    if (request.Parameters.ContainsKey(nameof(Elmah.PetStore.Models.Order.onecondition)) && request.Parameters[nameof(Elmah.PetStore.Models.Order.onecondition)] != null)
                        this.Criteria.Common.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.PetStore.Models.Order.onecondition)];
                    // can be more
                    //if (request.Parameters.ContainsKey(nameof(Elmah.PetStore.Models.Order.onecondition)) && request.Parameters[nameof(Elmah.PetStore.Models.Order.onecondition)] != null)
                        //this.Criteria.Common.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.PetStore.Models.Order.onecondition)];
                }
                CachingOption = Framework.Xaml.CachingOptions.NoCaching  ?;
                QueryPagingSetting = GetDefaultQueryPagingSetting();
                QueryPagingSetting.CurrentPage = 1;
                await DoSearch(true, true);
                if(request.ActionWhenLaunch != null)
                    request.ActionWhenLaunch();
            });

            MessagingCenter.Subscribe<OrderVM, Framework.Xaml.LoadListDataRequest>(this, MessageTitle_LoadData_GetOrderById, async (sender, request) =>
            {
                CurrentGetAction = NavigationVM.OrderAction.GetOrderById;
                ListItemViewMode = request.ListItemViewMode;
                if(request.BindToGroupedResults.HasValue)
                {
                    if (!request.BindToGroupedResults.Value)
                        BindToGroupedResults = request.BindToGroupedResults.Value;
                    else
                        SetBindToGroupedResults(request.OrderByPropertyName, request.OrderByDirection);
                }
                // Set Critieria
                if(request.Parameters != null)
                {
                    if (request.Parameters.ContainsKey(nameof(Elmah.PetStore.Models.Order.onecondition)) && request.Parameters[nameof(Elmah.PetStore.Models.Order.onecondition)] != null)
                        this.Criteria.Common.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.PetStore.Models.Order.onecondition)];
                    // can be more
                    //if (request.Parameters.ContainsKey(nameof(Elmah.PetStore.Models.Order.onecondition)) && request.Parameters[nameof(Elmah.PetStore.Models.Order.onecondition)] != null)
                        //this.Criteria.Common.onecondition.NullableValueToCompare = (long)request.Parameters[nameof(Elmah.PetStore.Models.Order.onecondition)];
                }
                CachingOption = Framework.Xaml.CachingOptions.NoCaching  ?;
                QueryPagingSetting = GetDefaultQueryPagingSetting();
                QueryPagingSetting.CurrentPage = 1;
                await DoSearch(true, true);
                if(request.ActionWhenLaunch != null)
                    request.ActionWhenLaunch();
            });

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
                if (Result.Any(t => t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
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
                if (Result.Any(t => t.Id == SelectedItem.Id))
                {
                    Result.Add(SelectedItem);
                }
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

        public override Task DoSearch(bool isToClearExistingResult, bool isToLoadFromCache = false, bool enablePopup = true)
        {
            throw new NotImplementedException();
        }

        public override List<Framework.Queries.QueryOrderBySetting> GetDefaultQueryOrderBySettingCollection()
        {
            return new List<Framework.Queries.QueryOrderBySetting> {
                new Framework.Queries.QueryOrderBySetting { IsSelected = true, DisplayName = Elmah.PetStore.Resx.UIStringResource.Name, PropertyName = nameof(Elmah.PetStore.Models.Order.Name), Direction = Framework.Queries.QueryOrderDirections.Ascending, FontIcon = Framework.Xaml.FontAwesomeIcons.Font, FontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString(),
                        ClientSideActions = new QueryOrderBySettingClientSideActions {
                         GetGroupResults = list => {
                            var groupedResult =
                                from t in list
                                group t by new { FirstLetter = !string.IsNullOrEmpty(t.Name) && Char.IsLetter(t.Name.First()) ? t.Name.Substring(0, 1) : "?!#1-9" } into tg
                                select new GroupedResult(tg.Key.FirstLetter, tg.Key.FirstLetter, tg.Select(t => t.GetAClone()).ToList());
                            return groupedResult.ToList();
                         },
                         //GetSQLiteSortTableQuery = (tableQuery, direction) => {
                         //   tableQuery = tableQuery.Sort(t => t.Type, direction);
                         //    return tableQuery;
                         //}
                }}
            };
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

