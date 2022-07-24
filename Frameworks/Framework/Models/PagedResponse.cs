using System;
namespace Framework.Models
{
    public class PagedResponse<TStatus, TRequestBody> : Response<TStatus, TRequestBody>
    {
        // public bool AdvancedQuery { get; set; } = false;
        public PagedViewOptions PagedViewOption { get; set; } = PagedViewOptions.List;

        public PaginationResponse? Pagination { get; set; }
    }
    public class PagedResponse<TResponseBody> : PagedResponse<System.Net.HttpStatusCode, TResponseBody>
    {
    }
}

