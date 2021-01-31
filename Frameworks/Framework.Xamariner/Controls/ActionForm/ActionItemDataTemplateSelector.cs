using Xamarin.Forms;

namespace Framework.Xamariner.Controls.ActionForm
{
    // May need other conditions: e.g. has icon, where is text(on right or below icon)
    public class ActionItemDataTemplateSelector : DataTemplateSelector
    {
        
        public DataTemplate CommandItemMultiplePerRow { get; set; }
        public DataTemplate CommandItemOnePerRow { get; set; }
        public DataTemplate NavigationItem { get; set; }
        public DataTemplate SortCommandItem { get; set; }
        public DataTemplate ToggleCommandItem { get; set; }
        public DataTemplate CustomControl { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var actionFormItemType = ((Framework.Xaml.ActionForm.ActionItemModel)item).ActionFormItemType;
            if (container is CollectionView)
            {
                var typedContainer = (CollectionView)container;
                if ((typedContainer).ItemsLayout is LinearItemsLayout)
                {
                    if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.NavigationItem)
                        return NavigationItem;
                    else if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem)
                        return CommandItemOnePerRow;
                    else if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.SortCommandItem)
                        return SortCommandItem;
                    else if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.ToggleItem)
                        return ToggleCommandItem;
                    return CustomControl;
                }
                else if (typedContainer.ItemsLayout is GridItemsLayout)
                {
                    var collectionViewSpan = ((GridItemsLayout)(typedContainer).ItemsLayout).Span;
                    var collectionViewOrientation = ((GridItemsLayout)(typedContainer).ItemsLayout).Orientation;
                    if (collectionViewOrientation == ItemsLayoutOrientation.Vertical)
                    {
                        if (collectionViewSpan > 1)
                        {
                            if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem)
                                return CommandItemMultiplePerRow;
                            return CustomControl;
                        }
                        else
                        {
                            if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.NavigationItem)
                                return NavigationItem;
                            else if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem)
                                return CommandItemOnePerRow;
                            else if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.SortCommandItem)
                                return SortCommandItem;
                            else if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.ToggleItem)
                                return ToggleCommandItem;
                            return CustomControl;
                        }
                    }
                }
            }
            else if (container is StackLayout)
            {
                var typedContainer = (StackLayout)container;
                if(typedContainer.Orientation == StackOrientation.Horizontal)
                {
                    if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.NavigationItem)
                        return NavigationItem;
                    else if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.CommandItem)
                        return CommandItemMultiplePerRow;
                    else if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.SortCommandItem)
                        return SortCommandItem;
                    else if (actionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.ToggleItem)
                        return ToggleCommandItem;
                    return CustomControl;
                }
            }
            return null;
        }
    }
}
