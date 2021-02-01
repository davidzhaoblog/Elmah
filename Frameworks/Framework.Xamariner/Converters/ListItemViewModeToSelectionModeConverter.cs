using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Converters
{
    public class ListItemViewModeToSelectionModeConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Framework.Xaml.ListItemViewModes))
                return Xamarin.Forms.SelectionMode.None;

            var typedValue = (Framework.Xaml.ListItemViewModes)value;

            if (typedValue == Framework.Xaml.ListItemViewModes.SingleSelection)
                return Xamarin.Forms.SelectionMode.Single;

            if (typedValue == Framework.Xaml.ListItemViewModes.MultipleSelection)
                return Xamarin.Forms.SelectionMode.Multiple;

            return Xamarin.Forms.SelectionMode.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

