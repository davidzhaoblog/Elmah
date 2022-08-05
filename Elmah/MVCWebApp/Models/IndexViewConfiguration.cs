using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public static class IndexViewConfiguration
    {
        public static Elmah.MvcWebApp.Models.MvcListConfiguration GetElmahError()
        {
            return new MvcListConfiguration 
            {
                 //AvailablePagedViewOptions = new Framework.Models.PagedViewOptions[] { Framework.Models.PagedViewOptions.EditableList, PagedViewOptions.List, Framework.Models.PagedViewOptions.Tiles, PagedViewOptions.SlideShow },
                 BindingPath = "Data", 
                //HasBulkActions = true, ShowCRUDButtons = true, ShowToolbar = true
            };
        }
    }
}

