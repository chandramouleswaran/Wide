using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Wide.Interfaces
{
    public class ToolBarItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ButtonTemplate { get; set; }
        public DataTemplate SeparatorTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var toolBarItem = (MenuItemViewModel)item;
            Debug.Assert(toolBarItem != null);
            if (!toolBarItem.IsSeparator)
            {
                return ButtonTemplate;
            }
            return SeparatorTemplate;
        }
    }
}
