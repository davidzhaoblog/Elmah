namespace Framework.Models
{
    public class PagedSearchViewModel<TQuery, TResponseBody>: Framework.Models.PagedViewModel<TResponseBody>
        where TQuery : class
    {
        public TQuery Query { get; set; } = null!;
    }
}

