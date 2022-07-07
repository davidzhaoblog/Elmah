namespace Elmah.MvcWebApp.Models
{
    public class ListItemTrViewModal<TModel>
        where TModel : class
    {
        public System.Net.HttpStatusCode Status { get; set; }
        public string? Action { get; set; }

        /// <summary>
        /// Item1 is the partial view url
        /// Item2 is the Modal
        /// </summary>
        public TModel? Model { get; set; } = null;
    }
}

