using BeautySalon.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace BeautySalon.Resources.Converters
{
    public class StatusToBackgroundColor : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StatusOrder status)
            {
                return status switch
                {
                    0 => "white",
                    StatusOrder.Выполнен => "#cdf7b1",
                    StatusOrder.Выполняется => "#cff6ff",
                    StatusOrder.Отменен => "#fd877d",
                    _ => "Transparent",
                };
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
