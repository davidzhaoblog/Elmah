using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class ViewModelBaseWithResultAndUIElement<TSearchCriteria, TSearchResultEntityCollection, TSearchResultEntityItem, TViewModelData>
        : ViewModelBaseWithResultAndUIElement<TSearchResultEntityItem>, Framework.ViewModels.IViewModelBase<TSearchCriteria>
        where TSearchCriteria : class, new()
        where TSearchResultEntityCollection : List<TSearchResultEntityItem>, new()
        where TSearchResultEntityItem : class, new()
        where TViewModelData : Framework.ViewModels.ViewModelBase<TSearchCriteria, TSearchResultEntityCollection>, new()
    {
        #region 1. Properties

        protected TSearchCriteria m_Criteria;

        public TSearchCriteria Criteria
        {
            get { return m_Criteria; }
            set
            {
                Set(nameof(Criteria), ref m_Criteria, value);
            }
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

        #endregion 2. Commands

        public ViewModelBaseWithResultAndUIElement()
            : base()
        {
            this.Criteria = new TSearchCriteria();

            this.TextFilterCommand = new Command<string>(this.OnTextFilterCommand);
            this.SearchCommand = new Command(this.Search, this.CanSearch);
            this.LoadMoreCommand = new Command(this.LoadMore, this.CanSearch);

            this.Criteria = new TSearchCriteria();

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
    }
}

