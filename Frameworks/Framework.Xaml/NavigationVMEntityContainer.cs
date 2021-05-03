using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class NavigationVMEntityContainer<TItemVM, TSearchCriteria, TIdentifierContract, TItem> : NavigationVMEntityContainer
        where TItemVM : ViewModelItemBase<TSearchCriteria, TIdentifierContract, TItem>
        where TSearchCriteria : class, TIdentifierContract, Framework.Models.IClone<TIdentifierContract>, new()
        where TItem : Framework.Models.PropertyChangedNotifier, TIdentifierContract, new()
    {
        #region 1. ItemVM related Command and ActionSheetVM on UI

        protected TItemVM ItemVM
        {
            get
            {
                return DependencyService.Resolve<TItemVM>();
            }
        }

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

        #endregion 1. ItemVM related Command and ActionSheetVM on UI

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

            //EditFormActionSheet = GetEditFormActionSheet(ItemVM);
            //CreateNewFormActionSheet = GetCreateNewFormActionSheet(ItemVM);
            ////DeleteFormActionSheet = GetDeleteFormActionSheet(ItemVM);
            //DetailsFormActionSheet = GetDetailsFormActionSheet(ItemVM);

            //DetailsFormActionSheetWhenReadOnly = GetDetailsFormActionSheetWhenReadOnly(ItemVM);
            //DetailsFormActionSheetWhenEdit = GetDetailsFormActionSheetWhenEdit(ItemVM);
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

        protected Framework.Xaml.ActionForm.ActionSheetVM GetEditFormActionSheet(TItemVM itemVM, IEnumerable<Framework.Xaml.ActionForm.ActionItemModel> additionals = null)
        {
            List<Framework.Xaml.ActionForm.ActionItemModel> list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            list.Add(GetSaveActionItemModel(itemVM));
            list.Add(GetRefreshActionItemModel(itemVM));
            //list.Add(popupVM.CloseItemControlPopupActionItemModel);
            if (additionals != null)
                list.AddRange(additionals);
            return new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = false, ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position)) };
        }

        protected virtual Framework.Xaml.ActionForm.ActionSheetVM GetCreateNewFormActionSheet(TItemVM itemVM, IEnumerable<Framework.Xaml.ActionForm.ActionItemModel> additionals = null)
        {
            List<Framework.Xaml.ActionForm.ActionItemModel> list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            list.Add(GetAddActionItemModel(itemVM));
            //list.Add(popupVM.CloseItemControlPopupActionItemModel);
            if (additionals != null)
                list.AddRange(additionals);
            return new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = false, ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position)) };
        }

        protected virtual Framework.Xaml.ActionForm.ActionSheetVM GetDetailsFormActionSheet(TItemVM itemVM, IEnumerable<Framework.Xaml.ActionForm.ActionItemModel> additionals = null)
        {
            List<Framework.Xaml.ActionForm.ActionItemModel> list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            //list.Add(GetSwitchToEditActionItemModel(itemVM));
            //list.Add(GetDeleteActionItemModel(itemVM));
            //list.Add(popupVM.GetCloseActionItemModel());
            if (additionals != null)
                list.AddRange(additionals);
            return new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = false, ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position)) };
        }

        protected virtual Framework.Xaml.ActionForm.ActionSheetVM GetDetailsFormActionSheetWhenReadOnly(TItemVM itemVM, IEnumerable<Framework.Xaml.ActionForm.ActionItemModel> additionals = null, bool isMultiplePartsMenu = false)
        {
            if (isMultiplePartsMenu)
            {
                List<Framework.Xaml.ActionForm.ActionItemModel> startList = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                List<Framework.Xaml.ActionForm.ActionItemModel> centerList = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                List<Framework.Xaml.ActionForm.ActionItemModel> endList = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                startList.Add(GetSwitchToEditActionItemModel(itemVM));
                centerList.Add(GetDeleteActionItemModel(itemVM));
                //endList.Add(popupVM.GetCloseActionItemModel());
                if (additionals != null)
                    centerList.AddRange(additionals);
                return new Framework.Xaml.ActionForm.ActionSheetVM
                {
                    HasIcon = false
                    ,
                    StartActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(startList.OrderBy(t => t.Position))
                    ,
                    CenterActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(centerList.OrderBy(t => t.Position))
                    ,
                    EndActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(endList.OrderBy(t => t.Position))
                };
            }

            // Single part to ActionItems property
            var list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            list.Add(GetSwitchToEditActionItemModel(itemVM));
            list.Add(GetDeleteActionItemModel(itemVM));
            //list.Add(popupVM.GetCloseActionItemModel());
            if (additionals != null)
                list.AddRange(additionals);
            return new Framework.Xaml.ActionForm.ActionSheetVM
            {
                HasIcon = false
                ,
                ActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position))
            };
        }

        protected virtual Framework.Xaml.ActionForm.ActionSheetVM GetDetailsFormActionSheetWhenEdit(TItemVM itemVM, IEnumerable<Framework.Xaml.ActionForm.ActionItemModel> additionals = null, bool isMultiplePartsMenu = false)
        {
            if (isMultiplePartsMenu)
            {
                List<Framework.Xaml.ActionForm.ActionItemModel> startList = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                List<Framework.Xaml.ActionForm.ActionItemModel> centerList = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                List<Framework.Xaml.ActionForm.ActionItemModel> endList = new List<Framework.Xaml.ActionForm.ActionItemModel>();

                startList.Add(GetSaveActionItemModel(itemVM));
                //endList.Add(GetSwitchToReadOnlyActionItemModel(itemVM));
                //endList.Add(popupVM.GetCloseActionItemModel());
                if (additionals != null)
                    centerList.AddRange(additionals);
                return new Framework.Xaml.ActionForm.ActionSheetVM
                {
                    HasIcon = false
                    ,
                    StartActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(startList.OrderBy(t => t.Position))
                    ,
                    CenterActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(centerList.OrderBy(t => t.Position))
                    ,
                    EndActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(endList.OrderBy(t => t.Position))
                };
            }

            var list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            list.Add(GetSaveActionItemModel(itemVM));
            //list.Add(GetSwitchToReadOnlyActionItemModel(itemVM));
            //list.Add(popupVM.GetCloseActionItemModel());
            if (additionals != null)
                list.AddRange(additionals);
            return new Framework.Xaml.ActionForm.ActionSheetVM
            {
                HasIcon = false
                ,
                ActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position))
            };
        }

        protected virtual Framework.Xaml.ActionForm.ActionSheetVM GetDetailsFormActionSheetWhenCreate(TItemVM itemVM, IEnumerable<Framework.Xaml.ActionForm.ActionItemModel> additionals = null, bool isMultiplePartsMenu = false)
        {
            if (isMultiplePartsMenu)
            {
                List<Framework.Xaml.ActionForm.ActionItemModel> startList = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                List<Framework.Xaml.ActionForm.ActionItemModel> centerList = new List<Framework.Xaml.ActionForm.ActionItemModel>();
                List<Framework.Xaml.ActionForm.ActionItemModel> endList = new List<Framework.Xaml.ActionForm.ActionItemModel>();

                startList.Add(GetSaveActionItemModel(itemVM));
                //endList.Add(GetSwitchToReadOnlyActionItemModel(itemVM));
                //endList.Add(popupVM.GetCloseActionItemModel());
                if (additionals != null)
                    centerList.AddRange(additionals);
                return new Framework.Xaml.ActionForm.ActionSheetVM
                {
                    HasIcon = false
                    ,
                    StartActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(startList.OrderBy(t => t.Position))
                    ,
                    CenterActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(centerList.OrderBy(t => t.Position))
                    ,
                    EndActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(endList.OrderBy(t => t.Position))
                };
            }

            var list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            list.Add(GetSaveActionItemModel(itemVM));
            //endList.Add(GetSwitchToReadOnlyActionItemModel(itemVM));
            //endList.Add(popupVM.GetCloseActionItemModel());
            if (additionals != null)
                list.AddRange(additionals);
            return new Framework.Xaml.ActionForm.ActionSheetVM
            {
                HasIcon = false
                ,
                ActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position))
            };
        }

        //public virtual Framework.Xaml.ActionForm.ActionSheetVM GetDeleteFormActionSheet(TItemVM itemVM, IEnumerable<Framework.Xaml.ActionForm.ActionItemModel> additionals= null)
        //{
        //    List<Framework.Xaml.ActionForm.ActionItemModel> list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
        //    list.Add(GetDeleteActionItemModel(itemVM));
        //    list.Add(GetCloseActionItemModel(itemVM));
        //    if (additionals != null)
        //        list.AddRange(additionals);
        //    return new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = false, ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position)) };
        //}

        protected virtual Framework.Xaml.ActionForm.ActionSheetVM GetListActionSheet(TItemVM itemVM, IEnumerable<Framework.Xaml.ActionForm.ActionItemModel> additionals = null)
        {
            List<Framework.Xaml.ActionForm.ActionItemModel> list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            list.Add(GetAddActionItemModel(itemVM));
            //list.Add(popupVM.GetCloseActionItemModel());
            if (additionals != null)
                list.AddRange(additionals);
            return new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = false, ActionItems = new System.Collections.ObjectModel.ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list.OrderBy(t => t.Position)) };
        }

        #region Get...ActionItemModel(...)

        /// <summary>
        /// On Details Form Header when ReadOnly
        /// </summary>
        /// <param name="itemVM"></param>
        /// <returns></returns>
        protected virtual Framework.Xaml.ActionForm.ActionItemModel GetSwitchToEditActionItemModel(TItemVM itemVM)
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Edit,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Edit, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = itemVM.ToggleContentCommand,
                CommandParameter = true,
                Position = 10,
            };
        }

        /// <summary>
        /// On Details Form Header when Edit
        /// </summary>
        /// <param name="itemVM"></param>
        /// <returns></returns>
        protected Framework.Xaml.ActionForm.ActionItemModel GetSwitchToReadOnlyActionItemModel(TItemVM itemVM)
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Cancel,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.TimesCircle, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = itemVM.ToggleContentCommand,
                CommandParameter = false,
                Position = 10,
            };
        }

        /// <summary>
        /// On Edit Form
        /// </summary>
        /// <param name="itemVM"></param>
        /// <returns></returns>
        protected Framework.Xaml.ActionForm.ActionItemModel GetSaveActionItemModel(TItemVM itemVM)
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Save,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Save, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = itemVM.SaveCommand,
                Position = 10,
            };
        }

        /// <summary>
        /// On Add Form
        /// </summary>
        /// <param name="itemVM"></param>
        /// <returns></returns>
        protected Framework.Xaml.ActionForm.ActionItemModel GetAddActionItemModel(TItemVM itemVM)
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Save,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Save, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = itemVM.AddCommand,
                Position = 10,
            };
        }

        /// <summary>
        /// On Delete Form
        /// </summary>
        /// <param name="itemVM"></param>
        /// <returns></returns>
        protected Framework.Xaml.ActionForm.ActionItemModel GetDeleteActionItemModel(TItemVM itemVM)
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Delete,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Trash, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = itemVM.DeleteCommand,
                Position = 10,
            };
        }

        ///// <summary>
        ///// On All Forms
        ///// </summary>
        ///// <param name="itemVM"></param>
        ///// <returns></returns>
        //protected Framework.Xaml.ActionForm.ActionItemModel GetCloseActionItemModel(TItemVM itemVM)
        //{
        //    return new Framework.Xaml.ActionForm.ActionItemModel
        //    {
        //        ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
        //        Title = Framework.Resx.UIStringResource.Cancel,
        //        FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.TimesCircle, MasterFontIconFamily = "FontAwesomeSolid" },
        //        Command = popupVM.Command_Close,
        //        Position = 100,
        //    };
        //}

        /// <summary>
        /// On Edit/Add Forms
        /// </summary>
        /// <param name="itemVM"></param>
        /// <returns></returns>
        protected Framework.Xaml.ActionForm.ActionItemModel GetRefreshActionItemModel(TItemVM itemVM)
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Refresh,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Sync, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = itemVM.AddCommand,
                Position = 20,
            };
        }

        #endregion Get...ActionItemModel(...)

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
    public abstract class NavigationVMEntityContainer : Framework.Xaml._ViewModelBase
    {
        static PopupVM popupVM = DependencyService.Resolve<PopupVM>(DependencyFetchTarget.GlobalInstance);

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
    }
}
