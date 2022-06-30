namespace Framework.Models
{
    public class ItemViewModel<TResponseBody>
        where TResponseBody: class
    {
        public ViewItemTemplateNames ViewItemTemplate { get; set; }
        public Framework.Models.Response<TResponseBody> Result { get; set; } = null!;
    }
}
