using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Converters
{
    public class StringToFontIconSettingsConverter : IValueConverter//, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if (string.IsNullOrEmpty(value.ToString().Trim()))
                return null;
            var fontIconSettings = Framework.Xaml.FontIconSettings.ParseFontIconSettings(value.ToString());
            return fontIconSettings;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
            //return value;
        }
        //public object ProvideValue(IServiceProvider serviceProvider)
        //{
        //    return this;
        //}
    }
}

