namespace Elmah.MvcWebApp.Models
{
    public class ListItemTrViewModal<TModel>
        where TModel : class
    {
        public System.Net.HttpStatusCode Status { get; set; }
        public string? Template { get; set; }
        public bool IsCurrentItem { get; set; } = false;
        /// <summary>
        /// Item1 is the partial view url
        /// Item2 is the Modal
        /// </summary>
        public TModel? Model { get; set; } = null;
    }
}

