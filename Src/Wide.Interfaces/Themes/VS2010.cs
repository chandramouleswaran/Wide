using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wide.Interfaces;

namespace Wide.Interfaces.Themes
{
    public sealed class VS2010 : ITheme
    {
        public VS2010()
        {
            this.UriList = new List<Uri>();
            this.Name = "VS2010";
            this.UriList.Add(new Uri("pack://application:,,,/AvalonDock.Themes.VS2010;component/Theme.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/Wide.Interfaces;component/Themes/VS2010/Theme.xaml"));
        }

        public IList<Uri> UriList { get; private set; }
        public string Name { get; private set; }
    }
}
