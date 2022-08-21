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

        public async Task<MvcItemViewModel<ElmahErrorDataModel.DefaultView>> GetElmahErrorMvcItemViewModel(
            ElmahErrorDataModel.DefaultView responseBody,
            CompositeItemModel compositeItem,
            bool loadTopLevelDropDownListsFromDatabase = false,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase = null)
        {
            return await _mvcItemViewModelHelper.GetElmahErrorMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody, loadTopLevelDropDownListsFromDatabase, topLevelDropDownListsFromDatabase);
        }
        public async Task<PagedSearchViewModel<ElmahErrorAdvancedQuery, ElmahErrorDataModel.DefaultView[]>> GetElmahErrorPagedSearchViewModel(
            ElmahErrorAdvancedQuery query,
            ElmahErrorDataModel.DefaultView[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahErrorPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahApplicationDataModel>> GetElmahApplicationMvcItemViewModel(
            ElmahApplicationDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahApplicationMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahApplicationAdvancedQuery, ElmahApplicationDataModel[]>> GetElmahApplicationPagedSearchViewModel(
            ElmahApplicationAdvancedQuery query,
            ElmahApplicationDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahApplicationPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahHostDataModel>> GetElmahHostMvcItemViewModel(
            ElmahHostDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahHostMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahHostAdvancedQuery, ElmahHostDataModel[]>> GetElmahHostPagedSearchViewModel(
            ElmahHostAdvancedQuery query,
            ElmahHostDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahHostPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahSourceDataModel>> GetElmahSourceMvcItemViewModel(
            ElmahSourceDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahSourceMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahSourceAdvancedQuery, ElmahSourceDataModel[]>> GetElmahSourcePagedSearchViewModel(
            ElmahSourceAdvancedQuery query,
            ElmahSourceDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahSourcePagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahStatusCodeDataModel>> GetElmahStatusCodeMvcItemViewModel(
            ElmahStatusCodeDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahStatusCodeMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahStatusCodeAdvancedQuery, ElmahStatusCodeDataModel[]>> GetElmahStatusCodePagedSearchViewModel(
            ElmahStatusCodeAdvancedQuery query,
            ElmahStatusCodeDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahStatusCodePagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahTypeDataModel>> GetElmahTypeMvcItemViewModel(
            ElmahTypeDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahTypeMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahTypeAdvancedQuery, ElmahTypeDataModel[]>> GetElmahTypePagedSearchViewModel(
            ElmahTypeAdvancedQuery query,
            ElmahTypeDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahTypePagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }

        public async Task<MvcItemViewModel<ElmahUserDataModel>> GetElmahUserMvcItemViewModel(
            ElmahUserDataModel responseBody,
            CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahUserMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody);
        }
        public async Task<PagedSearchViewModel<ElmahUserAdvancedQuery, ElmahUserDataModel[]>> GetElmahUserPagedSearchViewModel(
            ElmahUserAdvancedQuery query,
            ElmahUserDataModel[]? responseBody,
            CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahUserPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }
    }
}

