using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    /// <summary>
    /// <seealso cref="Elmah.MvcWebApp.Models.MvcListConfiguration"/>
    /// This class is used to pass Mvc List Settings between .cshtml and controller.
    /// </summary>
    public class MvcListSetting
    {
        public bool AdvancedQuery { get; set; } = false;
        public Framework.Models.PagedViewOptions PagedViewOption { get; set; } = Framework.Models.PagedViewOptions.List;
        public Framework.Models.ViewItemTemplateNames Template { get; set; } = Framework.Models.ViewItemTemplateNames.Details;
    }
}

