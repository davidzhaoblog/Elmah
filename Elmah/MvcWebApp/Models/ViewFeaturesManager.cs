using Framework.Models;
using Elmah.Resx;

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

        public void DefaultUIParamsIfNeeds(UIParams uiParams, PagedViewOptions defaultPagedViewOption)
        {
            if (!uiParams.PagedViewOption.HasValue)
            {
                uiParams.PagedViewOption = defaultPagedViewOption;
            }
            if (uiParams.PagedViewOption == PagedViewOptions.EditableTable)
            {
                uiParams.Template = ViewItemTemplateNames.Edit;
            }
            else if(!uiParams.Template.HasValue)
            {
                uiParams.Template = ViewItemTemplateNames.Delete;
            }
        }

        public PaginationOptions HardCodePaginationOption(PagedViewOptions pagedViewOption, PaginationOptions original)
        {
            if (original == PaginationOptions.NoPagination)
                return PaginationOptions.NoPagination;
            else if (pagedViewOption == PagedViewOptions.Table || pagedViewOption == PagedViewOptions.EditableTable)
                return PaginationOptions.Paged;
            return original;
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

        public UIListSettingModel GetElmahErrorUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = PagedViewOptions.Card,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = true,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        PagedViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahApplicationUIItemFeatures()
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

        public UIListSettingModel GetElmahApplicationUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = PagedViewOptions.EditableTable,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        PagedViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahHostUIItemFeatures()
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

        public UIListSettingModel GetElmahHostUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = PagedViewOptions.EditableTable,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        PagedViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahSourceUIItemFeatures()
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

        public UIListSettingModel GetElmahSourceUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = PagedViewOptions.EditableTable,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        PagedViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahStatusCodeUIItemFeatures()
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

        public UIListSettingModel GetElmahStatusCodeUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = PagedViewOptions.EditableTable,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        PagedViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahTypeUIItemFeatures()
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

        public UIListSettingModel GetElmahTypeUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = PagedViewOptions.EditableTable,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        PagedViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahUserUIItemFeatures()
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

        public UIListSettingModel GetElmahUserUIListSetting(string key, UIParams uiParams)
        {
            var result = new UIListSettingModel
            {
                // 1. From UI, assigned at the end of this method with default values
                UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    ListWrapperId = key + "ListWrapper",
                    SearchFormId = key + "SearchForm",

                    PrimaryPagedViewOption = PagedViewOptions.EditableTable,

                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,
                    CanBulkActions = false,

                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.Table,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        PagedViewOptions.EditableTable
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableTable;
            return result;
        }
    }
}

