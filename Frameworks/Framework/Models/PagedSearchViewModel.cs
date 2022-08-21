namespace Framework.Models
{
    public class PagedSearchViewModel<TQuery, TResponseBody>: PagedViewModel<TResponseBody>
        where TQuery : class
    {
        public TQuery Query { get; set; } = null!;
        public List<Framework.Models.NameValuePair> PageSizeList { get; set; } = null!;
        public List<Framework.Models.NameValuePair> OrderByList { get; set; } = null!;
        public List<Framework.Models.NameValuePair> TextSearchTypeList { get; set; } = null!;
        public Dictionary<string, List<Framework.Models.NameValuePair>> OtherDropDownLists { get; set; } = new Dictionary<string, List<Framework.Models.NameValuePair>>();

        public Framework.Models.PagedViewOptions GetCurrent()
        {
            return this.UIListSetting.UIParams.PagedViewOption ?? Framework.Models.PagedViewOptions.Table;
        }
    }
}

