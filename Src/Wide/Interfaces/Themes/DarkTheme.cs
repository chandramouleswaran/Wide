// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;

namespace Wide.Interfaces.Themes
{
    public sealed class DarkTheme : ITheme
    {
        public DarkTheme()
        {
            UriList = new List<Uri>();
            UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml"));
            UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml"));
            UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml"));
            UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml"));
            UriList.Add(new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml"));
            UriList.Add(new Uri("pack://application:,,,/Wide;component/Interfaces/Styles/VS2012/DarkTheme.xaml"));
            UriList.Add(new Uri("pack://application:,,,/AvalonDock.Themes.VS2012;component/DarkTheme.xaml"));
        }

        #region ITheme Members

        public IList<Uri> UriList { get; internal set; }

        public string Name
        {
            get { return "Dark"; }
        }

        #endregion
    }
}