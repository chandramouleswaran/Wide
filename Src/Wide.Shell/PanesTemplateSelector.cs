using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AvalonDock.Layout;
using Wide.Interfaces;

namespace Wide.Shell
{
    public class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {
            Console.WriteLine("I come here");
        }


        public DataTemplate ContentViewTemplate
        {
            get;
            set;
        }

        public DataTemplate ToolViewTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is ContentViewModel)
                return ContentViewTemplate;

            if (item is ToolViewModel)
                return ToolViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
