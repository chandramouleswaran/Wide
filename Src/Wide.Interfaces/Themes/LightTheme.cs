using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wide.Interfaces;

namespace Wide.Interfaces.Themes
{
    public sealed class LightTheme: ITheme
    {
        public LightTheme()
        {
            this.UriList = new List<Uri>();
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/Wide.Interfaces;component/Styles/LightTheme.xaml"));
            this.UriList.Add(new Uri("pack://application:,,,/AvalonDock.Themes.VS2012;component/Theme.xaml"));
        }

        public IList<Uri> UriList { get; internal set; }

        public string Name
        {
            get { return "Light"; }
        }
    }
}
