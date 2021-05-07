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
    public partial class OrderListVM
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

        public OrderListVM()
            : base()
        {

        }

        public override async Task DoSearch(bool isToClearExistingResult, bool isToLoadFromCache = false, bool enablePopup = true)
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            try
            {
                Framework.WebApi.Response<Elmah.PetStore.Models.Order[]> result;
                if(false)
                {}

                else
                {
                    // Do something, developer coding error: parameter is wrong
                    PopupVM.HidePopup();
                    return;
                }

                if (result.Status == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                // success, will close Item Popup and popup message box
                {
                    BindResult(result.Message, isToClearExistingResult);
                }
                else
                // failed
                {
                    // TODO: should display error message, no change to binding?
                    this.StatusMessageOfResult = result.StatusMessageOfResult;
                    this.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                }
            }
            catch //(Exception ex)
            {
            }

            if (enablePopup)
                PopupVM.HidePopup();
        }

        /*
        // TODO: you can customize Search()/CanSearch()/LoadMore()
        protected override async void Search()
        {
        }

        protected override bool CanSearch()
        {
        }

        protected override async void LoadMore()
        {
        }
        */

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

