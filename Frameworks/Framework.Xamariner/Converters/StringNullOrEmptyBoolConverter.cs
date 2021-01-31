using System;
using System.Globalization;
using Xamarin.Forms;

namespace Framework.Xamariner.Converters
{
    public class StringNullOrEmptyBoolConverter : IValueConverter
    {
        public bool Invert { get; set; } = false;

        /// <summary>Returns false if string is null or empty
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = value as string;
            if(!Invert)
                return !string.IsNullOrEmpty(s);
            else
                return string.IsNullOrEmpty(s);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

