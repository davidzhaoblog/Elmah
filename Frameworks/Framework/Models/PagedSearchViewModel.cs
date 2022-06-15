namespace Framework.Models
{
    public class PagedSearchViewModel<TQuery, TResponseBody>
        where TQuery : class
    {
        public TQuery? Query { get; set; } = null!;
        public PagedResponse<TResponseBody> Result { get; set; } = null!;
    }
}

