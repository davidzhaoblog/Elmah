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
    }
}

