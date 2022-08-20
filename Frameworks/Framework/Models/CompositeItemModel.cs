namespace Framework.Models
{
    public class CompositeItemModel
    {
        public string Key { get; set; } = null!;

        public Framework.Models.Response Response { get; set; } = null!;

        public Framework.Models.UIParams UIParams { get; set; } = null!;
    }
}

