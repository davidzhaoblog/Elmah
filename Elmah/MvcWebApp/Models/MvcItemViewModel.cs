using Framework.Models;

namespace Elmah.MvcWebApp.Models
{
    public class MvcItemViewModel<TModel>: ItemViewModel<TModel>
        where TModel : class
    {
        public Framework.Models.UIListSettingModel? UIListSetting { get; set; }
        
        public MvcListSetting ListSetting { get; set; } = null!;
        public MvcListFeatures? ListFeatures { get; set; }

        public int IndexInArray { get; set; }

        public string HtmlId(string propertyName)
        {
            var name = HtmlName(propertyName);
            return name.Replace("[", "_").Replace("]", "_").Replace(".", "_");
        }
        public string HtmlName(string propertyName)
        {
            if(UIListSetting == null || 
                UIListSetting.UIParams == null ||
                UIListSetting.UIListFeatures == null)
            {
                return propertyName;
            }

            if(!UIListSetting.UIListFeatures.IsArrayBinding)
            {
                return string.IsNullOrEmpty(UIListSetting.UIListFeatures.BindingPath)
                    ? propertyName
                    : string.Format("{0}.{1}", UIListSetting.UIListFeatures.BindingPath, propertyName);
            }

            return string.Format("{0}[{1}].{2}", UIListSetting.UIListFeatures.BindingPath, IndexInArray, propertyName);
        }
    }
}
