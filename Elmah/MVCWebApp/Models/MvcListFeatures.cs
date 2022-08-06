using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    /// <summary>
    /// <seealso cref="Elmah.MvcWebApp.Models.MvcListSetting"/>
    /// this class is used by developer to enable/disable features.
    /// </summary>
    public class MvcListFeatures
    {
        ///// <summary>
        ///// Show/Hide Toolbar above the list:
        ///// 1. Bulk Select Checkbox
        ///// 2. Bulk Delete and Bulk Action buttons
        ///// 3. Page Size dropdown
        ///// 4. Order By dropdown
        ///// 5. PagedViewOptions: List/Tiles/Slideshow/EditableList
        ///// 6. EditableList: Save/Cancel buttons.
        ///// </summary>
        //public bool ShowToolbar { get; set; } = true;
        ///// <summary>
        ///// 1. show Bulk Select Checkbox
        ///// 1.1. show QuickSelect
        ///// 2. show BulkDelete and BulkActions
        ///// 3. show individual Select checkbox
        ///// </summary>
        //public bool HasBulkActions { get; set; } = true;
        ///// <summary>
        ///// Show/hide Launch CRUD buttons and CRUD submit buttons column in html table.
        ///// </summary>
        //public bool ShowCRUDButtons { get; set; } = true;
        /// <summary>
        /// For <form></form> postback
        /// 1. empty when current PagedViewOptions=List/Tiles/Slideshow, id/name={property name}
        /// 2. not empty when EditableList, for array binding, e.g. "Data" when EditableList in Index.cshtml, then id/name="Data[i].{property name}"
        /// </summary>
        public string? BindingPath { get; set; }
        ///// <summary>
        ///// The following 3 is used for batch editing, to construct Html Name attribute of form-control/form-select/form-check-input.
        ///// </summary>
        //public bool UseArrayIndex { get; set; } = false;
        ///// <summary>
        ///// The first item is default
        ///// </summary>
        //public Framework.Models.PagedViewOptions[]? AvailablePagedViewOptions { get; set; }
    }
}

