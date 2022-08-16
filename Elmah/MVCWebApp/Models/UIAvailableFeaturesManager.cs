using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class UIAvailableFeaturesManager
    {
        public Framework.Models.UIAvailableFeatures GetFullAvailableFeatures(Framework.Models.UIParams? uiParams)
        {
            return new Framework.Models.UIAvailableFeatures
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
            };
        }
    }
}
