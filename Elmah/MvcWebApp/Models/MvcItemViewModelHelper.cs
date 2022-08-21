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

        public async Task<MvcItemViewModel<ElmahErrorDataModel.DefaultView>> GetElmahErrorMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahErrorDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahErrorDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahErrorUIItemFeatures(),
            });

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<MvcItemViewModel<ElmahApplicationDataModel>> GetElmahApplicationMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahApplicationDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahApplicationDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahApplicationUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahHostDataModel>> GetElmahHostMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahHostDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahHostDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahHostUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahSourceDataModel>> GetElmahSourceMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahSourceDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahSourceDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahSourceUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahStatusCodeDataModel>> GetElmahStatusCodeMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahStatusCodeDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahStatusCodeDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahStatusCodeUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahTypeDataModel>> GetElmahTypeMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahTypeDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahTypeDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahTypeUIItemFeatures(),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahUserDataModel>> GetElmahUserMvcItemViewModel(
            UIParams uiParams,
            Response response,
            ElmahUserDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahUserDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template.HasValue ? uiParams.Template.ToString() : ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahUserUIItemFeatures(),
            });

            return result;
        }

    }
}

