using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public static class IndexViewFeatures
    {
        public static MvcListFeatures GetElmahErrorEditableList()
        {
            return new MvcListFeatures
            {
                 //AvailablePagedViewOptions = new PagedViewOptions[] { PagedViewOptions.EditableList, PagedViewOptions.List, PagedViewOptions.Tiles, PagedViewOptions.SlideShow },
                 BindingPath = "",
                //HasBulkActions = true, ShowCRUDButtons = true, ShowToolbar = true
            };
        }
    }
}

