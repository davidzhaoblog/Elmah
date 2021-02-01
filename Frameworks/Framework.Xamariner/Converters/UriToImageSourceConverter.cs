using System;
using System.Globalization;
using Xamarin.Forms;

namespace Framework.Xamariner.Converters
{
    public class UriToImageSourceConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(string.IsNullOrEmpty((string)value)) { return null; }

            return ImageSource.FromUri(new Uri((string)value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

