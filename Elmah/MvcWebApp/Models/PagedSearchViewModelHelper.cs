using Elmah.ServiceContracts;
using Framework.Models;
using Elmah.Models;
using Elmah.Resx;
namespace Elmah.MvcWebApp.Models
{
    public class PagedSearchViewModelHelper
    {
        private readonly IDropDownListService _dropDownListService;
        private readonly OrderBysListHelper _orderBysListHelper;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeaturesManager;
        private readonly IUIStrings _localizor;
        private readonly ILogger<PagedSearchViewModelHelper> _logger;

        public PagedSearchViewModelHelper(
            IDropDownListService dropDownListService,
            OrderBysListHelper orderBysListHelper,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeaturesManager,
            IUIStrings localizor,
            ILogger<PagedSearchViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _orderBysListHelper = orderBysListHelper;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }

        public async Task<PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorDataModel.DefaultView[]>> GetElmahErrorPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahErrorAdvancedQuery query,
            Response<PaginationResponse> response,
            ElmahErrorDataModel.DefaultView[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahErrorPagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahErrorDataModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorDataModel.DefaultView[]>> GetElmahErrorPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahErrorAdvancedQuery query,
            PagedResponse<ElmahErrorDataModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorDataModel.DefaultView[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetElmahErrorOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetElmahErrorUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

            otherDropDownLists.Add(nameof(ElmahErrorAdvancedQuery.TimeUtcRange), _selectListHelper.GetDefaultPredefinedDateTimeRange());
                result.OtherDropDownLists = otherDropDownLists;
            }
            if (loadTopLevelDropDownListsFromDatabase)
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationDataModel[]>> GetElmahApplicationPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahApplicationAdvancedQuery query,
            Response<PaginationResponse> response,
            ElmahApplicationDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahApplicationPagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahApplicationDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationDataModel[]>> GetElmahApplicationPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahApplicationAdvancedQuery query,
            PagedResponse<ElmahApplicationDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetElmahApplicationOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetElmahApplicationUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<PagedSearchViewModel<ElmahHostAdvancedQuery, ElmahHostDataModel[]>> GetElmahHostPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahHostAdvancedQuery query,
            Response<PaginationResponse> response,
            ElmahHostDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahHostPagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahHostDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahHostAdvancedQuery, ElmahHostDataModel[]>> GetElmahHostPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahHostAdvancedQuery query,
            PagedResponse<ElmahHostDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahHostAdvancedQuery, ElmahHostDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetElmahHostOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetElmahHostUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceDataModel[]>> GetElmahSourcePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahSourceAdvancedQuery query,
            Response<PaginationResponse> response,
            ElmahSourceDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahSourcePagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahSourceDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceDataModel[]>> GetElmahSourcePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahSourceAdvancedQuery query,
            PagedResponse<ElmahSourceDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetElmahSourceOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetElmahSourceUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeDataModel[]>> GetElmahStatusCodePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahStatusCodeAdvancedQuery query,
            Response<PaginationResponse> response,
            ElmahStatusCodeDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahStatusCodePagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahStatusCodeDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeDataModel[]>> GetElmahStatusCodePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahStatusCodeAdvancedQuery query,
            PagedResponse<ElmahStatusCodeDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetElmahStatusCodeOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetElmahStatusCodeUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeDataModel[]>> GetElmahTypePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahTypeAdvancedQuery query,
            Response<PaginationResponse> response,
            ElmahTypeDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahTypePagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahTypeDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeDataModel[]>> GetElmahTypePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahTypeAdvancedQuery query,
            PagedResponse<ElmahTypeDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetElmahTypeOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetElmahTypeUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

        public async Task<PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserDataModel[]>> GetElmahUserPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahUserAdvancedQuery query,
            Response<PaginationResponse> response,
            ElmahUserDataModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahUserPagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahUserDataModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserDataModel[]>> GetElmahUserPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahUserAdvancedQuery query,
            PagedResponse<ElmahUserDataModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserDataModel[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetElmahUserOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetElmahUserUIListSetting(key, uiParams),
            });
            if (loadSearchRelatedDropDownLists)
            {
                result.TextSearchTypeList = _selectListHelper.GetTextSearchTypeList();

                var otherDropDownLists = new Dictionary<string, List<NameValuePair>>();

                result.OtherDropDownLists = otherDropDownLists;
            }

            return result;
        }

    }
}

