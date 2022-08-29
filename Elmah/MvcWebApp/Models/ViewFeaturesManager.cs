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

        public void DefaultUIParamsIfNeeds(UIParams uiParams, ListViewOptions defaultPagedViewOption)
        {
            if (!uiParams.PagedViewOption.HasValue)
            {
                uiParams.PagedViewOption = defaultPagedViewOption;
            }
            if (uiParams.PagedViewOption == ListViewOptions.EditableTable)
            {
                uiParams.Template = ViewItemTemplates.Edit.ToString();
            }
            else if(string.IsNullOrEmpty(uiParams.Template))
            {
                uiParams.Template = ViewItemTemplates.Delete.ToString();
            }
        }

        public PaginationOptions HardCodePaginationOption(ListViewOptions pagedViewOption, PaginationOptions original)
        {
            if (original == PaginationOptions.NoPagination)
                return PaginationOptions.NoPagination;
            else if (pagedViewOption == ListViewOptions.Table || pagedViewOption == ListViewOptions.EditableTable)
                return PaginationOptions.PageIndexesAndAllButtons;
            return original;
        }

        private static UIParams DefaultUIParams(ListViewOptions defaultPagedViewOption)
        {
            return new UIParams
            {
                AdvancedQuery = false, //
                PagedViewOption = defaultPagedViewOption,
                Template = defaultPagedViewOption == ListViewOptions.EditableTable
                        ? ViewItemTemplates.Edit.ToString()
                        : ViewItemTemplates.Details.ToString(),
            };
        }

        public UIItemFeatures GetElmahErrorUIItemFeatures(ListViewOptions pagedViewOptionForBulkSelectCheckBox)
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.None,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                PrimayEditViewContainer = CrudViewContainers.None,

                ShowListBulkSelectCheckbox = pagedViewOptionForBulkSelectCheckBox != ListViewOptions.Card,
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

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.None,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.None,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = true,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahApplicationUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Inline,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Inline,
                PrimayEditViewContainer = CrudViewContainers.Inline,

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

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahHostUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Inline,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Inline,
                PrimayEditViewContainer = CrudViewContainers.Inline,

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

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahSourceUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Inline,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Inline,
                PrimayEditViewContainer = CrudViewContainers.Inline,

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

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahStatusCodeUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Inline,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Inline,
                PrimayEditViewContainer = CrudViewContainers.Inline,

                ShowItemButtons = true,
                CanGotoDashboard = true,
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

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = true,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahTypeUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Inline,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Inline,
                PrimayEditViewContainer = CrudViewContainers.Inline,

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

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }

        public UIItemFeatures GetElmahUserUIItemFeatures()
        {
            var result = new UIItemFeatures
            {
                PrimayCreateViewContainer = CrudViewContainers.Inline,
                PrimayDeleteViewContainer = CrudViewContainers.None,
                PrimayDetailsViewContainer = CrudViewContainers.Inline,
                PrimayEditViewContainer = CrudViewContainers.Inline,

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

                    PrimaryPagedViewOption = ListViewOptions.Table,

                    PrimayCreateViewContainer = CrudViewContainers.Inline,
                    PrimayDeleteViewContainer = CrudViewContainers.None,
                    PrimayDetailsViewContainer = CrudViewContainers.Inline,
                    PrimayEditViewContainer = CrudViewContainers.Inline,

                    CanGotoDashboard = false,
                    CanBulkDelete = false,
                    CanBulkActions = false,

                    AvailableListViews = new List<ListViewOptions>
                    {
                        ListViewOptions.Table
                    },
                },
            };

            result.UIParams = uiParams ?? DefaultUIParams(result.UIListFeatures.PrimaryPagedViewOption);
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == ListViewOptions.EditableTable;
            return result;
        }
    }
}

