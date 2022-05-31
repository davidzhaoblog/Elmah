namespace Framework.Models
{
    public class PagedRequest<TStatus, TResponseBody> : Request<TStatus, TResponseBody>
    {
        public Pagination? Paging { get; set; }
    }
}

