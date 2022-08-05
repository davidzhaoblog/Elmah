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
            if (ListConfiguration == null || !ListConfiguration.UseArrayIndex || string.IsNullOrEmpty(ListConfiguration?.BindingPath))
            {
                return propertyName;
            }

            return "";
        }
        public string HtmlName(string propertyName)
        {
            return propertyName;
        }
    }
}

