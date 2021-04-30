using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class ViewModelItemBase<TSearchCriteria, TIdentifierContract, TItem, TViewModelDataItemVM, TResponse, TSQLiteRepository, TSQLiteItemType>
        : ViewModelItemBase<TSearchCriteria, TIdentifierContract, TItem, TViewModelDataItemVM, TResponse>
        where TSearchCriteria : class, TIdentifierContract, Framework.Models.IClone<TIdentifierContract>, new()
        where TItem : Framework.Models.PropertyChangedNotifier, TIdentifierContract, new()
        where TViewModelDataItemVM : Framework.ViewModels.ViewModelItemBase<TSearchCriteria, TItem>, new()
        where TResponse : Framework.Services.BusinessLogicLayerResponseMessageBase<List<TItem>>, new()
        where TSQLiteRepository : class, Framework.Xaml.SQLite.ISQLiteTableItemCRUD<TSQLiteItemType, TItem, TIdentifierContract>
        where TSQLiteItemType : TItem, new()
    {
        public virtual TSQLiteRepository CacheInstance
        {
            get
            {
                return default(TSQLiteRepository);
            }
        }

        public override async Task Upsert(bool useExtendedVM = true)
        {
            try
            {
                if (useExtendedVM)
                {
                    GetValueFromDropDownListSelectedItems();
                }

                var result = await UpsertToServer();

                this.StatusOfResult = result.BusinessLogicLayerResponseStatus;
                this.StatusMessageOfResult = result.GetStatusMessage();

                if (result.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || result.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                {
                    await CacheInstance.Save(result.Message[0]);

                    RaisePropertyChanged("Item");

                    // TODO: you can change property name to show proper message on Popup
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Success, string.Format(Framework.Resx.UIStringResource.SuccessfullySaved, GetDisplayString(Item)));
                }
                else
                {
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                StatusMessageOfResult = ex.Message;
                SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
            }
        }

        public override async Task LoadItem(TIdentifierContract identifier, bool refresh = false, bool loadExtraDataIfNeeded = false)
        {
            var cacheItem = await CacheInstance.Get(identifier);

            if (refresh = false && cacheItem != null)
            {
                this.Item = cacheItem;
                return;
            }

            try
            {
                var result = await GetFromServer(identifier);

                this.StatusOfResult = result.StatusOfResult;
                this.StatusMessageOfResult = result.StatusMessageOfResult;
                if (result.StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || result.StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                {
                    this.Item = result.Item;

                    if (refresh)
                    {
                        if (result.Item != null)
                            await CacheInstance.Save(result.Item);
                    }

                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Launch);
                }
                else
                {
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                StatusMessageOfResult = ex.Message;
                SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
            }
        }

        protected override async Task Delete()
        {
            CurrentUIAction = Framework.ViewModels.UIAction.Delete;

            try
            {
                await DeleteFromServer();

                if (StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                {
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Success, string.Format(Framework.Resx.UIStringResource.SuccessfullySaved, GetDisplayString(Item)));

                    await CacheInstance.Delete(Item);

                    RunItemDeletedEventHandlers();
                    Item = new TItem();
                }
                else
                {
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                StatusMessageOfResult = ex.Message;
                SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
            }
        }
    }

    public abstract class ViewModelItemBase<TSearchCriteria, TIdentifierContract, TItem, TViewModelDataItemVM, TResponse>
        : ViewModelItemBase<TSearchCriteria, TIdentifierContract, TItem>
    where TSearchCriteria : class, TIdentifierContract, Framework.Models.IClone<TIdentifierContract>, new()
    where TItem : Framework.Models.PropertyChangedNotifier, TIdentifierContract, new()
    where TViewModelDataItemVM : Framework.ViewModels.ViewModelItemBase<TSearchCriteria, TItem>, new()
    where TResponse : Framework.Services.BusinessLogicLayerResponseMessageBase<List<TItem>>, new()
    {
        public override async Task Upsert(bool useExtendedVM = true)
        {
            try
            {
                if (useExtendedVM)
                {
                    GetValueFromDropDownListSelectedItems();
                }

                var result = await UpsertToServer();

                this.StatusOfResult = result.BusinessLogicLayerResponseStatus;
                this.StatusMessageOfResult = result.GetStatusMessage();

                if (result.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || result.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                {
                    RaisePropertyChanged("Item");

                    // TODO: you can change property name to show proper message on Popup
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Success, string.Format(Framework.Resx.UIStringResource.SuccessfullySaved, GetDisplayString(Item)));
                }
                else
                {
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                StatusMessageOfResult = ex.Message;
                SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
            }
        }

        protected virtual async Task<TResponse> UpsertToServer()
        {
            return await Task.FromResult(new TResponse());
        }

        public override async Task LoadItem(TIdentifierContract identifier, bool refresh = false, bool loadExtraDataIfNeeded = false)
        {
            try
            {
                var result = await GetFromServer(identifier);

                this.StatusOfResult = result.StatusOfResult;
                this.StatusMessageOfResult = result.StatusMessageOfResult;
                if (result.StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || result.StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                {
                    this.Item = result.Item;

                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Launch);
                }
                else
                {
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                StatusMessageOfResult = ex.Message;
                SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
            }
        }

        protected virtual async Task<TViewModelDataItemVM> GetFromServer(TIdentifierContract identifier)
        {
            return await Task.FromResult(new TViewModelDataItemVM());
        }

        protected override async Task Delete()
        {
            CurrentUIAction = Framework.ViewModels.UIAction.Delete;

            try
            {
                await DeleteFromServer();

                if (StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK || StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady)
                {
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Success, string.Format(Framework.Resx.UIStringResource.SuccessfullySaved, GetDisplayString(Item)));

                    RunItemDeletedEventHandlers();
                    Item = new TItem();
                }
                else
                {
                    SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                StatusMessageOfResult = ex.Message;
                SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus.Failed);
            }
        }

        protected virtual async Task DeleteFromServer()
        {
            await Task.FromException(new NotImplementedException());
        }
    }

    public abstract class ViewModelItemBase<TSearchCriteria, TIdentifierContract, TItem>
        : ViewModelBase2, Framework.ViewModels.IViewModelItemBase<TSearchCriteria, TItem>
        where TSearchCriteria : class, TIdentifierContract, Framework.Models.IClone<TIdentifierContract>, new()
        where TItem : Framework.Models.PropertyChangedNotifier, TIdentifierContract, new()
    {
        #region 1. properties

        protected TSearchCriteria m_Criteria;
        public TSearchCriteria Criteria
        {
            get { return m_Criteria; }
            set
            {
                m_Criteria = value;
                RaisePropertyChanged("Criteria");
            }
        }

        protected TItem m_Item;
        public TItem Item
        {
            get { return m_Item; }
            set
            {
                m_Item = value != null ? GetAClone(value) : new TItem();
                m_Item.PropertyChanged += Item_PropertyChanged;
                RaisePropertyChanged("Item");
            }
        }

        protected TItem m_DefaultItem;
        public TItem DefaultItem
        {
            get { return m_DefaultItem; }
            set
            {
                Set(nameof(DefaultItem), ref m_DefaultItem, value);
            }
        }

        private Framework.ViewModels.UIAction m_CurrentUIAction;
        public Framework.ViewModels.UIAction CurrentUIAction
        {
            get { return m_CurrentUIAction; }
            set
            {
                Set(nameof(CurrentUIAction), ref m_CurrentUIAction, value);
            }
        }

        //protected Framework.ViewModels.ContentData m_ContentData;
        //public Framework.ViewModels.ContentData ContentData
        //{
        //    get { return m_ContentData; }
        //    set
        //    {
        //        m_ContentData = value;
        //        RaisePropertyChanged("ContentData");
        //    }
        //}

        protected Framework.ViewModels.SearchStatus m_SearchStatus;
        public Framework.ViewModels.SearchStatus SearchStatus
        {
            get { return m_SearchStatus; }
            set
            {
                m_SearchStatus = value;
                RaisePropertyChanged("SearchStatus");
            }
        }

        protected string m_StatusMessageOfResult;
        public string StatusMessageOfResult
        {
            get { return m_StatusMessageOfResult; }
            set
            {
                m_StatusMessageOfResult = value;
                RaisePropertyChanged("StatusMessageOfResult");
            }
        }

        protected Framework.Services.BusinessLogicLayerResponseStatus m_StatusOfResult;
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfResult
        {
            get { return m_StatusOfResult; }
            set
            {
                m_StatusOfResult = value;
                RaisePropertyChanged("StatusOfResult");
            }
        }

        protected Framework.ViewModels.UIActionStatusMessage m_UIActionStatusMessage = new Framework.ViewModels.UIActionStatusMessage();
        public Framework.ViewModels.UIActionStatusMessage UIActionStatusMessage
        {
            get { return m_UIActionStatusMessage; }
            set
            {
                m_UIActionStatusMessage = value;
                RaisePropertyChanged("UIActionStatusMessage");
            }
        }

        #endregion 1. properties

        #region 2.1. SaveCommand

        public ICommand SaveCommand { get; protected set; }

        public async void OnSave()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);
            await Save();

            if (StatusOfResult == Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (ItemUpdated != null)
                    ItemUpdated(this, null);

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

        public virtual async Task Save(bool useExtendedVM = true)
        {
            CurrentUIAction = Framework.ViewModels.UIAction.Update;
            await Upsert(useExtendedVM);
        }

        protected virtual bool CanSave()
        {
            return this.Item != null;
        }

        #endregion 2.1. SaveCommand

        #region 2.2. AddCommand

        public ICommand AddCommand { get; protected set; }

        public async void OnAdd()
        {
            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Saving, false);
            await Add();

            if (StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                if (ItemAdded != null)
                    ItemAdded(this, null);

                // success, will close Item Popup and popup message box
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.CloseItemControlPopup, Framework.Resx.UIStringResource.Info_Successfullyadded, GetThisItemDisplayString(), "!");
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtoadd, GetThisItemDisplayString(), "!");
            }
        }

        public virtual async Task Add(bool useExtendedVM = true)
        {
            CurrentUIAction = Framework.ViewModels.UIAction.Create;
            await Upsert(useExtendedVM);
        }

        protected bool CanAdd()
        {
            return true;
        }

        #endregion 2.2. AddCommand

        #region 2.3. DeleteCommand

        public ICommand DeleteCommand { get; protected set; }
        public ICommand ConfirmDeleteCommand { get; protected set; }

        protected virtual bool CanDelete()
        {
            return this.Item != null;
        }

        #endregion 2.3. DeleteCommand

        #region 2.4. RefreshCurrentItemCommand

        public ICommand RefreshCurrentItemCommand { get; protected set; }

        protected virtual async Task RefreshItem()
        {
            await RefreshItemNoMessage();
        }

        protected async Task RefreshItemNoMessage()
        {
            await LoadItem();
        }

        #endregion 2.4. RefreshCurrentItemCommand

        #region 2.5. RaiseItemPropertyChangedEventCommand

        public ICommand RaiseItemPropertyChangedEventCommand { get; protected set; }

        public void RaiseItemPropertyChangedEvent()
        {
            this.RaisePropertyChanged("Item");
        }

        #endregion 2.5. RaiseItemPropertyChangedEventCommand

        public ICommand EnableContentCommand { get; private set; }
        public ICommand DisableContentCommand { get; private set; }

        public ICommand ToggleContentCommand { get; private set; }

        public event EventHandler ItemAdded;
        public event EventHandler ItemDeleted;
        public event EventHandler ItemUpdated;

        public ViewModelItemBase()
            : base()
        {
            this.Item = new TItem();

            this.SaveCommand = new Command(OnSave, CanSave);
            this.AddCommand = new Command(OnAdd, CanAdd);
            this.DeleteCommand = new Command(OnDelete, CanDelete);
            this.ConfirmDeleteCommand = new Command(OnConfirmDelete);
            this.RefreshCurrentItemCommand = new Command(async () => { await RefreshItem(); });
            this.RaiseItemPropertyChangedEventCommand = new Command(this.RaiseItemPropertyChangedEvent);

            EnableContentCommand = new Command(() => IsContentEnable = true);
            DisableContentCommand = new Command(() => IsContentEnable = false);
            ToggleContentCommand = new Command<bool>(t => {
                IsContentEnable = t;
                PopupVM.ItemControlKey = t ? "Edit" : "Details";
            });
        }

        protected virtual async Task ProcessLoadItemRequest(LoadItemDataRequest<TSearchCriteria> request)
        {
            CurrentUIAction = request.UIAction;
            ShowSavingPopup = request.ShowSavingPopup;
            ShowSaveFailedPopup = request.ShowSaveFailedPopup;
            ShowSuccessfullySavedPopup = request.ShowSuccessfullySavedPopup;
            ControlParentOption = request.ControlParentOption;
            IsContentEnable = request.IsContentEnable;
            if (request.ShowLoadingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);
            await this.LoadItemWithDropDowns(request.Criteria, request.Refresh, request.LoadExtraDataIfNeeded, request.LoadDropDownContents, request.SetDropDownSelectedItems);
            if (request.ShowLoadingPopup)
                PopupVM.HidePopup();
        }

        protected virtual async Task ProcessAssignItemRequest(LoadItemDataRequest<TItem> request)
        {
            CurrentUIAction = request.UIAction;
            ShowSavingPopup = request.ShowSavingPopup;
            ShowSaveFailedPopup = request.ShowSaveFailedPopup;
            ShowSuccessfullySavedPopup = request.ShowSuccessfullySavedPopup;
            ControlParentOption = request.ControlParentOption;
            IsContentEnable = request.IsContentEnable;
            Item = request.Criteria;
            if (request.LoadDropDownContents)
                await LoadDropDownListContents();
            if (request.SetDropDownSelectedItems)
                SetDropDownListSelectedItems(Item);
        }

        public virtual async Task Upsert(bool useExtendedVM = true)
        {
            // this is a placeholder
            await Task.FromException(new NotImplementedException("Please implement Upsert in ItemVM"));
        }

        public virtual void OnDelete()
        {
            List<Framework.Xaml.ActionForm.ActionItemModel> list = new List<Framework.Xaml.ActionForm.ActionItemModel>();
            list.Add(GetConfirmDeleteActionItemModel());
            list.Add(PopupVM.GetCloseActionItemModel());
            var actionSheet = new Framework.Xaml.ActionForm.ActionSheetVM { HasIcon = false, ActionItems = new ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel>(list) };

            PopupVM.ShowPopup(Framework.Resx.UIStringResource.Warning_Areyousureyouwanttodelete, GetThisItemDisplayString(), "?", actionSheet);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        public virtual async void OnConfirmDelete()
        {
            PopupVM.HidePopup();

            if (ShowSavingPopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Deleting, false);
            await Delete();

            if (StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            // success, will close Item Popup and popup message box
            {
                PostAction(false);
            }
            else
            // failed
            {
                // failed, will close popup message box, stay at Item Popup
                PostAction(true, Framework.Xaml.BuiltInPopupTypes.ClosePopup, Framework.Resx.UIStringResource.Error_Failedtodelete, GetThisItemDisplayString(), "!");
            }
        }

        protected void RunItemDeletedEventHandlers()
        {
            if (ItemDeleted != null)
                ItemDeleted(this, null);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        protected virtual async Task Delete()
        {
            throw new NotImplementedException(
                "Delete Command/OnDelete is commented out in concrete class, please uncomment OnDelete() and Delete() methods in concrete class." +
                "Then goto WebApliClient and WebApi class, uncomment DeleteEntityAsync/DeleteEntity methods.");
        }

        public virtual async Task LoadItem()
        {
            await this.LoadItem(this.Criteria);
        }

        public abstract Task LoadItem(TIdentifierContract identifier, bool refresh = false, bool loadExtraDataIfNeeded = false);

        public virtual async Task LoadItemWithDropDowns(TIdentifierContract identifier, bool refresh = false, bool loadExtraDataIfNeeded = false, bool loadDropDownContents = true, bool setDropDownSelectedItems = true)
        {
            await LoadItem(identifier, refresh, loadExtraDataIfNeeded);

            if (loadDropDownContents)
                await LoadDropDownListContents();

            if (setDropDownSelectedItems)
                SetDropDownListSelectedItems(Item);
        }

        public virtual async Task ReLoadItem(TIdentifierContract identifier, bool refresh = false)
        {
            await this.LoadItem(identifier, refresh);
        }

        public override void Cleanup()
        {
            base.Cleanup();
            this.m_Item = new TItem();
        }

        public bool IsValid
        {
            get
            {
                return !this.Item.HasErrors;
            }
        }

        protected override void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            base.ValidateProperty(value, propertyName);

            RaisePropertyChanged("IsValid");
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("IsValid");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        public virtual async Task LoadDropDownListContents() { }

        protected virtual void GetValueFromDropDownListSelectedItems() { }

        public virtual void SetDropDownListSelectedItems(TItem o) { }

        public virtual void SetUIActionStatusMessage(Framework.ViewModels.UIActionStatus uiActionStatus, string uiMessage = null)
        {
            UIActionStatusMessage.UIAction = CurrentUIAction;
            UIActionStatusMessage.UIActionStatus = uiActionStatus;
            if (string.IsNullOrEmpty(uiMessage))
            {
                string actionString = string.Empty;
                if (CurrentUIAction == Framework.ViewModels.UIAction.Create)
                    actionString = Framework.Resx.UIStringResource.AddNew.ToString();
                else if (CurrentUIAction == Framework.ViewModels.UIAction.Delete)
                    actionString = Framework.Resx.UIStringResource.Delete.ToString();
                else if (CurrentUIAction == Framework.ViewModels.UIAction.Update)
                    actionString = Framework.Resx.UIStringResource.Update.ToString();

                // We only support Succeeded/Failed status
                string actionStatus;
                if (uiActionStatus == Framework.ViewModels.UIActionStatus.Success)
                    actionStatus = Framework.Resx.UIStringResource.Succeeded.ToString();
                else
                    actionStatus = Framework.Resx.UIStringResource.Failed.ToString();

                if (string.IsNullOrEmpty(actionString))
                {
                    UIActionStatusMessage.UIMessage = actionStatus;
                }
                else
                {
                    UIActionStatusMessage.UIMessage = $@"{actionString} {actionStatus}";
                }
            }
            else
            {
                UIActionStatusMessage.UIMessage = uiMessage;
            }
        }

        /// <summary>
        /// On Delete Form
        /// </summary>
        /// <param name="itemVM"></param>
        /// <returns></returns>
        protected Framework.Xaml.ActionForm.ActionItemModel GetDeleteActionItemModel()
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Delete,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Trash, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = DeleteCommand,
                Position = 10,
            };
        }

        protected Framework.Xaml.ActionForm.ActionItemModel GetConfirmDeleteActionItemModel()
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Delete,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Trash, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = ConfirmDeleteCommand,
                Position = 10,
            };
        }

        protected abstract TItem GetAClone(TItem item);

        public override string GetThisItemDisplayString()
        {
            return GetDisplayString(Item);
        }

        public virtual string GetDisplayString(TItem item)
        {
            return Framework.Resx.UIStringResource.ThisItem;
        }
    }
}

