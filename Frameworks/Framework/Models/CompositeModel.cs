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
        // this is for Mvc for now, wil be populated in Mvc Controller
        public Dictionary<TPropertyEnum, Framework.Models.UIParams> UIParamsList { get; set; } = new Dictionary<TPropertyEnum, Framework.Models.UIParams>();

        public Framework.Models.CompositeItemModel Get(TPropertyEnum key)
        {
            return new Framework.Models.CompositeItemModel
            {
                Key = key.ToString(),
                Response = Responses[key],
                UIParams = UIParamsList[key],
            };
        }
    }
}

