namespace Elmah.MvcWebApp.Models
{
    public class AjaxResponseViewModel
    {
        public System.Net.HttpStatusCode Status { get; set; }
        public string? Message { get; set; }
        public string? RequestId { get; set; }
        public bool ShowRequestId { get; set; }

        /// <summary>
        /// Item1 is the partial view url
        /// Item2 is the Modal
        /// </summary>
        public List<Tuple<string, object>>? PartialViews = null;
    }
}

