namespace Framework.Models
{
    public class PagedSearchViewModel<TQuery, TUISetting, TResponseBody> : PagedViewModel<TUISetting, TResponseBody>
        where TUISetting : class
        where TQuery : class
    {
        public TQuery Query { get; set; } = null!;
    }
    public class PagedSearchViewModel<TQuery, TResponseBody> : PagedViewModel<TResponseBody>
        where TQuery : class
    {
        public TQuery Query { get; set; } = null!;
    }
}

