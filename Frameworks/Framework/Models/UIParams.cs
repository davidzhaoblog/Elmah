namespace Framework.Models
{
    /// <summary>
    /// parameters between UI View and Controllers
    /// </summary>
    public class UIParams
    {
        public bool AdvancedQuery { get; set; } = false;
        public int IndexInArray { get; set; }
        public Framework.Models.PagedViewOptions PagedViewOption { get; set; } = Framework.Models.PagedViewOptions.List;
        public Framework.Models.ViewItemTemplateNames Template { get; set; } = Framework.Models.ViewItemTemplateNames.Details;
    }
}
