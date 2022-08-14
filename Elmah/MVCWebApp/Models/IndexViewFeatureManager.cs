using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class IndexViewFeatureManager
    {
        public Framework.Models.UIListSettingModel GetElmahError(Framework.Models.UIParams? uiParams)
        {
            var result = new Framework.Models.UIListSettingModel
            {
                //// 1. From UI, assigned at the end of this method with default values
                //UIParams = uiParams,
                // 2. Customized By Developer
                UIListFeatures = new Framework.Models.UIListFeatures
                {
                    PrimayCreateViewContainer = Framework.Models.CrudViewContainers.Dialog,
                    PrimayDeleteViewContainer = Framework.Models.CrudViewContainers.Dialog,
                    PrimayDetailsViewContainer = Framework.Models.CrudViewContainers.Dialog,
                    PrimayEditViewContainer = Framework.Models.CrudViewContainers.Dialog,

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
                // 3. Available UIViews Features
                UIAvailableFeatures = new Framework.Models.UIAvailableFeatures
                {
                    AvailableUISearchOptions = new List<Framework.Models.UISearchOptions> { Framework.Models.UISearchOptions.TextSearch, Framework.Models.UISearchOptions.RegularSearch, Framework.Models.UISearchOptions.AdvancedSearch, Framework.Models.UISearchOptions.DynamicSearch, Framework.Models.UISearchOptions.SpecialSearchParameters },

                    HasCreateView = true,
                    HasDeleteView = true,
                    HasDetailsView = true,
                    HasEditView = true,
                    HasDashboard = true,
                    HasSearchView = true,

                    HasBulkDelete = true,
                    BulkActions = null,
                    AvailableListViewFeatures = new Dictionary<Framework.Models.PagedViewOptions, Framework.Models.ViewItemTemplateNames[]>
                    {
                        { Framework.Models.PagedViewOptions.List, new Framework.Models.ViewItemTemplateNames[] { Framework.Models.ViewItemTemplateNames.Create, Framework.Models.ViewItemTemplateNames.Delete, Framework.Models.ViewItemTemplateNames.Details, Framework.Models.ViewItemTemplateNames.Edit } },
                        { Framework.Models.PagedViewOptions.Tiles, new Framework.Models.ViewItemTemplateNames[] { Framework.Models.ViewItemTemplateNames.Create, Framework.Models.ViewItemTemplateNames.Delete, Framework.Models.ViewItemTemplateNames.Details, Framework.Models.ViewItemTemplateNames.Edit } },
                        { Framework.Models.PagedViewOptions.SlideShow, new Framework.Models.ViewItemTemplateNames[] { Framework.Models.ViewItemTemplateNames.Details } },
                        { Framework.Models.PagedViewOptions.EditableList, new Framework.Models.ViewItemTemplateNames[] { Framework.Models.ViewItemTemplateNames.Create, Framework.Models.ViewItemTemplateNames.Edit } },
                    },
                }
            };

            // Default UIParams 
            if (uiParams == null)
            {
                result.UIParams = new Framework.Models.UIParams
                {
                    AdvancedQuery = false, //
                    PagedViewOption =
                        result.UIListFeatures.AvailableListViews != null && result.UIListFeatures.AvailableListViews.Count > 0
                            ? result.UIListFeatures.AvailableListViews[0]
                            : Framework.Models.PagedViewOptions.List,
                    Template =
                        result.UIListFeatures.AvailableListViews != null && result.UIListFeatures.AvailableListViews.Count > 0 && result.UIListFeatures.AvailableListViews[0] == Framework.Models.PagedViewOptions.EditableList
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
