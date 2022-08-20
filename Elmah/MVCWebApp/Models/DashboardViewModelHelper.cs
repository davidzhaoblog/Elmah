using Elmah.Models;
using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class DashboardViewModelHelper
    {
        private readonly Elmah.ServiceContracts.IDropDownListService _dropDownListService;
        private readonly Elmah.MvcWebApp.Models.SelectListHelper _selectListHelper;
        private readonly Elmah.MvcWebApp.Models.ViewFeaturesManager _viewFeaturesManager;
        private readonly Elmah.Resx.IUIStrings _localizor;
        private readonly ILogger<DashboardViewModelHelper> _logger;

        public DashboardViewModelHelper(
            Elmah.ServiceContracts.IDropDownListService dropDownListService,
            Elmah.MvcWebApp.Models.SelectListHelper selectListHelper,
            Elmah.MvcWebApp.Models.ViewFeaturesManager viewFeaturesManager,
            Elmah.Resx.IUIStrings localizor,
            ILogger<DashboardViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }

        public async Task<Elmah.MvcWebApp.Models.MvcItemViewModel<Elmah.Models.ElmahApplicationModel>> GetElmahApplicationMvcItemViewModel(
            Elmah.Models.ElmahApplicationModel responseBody,
            Framework.Models.CompositeItemModel compositeItem)
        {
            return new Elmah.MvcWebApp.Models.MvcItemViewModel<Elmah.Models.ElmahApplicationModel>
            {
                Model = responseBody,
                Status = compositeItem.Response.Status,
                StatusMessage = compositeItem.Response.StatusMessage,
                Template = compositeItem.UIParams.Template.HasValue ? compositeItem.UIParams.Template.ToString() : Framework.Models.ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahApplicationUIItemFeatures(),
            };
        }

        public async Task<Framework.Models.PagedSearchViewModel<Elmah.Models.ElmahErrorAdvancedQuery, Elmah.Models.ElmahErrorModel.DefaultView[]>> GetElmahErrorPagedSearchViewModel(
            Elmah.Models.ElmahErrorAdvancedQuery query,
            Elmah.Models.ElmahErrorModel.DefaultView[]? responseBody,
            Framework.Models.CompositeItemModel compositeItem)
        {
            var orderByList = new List<Framework.Models.NameValuePair>(new[] {
                new Framework.Models.NameValuePair{ Name = String.Format("{0} A-Z", _localizor.Get("TimeUtc")), Value = "TimeUtc~ASC" },
                new Framework.Models.NameValuePair{ Name = String.Format("{0} Z-A", _localizor.Get("TimeUtc")), Value = "TimeUtc~DESC" },
            });
            if (string.IsNullOrEmpty(query.OrderBys))
            {
                query.OrderBys = orderByList.First().Value;
            }

            var uIListSetting = _viewFeaturesManager.GetElmahErrorUIListSetting(compositeItem.Key, compositeItem.UIParams);
            var result = new Framework.Models.PagedSearchViewModel<Elmah.Models.ElmahErrorAdvancedQuery, Elmah.Models.ElmahErrorModel.DefaultView[]>
            {
                Query = query,
                Result = new Framework.Models.PagedResponse<Elmah.Models.ElmahErrorModel.DefaultView[]>
                {
                    ResponseBody = responseBody,
                    Status = compositeItem.Response.Status,
                    StatusMessage = compositeItem.Response.StatusMessage,
                },
                UIListSetting = uIListSetting,
                PageSizeList = _selectListHelper.GetDefaultPageSizeList(),
                OrderByList = orderByList,
            };
            if(compositeItem.UIParams.Template == Framework.Models.ViewItemTemplateNames.Create || compositeItem.UIParams.Template == Framework.Models.ViewItemTemplateNames.Edit)
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();
            }
            return result;
        }
    }
}
