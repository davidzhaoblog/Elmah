namespace Elmah.MvcWebApp.Models
{
    public class MvcItemViewModel<TModel>: Framework.Models.ItemViewModel<TModel>
        where TModel : class
    {

        public Elmah.MvcWebApp.Models.MvcListSetting ListSetting { get; set; } = null!;
        public Elmah.MvcWebApp.Models.MvcListFeatures? ListFeatures { get; set; }

        public int IndexInArray { get; set; }

        public string HtmlId(string propertyName)
        {
            var name = HtmlName(propertyName);
            return name.Replace("[", "_").Replace("]", "_").Replace(".", "_");
        }
        public string HtmlName(string propertyName)
        {
            if (ListFeatures == null)
            {
                return propertyName;
            }

            if (string.IsNullOrEmpty(ListFeatures?.BindingPath) && ListSetting.PagedViewOption != Framework.Models.PagedViewOptions.EditableList)
            {
                return propertyName;
            }

            if (ListSetting.PagedViewOption != Framework.Models.PagedViewOptions.EditableList)
            {
                return string.Format("{0}.{1}", ListFeatures!.BindingPath, propertyName);
            }

            return string.Format("{0}[{1}].{2}", ListFeatures!.BindingPath, IndexInArray, propertyName);
        }
    }
}
