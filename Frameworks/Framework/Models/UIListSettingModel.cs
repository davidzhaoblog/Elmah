namespace Framework.Models
{
    public class UIListSettingModel
    {
        public Framework.Models.UIParams UIParams { get; set; } = null!;
        public Framework.Models.UIListFeatures UIListFeatures { get; set; } = null!;
        public Framework.Models.UIAvailableFeatures? UIAvailableFeatures { get; set; }

        // 1.Start List/Editable list related

        public bool ShowListBulkActionRelated(bool withBulkDelete)
        {
            return (UIParams.PagedViewOption == Framework.Models.PagedViewOptions.List || UIParams.PagedViewOption == Framework.Models.PagedViewOptions.Tiles) &&
                (withBulkDelete && UIListFeatures.CanBulkDelete || UIListFeatures.CanBulkActions) &&
                UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Any(t=> t == Framework.Models.PagedViewOptions.List && t != Framework.Models.PagedViewOptions.Tiles) &&
                (UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || (UIAvailableFeatures.HasBulkDelete || UIAvailableFeatures.HasBulkActions) && UIAvailableFeatures.AvailableListViewFeatures!.Any(t=>t.Key != Framework.Models.PagedViewOptions.EditableList && t.Key != Framework.Models.PagedViewOptions.Single));
        }

        public bool HasEditableList()
        {
            return UIListFeatures.AvailableListViews != null && UIListFeatures.AvailableListViews.Contains(Framework.Models.PagedViewOptions.EditableList) &&
                (UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(Framework.Models.PagedViewOptions.EditableList));
        }

        public bool ShowItemUIStatus()
        {
            return UIParams.PagedViewOption == Framework.Models.PagedViewOptions.EditableList && HasEditableList();
        }

        public bool ShowEditableListDeleteSelect()
        {
            return UIParams.PagedViewOption == Framework.Models.PagedViewOptions.EditableList && UIListFeatures.CanBulkDelete && HasEditableList();
        }

        public bool ShowItemButtons()
        {
            return UIParams.PagedViewOption != Framework.Models.PagedViewOptions.EditableList;
        }

        public List<Framework.Models.PagedViewOptions> GetAvailablePagedViewOptions()
        {
            return UIListFeatures.AvailableListViews != null
                ? UIListFeatures.AvailableListViews.Where(t => UIAvailableFeatures == null || UIAvailableFeatures.AvailableListViewFeatures == null || UIAvailableFeatures.AvailableListViewFeatures.ContainsKey(t)).ToList()
                : Enumerable.Empty<Framework.Models.PagedViewOptions>().ToList();
        }

        public bool CanGotoCreate(Framework.Models.CrudViewContainers crudViewContainers)
        {
            return (UIParams.PagedViewOption == Framework.Models.PagedViewOptions.List || UIParams.PagedViewOption == Framework.Models.PagedViewOptions.Tiles) && UIListFeatures.PrimayEditViewContainer == crudViewContainers ||
                UIParams.PagedViewOption == Framework.Models.PagedViewOptions.EditableList && crudViewContainers == Framework.Models.CrudViewContainers.Inline;
        }

        // 1.end List/Editable list related
    }
}
