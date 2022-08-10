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

        public bool HasListBulkActionRelated(MvcListFeatures? listFeatures)
        {
            return listFeatures != null && listFeatures.HasBulkActions;
        }

        public bool ShowListBulkActionRelated(MvcListFeatures? listFeatures)
        {
            return HasListBulkActionRelated(listFeatures) && PagedViewOption != PagedViewOptions.EditableList;
        }

        public bool HasEditableList(MvcListFeatures? listFeatures)
        {
            return listFeatures != null && listFeatures.AvailablePagedViewOptions != null &&
                listFeatures.AvailablePagedViewOptions.Contains(PagedViewOptions.EditableList);
        }

        public bool ShowItemUIStatus(MvcListFeatures? listFeatures)
        {
            return HasEditableList(listFeatures)
                && listFeatures != null && PagedViewOption == PagedViewOptions.EditableList;
        }

        public bool ShowEditableListDeleteSelect(MvcListFeatures? listFeatures)
        {
            return HasEditableList(listFeatures) &&
                PagedViewOption == PagedViewOptions.EditableList &&
                listFeatures != null && listFeatures.CanDelete;
        }

        public bool ShowItemButtons(MvcListFeatures? listFeatures)
        {
            return PagedViewOption != PagedViewOptions.EditableList;
        }
    }
}

