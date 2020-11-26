using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjectManagerUI.ValueConverters
{
    public class TaskLevelToFontSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int taskLevel = (int)value;

            if (taskLevel == 1)
            {
                return 18;
            }
            else if (taskLevel == 2)
            {
                return 16;
            }
            else
            {
                return 14;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }

    }
}
