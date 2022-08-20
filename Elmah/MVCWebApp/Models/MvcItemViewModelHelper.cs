using Elmah.Models;
using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class MvcItemViewModelHelper
    {
        private readonly Elmah.ServiceContracts.IDropDownListService _dropDownListService;
        private readonly Elmah.MvcWebApp.Models.SelectListHelper _selectListHelper;
        private readonly Elmah.MvcWebApp.Models.ViewFeaturesManager _viewFeaturesManager;
        private readonly Elmah.Resx.IUIStrings _localizor;
        private readonly ILogger<MvcItemViewModelHelper> _logger;

        public MvcItemViewModelHelper(
            Elmah.ServiceContracts.IDropDownListService dropDownListService,
            Elmah.MvcWebApp.Models.SelectListHelper selectListHelper,
            Elmah.MvcWebApp.Models.ViewFeaturesManager viewFeaturesManager,
            Elmah.Resx.IUIStrings localizor,
            ILogger<MvcItemViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }

        public async Task<Elmah.MvcWebApp.Models.MvcItemViewModel<Elmah.Models.ElmahApplicationModel>> GetElmahApplicationMvcItemViewModel(
            Framework.Models.UIParams uiParams,
            Framework.Models.Response response,
            Elmah.Models.ElmahApplicationModel responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<Framework.Models.NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = new Elmah.MvcWebApp.Models.MvcItemViewModel<Elmah.Models.ElmahApplicationModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : Framework.Models.ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahApplicationUIItemFeatures(),
            };

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();
            }

            return result;
        }
    }
}
