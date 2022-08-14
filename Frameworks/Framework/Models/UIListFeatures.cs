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
        public Framework.Models.CrudViewContainers PrimayCreateViewContainer { get; set; } = Framework.Models.CrudViewContainers.Dialog;
        public Framework.Models.CrudViewContainers PrimayDeleteViewContainer { get; set; } = Framework.Models.CrudViewContainers.Dialog;
        public Framework.Models.CrudViewContainers PrimayDetailsViewContainer { get; set; } = Framework.Models.CrudViewContainers.None;
        public Framework.Models.CrudViewContainers PrimayEditViewContainer { get; set; } = Framework.Models.CrudViewContainers.Dialog;

        public bool CanGotoDashboard { get; set; } = false;

        public bool CanBulkDelete { get; set; } = false;

        public List<string>? BulkActions { get; set; }
        public bool CanBulkActions { get { return BulkActions != null && BulkActions.Count > 0; } }

        /// <summary>
        /// The first is default
        /// </summary>
        public List<Framework.Models.PagedViewOptions>? AvailableListViews { get; set; }
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
