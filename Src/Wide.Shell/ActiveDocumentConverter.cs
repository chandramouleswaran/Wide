using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Wide.Interfaces;

namespace Wide.Shell 
{
    public class ActiveDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ContentViewModel)
                return value;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ContentViewModel)
                return value;

            return Binding.DoNothing;
        }
    }
}
