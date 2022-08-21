using Elmah.Models;
using Elmah.Resx;
using Framework.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public void DefaultUIParamsIfNeeds(Framework.Models.UIParams uiParams, Framework.Models.PagedViewOptions defaultPagedViewOption)
        {
            if (!uiParams.PagedViewOption.HasValue)
            {
                uiParams.PagedViewOption = defaultPagedViewOption;
            }
            if (uiParams.PagedViewOption == Framework.Models.PagedViewOptions.EditableTable)
            {
                uiParams.Template = ViewItemTemplateNames.Edit;
            }
            else if(!uiParams.Template.HasValue)
            {
                uiParams.Template = Framework.Models.ViewItemTemplateNames.Delete;
            }
        }

        public Framework.Models.PaginationOptions HardCodePaginationOption(Framework.Models.PagedViewOptions pagedViewOption, Framework.Models.PaginationOptions original)
        {
            if (original == Framework.Models.PaginationOptions.NoPagination)
                return Framework.Models.PaginationOptions.NoPagination;
            else if (pagedViewOption == Framework.Models.PagedViewOptions.Table || pagedViewOption == Framework.Models.PagedViewOptions.EditableTable)
                return Framework.Models.PaginationOptions.Paged;
            return original;
        }

        public Framework.Models.UIItemFeatures GetElmahApplicationUIItemFeatures()
        {
            var result = new Framework.Models.UIItemFeatures
            {
                PrimayCreateViewContainer = Framework.Models.CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = Framework.Models.CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = Framework.Models.CrudViewContainers.Dialog,
                PrimayEditViewContainer = Framework.Models.CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

            return result;
        }


        public UIItemFeatures GetElmahErrorUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Dialog,
                PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.Dialog,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">for ListWrapperId and SearchFormId</param>
        /// <param name="uiParams"></param>
        /// <returns></returns>
        public Framework.Models.UIListSettingModel GetElmahErrorUIListSetting(string key, Framework.Models.UIParams uiParams)
        {
            var result = new Framework.Models.UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new Framework.Models.UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = PagedViewOptions.Table,

                    PrimayCreateViewContainer = Framework.Models.CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = Framework.Models.CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = Framework.Models.CrudViewContainers.Dialog,
                    PrimayEditViewContainer = Framework.Models.CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<Framework.Models.PagedViewOptions>
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
        public Framework.Models.UIItemFeatures GetDefaultReadonlyUIItemFeatures(
            CrudViewContainers primaryItemViewContainer = CrudViewContainers.Dialog)
        {
            var result = new Framework.Models.UIItemFeatures
            {
                PrimayCreateViewContainer = primaryItemViewContainer,
                PrimayDeleteViewContainer = primaryItemViewContainer,
                PrimayDetailsViewContainer = primaryItemViewContainer,
                PrimayEditViewContainer = primaryItemViewContainer,

                ShowItemButtons = true,
                CanGotoDashboard = false,
            };

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

