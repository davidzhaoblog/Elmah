namespace Framework.Models
{
    public class CompositeModel<TMaster, TPropertyEnum>
        where TMaster : class
        where TPropertyEnum : System.Enum
    {
        // TODO: temporary here to pass compile. to-be-removed
        public UIListSettingModel UIListSetting { get; set; } = null!;

        public TMaster __Master__ { get; set; } = null!;
        public Dictionary<TPropertyEnum, Response> Responses { get; set; } = new Dictionary<TPropertyEnum, Response>();
        public Dictionary<TPropertyEnum, UIListSettingModel> UIListSettings { get; set; } = new Dictionary<TPropertyEnum, UIListSettingModel>();
    }
}

