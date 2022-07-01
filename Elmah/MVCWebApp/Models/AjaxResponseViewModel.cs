namespace Elmah.MvcWebApp.Models
{
    public class AjaxResponseViewModel
    {
        public System.Net.HttpStatusCode Status { get; set; }
        public string? Message { get; set; }

        public string? RequestId { get; set; }

        public bool ShowRequestId { get; set; };
    }
}

