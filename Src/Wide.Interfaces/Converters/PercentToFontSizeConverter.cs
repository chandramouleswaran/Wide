using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Practices.Unity;

namespace Wide.Interfaces.Converters
{
    public class PercentToFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //For now lets assume 12.00 to be 100%
            double? fsize = value as double?;
            if(fsize != null)
            {
                return ((fsize/12.00)*100) + " %";
            }
            return "100 %";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double rValue = 12.0;
            if (value != null)
            {
                string final = value as string;
                final = final.Replace("%", "");
                if (double.TryParse(final, out rValue))
                {
                    rValue = (rValue/100.0)*12;
                }
                else
                {
                    rValue = 12.0;
                    value = "100 %";
                }
            }
            return rValue;
        }
    }
}
