using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class IndexViewFeatureManager
    {
        public UIListSettingModel GetElmahError(UIParams? uiParams)
        {
            var result = new UIListSettingModel
            {
                //// 1. From UI, assigned at the end of this method with default values
                //UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new UIListFeatures
                {
                    PrimayCreateViewContainer = CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = CrudViewContainers.Dialog,
                    PrimayEditViewContainer = CrudViewContainers.Dialog,

                    CanGotoDashboard = true,
                    CanBulkDelete = true,

                    BulkActions = null,
                    AvailableListViews = new List<PagedViewOptions>
                    {
                        PagedViewOptions.List,
                        PagedViewOptions.Tiles,
                        PagedViewOptions.SlideShow,
                        PagedViewOptions.EditableList,
                    },
                },
                // 3. Available UIViews Features
                UIAvailableFeatures = new UIAvailableFeatures
                {
                    AvailableUISearchOptions = new List<UISearchOptions> { UISearchOptions.TextSearch, UISearchOptions.RegularSearch, UISearchOptions.AdvancedSearch, UISearchOptions.DynamicSearch, UISearchOptions.SpecialSearchParameters },

                    HasCreateView = true,
                    HasDeleteView = true,
                    HasDetailsView = true,
                    HasEditView = true,
                    HasDashboard = true,
                    HasSearchView = true,

                    HasBulkDelete = true,
                    BulkActions = null,
                    AvailableListViewFeatures = new Dictionary<PagedViewOptions, ViewItemTemplateNames[]>
                    {
                        { PagedViewOptions.List, new ViewItemTemplateNames[] { ViewItemTemplateNames.Create, ViewItemTemplateNames.Delete, ViewItemTemplateNames.Details, ViewItemTemplateNames.Edit } },
                        { PagedViewOptions.Tiles, new ViewItemTemplateNames[] { ViewItemTemplateNames.Create, ViewItemTemplateNames.Delete, ViewItemTemplateNames.Details, ViewItemTemplateNames.Edit } },
                        { PagedViewOptions.SlideShow, new ViewItemTemplateNames[] { ViewItemTemplateNames.Details } },
                        { PagedViewOptions.EditableList, new ViewItemTemplateNames[] { ViewItemTemplateNames.Create, ViewItemTemplateNames.Edit } },
                    },
                }
            };

            // Default UIParams
            if (uiParams == null)
            {
                result.UIParams = new UIParams
                {
                    AdvancedQuery = false, //
                    PagedViewOption =
                        result.UIListFeatures.AvailableListViews != null && result.UIListFeatures.AvailableListViews.Count > 0
                            ? result.UIListFeatures.AvailableListViews[0]
                            : PagedViewOptions.List,
                    Template =
                        result.UIListFeatures.AvailableListViews != null && result.UIListFeatures.AvailableListViews.Count > 0 && result.UIListFeatures.AvailableListViews[0] == PagedViewOptions.EditableList
                            ? ViewItemTemplateNames.Edit
                            : ViewItemTemplateNames.Details,
                };
            }
            else
            {
                result.UIParams = uiParams;
            }
            result.UIListFeatures.IsArrayBinding = result.UIParams.PagedViewOption == PagedViewOptions.EditableList;

            return result;
        }
    }
}

