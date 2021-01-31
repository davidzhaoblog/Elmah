using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class ViewModelBaseWithResultAndUIElement<TSearchCriteria, TSearchResultEntityCollection, TSearchResultEntityItem, TViewModelData, TItemVM, TItemVMSearchCriteria, TNavigationVMEntityContainer, TSQLiteRepository, TSQLiteItemType, TIIdentifier>
        : ViewModelBaseWithResultAndUIElement<TSearchCriteria, TSearchResultEntityCollection, TSearchResultEntityItem, TViewModelData, TSQLiteRepository, TSQLiteItemType, TIIdentifier>
        where TSearchCriteria : class, new()
        where TSearchResultEntityCollection : List<TSearchResultEntityItem>, new()
        where TSearchResultEntityItem : Framework.Models.PropertyChangedNotifier, TIIdentifier, new()
        where TViewModelData : Framework.ViewModels.ViewModelBase<TSearchCriteria, TSearchResultEntityCollection>, new()
        where TItemVMSearchCriteria : class, TIIdentifier, Framework.Models.IClone<TIIdentifier>, new()
        where TItemVM : Framework.Xaml.ViewModelItemBase<TItemVMSearchCriteria, TIIdentifier, TSearchResultEntityItem>
        where TNavigationVMEntityContainer : Framework.Xaml.NavigationVMEntityContainer<TItemVM, TItemVMSearchCriteria, TIIdentifier, TSearchResultEntityItem>, new()
        where TSQLiteRepository : Framework.Xaml.SQLite.SQLiteTableRepositoryBase<TSQLiteItemType, TSearchCriteria, TSearchResultEntityItem, TIIdentifier>
        where TSQLiteItemType : TSearchResultEntityItem, new()
    {
        public virtual TNavigationVMEntityContainer NavigationContainer
        {
            get
            {
                return null;
            }
        }

        public virtual TItemVM ItemVM
        {
            get
            {
                return null;
            }
        }

        public ViewModelBaseWithResultAndUIElement()
        {
            ItemVM.ItemAdded += ItemVM_ItemAdded;
            ItemVM.ItemDeleted += ItemVM_ItemDeleted;
            ItemVM.ItemUpdated += ItemVM_ItemUpdated;
        }

        protected virtual void ItemVM_ItemAdded(object sender, EventArgs e)
        {
            ItemVM_ItemAddedOrUpdated();
        }

        protected virtual void ItemVM_ItemDeleted(object sender, EventArgs e)
        {
            if (BindToGroupedResults)
            {
                if (GroupedResults != null)
                {
                    foreach (var groupedResult in GroupedResults)
                    {
                        if (groupedResult.Any(GetPredicateToGetAnExistingItem(ItemVM.Item)))
                        {
                            groupedResult.Remove(Result.First(GetPredicateToGetAnExistingItem(ItemVM.Item)));
                        }
                    }
                }
            }
            else
                Result.Remove(Result.First(GetPredicateToGetAnExistingItem(ItemVM.Item)));
        }

        protected virtual void ItemVM_ItemUpdated(object sender, EventArgs e)
        {
            ItemVM_ItemAddedOrUpdated();
        }

        protected virtual void ItemVM_ItemAddedOrUpdated()
        {
            try
            {
                if (BindToGroupedResults)
                {
                    var updatedList = new List<TSearchResultEntityItem> { ItemVM.Item };
                    BindResult(updatedList, false);
                }
                else
                {
                    var item = Result.First(GetPredicateToGetAnExistingItem(ItemVM.Item));
                    if (item != null)
                        CopyToItemInList(item, ItemVM.Item);
                    else
                        Result.Add(ItemVM.Item);
                }
            }
            catch //(Exception ex)
            {
            }
        }
    }

    public abstract class ViewModelBaseWithResultAndUIElement<TSearchCriteria, TSearchResultEntityCollection, TSearchResultEntityItem, TViewModelData, TSQLiteRepository, TSQLiteItemType, TIIdentifier>
        : ViewModelBaseWithResultAndUIElement<TSearchCriteria, TSearchResultEntityCollection, TSearchResultEntityItem, TViewModelData>
        where TSearchCriteria : class, new()
        where TSearchResultEntityCollection : List<TSearchResultEntityItem>, new()
        where TSearchResultEntityItem : Framework.Models.PropertyChangedNotifier, TIIdentifier, new()
        where TViewModelData : Framework.ViewModels.ViewModelBase<TSearchCriteria, TSearchResultEntityCollection>, new()
        where TSQLiteRepository : Framework.Xaml.SQLite.SQLiteTableRepositoryBase<TSQLiteItemType, TSearchCriteria, TSearchResultEntityItem, TIIdentifier>
        where TSQLiteItemType : TSearchResultEntityItem, new()
    {
        public virtual TSQLiteRepository CacheInstance
        {
            get
            {
                return null;
            }
        }

        protected virtual async Task<List<TSearchResultEntityItem>> LoadFromCache()
        {
            try
            {
                if (this.QueryPagingSetting.CurrentIndex == 0)
                    this.Count = await CacheInstance.Count(this.Criteria);

                var selectedQueryOrderBySetting = this.QueryOrderBySettingCollection.FirstOrDefault(t => t.IsSelected);
                if (selectedQueryOrderBySetting == null)
                    selectedQueryOrderBySetting = this.QueryOrderBySettingCollection.FirstOrDefault();

                var results = await CacheInstance.Load(this.Criteria, this.QueryPagingSetting, selectedQueryOrderBySetting, ((QueryOrderBySettingClientSideActions)selectedQueryOrderBySetting?.ClientSideActions)?.GetSQLiteSortTableQuery);
                if (results != null && results.Count > 0)
                {
                    this.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.MessageOK;
                    this.QueryPagingSetting.CurrentPage++;
                    this.QueryPagingSetting.RecordCountOfCurrentPage = results.Count;
                }
                else
                    this.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.NoValueFromDataSource;

                return results;
            }
            catch (Exception ex)
            {
                // TODO: should popup error message
                //throw;

                this.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                return Enumerable.Empty<TSearchResultEntityItem>().ToList();
            }
        }

        public override async Task DoSearch(bool isToClearExistingResult, bool isToLoadFromCache = false, bool enablePopup = true)
        {
            if (enablePopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            GetCriteriaDDLValuesFromExtendedVM();

            this.SearchStatus = Framework.ViewModels.SearchStatus.Searching;
            if (isToClearExistingResult)
                this.QueryPagingSetting = new Framework.Queries.QueryPagingSetting(1, 10);

            try
            {
                bool isToLoadFromServer = !isToLoadFromCache;
                if (isToLoadFromCache)
                {
                    // TODO: To load properly from cache, comment out following line and
                    var result = await LoadFromCache();
                    if (StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK && result.Count > 0)
                        BindResult(result, isToClearExistingResult);
                    else // will LoadFromServer if no result in Cache, and ToClearExistingResult
                        isToLoadFromServer = isToClearExistingResult;
                }

                if (isToLoadFromServer)
                {
                    var result = await GetFromServer();

                    this.StatusOfResult = result.StatusOfResult;

                    if (StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK &&
                        CachingOption != Framework.Xaml.CachingOptions.UpdateCacheWhenMasterLoaded || CachingOption == Framework.Xaml.CachingOptions.UpdateCacheWhenMasterLoaded && isToLoadFromCache)
                    {
                        if (this.QueryPagingSetting.CurrentIndex == 0)
                        {
                            this.Count = result.QueryPagingSetting.CountOfRecords;
                        }
                        else
                            this.QueryPagingSetting = result.QueryPagingSetting;

                        BindResult(result.Result.Take(this.QueryPagingSetting.PageSize).ToList(), isToClearExistingResult);
                    }
                    else
                    {
                        // TODO: should display error message, no change to binding?
                        this.StatusMessageOfResult = result.StatusMessageOfResult;
                        this.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.RequestError;
                    }
                }
            }
            catch //(Exception ex)
            {
            }

            if (enablePopup)
                PopupVM.HidePopup();
        }

        protected override List<GroupedResult> ConvertToGroupdResults(List<TSearchResultEntityItem> list)
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

        public virtual async Task ClearCache()
        {
            await CacheInstance.DeleteAllItemsFromTableAsync();
        }

        public new class QueryOrderBySettingClientSideActions : Framework.Xaml.QueryOrderBySettingClientSideActions<GroupedResult, TSearchResultEntityItem, TSQLiteItemType>
        {
        }
    }

    public abstract class ViewModelBaseWithResultAndUIElement<TSearchCriteria, TSearchResultEntityCollection, TSearchResultEntityItem, TViewModelData>
        : ViewModelBase, Framework.ViewModels.IViewModelBase<TSearchCriteria>
        where TSearchCriteria : class, new()
        where TSearchResultEntityCollection : List<TSearchResultEntityItem>, new()
        where TSearchResultEntityItem : class, new()
        where TViewModelData : Framework.ViewModels.ViewModelBase<TSearchCriteria, TSearchResultEntityCollection>, new()
    {
        public class QueryOrderBySettingClientSideActions : Framework.Xaml.QueryOrderBySettingClientSideActions<GroupedResult, TSearchResultEntityItem>
        {
        }

        #region 1. Properties

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

        protected TSearchCriteria m_Criteria;

        public TSearchCriteria Criteria
        {
            get { return m_Criteria; }
            set
            {
                Set(nameof(Criteria), ref m_Criteria, value);
            }
        }

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
            if(direction.HasValue)
                newSelected.Direction = direction.Value;
        }

        //protected Framework.Queries.QueryOrderBySetting m_SelectedQueryOrderBySetting;
        //public Framework.Queries.QueryOrderBySetting SelectedQueryOrderBySetting
        //{
        //    get { return m_SelectedQueryOrderBySetting; }
        //    set
        //    {
        //        Set(nameof(SelectedQueryOrderBySetting), ref m_SelectedQueryOrderBySetting, value);
        //    }
        //}

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

        #endregion 1. Properties

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

        protected async void Search()
        {
            IsRemainingItemsZero = false;

            if (IsRefreshing)
                return;

            IsRefreshing = true;

            if (this.QueryPagingSetting != null && this.QueryPagingSetting.CurrentPage == 0)
            {
                this.QueryPagingSetting.CurrentPage = 1;
            }

            QueryPagingSetting = GetDefaultQueryPagingSetting();
            QueryPagingSetting.CurrentPage = 1;

            await this.DoSearch(true);

            IsRefreshing = false;
        }

        protected bool CanSearch()
        {
            return true; // !(this.SearchStatus == Framework.ViewModels.SearchStatus.Searching);
        }

        /// <summary>
        /// Xamarin.Forms CollectionView.RemainingItemsThresholdReachedCommand
        /// </summary>
        public ICommand LoadMoreCommand { get; protected set; }

        protected async void LoadMore()
        {
            if (IsRemainingItemsZero && (DateTime.Now - LastRemainingItemsZeroDateTime).TotalMinutes < RemainingItemsZeroNoResponseTimeSpanInMinutes)
                return;
            this.QueryPagingSetting.CurrentPage += 1;
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
            : base()
        {
            this.Criteria = new TSearchCriteria();

            this.TextFilterCommand = new Command<string>(this.OnTextFilterCommand);
            this.SearchCommand = new Command(this.Search, this.CanSearch);
            this.LoadMoreCommand = new Command(this.LoadMore, this.CanSearch);

            this.Criteria = new TSearchCriteria();
            this.QueryPagingSetting = GetDefaultQueryPagingSetting();
            this.QueryPagingSetting.CurrentPage = 1;

            var tableCachingItem = Framework.Xaml.ApplicationPropertiesHelper.GetTableCachingData(typeof(TSearchResultEntityItem).FullName);
            this.QueryOrderBySettingCollection = GetQueryOrderBySettingCollection(tableCachingItem);
            this.BindToGroupedResults = tableCachingItem.UIListBindToGroupedResults;

            //_ = GetDefaultPerViewModel();

            //// the following command are not in use
            //this.ListOfQueryOrderBySettingCollecionInString = GetDefaultListOfQueryOrderBySettingCollecionInString();
            //this.ClearSearchResultCommand = new Command(ClearSearchResult, CanClearSearchResult);
            //this.PaginationFirstPageCommand = new Command(PaginationFirstPage);
            //this.PaginationPreviousPageCommand = new Command(PaginationPreviousPage);
            //this.PaginationNextPageCommand = new Command(PaginationNextPage);
            //this.PaginationLastPageCommand = new Command(PaginationLastPage);

            AdditionalInitialization();
        }

        public virtual async Task DoSearch(bool isToClearExistingResult, bool isToLoadFromCache = false, bool enablePopup = true)
        {
            if (enablePopup)
                PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            GetCriteriaDDLValuesFromExtendedVM();

            this.SearchStatus = Framework.ViewModels.SearchStatus.Searching;
            if (isToClearExistingResult)
                this.QueryPagingSetting = new Framework.Queries.QueryPagingSetting(1, 10);

            try
            {
                var result = await GetFromServer();

                this.StatusOfResult = result.StatusOfResult;

                if (StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK &&
                    CachingOption != Framework.Xaml.CachingOptions.UpdateCacheWhenMasterLoaded || CachingOption == Framework.Xaml.CachingOptions.UpdateCacheWhenMasterLoaded && isToLoadFromCache)
                {
                    if (this.QueryPagingSetting.CurrentIndex == 0)
                    {
                        this.Count = result.QueryPagingSetting.CountOfRecords;
                    }
                    else
                        this.QueryPagingSetting = result.QueryPagingSetting;

                    BindResult(result.Result.Take(this.QueryPagingSetting.PageSize).ToList(), isToClearExistingResult);
                }
                else
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
        protected virtual async Task<TViewModelData> GetFromServer()
        {
            return await Task.FromResult(new TViewModelData());
        }

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

        private Framework.Queries.QueryOrderBySettingCollection GetQueryOrderBySettingCollection(Framework.Xaml.TableCachingItem tableCachingItem)
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

        protected virtual void GetCriteriaDDLValuesFromExtendedVM()
        {
        }

        public virtual void AdditionalInitialization() { }

        public Framework.Xaml.ActionForm.ActionItemModel GetSearchActionItemModel()
        {
            return new Framework.Xaml.ActionForm.ActionItemModel
            {
                ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem,
                Title = Framework.Resx.UIStringResource.Search,
                FontIconSettings = new Framework.Xaml.FontIconSettings { MasterFontIcon = Framework.Xaml.FontAwesomeIcons.Search, MasterFontIconFamily = "FontAwesomeSolid" },
                Command = SearchCommand,
                Position = 100,
            };
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
                            foreach(var existingGroupedResult in existings)
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
                                if(existing.Any(GetPredicateToGetAnExistingItem(item1))) // 2.1. update existing items in same group
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

