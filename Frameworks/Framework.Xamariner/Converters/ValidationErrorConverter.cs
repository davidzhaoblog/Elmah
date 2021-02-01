using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Framework.Xamariner.Converters
{
    public class ValidationErrorConverter : Xamarin.Forms.IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return string.Empty;
            if (!(value is Dictionary<string, List<string>>))
                return string.Empty;
            var typedValue = (Dictionary<string, List<string>>)value;
            if (!typedValue.ContainsKey((string)parameter))
                return string.Empty;
            var errors = typedValue[(string)parameter];

            // Get first error if any
            return errors.FirstOrDefault();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}

