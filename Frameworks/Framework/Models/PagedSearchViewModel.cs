namespace Framework.Models
{
    public class PagedSearchViewModel<TQuery, TResponseBody>
        where TQuery : class
    {
        public TQuery Query { get; set; } = null!;
        public PagedResponse<TResponseBody> Result { get; set; } = null!;
        /// <summary>
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        public Dictionary<string, List<Framework.Models.NameValuePair>>? TopLevelDropDownListsFromDatabase { get; set; }
    }
}

