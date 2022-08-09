using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    /// <summary>
    /// <seealso cref="MvcListFeatures"/>
    /// This class is used to pass Mvc List Settings between .cshtml and controller.
    /// </summary>
    public class MvcListSetting
    {
        public bool AdvancedQuery { get; set; } = false;
        public PagedViewOptions PagedViewOption { get; set; } = PagedViewOptions.List;
        public ViewItemTemplateNames Template { get; set; } = ViewItemTemplateNames.Details;

        public bool HasListBulkActionRelated(Elmah.MvcWebApp.Models.MvcListFeatures? listFeatures)
        {
            return listFeatures != null && listFeatures.HasBulkActions;
        }

        public bool ShowListBulkActionRelated(Elmah.MvcWebApp.Models.MvcListFeatures? listFeatures)
        {
            return HasListBulkActionRelated(listFeatures) && PagedViewOption != Framework.Models.PagedViewOptions.EditableList;
        }

        public bool HasEditableList(Elmah.MvcWebApp.Models.MvcListFeatures? listFeatures)
        {
            return listFeatures != null && listFeatures.AvailablePagedViewOptions != null &&
                listFeatures.AvailablePagedViewOptions.Contains(Framework.Models.PagedViewOptions.EditableList);
        }


        public bool ShowItemUIStatus(Elmah.MvcWebApp.Models.MvcListFeatures? listFeatures)
        {
            return HasEditableList(listFeatures)
                && listFeatures != null && PagedViewOption == Framework.Models.PagedViewOptions.EditableList;
        }

        public bool ShowEditableListDeleteSelect(Elmah.MvcWebApp.Models.MvcListFeatures? listFeatures)
        {
            return HasEditableList(listFeatures) &&
                PagedViewOption == Framework.Models.PagedViewOptions.EditableList &&
                listFeatures != null && listFeatures.CanDelete;
        }

        public bool ShowItemButtons(Elmah.MvcWebApp.Models.MvcListFeatures? listFeatures)
        {
            return PagedViewOption != Framework.Models.PagedViewOptions.EditableList;
        }
    }
}

