using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wide.Interfaces;

namespace Wide.Shell
{
    public class PanesStyleSelector : StyleSelector
    {
        public Style ToolStyle
        {
            get;
            set;
        }

        public Style ContentStyle
        {
            get;
            set;
        }

        public override Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is ToolViewModel)
                return ToolStyle;

            if (item is ContentViewModel)
                return ContentStyle;

            return base.SelectStyle(item, container);
        }
    }
}
