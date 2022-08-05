using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class MvcUIListSetting
    {
        public bool AdvancedQuery { get; set; } = false;
        public Framework.Models.PagedViewOptions PagedViewOption { get; set; } = Framework.Models.PagedViewOptions.List;
        public Framework.Models.ViewItemTemplateNames Template { get; set; } = Framework.Models.ViewItemTemplateNames.Details;
    }
}

