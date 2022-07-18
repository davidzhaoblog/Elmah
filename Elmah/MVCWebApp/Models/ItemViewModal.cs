namespace Elmah.MvcWebApp.Models
{
    public class ItemViewModal<TModel>
        where TModel : class
    {
        public System.Net.HttpStatusCode Status { get; set; }

        /// <summary>
        /// It is a ToString() for known TemplateName
        /// <seealso cref="Framework.Models.ViewItemTemplateNames"/>
        /// </summary>
        public string? Template { get; set; }

        // this is used for inline-editing
        public bool IsCurrentItem { get; set; } = false;

        /// <summary>
        /// The following 3 is used for batch editing, to construct Html Name attribute of form-control/form-select/form-check-input.
        /// </summary>
        public bool HtmlNameUseArrayIndex { get; set; } = false;
        public int IndexInArray { get; set; }
        public string? HtmlNamePrefix { get; set; }

        /// <summary>
        /// Item1 is the partial view url
        /// Item2 is the Modal
        /// </summary>
        public TModel? Model { get; set; } = null;
    }
}

