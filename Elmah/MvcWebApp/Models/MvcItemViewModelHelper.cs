using Elmah.ServiceContracts;
using Framework.Models;
using Elmah.Resx;
using Elmah.Models;

namespace Elmah.MvcWebApp.Models
{
    public class MvcItemViewModelHelper
    {
        private readonly IDropDownListService _dropDownListService;
        private readonly SelectListHelper _selectListHelper;
        private readonly ViewFeaturesManager _viewFeaturesManager;
        private readonly IUIStrings _localizor;
        private readonly ILogger<MvcItemViewModelHelper> _logger;

        public MvcItemViewModelHelper(
            IDropDownListService dropDownListService,
            SelectListHelper selectListHelper,
            ViewFeaturesManager viewFeaturesManager,
            IUIStrings localizor,
            ILogger<MvcItemViewModelHelper> logger)
        {
            _dropDownListService = dropDownListService;
            _selectListHelper = selectListHelper;
            _viewFeaturesManager = viewFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }

        public async Task<MvcItemViewModel<ElmahErrorModel.DefaultView>> GetElmahErrorMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahErrorModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = new MvcItemViewModel<ElmahErrorModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahErrorUIItemFeatures(),
            };

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<MvcItemViewModel<ElmahApplicationModel>> GetElmahApplicationMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahApplicationModel responseBody)
        {
            var result = new MvcItemViewModel<ElmahApplicationModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahApplicationUIItemFeatures(),
            };

            return result;
        }

        public async Task<MvcItemViewModel<ElmahHostModel>> GetElmahHostMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahHostModel responseBody)
        {
            var result = new MvcItemViewModel<ElmahHostModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahHostUIItemFeatures(),
            };

            return result;
        }

        public async Task<MvcItemViewModel<ElmahSourceModel>> GetElmahSourceMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahSourceModel responseBody)
        {
            var result = new MvcItemViewModel<ElmahSourceModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahSourceUIItemFeatures(),
            };

            return result;
        }

        public async Task<MvcItemViewModel<ElmahStatusCodeModel>> GetElmahStatusCodeMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahStatusCodeModel responseBody)
        {
            var result = new MvcItemViewModel<ElmahStatusCodeModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahStatusCodeUIItemFeatures(),
            };

            return result;
        }

        public async Task<MvcItemViewModel<ElmahTypeModel>> GetElmahTypeMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahTypeModel responseBody)
        {
            var result = new MvcItemViewModel<ElmahTypeModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahTypeUIItemFeatures(),
            };

            return result;
        }

        public async Task<MvcItemViewModel<ElmahUserModel>> GetElmahUserMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahUserModel responseBody)
        {
            var result = new MvcItemViewModel<ElmahUserModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahUserUIItemFeatures(),
            };

            return result;
        }

    }
}

