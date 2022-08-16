namespace Framework.Models
{
    public class CompositeModel<TMaster, TPropertyEnum>
        where TMaster : class
        where TPropertyEnum : System.Enum
    {
        public TMaster __Master__ { get; set; } = null!;
        public Dictionary<TPropertyEnum, Response> Responses { get; set; } = new Dictionary<TPropertyEnum, Response>();
        public Dictionary<TPropertyEnum, UIListSettingModel> UIListSettings { get; set; } = new Dictionary<TPropertyEnum, UIListSettingModel>();
    }
}

