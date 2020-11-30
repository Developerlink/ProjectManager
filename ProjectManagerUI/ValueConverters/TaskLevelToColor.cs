using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjectManagerUI.ValueConverters
{
    public class TaskLevelToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int taskLevel = (int)value;

            if (taskLevel == 1)
            {
                return "#3E38F2";
            }
            else if (taskLevel == 2)
            {
                return "CornFlowerBlue";
            }
            else
            {
                return "#9ad3bc";
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
