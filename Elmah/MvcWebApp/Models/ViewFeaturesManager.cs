using Elmah.Resx;
using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class ViewFeaturesManager
    {
        private readonly UIAvailableFeaturesManager _uiAvailableFeaturesManager;
        private readonly IUIStrings _localizor;
        private readonly ILogger<ViewFeaturesManager> _logger;

        public ViewFeaturesManager(
            UIAvailableFeaturesManager uiAvailableFeaturesManager,
            IUIStrings localizor,
            ILogger<ViewFeaturesManager> logger)
        {
            _uiAvailableFeaturesManager = uiAvailableFeaturesManager;
            _localizor = localizor;
            _logger = logger;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="uiParams"></param>
        /// <param name="primaryItemViewContainer"></param>
        /// <param name="listWrapperId">default "thisList" for Index.cshtml, should be different in Dashboard.cshtml</param>
        /// <param name="searchFormId">default "thisForm" for Index.cshtml, should be different in Dashboard.cshtml</param>
        /// <returns></returns>
        public UIListSettingModel GetDefaultReadonlyList(
            UIParams? uiParams,
            CrudViewContainers primaryItemViewContainer = CrudViewContainers.None,
            string listWrapperId = "thisList",
            string searchFormId = "thisForm")
        {
            var result = new UIListSettingModel
            {
                //// 1. From UI, assigned at the end of this method with default values
                //UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = listWrapperId,
                    SearchFormId = searchFormId,
                    PrimayCreateViewContainer = primaryItemViewContainer,
                    PrimayDeleteViewContainer = primaryItemViewContainer,
                    PrimayDetailsViewContainer = primaryItemViewContainer,
                    PrimayEditViewContainer = primaryItemViewContainer,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        // PagedViewOptions.EditableTable,
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.AvailableListViews[0]);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="uiParams"></param>
        /// <param name="primaryItemViewContainer"></param>
        /// <param name="listWrapperId">default "thisList" for Index.cshtml, should be different in Dashboard.cshtml</param>
        /// <param name="searchFormId">default "thisForm" for Index.cshtml, should be different in Dashboard.cshtml</param>
        /// <returns></returns>
        public UIListSettingModel GetDefaultEditableList(
            UIParams? uiParams,
            CrudViewContainers primaryItemViewContainer = CrudViewContainers.Dialog,
            string listWrapperId = "thisList",
            string searchFormId = "thisForm")
        {
            var result = new UIListSettingModel
            {
                //// 1. From UI, assigned at the end of this method with default values
                //UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = listWrapperId,
                    SearchFormId = searchFormId,
                    PrimayCreateViewContainer = primaryItemViewContainer,
                    PrimayDeleteViewContainer = primaryItemViewContainer,
                    PrimayDetailsViewContainer = primaryItemViewContainer,
                    PrimayEditViewContainer = primaryItemViewContainer,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        PagedViewOptions.EditableTable,
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.AvailableListViews[0]);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;

            return result;
        }

        private static UIParams DefaultUIParams(PagedViewOptions defaultPagedViewOption)
        {
            return new UIParams
            {
                AdvancedQuery = false, //
                PagedViewOption = defaultPagedViewOption,
                Template = defaultPagedViewOption == PagedViewOptions.EditableTable
                        ? ViewItemTemplateNames.Edit
                        : ViewItemTemplateNames.Details,
            };
        }
    }
}

