namespace Framework.Models
{
    public class PagedViewModel<TUISetting, TUIFeatures, TResponseBody>
        where TUISetting : class
        where TUIFeatures : class
    {
        public TUISetting UISetting { get; set; } = null!;
        public TUIFeatures? UIFeatures { get; set; }

        public PagedResponse<TResponseBody> Result { get; set; } = null!;
        /// <summary>
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        public Dictionary<string, List<NameValuePair>>? TopLevelDropDownListsFromDatabase { get; set; }
    }

    public class PagedViewModel<TResponseBody>
    {
        public UIListSettingModel UIListSetting { get; set; } = null!;

        public PagedResponse<TResponseBody> Result { get; set; } = null!;
        /// <summary>
        /// the Key comes from {SolutionName}.Models.Definitions.TopLevelDropDownLists
        /// </summary>
        public Dictionary<string, List<NameValuePair>>? TopLevelDropDownListsFromDatabase { get; set; }
    }
}

