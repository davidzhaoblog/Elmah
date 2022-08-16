using Elmah.MvcWebApp.Controllers;
using Elmah.Resx;
using Elmah.ServiceContracts;
using Elmah.Services;
using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class ViewFeaturesManager
    {
        private readonly Elmah.MvcWebApp.Models.UIAvailableFeaturesManager _uiAvailableFeaturesManager;
        private readonly IUIStrings _localizor;
        private readonly ILogger<Elmah.MvcWebApp.Models.ViewFeaturesManager> _logger;

        public ViewFeaturesManager(
            Elmah.MvcWebApp.Models.UIAvailableFeaturesManager uiAvailableFeaturesManager,
            IUIStrings localizor,
            ILogger<Elmah.MvcWebApp.Models.ViewFeaturesManager> logger)
        {
            _uiAvailableFeaturesManager = uiAvailableFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }


        public Framework.Models.UIListSettingModel GetDefaultReadonly(
            Framework.Models.UIParams? uiParams,
            Framework.Models.CrudViewContainers primaryItemViewContainer = Framework.Models.CrudViewContainers.None,
            string listWrapperId = "thisList",
            string searchFormId = "thisForm")
        {
            var result = new Framework.Models.UIListSettingModel
            {
                //// 1. From UI, assigned at the end of this method with default values
                //UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new Framework.Models.UIListFeatures
                {
                    ListWrapperId = listWrapperId,
                    SearchFormId = searchFormId,
                    PrimayCreateViewContainer = primaryItemViewContainer,
                    PrimayDeleteViewContainer = primaryItemViewContainer,
                    PrimayDetailsViewContainer = primaryItemViewContainer,
                    PrimayEditViewContainer = primaryItemViewContainer,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,

                    BulkActions = null,
                    AvailableListViews = new List<Framework.Models.PagedViewOptions>
                    {
                        Framework.Models.PagedViewOptions.List,
                        Framework.Models.PagedViewOptions.Tiles,
                        Framework.Models.PagedViewOptions.SlideShow,
                        // Framework.Models.PagedViewOptions.EditableList,
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.AvailableListViews[0]);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableList;

            return result;
        }

        public Framework.Models.UIListSettingModel GetDefaultEditable(
            Framework.Models.UIParams? uiParams,
            Framework.Models.CrudViewContainers primaryItemViewContainer = Framework.Models.CrudViewContainers.Dialog,
            string listWrapperId = "thisList", 
            string searchFormId = "thisForm")
        {
            var result = new Framework.Models.UIListSettingModel
            {
                //// 1. From UI, assigned at the end of this method with default values
                //UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new Framework.Models.UIListFeatures
                {
                    ListWrapperId = listWrapperId,
                    SearchFormId = searchFormId,
                    PrimayCreateViewContainer = primaryItemViewContainer,
                    PrimayDeleteViewContainer = primaryItemViewContainer,
                    PrimayDetailsViewContainer = primaryItemViewContainer,
                    PrimayEditViewContainer = primaryItemViewContainer,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,

                    BulkActions = null,
                    AvailableListViews = new List<Framework.Models.PagedViewOptions>
                    {
                        Framework.Models.PagedViewOptions.List,
                        Framework.Models.PagedViewOptions.Tiles,
                        Framework.Models.PagedViewOptions.SlideShow,
                        Framework.Models.PagedViewOptions.EditableList,
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.AvailableListViews[0]);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableList;

            return result;
        }

        private static Framework.Models.UIParams DefaultUIParams(Framework.Models.PagedViewOptions defaultPagedViewOption)
        {
            return new Framework.Models.UIParams
            {
                AdvancedQuery = false, //
                PagedViewOption = defaultPagedViewOption,
                Template = defaultPagedViewOption == Framework.Models.PagedViewOptions.EditableList
                        ? ViewItemTemplateNames.Edit
                        : ViewItemTemplateNames.Details,
            };
        }
    }
}
