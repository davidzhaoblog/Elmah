namespace Framework.Models
{
    public class PagedSearchViewModel<TQuery, TUISetting, TUIFeatures, TResponseBody>: PagedViewModel<TUISetting, TUIFeatures, TResponseBody>
        where TUISetting : class
        where TUIFeatures : class
        where TQuery : class
    {
        public TQuery Query { get; set; } = null!;
    }
}

