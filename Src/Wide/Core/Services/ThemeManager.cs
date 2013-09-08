#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Wide.Core.Settings;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    /// <summary>
    /// The main theme manager used in Wide
    /// </summary>
    internal sealed class ThemeManager : IThemeManager
    {
        /// <summary>
        /// Dictionary of different themes
        /// </summary>
        private static readonly Dictionary<string, ITheme> ThemeDictionary = new Dictionary<string, ITheme>();

        /// <summary>
        /// The injected event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The injected logger
        /// </summary>
        private readonly ILoggerService _logger;

        /// <summary>
        /// The theme manager constructor
        /// </summary>
        /// <param name="eventAggregator">The injected event aggregator</param>
        /// <param name="logger">The injected logger</param>
        public ThemeManager(IEventAggregator eventAggregator, ILoggerService logger)
        {
            Themes = new ObservableCollection<ITheme>();
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        /// <summary>
        /// The current theme set in the theme manager
        /// </summary>
        public ITheme CurrentTheme { get; internal set; }

        #region IThemeManager Members

        /// <summary>
        /// The collection of themes
        /// </summary>
        public ObservableCollection<ITheme> Themes { get; internal set; }

        /// <summary>
        /// Set the current theme
        /// </summary>
        /// <param name="name">The name of the theme</param>
        /// <returns>true if the new theme is set, false otherwise</returns>
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
                theme.BeginInit();
                theme.MergedDictionaries.Clear();
                if (appTheme != null)
                {
                    appTheme.BeginInit();
                    appTheme.MergedDictionaries.Clear();
                }
                else
                {
                    appTheme = new ResourceDictionary();
                    appTheme.BeginInit();
                    Application.Current.Resources.MergedDictionaries.Add(appTheme);
                }
                foreach (Uri uri in newTheme.UriList)
                {
                    ResourceDictionary newDict = new ResourceDictionary {Source = uri};
                    /*AvalonDock and menu style needs to move to the application
                     * 1. AvalonDock needs global styles as floatable windows can be created
                     * 2. Menu's need global style as context menu can be created
                    */
                    if (uri.ToString().Contains("AvalonDock") ||
                        uri.ToString().Contains("Wide;component/Interfaces/Styles/VS2012/Menu.xaml"))
                    {
                        appTheme.MergedDictionaries.Add(newDict);
                    }
                    else
                    {
                        theme.MergedDictionaries.Add(newDict);
                    }
                }
                appTheme.EndInit();
                theme.EndInit();
                _logger.Log("Theme set to " + name, LogCategory.Info, LogPriority.None);
                _eventAggregator.GetEvent<ThemeChangeEvent>().Publish(newTheme);
            }
            return false;
        }

        /// <summary>
        /// Adds a theme to the theme manager
        /// </summary>
        /// <param name="theme">The theme to add</param>
        /// <returns>true, if successful - false, otherwise</returns>
        public bool AddTheme(ITheme theme)
        {
            if (!ThemeDictionary.ContainsKey(theme.Name))
            {
                ThemeDictionary.Add(theme.Name, theme);
                Themes.Add(theme);
                return true;
            }
            return false;
        }

        #endregion
    }
}