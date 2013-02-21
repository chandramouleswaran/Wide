using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wide.Interfaces.Themes
{
    public sealed class DefaultTheme : ITheme
    {
        public DefaultTheme()
        {
            this.UriList = new List<Uri>();
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml"));
        }

        public IList<Uri> UriList { get; internal set; }

        public string Name
        {
            get { return "Default"; }
        }
    }
}
