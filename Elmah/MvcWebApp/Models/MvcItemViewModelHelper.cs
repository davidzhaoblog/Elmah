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
            Response<PaginationResponse> response,
            ElmahErrorDataModel.DefaultView responseBody,
            bool loadTopLevelDropDownListsFromDatabase,
            Dictionary<string, List<NameValuePair>>? topLevelDropDownListsFromDatabase)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahErrorDataModel.DefaultView>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahErrorUIItemFeatures(uiParams.PagedViewOption ?? PagedViewOptions.Table),
            });

            if(loadTopLevelDropDownListsFromDatabase && (topLevelDropDownListsFromDatabase == null || !topLevelDropDownListsFromDatabase.Any()))
            {
                result.TopLevelDropDownListsFromDatabase = await _dropDownListService.GetElmahErrorTopLevelDropDownListsFromDatabase();
            }

            return result;
        }

        public async Task<MvcItemViewModel<ElmahApplicationDataModel>> GetElmahApplicationMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ElmahApplicationDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahApplicationDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahApplicationUIItemFeatures(uiParams.PagedViewOption ?? PagedViewOptions.Table),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahHostDataModel>> GetElmahHostMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ElmahHostDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahHostDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahHostUIItemFeatures(uiParams.PagedViewOption ?? PagedViewOptions.Table),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahSourceDataModel>> GetElmahSourceMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ElmahSourceDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahSourceDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahSourceUIItemFeatures(uiParams.PagedViewOption ?? PagedViewOptions.Table),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahStatusCodeDataModel>> GetElmahStatusCodeMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ElmahStatusCodeDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahStatusCodeDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahStatusCodeUIItemFeatures(uiParams.PagedViewOption ?? PagedViewOptions.Table),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahTypeDataModel>> GetElmahTypeMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ElmahTypeDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahTypeDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahTypeUIItemFeatures(uiParams.PagedViewOption ?? PagedViewOptions.Table),
            });

            return result;
        }

        public async Task<MvcItemViewModel<ElmahUserDataModel>> GetElmahUserMvcItemViewModel(
            UIParams uiParams,
            Response<PaginationResponse> response,
            ElmahUserDataModel responseBody)
        {
            var result = await Task.FromResult(new MvcItemViewModel<ElmahUserDataModel>
            {
                Model = responseBody,
                Status = response.Status,
                StatusMessage = response.StatusMessage,
                Template = uiParams.Template ?? ViewItemTemplateNames.Details.ToString(),
                UIItemFeatures = _viewFeaturesManager.GetElmahUserUIItemFeatures(uiParams.PagedViewOption ?? PagedViewOptions.Table),
            });

            return result;
        }

    }
}

