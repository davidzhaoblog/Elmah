namespace Framework.Models
{
    public enum CrudViewContainers
    {
        None,
        StandaloneView,
        Dialog,
        Inline, // always inline if PagedViewOptions.EditableList
        // EditableList,
    }
}

