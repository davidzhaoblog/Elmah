using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Converters
{
    public class AgeConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
                return null;
            var timespan = DateTime.Now - (DateTime)value;
            var years = (int)(timespan.TotalDays / 365);
            var months = (int)(timespan.TotalDays % 365 / 30);
            if (years >= 1)
                return string.Format("{0:G0} {1} {2:G0} {3}", years, years == 1 ? Framework.Resx.UIStringResource.Year : Framework.Resx.UIStringResource.Years, months, months == 1 ? Framework.Resx.UIStringResource.Month : Framework.Resx.UIStringResource.Months);

            var days = (int)(timespan.TotalDays % 365 % 30);
            if (months >= 1)
                return string.Format("{0:G0} {1} {2:G0} {3}", months, months == 1 ? Framework.Resx.UIStringResource.Month : Framework.Resx.UIStringResource.Months, days, days == 1 ? Framework.Resx.UIStringResource.Day : Framework.Resx.UIStringResource.Days);

            if (days >=1)
                return string.Format("{0:G0} {1} {2:G0} {3}", days, days == 1 ? Framework.Resx.UIStringResource.Day : Framework.Resx.UIStringResource.Days, timespan.Hours, timespan.Hours == 1 ? Framework.Resx.UIStringResource.Hour : Framework.Resx.UIStringResource.Hours);

            if (timespan.Hours >= 1)
                return string.Format("{0:G0} {1} {2:G0} {3}", timespan.Hours, timespan.Hours == 1 ? Framework.Resx.UIStringResource.Hour : Framework.Resx.UIStringResource.Hours, timespan.Minutes, timespan.Minutes == 1 ? Framework.Resx.UIStringResource.Minute : Framework.Resx.UIStringResource.Minutes);

            if (timespan.Minutes >= 1)
                return string.Format("{0:G0} {1}", timespan.Minutes, timespan.Minutes == 1 ? Framework.Resx.UIStringResource.Minute : Framework.Resx.UIStringResource.Minutes);

            return Framework.Resx.UIStringResource.Now;
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

