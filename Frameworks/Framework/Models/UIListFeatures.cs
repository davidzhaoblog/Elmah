namespace Framework.Models
{
    public class UIListFeatures
    {
        public bool HasPageSizeList { get; set; } = true;
        public bool HasOrderByList { get; set; } = true;
        public bool HasPagination { get; set; } = true;
        /// <summary>
        /// PrimayCreateViewContainer must be Inline when EditableList
        /// </summary>
        public CrudViewContainers PrimayCreateViewContainer { get; set; } = CrudViewContainers.Dialog;
        public CrudViewContainers PrimayDeleteViewContainer { get; set; } = CrudViewContainers.Dialog;
        public CrudViewContainers PrimayDetailsViewContainer { get; set; } = CrudViewContainers.None;
        public CrudViewContainers PrimayEditViewContainer { get; set; } = CrudViewContainers.Dialog;

        public bool CanGotoDashboard { get; set; } = false;

        public bool CanBulkDelete { get; set; } = false;

        public List<string>? BulkActions { get; set; }
        public bool CanBulkActions { get { return BulkActions != null && BulkActions.Count > 0; } }

        /// <summary>
        /// The first is default
        /// </summary>
        public List<PagedViewOptions>? AvailableListViews { get; set; }
        public bool HasListViews { get { return AvailableListViews != null && AvailableListViews.Count > 0; } }

        /// <summary>
        /// For Mvc Core
        /// For <form></form> postback
        /// 1. empty when current PagedViewOptions=List/Tiles/Slideshow, id/name={property name}
        /// 2. not empty when EditableList, for array binding, e.g. "Data" when EditableList in Index.cshtml, then id/name="Data[i].{property name}"
        /// </summary>
        public bool IsArrayBinding { get; set; } = false;
        public string? BindingPath { get; set; }
    }
}

