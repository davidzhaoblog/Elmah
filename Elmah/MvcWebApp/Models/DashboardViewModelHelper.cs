using Elmah.ServiceContracts;
using Framework.Models;
using Elmah.Models;
using Elmah.Resx;
namespace Elmah.MvcWebApp.Models
{
    public class DashboardViewModelHelper
    {
        private readonly IDropDownListService _dropDownListService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeaturesManager;
        private readonly MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly PagedSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly IUIStrings _localizor;
        private readonly ILogger<DashboardViewModelHelper> _logger;

        public DashboardViewModelHelper(
            IDropDownListService dropDownListService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeaturesManager,
            MvcItemViewModelHelper mvcItemViewModelHelper,
            PagedSearchViewModelHelper pagedSearchViewModelHelper,
            IUIStrings localizor,
            ILogger<DashboardViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _mvcItemViewModelHelper = mvcItemViewModelHelper;
            _pagedSearchViewModelHelper = pagedSearchViewModelHelper;
            _localizor = localizor;
            _logger = logger;
        }

        public async Task<MvcItemViewModel<ElmahErrorModel.DefaultView>> GetElmahErrorMvcItemViewModel(
            ElmahErrorModel.DefaultView responseBody,
            CompositeItemModel compositeItem,
            bool loadTopLevelDropDownListsFromDatabase = false,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase = null)
        {
            return await _mvcItemViewModelHelper.GetElmahErrorMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody, loadTopLevelDropDownListsFromDatabase, topLevelDropDownListsFromDatabase);
        }
        public async Task<PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorModel.DefaultView[]>> GetElmahErrorPagedSearchViewModel(
            ElmahErrorAdvancedQuery query,
            ElmahErrorModel.DefaultView[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahErrorPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahApplicationModel>> GetElmahApplicationMvcItemViewModel(
            ElmahApplicationModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahApplicationMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationModel[]>> GetElmahApplicationPagedSearchViewModel(
            ElmahApplicationAdvancedQuery query,
            ElmahApplicationModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahApplicationPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahHostModel>> GetElmahHostMvcItemViewModel(
            ElmahHostModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahHostMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahHostAdvancedQuery, ElmahHostModel[]>> GetElmahHostPagedSearchViewModel(
            ElmahHostAdvancedQuery query,
            ElmahHostModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahHostPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahSourceModel>> GetElmahSourceMvcItemViewModel(
            ElmahSourceModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahSourceMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceModel[]>> GetElmahSourcePagedSearchViewModel(
            ElmahSourceAdvancedQuery query,
            ElmahSourceModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahSourcePagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahStatusCodeModel>> GetElmahStatusCodeMvcItemViewModel(
            ElmahStatusCodeModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahStatusCodeMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeModel[]>> GetElmahStatusCodePagedSearchViewModel(
            ElmahStatusCodeAdvancedQuery query,
            ElmahStatusCodeModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahStatusCodePagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahTypeModel>> GetElmahTypeMvcItemViewModel(
            ElmahTypeModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahTypeMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeModel[]>> GetElmahTypePagedSearchViewModel(
            ElmahTypeAdvancedQuery query,
            ElmahTypeModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahTypePagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahUserModel>> GetElmahUserMvcItemViewModel(
            ElmahUserModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahUserMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserModel[]>> GetElmahUserPagedSearchViewModel(
            ElmahUserAdvancedQuery query,
            ElmahUserModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahUserPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }
    }
}

