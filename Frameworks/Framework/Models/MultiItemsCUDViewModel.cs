namespace Framework.Models
{
    public class MultiItemsCUDViewModel<TIdentifier, TItem>
        where TIdentifier : class
        where TItem : class
    {
        public List<TIdentifier>? DeleteItems { get; set; }
        public List<TItem>? NewItems { get; set; }
        public List<TItem>? UpdateItems { get; set; }
    }
}
