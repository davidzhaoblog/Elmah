namespace Framework.Models
{
    public class CompositeModel<TMaster, TPropertyEnum>
        where TMaster : class
        where TPropertyEnum : System.Enum
    {
        public TMaster __Master__ { get; set; } = null!;
        public Dictionary<TPropertyEnum, Response> Responses { get; set; } = new Dictionary<TPropertyEnum, Response>();

        /// <summary>
        /// This property will be populated in UI layer
        /// </summary>
        public Dictionary<TPropertyEnum, Framework.Models.UIListSettingModel> UIListSettings { get; set; } = new Dictionary<TPropertyEnum, Framework.Models.UIListSettingModel>();
    }
}

