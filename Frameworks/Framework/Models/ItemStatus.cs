namespace Framework.Models
{
    public enum ItemStatus
    {
        NoChange, // when item loaded from database
        New, // when try to create a new item
        Updated, // any value changed in one item
    }
}

