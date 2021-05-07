using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class ViewModelBaseWithResultAndUIElement<TSearchResultEntityItem> : ViewModelBase
        where TSearchResultEntityItem : class, new()
    {
        public class QueryOrderBySettingClientSideActions : Framework.Xaml.QueryOrderBySettingClientSideActions<GroupedResult, TSearchResultEntityItem>
        {
        }

        #region 1. Properties


        private Framework.Services.BusinessLogicLayerResponseStatus m_StatusOfResult;
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfResult
        {
            get { return m_StatusOfResult; }
            set
            {
                Set(nameof(StatusOfResult), ref m_StatusOfResult, value);

                IsRemainingItemsZero = StatusOfResult == Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource;
            }
        }

        private string m_StatusMessageOfResult;
        public string StatusMessageOfResult
        {
            get { return m_StatusMessageOfResult; }
            set
            {
                Set(nameof(StatusMessageOfResult), ref m_StatusMessageOfResult, value);
            }
        }

        public virtual string SearchBarPlaceHolder
        {
            get { return null; }
        }

        private Framework.Xaml.ListItemViewModes m_ListItemViewMode;
        public Framework.Xaml.ListItemViewModes ListItemViewMode
        {
            get { return m_ListItemViewMode; }
            set
            {
                Set(nameof(ListItemViewMode), ref m_ListItemViewMode, value);
                RaisePropertyChanged(nameof(IsSelectionList));
                RaisePropertyChanged(nameof(IsRegularList));
            }
        }

        public bool IsSelectionList
        {
            get { return m_ListItemViewMode == Framework.Xaml.ListItemViewModes.SingleSelection || m_ListItemViewMode == Framework.Xaml.ListItemViewModes.MultipleSelection; }
        }

        public bool IsRegularList
        {
            get { return m_ListItemViewMode == Framework.Xaml.ListItemViewModes.NavigationWhenClickItem || m_ListItemViewMode == Framework.Xaml.ListItemViewModes.NavigationWhenRightArrow; }
        }

        public bool IsInWizard
        {
            get { return m_ListItemViewMode == Framework.Xaml.ListItemViewModes.InWizard; }
        }

        public Framework.Xaml.CachingOptions CachingOption { get; set; } = Framework.Xaml.CachingOptions.NoCaching;

        private bool m_BindToGroupedResults = false;
        public bool BindToGroupedResults
        {
            get { return m_BindToGroupedResults; }
            protected set
            {
                Set(nameof(BindToGroupedResults), ref m_BindToGroupedResults, value);
            }
        }

        private bool m_IsRefreshing = false;
        public bool IsRefreshing
        {
            get { return m_IsRefreshing; }
            set
            {
                Set(nameof(IsRefreshing), ref m_IsRefreshing, value);
            }
        }

        private byte m_RemainingItemsThreshold = 10;
        public byte RemainingItemsThreshold
        {
            get { return m_RemainingItemsThreshold; }
            set
            {
                Set(nameof(RemainingItemsThreshold), ref m_RemainingItemsThreshold, value);
            }
        }

        private bool m_IsRemainingItemsZero = false;
        public bool IsRemainingItemsZero
        {
            get { return m_IsRemainingItemsZero; }
            set
            {
                if (value != m_IsRemainingItemsZero && value)
                    LastRemainingItemsZeroDateTime = DateTime.Now;
                Set(nameof(IsRemainingItemsZero), ref m_IsRemainingItemsZero, value);
            }
        }

        private DateTime m_LastRemainingItemsZeroDateTime = DateTime.Now;
        public DateTime LastRemainingItemsZeroDateTime
        {
            get { return m_LastRemainingItemsZeroDateTime; }
            set
            {
                Set(nameof(LastRemainingItemsZeroDateTime), ref m_LastRemainingItemsZeroDateTime, value);
            }
        }

        private byte m_RemainingItemsZeroNoResponseTimeSpanInMinutes = 5;
        public byte RemainingItemsZeroNoResponseTimeSpanInMinutes
        {
            get { return m_RemainingItemsZeroNoResponseTimeSpanInMinutes; }
            set
            {
                Set(nameof(RemainingItemsZeroNoResponseTimeSpanInMinutes), ref m_RemainingItemsZeroNoResponseTimeSpanInMinutes, value);
            }
        }

        protected ObservableCollection<TSearchResultEntityItem> m_Result = new ObservableCollection<TSearchResultEntityItem>();

        /// <summary>
        /// Gets or sets the DataSourceEntities list.
        /// should investigate whether can remove RaisePropertyChanged
        /// </summary>
        /// <value>The DataSourceEntities list.</value>
        public ObservableCollection<TSearchResultEntityItem> Result
        {
            get { return m_Result; }
            set
            {
                m_Result = value;
                RaisePropertyChanged("Result");
            }
        }

        public Framework.ViewModels.SearchStatus m_SearchStatus;
        // should investigate whether can remove RaisePropertyChanged
        public Framework.ViewModels.SearchStatus SearchStatus
        {
            get
            {
                return this.m_SearchStatus;
            }
            set
            {
                if (this.m_SearchStatus != value)
                {
                    this.m_SearchStatus = value;
                    RaisePropertyChanged("SearchStatus");
                }
            }
        }

        private TSearchResultEntityItem m_SelectedItem;
        public TSearchResultEntityItem SelectedItem
        {
            get { return m_SelectedItem; }
            set
            {
                Set(nameof(SelectedItem), ref m_SelectedItem, value);
            }
        }

        private ObservableCollection<TSearchResultEntityItem> m_SelectedCollection = new ObservableCollection<TSearchResultEntityItem>();
        public ObservableCollection<TSearchResultEntityItem> SelectedCollection
        {
            get { return m_SelectedCollection; }
            set
            {
                Set(nameof(SelectedCollection), ref m_SelectedCollection, value);
            }
        }

        private int m_Count;
        public int Count
        {
            get { return m_Count; }
            set
            {
                Set(nameof(Count), ref m_Count, value);
            }
        }

        protected Framework.Queries.QueryPagingSetting m_QueryPagingSetting;
        /// <summary>
        /// Gets or sets the query paging setting for UI.
        /// </summary>
        /// <value>
        /// The query paging setting.
        /// </value>
        public Framework.Queries.QueryPagingSetting QueryPagingSetting
        {
            get { return m_QueryPagingSetting; }
            set
            {
                Set(nameof(QueryPagingSetting), ref m_QueryPagingSetting, value);
            }
        }

        protected Framework.Queries.QueryOrderBySettingCollection m_QueryOrderBySettingCollection;
        public Framework.Queries.QueryOrderBySettingCollection QueryOrderBySettingCollection
        {
            get { return m_QueryOrderBySettingCollection; }
            set
            {
                Set(nameof(QueryOrderBySettingCollection), ref m_QueryOrderBySettingCollection, value);
            }
        }

        #endregion 1. Properties

        #region 1.1. SelectedItem Properties

        private bool m_ShowSavingPopup;
        public bool ShowSavingPopup
        {
            get { return m_ShowSavingPopup; }
            set
            {
                Set(nameof(ShowSavingPopup), ref m_ShowSavingPopup, value);
            }
        }

        private bool m_ShowSuccessfullySavedPopup;
        public bool ShowSuccessfullySavedPopup
        {
            get { return m_ShowSuccessfullySavedPopup; }
            set
            {
                Set(nameof(ShowSuccessfullySavedPopup), ref m_ShowSuccessfullySavedPopup, value);
            }
        }

        private bool m_ShowSaveFailedPopup;
        public bool ShowSaveFailedPopup
        {
            get { return m_ShowSaveFailedPopup; }
            set
            {
                Set(nameof(ShowSaveFailedPopup), ref m_ShowSaveFailedPopup, value);
            }
        }

        private Framework.Xaml.ControlParentOptions m_ControlParentOption = ControlParentOptions.InPage;
        public Framework.Xaml.ControlParentOptions ControlParentOption
        {
            get { return m_ControlParentOption; }
            set
            {
                Set(nameof(ControlParentOption), ref m_ControlParentOption, value);
            }
        }

        private bool m_IsContentEnable = false;
        public bool IsContentEnable
        {
            get { return m_IsContentEnable; }
            set
            {
                Set(nameof(IsContentEnable), ref m_IsContentEnable, value);
            }
        }

        #endregion 1.1. SelectedItem Properties

        #region 2. Commands

        public ICommand TextFilterCommand { get; protected set; }

        /// <summary>
        /// FilterCommand/OnFilter is used to text, usually should be able to filter text field/datetime field...
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        protected virtual async void OnTextFilterCommand(string filter)
        {
            throw new NotImplementedException("CollectionView SearchBar Text Filter Command not implemented. Please override this method.");
        }

        public ICommand SearchCommand { get; protected set; }

        protected virtual async void Search()
        {
            IsRemainingItemsZero = false;

            if (IsRefreshing)
                return;

            IsRefreshing = true;

            await this.DoSearch(true);

            IsRefreshing = false;
        }

        protected virtual bool CanSearch()
        {
            return true; // !(this.SearchStatus == Framework.ViewModels.SearchStatus.Searching);
        }

        /// <summary>
        /// Xamarin.Forms CollectionView.RemainingItemsThresholdReachedCommand
        /// </summary>
        public ICommand LoadMoreCommand { get; protected set; }

        protected virtual async void LoadMore()
        {
            await this.DoSearch(false, CachingOption != CachingOptions.NoCaching, false);
        }

        public ICommand SortCommand => new Command<Framework.Xaml.ActionForm.SortActionItemModel>(OnSortCommand);
        async void OnSortCommand(Framework.Xaml.ActionForm.SortActionItemModel sortActionItemModel)
        {
            if (sortActionItemModel.QueryOrderBySetting.IsSelected)
                sortActionItemModel.QueryOrderBySetting.ToggleDirection();
            else
            {
                QueryOrderBySettingCollection.DeSelectAll();
                sortActionItemModel.QueryOrderBySetting.IsSelected = true;
            }

            await Framework.Xaml.ApplicationPropertiesHelper.SetTableUIListSetting(typeof(TSearchResultEntityItem).FullName, BindToGroupedResults, QueryOrderBySettingCollection?.ToList());

            await DoSearch(true, true);

            if (PostOnSortCommand != null)
                PostOnSortCommand();
        }

        public Action PostOnSortCommand;

        public ICommand ToggleGroupedResultsViewCommand => new Command(OnToggleGroupedResultsViewCommand);
        async void OnToggleGroupedResultsViewCommand()
        {
            BindToGroupedResults = !BindToGroupedResults;

            await Framework.Xaml.ApplicationPropertiesHelper.SetTableUIListSetting(typeof(TSearchResultEntityItem).FullName, BindToGroupedResults, QueryOrderBySettingCollection?.ToList());

            if (BindToGroupedResults)
                Result.Clear();
            else
                GroupedResults.Clear();

            if (PostOnToggleGroupedResultsViewCommand != null)
                PostOnToggleGroupedResultsViewCommand();
            await DoSearch(true, true);
        }

        public Action PostOnToggleGroupedResultsViewCommand;

        public ICommand ClearSelectionCommand => new Command(OnClearSelectionCommand);

        private async void OnClearSelectionCommand()
        {
            SelectedItem = null;
            SelectedCollection = new ObservableCollection<TSearchResultEntityItem>();
        }
        public ICommand DoneSelectionCommand => new Command(OnDoneSelectionCommand);

        private async void OnDoneSelectionCommand()
        {
        }

        #endregion 2. Commands

        public ViewModelBaseWithResultAndUIElement()
        {
            this.TextFilterCommand = new Command<string>(this.OnTextFilterCommand);
            this.SearchCommand = new Command(this.Search, this.CanSearch);
            this.LoadMoreCommand = new Command(this.LoadMore, this.CanSearch);

            this.QueryPagingSetting = GetDefaultQueryPagingSetting();
            this.QueryPagingSetting.CurrentPage = 1;

            var tableCachingItem = Framework.Xaml.ApplicationPropertiesHelper.GetTableCachingData(typeof(TSearchResultEntityItem).FullName);
            this.QueryOrderBySettingCollection = GetQueryOrderBySettingCollection(tableCachingItem);
            this.BindToGroupedResults = tableCachingItem.UIListBindToGroupedResults;
        }

        public abstract Task DoSearch(bool isToClearExistingResult, bool isToLoadFromCache = false, bool enablePopup = true);

        protected virtual List<GroupedResult> ConvertToGroupdResults(List<TSearchResultEntityItem> list)
        {
            //??
            var queryOrderBySetting = QueryOrderBySettingCollection.First(t => t.IsSelected);
            if (queryOrderBySetting == null)
                queryOrderBySetting = QueryOrderBySettingCollection.First();
            if (queryOrderBySetting != null && queryOrderBySetting?.ClientSideActions != null && ((QueryOrderBySettingClientSideActions)queryOrderBySetting.ClientSideActions).GetGroupResults != null)
                return ((QueryOrderBySettingClientSideActions)queryOrderBySetting?.ClientSideActions)?.GetGroupResults(list);
            else
                return Enumerable.Empty<GroupedResult>().ToList();
        }

        public virtual Framework.Queries.QueryPagingSetting GetDefaultQueryPagingSetting()
        {
            Framework.Queries.QueryPagingSetting queryPagingSetting = new Framework.Queries.QueryPagingSetting(1, 10);
            if (queryPagingSetting.CountOfPages == 0 || queryPagingSetting.CountOfRecords == 0)
            {
                queryPagingSetting.CurrentPage = 0;
            }
            return queryPagingSetting;
        }

        protected Framework.Queries.QueryPagingSetting GetQueryPagingSettingFromQueryPagingResult(Framework.Queries.QueryPagingResult queryPagingResult)
        {
            Framework.Queries.QueryPagingSetting queryPagingSetting = new Framework.Queries.QueryPagingSetting
            {
                CountOfRecords = queryPagingResult.CountOfRecords,
                PageSize = queryPagingResult.PageSize,
                CurrentPage = queryPagingResult.CurrentIndexOfStartRecord / queryPagingResult.PageSize + 1,
                RecordCountOfCurrentPage = queryPagingResult.RecordCountOfCurrentPage,
            };
            return queryPagingSetting;
        }

        public virtual List<Framework.Queries.QueryOrderBySetting> GetDefaultQueryOrderBySettingCollection()
        {
            return Enumerable.Empty<Framework.Queries.QueryOrderBySetting>().ToList();
        }

        protected Framework.Queries.QueryOrderBySettingCollection GetQueryOrderBySettingCollection(Framework.Xaml.TableCachingItem tableCachingItem)
        {
            var defaultList = GetDefaultQueryOrderBySettingCollection();
            if (defaultList == null || defaultList.Count == 0)
            {
                return new Framework.Queries.QueryOrderBySettingCollection();
            }

            if (tableCachingItem.UIListQueryOrderBySettings == null || tableCachingItem.UIListQueryOrderBySettings.Count() == 0)
                return new Framework.Queries.QueryOrderBySettingCollection(defaultList);

            var result = new Framework.Queries.QueryOrderBySettingCollection();
            foreach (var defaultQueryOrderBySetting in defaultList)
            {
                if (tableCachingItem.UIListQueryOrderBySettings.Any(t => t.PropertyName == defaultQueryOrderBySetting.PropertyName))
                {
                    var queryOrderBySetting = tableCachingItem.UIListQueryOrderBySettings.First(t => t.PropertyName == defaultQueryOrderBySetting.PropertyName);
                    defaultQueryOrderBySetting.Direction = queryOrderBySetting.Direction;
                    defaultQueryOrderBySetting.IsSelected = queryOrderBySetting.IsSelected;
                }
            }

            return new Framework.Queries.QueryOrderBySettingCollection(defaultList);
        }

        public Framework.Xaml.ActionForm.ActionItemModel GetToggleGroupedResultsViewSearchActionItemModel()
        {
            var retval = new Framework.Xaml.ActionForm.ToggleItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.ToggleItem,
                Title = Framework.Resx.UIStringResource.GroupYourResults,
                //FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Search, MasterFontIconFamily = "FontAwesomeSolid" },
                ToggleStatus = BindToGroupedResults ? Models.ToggleStatus.On : Models.ToggleStatus.Off,
                Command = ToggleGroupedResultsViewCommand,
                Position = 100,
            };
            PostOnToggleGroupedResultsViewCommand = retval.Toggle;
            return retval;
        }

        public void SetBindToGroupedResults(string propertName, Framework.Queries.QueryOrderDirections? direction)
        {
            if (string.IsNullOrEmpty(propertName))
                return;

            var currentSelecteds = this.QueryOrderBySettingCollection?.Where(t => t.IsSelected);
            foreach (var currentSelected in currentSelecteds)
                currentSelected.IsSelected = false;

            var newSelected = this.QueryOrderBySettingCollection?.FirstOrDefault(t => t.PropertyName == propertName);
            if (newSelected == null)
                newSelected = this.QueryOrderBySettingCollection?.FirstOrDefault();
            if (newSelected == null)
            {
                BindToGroupedResults = false;
                return;
            }

            BindToGroupedResults = true;
            newSelected.IsSelected = true;
            if (direction.HasValue)
                newSelected.Direction = direction.Value;
        }

        protected void BindResult(List<TSearchResultEntityItem> list, bool isToClearExistingResult)
        {
            Device.InvokeOnMainThreadAsync(() =>
            {
                if (BindToGroupedResults)
                {
                    var groupedResults = ConvertToGroupdResults(list);
                    if (isToClearExistingResult)
                    {
                        this.GroupedResults.Clear();
                    }

                    foreach (var inputGroupedResult in groupedResults)
                    {
                        // 1. remove from different group
                        foreach (var item1 in inputGroupedResult)
                        {
                            var existings = GroupedResults.Where(t => t.GroupID?.ToString() != inputGroupedResult.GroupID?.ToString() && t.Any(GetPredicateToGetAnExistingItem(item1)));
                            foreach (var existingGroupedResult in existings)
                            {
                                existingGroupedResult.Remove(existingGroupedResult.First(GetPredicateToGetAnExistingItem(item1)));
                            }
                        }

                        // 2. add / update item in same group
                        if (GroupedResults.Any(t => t.GroupID?.ToString() == inputGroupedResult.GroupID?.ToString()))
                        {
                            var existing = GroupedResults.First(t => t.GroupID?.ToString() == inputGroupedResult.GroupID?.ToString());
                            foreach (var item1 in inputGroupedResult)
                            {
                                if (existing.Any(GetPredicateToGetAnExistingItem(item1))) // 2.1. update existing items in same group
                                    CopyToItemInList(existing.First(GetPredicateToGetAnExistingItem(item1)), item1);
                                else // 2.2. add to existing group
                                    existing.Add(item1);
                            }
                        }
                        else
                        // 3. Add new Group
                        {
                            GroupedResults.Add(inputGroupedResult);
                        }
                    }
                }
                else
                {
                    if (isToClearExistingResult)
                    {
                        this.Result = new ObservableCollection<TSearchResultEntityItem>(list);
                    }
                    else
                    {
                        foreach (var item in list)
                        {
                            this.Result.Add(item);
                        }
                    }
                }
            });
        }

        protected void PostItemAction(bool showBuiltInPopup = true, BuiltInPopupTypes builtInPopupType = BuiltInPopupTypes.Custom, string message = null, string highlightedMessage = null, string endMessage = null)
        {
            if (ShowSavingPopup)
                PopupVM.HidePopup();

            if (showBuiltInPopup)
                PopupVM.ShowBuiltInPopup(builtInPopupType, message, highlightedMessage, endMessage, null, true, false);
            else
                PopupVM.HideItemControlPopup();
        }

        public virtual string GetThisItemDisplayString()
        {
            return Framework.Resx.UIStringResource.ThisItem;
        }

        protected virtual void CopyToItemInList(TSearchResultEntityItem itemInList, TSearchResultEntityItem newItem)
        {
            throw new NotImplementedException("Please implement CopyToItemInList(TSearchResultEntityItem itemInList, TSearchResultEntityItem newItem)");
        }

        protected virtual Func<TSearchResultEntityItem, bool> GetPredicateToGetAnExistingItem(TSearchResultEntityItem item)
        {
            throw new NotImplementedException("Please implement protected virtual Func<TSearchResultEntityItem, bool> GetPredicateToGetAnExistingItem(TSearchResultEntityItem item)");
        }

        private ObservableCollection<GroupedResult> m_GroupedResults = new ObservableCollection<GroupedResult>();
        public ObservableCollection<GroupedResult> GroupedResults
        {
            get { return m_GroupedResults; }
            set
            {
                Set(nameof(GroupedResults), ref m_GroupedResults, value);
            }
        }

        public class GroupedResult : ObservableCollection<TSearchResultEntityItem>
        {
            public object GroupID { get; private set; }
            public string GroupName { get; private set; }

            public GroupedResult(object groupID, string groupName, List<TSearchResultEntityItem> lists) : base(lists)
            {
                GroupID = groupID;
                GroupName = groupName;
            }

            public override string ToString()
            {
                return string.Format("{0}({1})", GroupName, GroupID);
            }
        }
    }
}

