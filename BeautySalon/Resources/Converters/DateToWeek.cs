using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BeautySalon.Resources.Converters
{
    public class DateToWeek : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime time)
            {
                return DateTime.Now.Year - time.Year;
            }
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
             return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
