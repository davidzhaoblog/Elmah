using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Converters
{
    public class ToggleStatusToIconFontConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Framework.Models.ToggleStatus && (Framework.Models.ToggleStatus)value == Models.ToggleStatus.On)
                return Framework.Xaml.FontAwesomeIcons.ToggleOn;
            return Framework.Xaml.FontAwesomeIcons.ToggleOff;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && (string)value == Framework.Xaml.FontAwesomeIcons.ToggleOn)
                return Framework.Models.ToggleStatus.On;
            return Framework.Models.ToggleStatus.Off;
        }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

