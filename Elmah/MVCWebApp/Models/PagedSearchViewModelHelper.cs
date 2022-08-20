using Elmah.Models;
using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class PagedSearchViewModelHelper
    {
        private readonly Elmah.ServiceContracts.IDropDownListService _dropDownListService;
        private readonly Elmah.MvcWebApp.Models.OrderBysListHelper _orderBysListHelper;
        private readonly Elmah.MvcWebApp.Models.SelectListHelper _selectListHelper;
        private readonly Elmah.MvcWebApp.Models.ViewFeaturesManager _viewFeaturesManager;
        private readonly Elmah.Resx.IUIStrings _localizor;
        private readonly ILogger<DashboardViewModelHelper> _logger;

        public PagedSearchViewModelHelper(
            Elmah.ServiceContracts.IDropDownListService dropDownListService,
            Elmah.MvcWebApp.Models.OrderBysListHelper orderBysListHelper,
            Elmah.MvcWebApp.Models.SelectListHelper selectListHelper,
            Elmah.MvcWebApp.Models.ViewFeaturesManager viewFeaturesManager,
            Elmah.Resx.IUIStrings localizor,
            ILogger<DashboardViewModelHelper> logger)
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
            var result = new PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorModel.DefaultView[]>
            {
                Query = query,
                Result = response,
                OrderByList = _orderBysListHelper.GetElmahErrorOrderBys(),
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                UIListSetting = _viewFeaturesManager.GetElmahErrorUIListSetting(key, uiParams),
            };
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
    }
}
