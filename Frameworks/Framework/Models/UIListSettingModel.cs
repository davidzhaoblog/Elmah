namespace Framework.Models
{
    public class UIListSettingModel
    {
        public UIParams UIParams { get; set; } = null!;
        public UIListFeatures UIListFeatures { get; set; } = null!;

        // Use this if you want more control
        //public UIAvailableFeatures? UIAvailableFeatures { get; set; }

        // 1.Start List/Editable list related

        public bool ShowListBulkActionRelated(bool withBulkDelete)
        {
            return (UIParams.PagedViewOption == PagedViewOptions.List || UIParams.PagedViewOption == PagedViewOptions.Tiles) &&
                (withBulkDelete && UIListFeatures.CanBulkDelete || UIListFeatures.CanBulkActions) &&
                UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Any(t=> t == PagedViewOptions.List && t != PagedViewOptions.Tiles);
            //return (UIParams.PagedViewOption == PagedViewOptions.List || UIParams.PagedViewOption == PagedViewOptions.Tiles) &&
            //    (withBulkDelete && UIListFeatures.CanBulkDelete || UIListFeatures.CanBulkActions) &&
            //    UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Any(t=> t == PagedViewOptions.List && t != PagedViewOptions.Tiles) &&
            //    (UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || (UIAvailableFeatures.HasBulkDelete || UIAvailableFeatures.HasBulkActions) && UIAvailableFeatures.AvailableListViewFeatures!.Any(t=>t.Key != PagedViewOptions.EditableList && t.Key != PagedViewOptions.Single));
        }

        public bool HasEditableList()
        {
            return UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Contains(PagedViewOptions.EditableList) &&
                (UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(PagedViewOptions.EditableList));
            //return UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Contains(PagedViewOptions.EditableList) &&
            //    (UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(PagedViewOptions.EditableList));
        }

        public bool ShowItemUIStatus()
        {
            return UIParams.PagedViewOption == PagedViewOptions.EditableList && HasEditableList();
        }

        public bool ShowEditableListDeleteSelect()
        {
            return UIParams.PagedViewOption == PagedViewOptions.EditableList && UIListFeatures.CanBulkDelete && HasEditableList();
        }

        public bool ShowItemButtons()
        {
            return UIParams.PagedViewOption != PagedViewOptions.EditableList;
        }

        public List<PagedViewOptions> GetAvailablePagedViewOptions()
        {
            return UIListFeatures.AvailableListViews != null
                ? UIListFeatures.AvailableListViews.Where(t => UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(t)).ToList()
                : Enumerable.Empty<PagedViewOptions>().ToList();
            //return UIListFeatures.AvailableListViews != null
            //    ? UIListFeatures.AvailableListViews.Where(t => UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(t)).ToList()
            //    : Enumerable.Empty<PagedViewOptions>().ToList();
        }

        public bool CanGotoCreate(CrudViewContainers crudViewContainers)
        {
            return (UIParams.PagedViewOption == PagedViewOptions.List || UIParams.PagedViewOption == PagedViewOptions.Tiles) && UIListFeatures.PrimayEditViewContainer == crudViewContainers ||
                UIParams.PagedViewOption == PagedViewOptions.EditableList && crudViewContainers == CrudViewContainers.Inline;
        }

        // 1.end List/Editable list related
    }
}

