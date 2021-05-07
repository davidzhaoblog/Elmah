using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class NavigationVMEntityContainer<TItemVM, TSearchCriteria, TIdentifierContract, TItem> : NavigationVMEntityContainer<TItem>
        where TItemVM : ViewModelItemBase<TSearchCriteria, TIdentifierContract, TItem>
        where TSearchCriteria : class, TIdentifierContract, Framework.Models.IClone<TIdentifierContract>, new()
        where TItem : Framework.Models.PropertyChangedNotifier, TIdentifierContract, new()
    {
        protected TItemVM ItemVM
        {
            get
            {
                return DependencyService.Resolve<TItemVM>();
            }
        }

        public NavigationVMEntityContainer(): base()
        {

            //EditFormActionSheet = GetEditFormActionSheet(ItemVM);
            //CreateNewFormActionSheet = GetCreateNewFormActionSheet(ItemVM);
            ////DeleteFormActionSheet = GetDeleteFormActionSheet(ItemVM);
            //DetailsFormActionSheet = GetDetailsFormActionSheet(ItemVM);

            //DetailsFormActionSheetWhenReadOnly = GetDetailsFormActionSheetWhenReadOnly(ItemVM);
            //DetailsFormActionSheetWhenEdit = GetDetailsFormActionSheetWhenEdit(ItemVM);
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
    }
}

