using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Converters
{
    public class VerticalActionSheetHeightConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Framework.Xaml.ActionForm.ActionSheetVM))
                return 0;

            var typedValue = (Framework.Xaml.ActionForm.ActionSheetVM)value;
            if (typedValue.ActionItems == null || typedValue.ActionItems.Count == 0)
                return 0;

            return 25 + 50 * typedValue.ActionItems.Count;
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

