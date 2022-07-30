namespace Elmah.MvcWebApp.Models
{
    public class ItemViewModel<TModel>: Framework.Models.ItemViewModel<TModel>
        where TModel : class
    {
        /// <summary>
        /// The following 3 is used for batch editing, to construct Html Name attribute of form-control/form-select/form-check-input.
        /// </summary>
        public bool HtmlNameUseArrayIndex { get; set; } = false;
        public int IndexInArray { get; set; }
        public string? HtmlNamePrefix { get; set; }
    }
}
