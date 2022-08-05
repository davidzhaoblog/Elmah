namespace Framework.Models
{
    public class PagedViewModel<TUISetting, TResponseBody>
        where TUISetting : class
    {
        public TUISetting UISetting { get; set; } = null!;
        public PagedResponse<TResponseBody> Result { get; set; } = null!;
        /// <summary>
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        public Dictionary<string, List<NameValuePair>>? TopLevelDropDownListsFromDatabase { get; set; }
    }
    public class PagedViewModel<TResponseBody>
    {
        public PagedResponse<TResponseBody> Result { get; set; } = null!;
        /// <summary>
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        public Dictionary<string, List<NameValuePair>>? TopLevelDropDownListsFromDatabase { get; set; }
    }
}

