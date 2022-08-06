using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public static class IndexViewFeatures
    {
        public static Elmah.MvcWebApp.Models.MvcListFeatures GetElmahErrorEditableList()
        {
            return new MvcListFeatures 
            {
                 //AvailablePagedViewOptions = new Framework.Models.PagedViewOptions[] { Framework.Models.PagedViewOptions.EditableList, PagedViewOptions.List, Framework.Models.PagedViewOptions.Tiles, PagedViewOptions.SlideShow },
                 BindingPath = "",
                //HasBulkActions = true, ShowCRUDButtons = true, ShowToolbar = true
            };
        }
    }
}

