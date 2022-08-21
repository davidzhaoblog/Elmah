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

        public async Task<PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorModel.DefaultView[]>> GetElmahErrorPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahErrorAdvancedQuery query,
            Response response,
            ElmahErrorModel.DefaultView[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahErrorPagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahErrorModel.DefaultView[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorModel.DefaultView[]>> GetElmahErrorPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahErrorAdvancedQuery query,
            PagedResponse<ElmahErrorModel.DefaultView[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorModel.DefaultView[]>
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

        public async Task<PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationModel[]>> GetElmahApplicationPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahApplicationAdvancedQuery query,
            Response response,
            ElmahApplicationModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahApplicationPagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahApplicationModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationModel[]>> GetElmahApplicationPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahApplicationAdvancedQuery query,
            PagedResponse<ElmahApplicationModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationModel[]>
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

        public async Task<PagedSearchViewModel<ElmahHostAdvancedQuery, ElmahHostModel[]>> GetElmahHostPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahHostAdvancedQuery query,
            Response response,
            ElmahHostModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahHostPagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahHostModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahHostAdvancedQuery, ElmahHostModel[]>> GetElmahHostPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahHostAdvancedQuery query,
            PagedResponse<ElmahHostModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahHostAdvancedQuery, ElmahHostModel[]>
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

        public async Task<PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceModel[]>> GetElmahSourcePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahSourceAdvancedQuery query,
            Response response,
            ElmahSourceModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahSourcePagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahSourceModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceModel[]>> GetElmahSourcePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahSourceAdvancedQuery query,
            PagedResponse<ElmahSourceModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceModel[]>
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

        public async Task<PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeModel[]>> GetElmahStatusCodePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahStatusCodeAdvancedQuery query,
            Response response,
            ElmahStatusCodeModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahStatusCodePagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahStatusCodeModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeModel[]>> GetElmahStatusCodePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahStatusCodeAdvancedQuery query,
            PagedResponse<ElmahStatusCodeModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeModel[]>
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

        public async Task<PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeModel[]>> GetElmahTypePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahTypeAdvancedQuery query,
            Response response,
            ElmahTypeModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahTypePagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahTypeModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeModel[]>> GetElmahTypePagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahTypeAdvancedQuery query,
            PagedResponse<ElmahTypeModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeModel[]>
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

        public async Task<PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserModel[]>> GetElmahUserPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahUserAdvancedQuery query,
            Response response,
            ElmahUserModel[]? responseBody,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            return await GetElmahUserPagedSearchViewModel(
                key,
                uiParams,
                query,
                new PagedResponse<ElmahUserModel[]>
                {
                    Status = response.Status,
                    StatusMessage = response.StatusMessage,
                    ResponseBody = responseBody
                },
                loadSearchRelatedDropDownLists,
                loadTopLevelDropDownListsFromDatabase);
        }

        public async Task<PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserModel[]>> GetElmahUserPagedSearchViewModel(
            string key,
            UIParams uiParams,
            ElmahUserAdvancedQuery query,
            PagedResponse<ElmahUserModel[]> response,
            bool loadSearchRelatedDropDownLists,
            bool loadTopLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserModel[]>
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

