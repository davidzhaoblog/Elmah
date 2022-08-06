namespace Elmah.MvcWebApp.Models
{
    public class MvcItemViewModel<TModel>: Framework.Models.ItemViewModel<TModel>
        where TModel : class
    {

        public Elmah.MvcWebApp.Models.MvcListSetting? ListSetting { get; set; }
        public Elmah.MvcWebApp.Models.MvcListConfiguration? ListConfiguration { get; set; }

        public int IndexInArray { get; set; }

        public string HtmlId(string propertyName)
        {
            var name = HtmlName(propertyName);
            return name.Replace("[", "_").Replace("]", "_").Replace(".", "_");
        }
        public string HtmlName(string propertyName)
        {
            if (ListConfiguration == null || !ListConfiguration.UseArrayIndex && string.IsNullOrEmpty(ListConfiguration?.BindingPath))
            {
                return propertyName;
            }

            if(!ListConfiguration.UseArrayIndex)
            {
                return string.Format("{0}.{1}", ListConfiguration.BindingPath, propertyName);
            }

            return string.Format("{0}[{1}].{2}", ListConfiguration.BindingPath, IndexInArray, propertyName);
        }
    }
}

