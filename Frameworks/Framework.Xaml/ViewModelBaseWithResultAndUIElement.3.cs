using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
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
}

