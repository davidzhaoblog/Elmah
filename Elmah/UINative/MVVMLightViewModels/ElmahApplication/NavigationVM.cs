using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class NavigationVM
    {

        public ElmahApplicationContainer ElmahApplication
        {
            get
            {
                return DependencyService.Resolve<ElmahApplicationContainer>();
            }
        }

        public partial class ElmahApplicationContainer : Framework.Xaml.NavigationVMEntityContainer<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Elmah.DataSourceEntities.ElmahApplicationIdentifier, Elmah.EntityContracts.IElmahApplicationIdentifier, Elmah.DataSourceEntities.ElmahApplication>
        {

            public const string DomainKey = "ElmahApplication";

            public NavigationVM NavigationVM
            {
                get
                {
                    return DependencyService.Resolve<NavigationVM>();
                }
            }

            public ElmahApplicationContainer(): base()
            {
                // 3. TabSearchResult/Results/Search/SearchResult
                SearchFormActionSheet = GetSearchFormActionSheet();
            }

            //public ICommand ShowItemBottomUpActionSheetCommand => new Command<Elmah.DataSourceEntities.ElmahApplication>(OnShowItemBottomUpActionSheetCommand);

            //private void OnShowItemBottomUpActionSheetCommand(Elmah.DataSourceEntities.ElmahApplication item)
            //{
            //    var selectedActionItemModels = new List<Tuple<Framework.Xaml.StandardRouteRelativeKey, Framework.Xaml.ControlParentOptions>>();
            //
            //    selectedActionItemModels.Add(new Tuple<Framework.Xaml.StandardRouteRelativeKey, Framework.Xaml.ControlParentOptions>(Framework.Xaml.StandardRouteRelativeKey., Framework.Xaml.ControlParentOptions.InPopup));
            //
            //    var actionSheetVM = GetItemActionSheet(item, selectedActionItemModels);
            //
            //    PopupVM.ShowBottomUpActionSheet(actionSheetVM);
            //}

            //public Framework.Xaml.ActionForm.ActionSheetVM GetItemInlineActionSheet(Elmah.DataSourceEntities.ElmahApplication item)
            //{
            //    var selectedActionItemModels = new List<Tuple<Framework.Xaml.StandardRouteRelativeKey, Framework.Xaml.ControlParentOptions>>();
            //
            //    selectedActionItemModels.Add(new Tuple<Framework.Xaml.StandardRouteRelativeKey, Framework.Xaml.ControlParentOptions>(Framework.Xaml.StandardRouteRelativeKey., Framework.Xaml.ControlParentOptions.InPopup));
            //
            //    return GetItemActionSheet(item, selectedActionItemModels);
            //}

            //public Framework.Xaml.ActionForm.ActionSheetVM GetItemActionSheet(
            //    Elmah.DataSourceEntities.ElmahApplication item
            //    , List<Tuple<Framework.Xaml.StandardRouteRelativeKey, Framework.Xaml.ControlParentOptions>> selectedList)
            //{
            //    var actionItems = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            //
            //    foreach (var selected in selectedList)
            //        actionItems.Add(GetActionItemModel(item, selected.Item1, selected.Item2));
            //
            //    var actionSheetVM = new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = true, ActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(actionItems) };
            //    return actionSheetVM;
            //}

            /*************************************
             * List of NavigateToCommand *
             *************************************/
            // 1. ItemVM
            #region 1. ItemVM

            // 1.1. NavigateTo Create
            // 1.1.1. NavigateToCommand_Create
            /*
            public ICommand NavigateToCommand_Create { get; private set; }
            public async void OnNavigateToCommand_Create(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var request = GetLoadDataRequest_NavigateToCommand_Create<Elmah.DataSourceEntities.ElmahApplication>(item);
                DetailsFormActionSheetWhenCreate = GetDetailsFormActionSheetWhenCreate(ItemVM);

                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplication>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_AssignItem, request);
                await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Create.ToString());

                //var parameters = new Dictionary<string, string>();

            //parameters.Add("application", item.Application.ToString());

                //await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Create.ToString(), parameters);
            }
            */

            // 1.1.2. ShowPopupCommand_Create

            public override void OnShowPopupCommand_Create(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var request = GetLoadDataRequest_ShowPopupCommand_Create<Elmah.DataSourceEntities.ElmahApplication>(item);
                DetailsFormActionSheetWhenCreate = GetDetailsFormActionSheetWhenCreate(ItemVM);

                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplication>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_AssignItem, request);

                PopupVM.ShowItemControlPopup(Framework.Xaml.StandardRouteRelativeKey.Create.ToString(), true);
            }

            public void DefaultItem(long entityID)
            {
                ItemVM.DefaultItem = new Elmah.DataSourceEntities.ElmahApplication {
                };
            }

            // 1.2. NavigateTo Delete
            // 1.2.1. NavigateToCommand_Delete
            /*
            public ICommand NavigateToCommand_Delete { get; private set; }
            public async void OnNavigateToCommand_Delete(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var criteria = new Elmah.DataSourceEntities.ElmahApplicationIdentifier {
                    Application = item.Application
                };
                var request = GetLoadDataRequest_NavigateToCommand_Delete<Elmah.DataSourceEntities.ElmahApplicationIdentifier>(criteria);

                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplicationIdentifier>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_LoadItem, request);
                await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Delete.ToString());

                //var parameters = new Dictionary<string, string>();

            //parameters.Add("application", item.Application.ToString());

                //await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Delete.ToString(), parameters);
            }
            */

            // 1.2.2. ShowPopupCommand_Delete
            /*
            public override void OnShowPopupCommand_Delete(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var criteria = new Elmah.DataSourceEntities.ElmahApplicationIdentifier {
                    Application = item.Application
                };
                var request = GetLoadDataRequest_ShowPopupCommand_Delete<Elmah.DataSourceEntities.ElmahApplicationIdentifier>(criteria);

                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplicationIdentifier>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_LoadItem, request);
                PopupVM.ShowItemControlPopup(Framework.Xaml.StandardRouteRelativeKey.Delete.ToString(), true);
            }
            */

            // 1.3. NavigateTo Details
            // 1.3.1. NavigateToCommand_Details
            /*
            public ICommand NavigateToCommand_Details { get; private set; }
            public async void OnNavigateToCommand_Details(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var criteria = new Elmah.DataSourceEntities.ElmahApplicationIdentifier {
                    Application = item.Application
                };
                var request = GetLoadDataRequest_NavigateToCommand_Details<Elmah.DataSourceEntities.ElmahApplicationIdentifier>(criteria);
                DetailsFormActionSheetWhenReadOnly = GetDetailsFormActionSheetWhenReadOnly(ItemVM);
                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplicationIdentifier>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_LoadItem, request);
                await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Details.ToString());

                //var parameters = new Dictionary<string, string>();

            //parameters.Add("application", item.Application.ToString());

                //await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Details.ToString(), parameters);
            }
            */

            // 1.3.2. ShowPopupCommand_Details
            public override async void OnShowPopupCommand_Details(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var criteria = new Elmah.DataSourceEntities.ElmahApplicationIdentifier {
                    Application = item.Application
                };
                var request = GetLoadDataRequest_ShowPopupCommand_Details<Elmah.DataSourceEntities.ElmahApplicationIdentifier>(criteria);
                DetailsFormActionSheetWhenReadOnly = GetDetailsFormActionSheetWhenReadOnly(ItemVM);
                DetailsFormActionSheetWhenEdit = GetDetailsFormActionSheetWhenEdit(ItemVM);
                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplicationIdentifier>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_LoadItem, request);
                PopupVM.ShowItemControlPopup(Framework.Xaml.StandardRouteRelativeKey.Details.ToString(), true);
            }

            // 1.4. NavigateTo Edit
            // 1.4.1. NavigateToCommand_Edit
            /*
            public ICommand NavigateToCommand_Edit { get; private set; }
            public async void OnNavigateToCommand_Edit(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var criteria = new Elmah.DataSourceEntities.ElmahApplicationIdentifier {
                    Application = item.Application
                };
                var request = GetLoadDataRequest_ShowPopupCommand_Details<Elmah.DataSourceEntities.ElmahApplicationIdentifier>(criteria);
                DetailsFormActionSheetWhenEdit = GetDetailsFormActionSheetWhenEdit(ItemVM);
                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplicationIdentifier>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_LoadItem, request);
                await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Edit.ToString());

                //var parameters = new Dictionary<string, string>();

            //parameters.Add("application", item.Application.ToString());

                //await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Edit.ToString(), parameters);
            }
            */

            // 1.4.2. ShowPopupCommand_Edit
            public override async void OnShowPopupCommand_Edit(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var criteria = new Elmah.DataSourceEntities.ElmahApplicationIdentifier {
                    Application = item.Application
                };
                var request = GetLoadDataRequest_ShowPopupCommand_Edit<Elmah.DataSourceEntities.ElmahApplicationIdentifier>(criteria);
                DetailsFormActionSheetWhenEdit = GetDetailsFormActionSheetWhenEdit(ItemVM);
                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplicationIdentifier>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_LoadItem, request);
                PopupVM.ShowItemControlPopup(Framework.Xaml.StandardRouteRelativeKey.Edit.ToString(), true);
            }

            // 1.5. NavigateTo Copy
            // 1.5.1. NavigateToCommand_Copy
            /*
            public ICommand NavigateToCommand_Copy { get; private set; }

            public void OnNavigateToCommand_Copy(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var cloned = item.GetAClone<Elmah.DataSourceEntities.ElmahApplication>();
                // TODO: set value when copy an item to create a new item, e.g. identifiers to default value.
                cloned.Application = default(string);

                var request = GetLoadDataRequest_NavigateToCommand_Copy<Elmah.DataSourceEntities.ElmahApplication>(cloned);
                DetailsFormActionSheetWhenCreate = GetDetailsFormActionSheetWhenCreate(ItemVM);

                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplication>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_AssignItem, request);
                await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Create.ToString());

                //var parameters = new Dictionary<string, string>();

            //parameters.Add("application", item.Application.ToString());

                //await NavigateAsync(DomainKey, Framework.Xaml.StandardRouteRelativeKey.Create.ToString(), parameters);
            }
            */

            // 1.5.2. ShowPopupCommand_Copy
            /*
            public override void OnShowPopupCommand_Copy(Elmah.DataSourceEntities.ElmahApplication item)
            {
                PopupVM.HidePopup();

                var cloned = item.GetAClone<Elmah.DataSourceEntities.ElmahApplication>();
                // TODO: set value when copy an item to create a new item, e.g. identifiers to default value.
                cloned.Application = default(string);

                var request = GetLoadDataRequest_ShowPopupCommand_Copy<Elmah.DataSourceEntities.ElmahApplication>(cloned);
                DetailsFormActionSheetWhenCreate = GetDetailsFormActionSheetWhenCreate(ItemVM);

                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.ItemVM, Framework.Xaml.LoadItemDataRequest<Elmah.DataSourceEntities.ElmahApplication>>(
                    ItemVM, Elmah.MVVMLightViewModels.ElmahApplication.ItemVM.MessageTitle_AssignItem, request);

                PopupVM.ShowItemControlPopup(Framework.Xaml.StandardRouteRelativeKey.Create.ToString(), true);
            }
            */

            #endregion 1. ItemVM

            // 2. SingleItemForm/SingleItemAggregateForm

            // 3. TabSearchResult/Results/Search/SearchResult
            // TODO: Will use NavigationVM.NavigationCommand for navigation,
            // 3.0.1. ActionParameter is defined below with Domain and Page, but empty ActionParameter.Parameters
            // 3.0.2. ActionParameter.SendMessage is defined
            // 3.0.3. The target ViewModel.Criteria will be set in SetCriteria_xxx method, SetCriteria_xxx method will be called when Source ViewModel or View.
            // 3.0.4. Using ListFooterActionSheet for now, ListFloatingActionSheet is obselete.

            protected virtual Framework.Xaml.ActionForm.ActionSheetVM GetListFooterActionSheet(Elmah.MVVMLightViewModels.ElmahApplication.IndexVM vm)
            {
                List<Framework.Xaml.ActionForm.ActionItemModel> list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                //list.Add(GetActionItemModel_LaunchCommonSearchView());
                list.Add(GetActionItemModel(ItemVM.DefaultItem, Framework.Xaml.StandardRouteRelativeKey.Create, Framework.Xaml.ControlParentOptions.InPopup));
                //list.Add(GetShowListFullScreenActionSheetActionItemModel(ShowListFullScreenActionSheetCommand));
                ShowListFullScreenActionSheetActionItemModel = GetShowListFullScreenActionSheetActionItemModel_Sort(ShowListFullScreenActionSheetCommand, vm.QueryOrderBySettingCollection.FirstOrDefault(t => t.IsSelected));
                list.Add(ShowListFullScreenActionSheetActionItemModel);
                return new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = true, ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position)) };
            }

            protected override void OnShowListFullScreenActionSheetCommand()
            {
                var actionItems = new List<Framework.Xaml.ActionForm.ActionItemModel>();

                var vm = DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahApplication.IndexVM>();
                vm.PostOnSortCommand = UpdateShowListFullScreenActionSheetActionItemModel;

                actionItems.Add(vm.GetToggleGroupedResultsViewSearchActionItemModel());

                actionItems.AddRange(GetSortActionItems(vm.QueryOrderBySettingCollection, vm.SortCommand));

                actionItems.Add(PopupVM.CloseActionItemModel);

                PopupVM.ShowVerticalActionSheet(new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = true, ActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(actionItems) });
            }

            private void UpdateShowListFullScreenActionSheetActionItemModel()
            {
                var vm = DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahApplication.IndexVM>();
                UpdateShowListFullScreenActionSheetActionItemModel(vm.QueryOrderBySettingCollection);
            }

            /*
            private Framework.Xaml.ActionForm.ActionSheetVM m_ListFloatingActionSheet;
            public Framework.Xaml.ActionForm.ActionSheetVM ListFloatingActionSheet
            {
                get { return m_ListFloatingActionSheet; }
                set
                {
                    Set(nameof(ListFloatingActionSheet), ref m_ListFloatingActionSheet, value);
                }
            }

            protected Framework.Xaml.ActionForm.ActionSheetVM GetListFloatingActionSheet()
            {
                List<Framework.Xaml.ActionForm.ActionItemModel> list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                //list.Add(GetActionItemModel_LaunchCommonSearchView());
                list.Add(GetActionItemModel(ItemVM.DefaultItem, Framework.Xaml.StandardRouteRelativeKey.Create, Framework.Xaml.ControlParentOptions.InPopup));
                return new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = false, ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position)) };
            }
            */

            protected Framework.Xaml.ActionForm.ActionSheetVM GetSearchFormActionSheet()
            {
                List<Framework.Xaml.ActionForm.ActionItemModel> startList = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                var vm = DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahApplication.IndexVM>();
                startList.Add(vm.GetSearchActionItemModel());
                List<Framework.Xaml.ActionForm.ActionItemModel> endList = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                endList.Add(PopupVM.GetHideRightSidePopupActionItemModel());
                return new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = false,
                    StartActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(startList.OrderBy(t => t.Position))
                    , EndActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(endList.OrderBy(t => t.Position))
                };
            }

            // 03.01. CommonSearchView -> Elmah.XamarinForms.Pages.ElmahApplication.CommonSearchView
            //TODO: change listItemViewMode will display different menus
            //TODO: #1: clear selection button and done button, IsSelectionList=true and IsRegularList=false when SingleSelection and MultipleSelection
            //TODO: #2: orderby list, IsSelectionList=false and IsRegularList=true when NavigationWhenClickItem and NavigationWhenRightArrow
/*
            public Framework.Xaml.ActionForm.ActionItemModel GetActionItemModel_LaunchCommonSearchView(
                long oneCondition // can be more
                , Framework.Xaml.ListItemViewModes listItemViewMode
                , bool? bindToGroupedResults
                , string orderByPropertyName
                , Framework.Queries.QueryOrderDirections? orderByDirection)
            {
                var actionItem = new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.NavigationItem, //CommandItem, open CommonSearchView
                    Title = Elmah.Resx.UIStringResourcePerApp.ElmahApplication, // some localized text here, e.g. Framework.Resx.UIStringResource. or NTierOnTime.Resx.UIStringResourcePerApp, or NTierOnTime.Resx.UIStringResourcePerEntity
                    FontIconSettings = new Framework.Xaml.FontIconSettings
                    {
                        MasterFontIcon = Framework.Xaml.FontAwesomeIcons. // Search, open CommonSearchView
                        , MasterFontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString()
                    },
                    NavigationCommand = NavigationVM.NavigationCommand,
                    NavigationCommandParam = GetNavigateToCommandParam_CommonSearchView(oneCondition, listItemViewMode, bindToGroupedResults, orderByPropertyName, orderByDirection)
                    // Command = popupVM.Command_ShowRightSidePopup, for open CommonSearchView
                };

                return actionItem;
            }

            public Framework.Xaml.ActionForm.ActionParameter GetNavigateToCommandParam_CommonSearchView(
                long oneCondition // can be more
                , Framework.Xaml.ListItemViewModes listItemViewMode
                , bool? bindToGroupedResults
                , string orderByPropertyName
                , Framework.Queries.QueryOrderDirections? orderByDirection)
            {
                return new Framework.Xaml.ActionForm.ActionParameter {
                     Domain = DomainKey
                    , Page = Framework.Xaml.StandardRouteRelativeKey.CommonSearchView.ToString()
                    , SendMessage = () => {
                        SendMessage_Init_CommonSearchView(oneCondition, listItemViewMode, bindToGroupedResults, orderByPropertyName, orderByDirection);
                    }
                };
            }

            /// <summary>
            /// this method can be called in other view models or anywhere to initialize CommonSearchView if referenced in other places.
            /// </summary>
            public void SendMessage_Init_CommonSearchView(
                long oneCondition // can be more
                , Framework.Xaml.ListItemViewModes listItemViewMode
                , bool? bindToGroupedResults
                , string orderByPropertyName
                , Framework.Queries.QueryOrderDirections? orderByDirection)
            {
                var vm = DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahApplication.IndexVM>();
                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.IndexVM, Framework.Xaml.LoadListDataRequest>(vm, Elmah.MVVMLightViewModels.ElmahApplication.IndexVM.MessageTitle_LoadData,
                    new Framework.Xaml.LoadListDataRequest
                    {
                        ListItemViewMode = listItemViewMode
                        , BindToGroupedResults = bindToGroupedResults
                        , OrderByPropertyName = orderByPropertyName
                        , OrderByDirection = orderByDirection
                        , Parameters = new Dictionary<string, object> {
                                    { nameof(Elmah.DataSourceEntities.ElmahApplication.oneCondition), oneCondition },
                                    // { nameof(Elmah.DataSourceEntities.ElmahApplication.BusinessEntityID), businessEntityID }, // can be more
                        }
                        , ActionWhenLaunch = () => { DefaultItem(oneCondition); ListFooterActionSheet = GetListFooterActionSheet(vm); }
                    });
            }
*/

            // 03.02. CommonResultView -> Elmah.XamarinForms.Pages.ElmahApplication.CommonResultView
            //TODO: change listItemViewMode will display different menus
            //TODO: #1: clear selection button and done button, IsSelectionList=true and IsRegularList=false when SingleSelection and MultipleSelection
            //TODO: #2: orderby list, IsSelectionList=false and IsRegularList=true when NavigationWhenClickItem and NavigationWhenRightArrow

            public Framework.Xaml.ActionForm.ActionItemModel GetActionItemModel_LaunchCommonResultView(
                long oneCondition // can be more
                , Framework.Xaml.ListItemViewModes listItemViewMode
                , bool? bindToGroupedResults
                , string orderByPropertyName
                , Framework.Queries.QueryOrderDirections? orderByDirection)
            {
                var actionItem = new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.NavigationItem, //CommandItem, open CommonSearchView
                    Title = Elmah.Resx.UIStringResourcePerApp.ElmahApplication, // some localized text here, e.g. Framework.Resx.UIStringResource. or NTierOnTime.Resx.UIStringResourcePerApp, or NTierOnTime.Resx.UIStringResourcePerEntity
                    FontIconSettings = new Framework.Xaml.FontIconSettings
                    {
                        MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Question // Search, open CommonSearchView
                        , MasterFontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString()
                    },
                    NavigationCommand = NavigationVM.NavigationCommand,
                    NavigationCommandParam = GetNavigateToCommandParam_CommonResultView(oneCondition, listItemViewMode, bindToGroupedResults, orderByPropertyName, orderByDirection)
                    // Command = popupVM.Command_ShowRightSidePopup, for open CommonSearchView
                };

                return actionItem;
            }

            public Framework.Xaml.ActionForm.ActionParameter GetNavigateToCommandParam_CommonResultView(
                long oneCondition // can be more
                , Framework.Xaml.ListItemViewModes listItemViewMode
                , bool? bindToGroupedResults
                , string orderByPropertyName
                , Framework.Queries.QueryOrderDirections? orderByDirection)
            {
                return new Framework.Xaml.ActionForm.ActionParameter {
                     Domain = DomainKey
                    , Page = Framework.Xaml.StandardRouteRelativeKey.CommonResultView.ToString()
                    , SendMessage = () => {
                        SendMessage_Init_CommonResultView(oneCondition, listItemViewMode, bindToGroupedResults, orderByPropertyName, orderByDirection);
                    }
                };
            }

            /// <summary>
            /// this method can be called in other view models or anywhere to initialize CommonResultView if referenced in other places.
            /// </summary>
            public void SendMessage_Init_CommonResultView(
                long oneCondition // can be more
                , Framework.Xaml.ListItemViewModes listItemViewMode
                , bool? bindToGroupedResults
                , string orderByPropertyName
                , Framework.Queries.QueryOrderDirections? orderByDirection)
            {
                var vm = DependencyService.Resolve<Elmah.MVVMLightViewModels.ElmahApplication.IndexVM>();
                MessagingCenter.Send<Elmah.MVVMLightViewModels.ElmahApplication.IndexVM, Framework.Xaml.LoadListDataRequest>(vm, Elmah.MVVMLightViewModels.ElmahApplication.IndexVM.MessageTitle_LoadData,
                    new Framework.Xaml.LoadListDataRequest
                    {
                        ListItemViewMode = listItemViewMode
                        , BindToGroupedResults = bindToGroupedResults
                        , OrderByPropertyName = orderByPropertyName
                        , OrderByDirection = orderByDirection
                        , Parameters = new Dictionary<string, object> {
                                    // { nameof(Elmah.DataSourceEntities.ElmahApplication.oneCondition), oneCondition },
                                    // { nameof(Elmah.DataSourceEntities.ElmahApplication.BusinessEntityID), businessEntityID }, // can be more
                        }
                        , ActionWhenLaunch = () => { DefaultItem(oneCondition); ListFooterActionSheet = GetListFooterActionSheet(vm); }
                    });
            }

            // 4. TabFullDetails/FullDetails/FunctionDashboard

            // 5. MasterViewAsideKeyInformation

            // 6. Custom Command and ActionItemViewModel

            // 7. override methods
            /*
            protected override Framework.Xaml.ActionForm.ActionItemModel GetSwitchToEditActionItemModel(Elmah.MVVMLightViewModels.ElmahApplication.ItemVM itemVM)
            {
                var retval = base.GetSwitchToEditActionItemModel(itemVM);

                retval.Command = ShowPopupCommand_Edit;
                retval.CommandParameter = itemVM.Item;

                return retval;
            }
            */
        }
    }
}

