using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class NavigationVMEntityContainer<TItem> : _ViewModelBase
        where TItem : Framework.Models.PropertyChangedNotifier, new()
    {
        static PopupVM popupVM = DependencyService.Resolve<PopupVM>(DependencyFetchTarget.GlobalInstance);

        public ICommand ShowPopupCommand_Create { get; private set; }
        public virtual void OnShowPopupCommand_Create(TItem item) { throw new NotImplementedException(); }
        public ICommand ShowPopupCommand_Delete { get; private set; }
        public virtual void OnShowPopupCommand_Delete(TItem item) { throw new NotImplementedException(); }
        public ICommand ShowPopupCommand_Details { get; private set; }
        public virtual void OnShowPopupCommand_Details(TItem item) { throw new NotImplementedException(); }
        public ICommand ShowPopupCommand_Edit { get; private set; }
        public virtual void OnShowPopupCommand_Edit(TItem item) { throw new NotImplementedException(); }
        public ICommand ShowPopupCommand_Copy { get; private set; }
        public void OnShowPopupCommand_Copy(TItem item) { throw new NotImplementedException(); }

        private Framework.Xaml.ActionForm.ActionSheetVM m_EditFormActionSheet;
        public Framework.Xaml.ActionForm.ActionSheetVM EditFormActionSheet
        {
            get { return m_EditFormActionSheet; }
            set
            {
                Set(nameof(EditFormActionSheet), ref m_EditFormActionSheet, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionSheetVM m_CreateNewFormActionSheet;
        public Framework.Xaml.ActionForm.ActionSheetVM CreateNewFormActionSheet
        {
            get { return m_CreateNewFormActionSheet; }
            set
            {
                Set(nameof(CreateNewFormActionSheet), ref m_CreateNewFormActionSheet, value);
            }
        }

        //private Framework.Xaml.ActionForm.ActionSheetVM m_DeleteFormActionSheet;
        //public Framework.Xaml.ActionForm.ActionSheetVM DeleteFormActionSheet
        //{
        //    get { return m_DeleteFormActionSheet; }
        //    set
        //    {
        //        Set(nameof(DeleteFormActionSheet), ref m_DeleteFormActionSheet, value);
        //    }
        //}

        private Framework.Xaml.ActionForm.ActionSheetVM m_DetailsFormActionSheet;
        public Framework.Xaml.ActionForm.ActionSheetVM DetailsFormActionSheet
        {
            get { return m_DetailsFormActionSheet; }
            set
            {
                Set(nameof(DetailsFormActionSheet), ref m_DetailsFormActionSheet, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionSheetVM m_DetailsFormActionSheetWhenReadOnly;
        public Framework.Xaml.ActionForm.ActionSheetVM DetailsFormActionSheetWhenReadOnly
        {
            get { return m_DetailsFormActionSheetWhenReadOnly; }
            set
            {
                Set(nameof(DetailsFormActionSheetWhenReadOnly), ref m_DetailsFormActionSheetWhenReadOnly, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionSheetVM m_DetailsFormActionSheetWhenEdit;
        public Framework.Xaml.ActionForm.ActionSheetVM DetailsFormActionSheetWhenEdit
        {
            get { return m_DetailsFormActionSheetWhenEdit; }
            set
            {
                Set(nameof(DetailsFormActionSheetWhenEdit), ref m_DetailsFormActionSheetWhenEdit, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionSheetVM m_DetailsFormActionSheetWhenCreate;
        public Framework.Xaml.ActionForm.ActionSheetVM DetailsFormActionSheetWhenCreate
        {
            get { return m_DetailsFormActionSheetWhenCreate; }
            set
            {
                Set(nameof(DetailsFormActionSheetWhenCreate), ref m_DetailsFormActionSheetWhenCreate, value);
            }
        }

        public ICommand ShowListFullScreenActionSheetCommand => new Command(OnShowListFullScreenActionSheetCommand);

        protected virtual void OnShowListFullScreenActionSheetCommand()
        {
            throw new NotImplementedException("OnShowListFullScreenActionSheetCommand");
        }

        private Framework.Xaml.ActionForm.ActionItemModel m_ShowListFullScreenActionSheetActionItemModel;
        /// <summary>
        /// Need this property, because Post-Sort should update icons of this ActinItemModel
        /// </summary>
        public Framework.Xaml.ActionForm.ActionItemModel ShowListFullScreenActionSheetActionItemModel
        {
            get { return m_ShowListFullScreenActionSheetActionItemModel; }
            set
            {
                Set(nameof(ShowListFullScreenActionSheetActionItemModel), ref m_ShowListFullScreenActionSheetActionItemModel, value);
            }
        }

        #region 2. IndexVM related Command and ActionSheetVM on UI

        private Framework.Xaml.ActionForm.ActionSheetVM m_ListFooterActionSheet;
        public Framework.Xaml.ActionForm.ActionSheetVM ListFooterActionSheet
        {
            get { return m_ListFooterActionSheet; }
            set
            {
                Set(nameof(ListFooterActionSheet), ref m_ListFooterActionSheet, value);
            }
        }

        private Framework.Xaml.ActionForm.ActionSheetVM m_SearchFormActionSheet;
        public Framework.Xaml.ActionForm.ActionSheetVM SearchFormActionSheet
        {
            get { return m_SearchFormActionSheet; }
            set
            {
                Set(nameof(SearchFormActionSheet), ref m_SearchFormActionSheet, value);
            }
        }

        #endregion 2. IndexVM related Command and ActionSheetVM on UI

        public NavigationVMEntityContainer()
        {
            // 1.1. Create
            ShowPopupCommand_Create = new Command<TItem>(OnShowPopupCommand_Create);

            // 1.2. Delete
            ShowPopupCommand_Delete = new Command<TItem>(OnShowPopupCommand_Delete);

            // 1.3. Details
            ShowPopupCommand_Details = new Command<TItem>(OnShowPopupCommand_Details);

            // 1.4. Edit
            ShowPopupCommand_Edit = new Command<TItem>(OnShowPopupCommand_Edit);

            // 1.5. Copy
            ShowPopupCommand_Copy = new Command<TItem>(OnShowPopupCommand_Copy);
        }

        protected static List<Framework.Xaml.ActionForm.ActionItemModel> GetSortActionItems(Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection, ICommand sortCommand)
        {
            var sortActionItems = new List<Framework.Xaml.ActionForm.ActionItemModel>();

            foreach (var queryOrderBySetting in queryOrderBySettingCollection)
            {
                var sortActionItemModel = new Framework.Xaml.ActionForm.SortActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.SortCommandItem, //SortCommandItem,
                    Title = queryOrderBySetting.DisplayName, // some localized text here, e.g. Framework.Resx.UIStringResource. or NTierOnTime.Resx.UIStringResourcePerApp, or NTierOnTime.Resx.UIStringResourcePerEntity
                    FontIconSettings = new Framework.Xaml.FontIconSettings
                    {
                        MasterFontIcon = queryOrderBySetting.FontIcon // Search, open CommonSearchView
    ,
                        MasterFontIconFamily = queryOrderBySetting.FontIconFamily
                    },
                    QueryOrderBySetting = queryOrderBySetting,
                    Command = sortCommand,
                };

                sortActionItems.Add(sortActionItemModel);
            }

            return sortActionItems;
        }

        protected virtual void UpdateShowListFullScreenActionSheetActionItemModel(Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection)
        {
            var selectedQueryOrderBySetting = queryOrderBySettingCollection?.FirstOrDefault(t => t.IsSelected);

            ShowListFullScreenActionSheetActionItemModel.Title = selectedQueryOrderBySetting.DisplayName;
            ShowListFullScreenActionSheetActionItemModel.FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = selectedQueryOrderBySetting.FontIcon, MasterFontIconFamily = selectedQueryOrderBySetting.FontIconFamily.ToString() };
            if (ShowListFullScreenActionSheetActionItemModel is Framework.Xaml.ActionForm.SortActionItemModel)
            {
                ((Framework.Xaml.ActionForm.SortActionItemModel)ShowListFullScreenActionSheetActionItemModel).QueryOrderBySetting = selectedQueryOrderBySetting;
            }
        }

        protected static Framework.Xaml.ActionForm.ActionItemModel GetShowListFullScreenActionSheetActionItemModel(ICommand command)
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.More,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.EllipsisV, MasterFontIconFamily = Framework.Xaml.IconFontFamily.FontAwesomeSolid.ToString() },
                Command = command,
                Position = 10,
            };
        }

        protected static Framework.Xaml.ActionForm.SortActionItemModel GetShowListFullScreenActionSheetActionItemModel_Sort(ICommand command, Framework.Queries.QueryOrderBySetting selectedQueryOrderBySetting)
        {
            return new Framework.Xaml.ActionForm.SortActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.SortCommandItem,
                Title = selectedQueryOrderBySetting.DisplayName,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = selectedQueryOrderBySetting.FontIcon, MasterFontIconFamily = selectedQueryOrderBySetting.FontIconFamily.ToString() },
                QueryOrderBySetting = selectedQueryOrderBySetting,
                Command = command,
                Position = 10,
            };
        }

        public virtual Framework.Xaml.ActionForm.ActionItemModel GetActionItemModel(TItem item, Framework.Xaml.StandardRouteRelativeKey routeRelativeKey, Framework.Xaml.ControlParentOptions controlParentOption)
        {
            if (false)
            { }
            #region 1. Create/Delete/Details/Edit/Copy UIViews
            // 1.1. Create

            else if (routeRelativeKey == Framework.Xaml.StandardRouteRelativeKey.Create)
            {
                //var command = controlParentOption == Framework.Xaml.ControlParentOptions.InPage ? NavigateToCommand_Create : ShowPopupCommand_Create;
                var command = ShowPopupCommand_Create;
                return new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                    Title = Framework.Resx.UIStringResource.AddNew,
                    FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Plus, MasterFontIconFamily = "FontAwesomeSolid" },
                    Command = command,
                    CommandParameter = item
                };
            }

            // 1.2. Delete
            /*
            else if (routeRelativeKey == Framework.Xaml.StandardRouteRelativeKey.Delete)
            {
                  var command = controlParentOption == Framework.Xaml.ControlParentOptions.InPage ? NavigateToCommand_Delete : ShowPopupCommand_Delete;
                return new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                    Title = Framework.Resx.UIStringResource.Delete,
                    FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Trash, MasterFontIconFamily = "FontAwesomeSolid" },
                    Command = command,
                    CommandParameter = item
                };
            }
            */

            // 1.3. Details

            else if (routeRelativeKey == Framework.Xaml.StandardRouteRelativeKey.Details)
            {
                //var command = controlParentOption == Framework.Xaml.ControlParentOptions.InPage ? NavigateToCommand_Details : ShowPopupCommand_Details;
                var command = ShowPopupCommand_Details;
                return new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                    Title = Framework.Resx.UIStringResource.Details,
                    FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Briefcase, MasterFontIconFamily = "FontAwesomeSolid" },
                    Command = command,
                    CommandParameter = item
                };
            }

            // 1.4. Edit

            else if (routeRelativeKey == Framework.Xaml.StandardRouteRelativeKey.Edit)
            {
                //var command = controlParentOption == Framework.Xaml.ControlParentOptions.InPage ? NavigateToCommand_Edit : ShowPopupCommand_Edit;
                var command = ShowPopupCommand_Edit;
                return new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                    Title = Framework.Resx.UIStringResource.Edit,
                    FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Edit, MasterFontIconFamily = "FontAwesomeSolid" },
                    Command = command,
                    CommandParameter = item
                };
            }

            // 1.5. Copy
            /*
            else if (routeRelativeKey == Framework.Xaml.StandardRouteRelativeKey.Copy)
            {
                  var command = controlParentOption == Framework.Xaml.ControlParentOptions.InPage ? NavigateToCommand_Copy : ShowPopupCommand_Copy;
                return new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                    Title = Framework.Resx.UIStringResource.Copy,
                    FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Copy, MasterFontIconFamily = "FontAwesomeSolid" },
                    Command = command,
                    CommandParameter = item
                };
            }
            */

            #endregion 1. Create/Delete/Details/Edit/Copy UIViews

            //    #region 2. SingleItemForm/SingleItemAggregateForm

            //    #endregion 2. SingleItemForm/SingleItemAggregateForm

            //    #region 3. TabSearchResult/Results/Search/SearchResult

            // 03.01. CommonSearchView -> NTierOnTime.XamarinForms.Pages.Course.CommonSearchView
            /*
            else if (routeRelativeKey == Framework.Xaml.StandardRouteRelativeKey.CommonSearchView)
            {
                return new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                    Title = Framework.Resx.UIStringResource.Search,
                    FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Search, MasterFontIconFamily = "FontAwesomeSolid" },
                    NavigationCommand = NavigationCommand,
                    NavigationCommandParam = NavigateToCommandParam_Course_CommonSearchView
                };
            }
            */

            // 03.02. CommonResultView -> NTierOnTime.XamarinForms.Pages.Course.CommonResultView
            /*
            else if (routeRelativeKey == Framework.Xaml.StandardRouteRelativeKey.CommonResultView)
            {
                return new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                    Title = Framework.Resx.UIStringResource.Results,
                    FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.List, MasterFontIconFamily = "FontAwesomeSolid" },
                    NavigationCommand = NavigationCommand,
                    NavigationCommandParam = NavigateToCommandParam_Course_CommonResultView
                };
            }
            */

            //    #endregion 3. TabSearchResult/Results/Search/SearchResult

            //    #region 4. TabFullDetails/FullDetails

            // 04.01. TabDashboard -> NTierOnTime.XamarinForms.Pages.Course.TabDashboard
            /* TODO: not finished
            else if (routeRelativeKey == Framework.Xaml.StandardRouteRelativeKey.TabDashboard)
            {
                  var command = controlParentOption == Framework.Xaml.ControlParentOptions.InPage ? NavigateToCommand_TabDashboard : ShowPopupCommand_TabDashboard;
                return new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                    Title = Framework.Resx.UIStringResource.Copy,
                    FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.QuestionCircle, MasterFontIconFamily = "FontAwesomeSolid" },
                    Command = NavigateToCommand_command,
                    CommandParameter = item
                };
            }
            */

            //    #endregion 4. TabFullDetails/FullDetails

            //    #region 5. MasterViewAsideKeyInformation

            // 05.01. DashboardMasterView -> NTierOnTime.XamarinForms.Pages.Course.DashboardMasterView
            /* TODO: not finished
            else if (routeRelativeKey == Framework.Xaml.StandardRouteRelativeKey.DashboardMasterView)
            {
                  var command = controlParentOption == Framework.Xaml.ControlParentOptions.InPage ? NavigateToCommand_DashboardMasterView : ShowPopupCommand_DashboardMasterView;
                return new Framework.Xaml.ActionForm.ActionItemModel
                {
                    ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                    Title = Framework.Resx.UIStringResource.Copy,
                    FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.QuestionCircle, MasterFontIconFamily = "FontAwesomeSolid" },
                    Command = NavigateToCommand_command,
                    CommandParameter = item
                };
            }
            */

            //    #endregion 5. MasterViewAsideKeyInformation

            return null;
        }


        // 1. Create

        protected static LoadItemDataRequest<T> GetLoadDataRequest_NavigateToCommand_Create<T>(T item)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = item
                ,
                UIAction = Framework.ViewModels.UIAction.Create
                ,
                ShowLoadingPopup = false
                ,
                ShowSaveFailedPopup = true
                ,
                ShowSavingPopup = true
                ,
                ShowSuccessfullySavedPopup = true
                ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPage
                ,
                LoadDropDownContents = true
                ,
                SetDropDownSelectedItems = true
                ,
                LoadExtraDataIfNeeded = true
                ,
                Refresh = false
            };
        }

        protected static LoadItemDataRequest<T> GetLoadDataRequest_ShowPopupCommand_Create<T>(T item)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = item
                ,
                UIAction = Framework.ViewModels.UIAction.Create
                ,
                ShowLoadingPopup = false
                ,
                ShowSaveFailedPopup = true
                ,
                ShowSavingPopup = true
                ,
                ShowSuccessfullySavedPopup = true
                ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPopup
                ,
                LoadDropDownContents = true
                ,
                SetDropDownSelectedItems = true
                ,
                LoadExtraDataIfNeeded = true
                ,
                IsContentEnable = true
                ,
                Refresh = false
            };
        }

        // 2. Delete

        protected static LoadItemDataRequest<T> GetLoadDataRequest_NavigateToCommand_Delete<T>(T criteria)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = criteria
                ,
                UIAction = Framework.ViewModels.UIAction.Delete
                ,
                ShowLoadingPopup = false
                ,
                ShowSaveFailedPopup = true
                ,
                ShowSavingPopup = true
                ,
                ShowSuccessfullySavedPopup = true
                ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPage
                ,
                LoadDropDownContents = true
                ,
                SetDropDownSelectedItems = true
                ,
                LoadExtraDataIfNeeded = true
                ,
                Refresh = false
            };
        }

        protected static LoadItemDataRequest<T> GetLoadDataRequest_ShowPopupCommand_Delete<T>(T criteria)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = criteria
                                ,
                UIAction = Framework.ViewModels.UIAction.Delete
                                ,
                ShowLoadingPopup = false
                                ,
                ShowSaveFailedPopup = true
                                ,
                ShowSavingPopup = true
                                ,
                ShowSuccessfullySavedPopup = true
                                ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPopup
                                ,
                LoadDropDownContents = true
                                ,
                SetDropDownSelectedItems = true
                                ,
                LoadExtraDataIfNeeded = true
                                ,
                Refresh = false
            };
        }

        // 3. Details
        protected static LoadItemDataRequest<T> GetLoadDataRequest_NavigateToCommand_Details<T>(T criteria)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = criteria
                                ,
                UIAction = Framework.ViewModels.UIAction.ViewDetails
                                ,
                ShowLoadingPopup = false
                                ,
                ShowSaveFailedPopup = true
                                ,
                ShowSavingPopup = true
                                ,
                ShowSuccessfullySavedPopup = true
                                ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPage
                                ,
                IsContentEnable = false
                                ,
                LoadDropDownContents = true
                                ,
                SetDropDownSelectedItems = true
                                ,
                LoadExtraDataIfNeeded = true
                                ,
                Refresh = false
            };
        }

        protected static LoadItemDataRequest<T> GetLoadDataRequest_ShowPopupCommand_Details<T>(T criteria)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = criteria
                ,
                UIAction = Framework.ViewModels.UIAction.ViewDetails
                ,
                ShowLoadingPopup = false
                ,
                ShowSaveFailedPopup = true
                ,
                ShowSavingPopup = true
                ,
                ShowSuccessfullySavedPopup = true
                ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPopup
                ,
                IsContentEnable = false
                ,
                LoadDropDownContents = true
                ,
                SetDropDownSelectedItems = true
                ,
                LoadExtraDataIfNeeded = true
                ,
                Refresh = false
            };
        }

        // 4. Edit

        protected static LoadItemDataRequest<T> GetLoadDataRequest_NavigateToCommand_Edit<T>(T criteria)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = criteria
                ,
                UIAction = Framework.ViewModels.UIAction.Update
                ,
                ShowLoadingPopup = false
                ,
                ShowSaveFailedPopup = true
                ,
                ShowSavingPopup = true
                ,
                ShowSuccessfullySavedPopup = true
                ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPage
                ,
                LoadDropDownContents = true
                ,
                SetDropDownSelectedItems = true
                ,
                LoadExtraDataIfNeeded = true
                ,
                Refresh = false
            };
        }

        protected static LoadItemDataRequest<T> GetLoadDataRequest_ShowPopupCommand_Edit<T>(T criteria)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = criteria
                    ,
                UIAction = Framework.ViewModels.UIAction.Update
                    ,
                ShowLoadingPopup = false
                    ,
                ShowSaveFailedPopup = true
                    ,
                ShowSavingPopup = true
                    ,
                ShowSuccessfullySavedPopup = true
                    ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPopup
                    ,
                LoadDropDownContents = true
                    ,
                SetDropDownSelectedItems = true
                    ,
                LoadExtraDataIfNeeded = true
                    ,
                IsContentEnable = true
                    ,
                Refresh = false
            };
        }

        // 5. Copy

        protected static LoadItemDataRequest<T> GetLoadDataRequest_NavigateToCommand_Copy<T>(T criteria)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = criteria
                ,
                UIAction = Framework.ViewModels.UIAction.Copy
                    ,
                ShowLoadingPopup = false
                    ,
                ShowSaveFailedPopup = true
                    ,
                ShowSavingPopup = true
                    ,
                ShowSuccessfullySavedPopup = true
                    ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPage
                    ,
                LoadDropDownContents = true
                    ,
                SetDropDownSelectedItems = true
                    ,
                LoadExtraDataIfNeeded = true
                    ,
                Refresh = false
            };
        }

        protected static LoadItemDataRequest<T> GetLoadDataRequest_ShowPopupCommand_Copy<T>(T criteria)
        {
            return new Framework.Xaml.LoadItemDataRequest<T>
            {
                Criteria = criteria
                    ,
                UIAction = Framework.ViewModels.UIAction.Copy
                    ,
                ShowLoadingPopup = false
                    ,
                ShowSaveFailedPopup = true
                    ,
                ShowSavingPopup = true
                    ,
                ShowSuccessfullySavedPopup = true
                    ,
                ControlParentOption = Framework.Xaml.ControlParentOptions.InPopup
                    ,
                LoadDropDownContents = true
                    ,
                SetDropDownSelectedItems = true
                    ,
                LoadExtraDataIfNeeded = true
                    ,
                Refresh = false
            };
        }

    }
}

