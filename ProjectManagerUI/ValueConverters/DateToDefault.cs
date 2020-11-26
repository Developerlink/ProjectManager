using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjectManagerUI.ValueConverters
{
    public class DateToDefault : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            var defaultDate = DateTime.Parse("1800-01-01");

            if(DateTime.Compare(date, defaultDate) == 0)
            {
                return String.Empty;
            }

            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
