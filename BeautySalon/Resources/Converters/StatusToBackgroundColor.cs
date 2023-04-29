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
                    StatusOrder.Выполнен => "#90ee90",
                    StatusOrder.Выполняется => "#f5dc38",
                    StatusOrder.Отменен => "#e84343",
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
