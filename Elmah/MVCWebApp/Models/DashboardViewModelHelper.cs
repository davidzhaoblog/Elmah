using Elmah.Models;
using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class DashboardViewModelHelper
    {
        private readonly Elmah.ServiceContracts.IDropDownListService _dropDownListService;
        private readonly Elmah.MvcWebApp.Models.SelectListHelper _selectListHelper;
        private readonly Elmah.MvcWebApp.Models.ViewFeaturesManager _viewFeaturesManager;
        private readonly Elmah.MvcWebApp.Models.MvcItemViewModelHelper _mvcItemViewModelHelper;
        private readonly Elmah.MvcWebApp.Models.PagedSearchViewModelHelper _pagedSearchViewModelHelper;
        private readonly Elmah.Resx.IUIStrings _localizor;
        private readonly ILogger<DashboardViewModelHelper> _logger;

        public DashboardViewModelHelper(
            Elmah.ServiceContracts.IDropDownListService dropDownListService,
            Elmah.MvcWebApp.Models.SelectListHelper selectListHelper,
            Elmah.MvcWebApp.Models.ViewFeaturesManager viewFeaturesManager,
            Elmah.MvcWebApp.Models.MvcItemViewModelHelper mvcItemViewModelHelper,
            Elmah.MvcWebApp.Models.PagedSearchViewModelHelper pagedSearchViewModelHelper,
            Elmah.Resx.IUIStrings localizor,
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

        public async Task<Elmah.MvcWebApp.Models.MvcItemViewModel<Elmah.Models.ElmahApplicationModel>> GetElmahApplicationMvcItemViewModel(
            Elmah.Models.ElmahApplicationModel responseBody,
            Framework.Models.CompositeItemModel compositeItem)
        {
            return await _mvcItemViewModelHelper.GetElmahApplicationMvcItemViewModel(compositeItem.UIParams, compositeItem.Response, responseBody, false, null);
        }

        public async Task<Framework.Models.PagedSearchViewModel<Elmah.Models.ElmahErrorAdvancedQuery, Elmah.Models.ElmahErrorModel.DefaultView[]>> GetElmahErrorPagedSearchViewModel(
            Elmah.Models.ElmahErrorAdvancedQuery query,
            Elmah.Models.ElmahErrorModel.DefaultView[]? responseBody,
            Framework.Models.CompositeItemModel compositeItem)
        {
            return await _pagedSearchViewModelHelper.GetElmahErrorPagedSearchViewModel(compositeItem.Key, compositeItem.UIParams, query, compositeItem.Response, responseBody, false, false);
        }
    }
}
