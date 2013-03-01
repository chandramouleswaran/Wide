// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    internal sealed class ThemeManager : IThemeManager
    {
        private static readonly Dictionary<string, ITheme> ThemeDictionary = new Dictionary<string, ITheme>();

        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerService _logger;

        public ThemeManager(IEventAggregator eventAggregator, ILoggerService logger)
        {
            Themes = new ObservableCollection<ITheme>();
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        public ITheme CurrentTheme { get; internal set; }

        #region IThemeManager Members

        public ObservableCollection<ITheme> Themes { get; internal set; }

        public bool SetCurrent(string name)
        {
            if (ThemeDictionary.ContainsKey(name))
            {
                ITheme newTheme = ThemeDictionary[name];
                CurrentTheme = newTheme;

                ResourceDictionary theme = Application.Current.MainWindow.Resources.MergedDictionaries[0];
                ResourceDictionary appTheme = Application.Current.Resources.MergedDictionaries.Count > 0
                                                  ? Application.Current.Resources.MergedDictionaries[0]
                                                  : null;
                theme.MergedDictionaries.Clear();
                if (appTheme != null)
                {
                    appTheme.MergedDictionaries.Clear();
                }
                else
                {
                    appTheme = new ResourceDictionary();
                    Application.Current.Resources.MergedDictionaries.Add(appTheme);
                }
                appTheme.MergedDictionaries.Clear();
                foreach (Uri uri in newTheme.UriList)
                {
                    theme.MergedDictionaries.Add(new ResourceDictionary {Source = uri});
                    if (uri.ToString().Contains("AvalonDock") && appTheme != null)
                    {
                        appTheme.MergedDictionaries.Add(new ResourceDictionary {Source = uri});
                    }
                }
                _logger.Log("Theme set to " + name, LogCategory.Info, LogPriority.None);
                _eventAggregator.GetEvent<ThemeChangeEvent>().Publish(newTheme);
            }
            return false;
        }

        public bool AddTheme(ITheme theme)
        {
            if (!ThemeDictionary.ContainsKey(theme.Name))
            {
                //theme.UriList.Add(new Uri("pack://application:,,,/Wide.Core;component/Styles/MenuResource.xaml"));
                ThemeDictionary.Add(theme.Name, theme);
                Themes.Add(theme);
                return true;
            }
            return false;
        }

        #endregion
    }
}