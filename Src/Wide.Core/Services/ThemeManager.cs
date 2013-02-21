using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;
using Microsoft.Practices.Prism.Events;

namespace Wide.Core.Services
{
    internal sealed class ThemeManager : IThemeManager
    {
        private static readonly Dictionary<string, ITheme> ThemeDictionary = new Dictionary<string, ITheme>();

        private IEventAggregator _eventAggregator;
        private ILoggerService _logger;

        public ThemeManager(IEventAggregator eventAggregator, ILoggerService logger)
        {
            Themes = new ObservableCollection<ITheme>();
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        public ObservableCollection<ITheme> Themes { get; internal set; }
        
        public ITheme CurrentTheme { get; internal set; }

        public bool SetCurrent(string name)
        {
            if(ThemeDictionary.ContainsKey(name))
            {
                ITheme newTheme = ThemeDictionary[name];
                this.CurrentTheme = newTheme;

                ResourceDictionary theme = Application.Current.MainWindow.Resources.MergedDictionaries[0];
                ResourceDictionary appTheme = Application.Current.Resources.MergedDictionaries.Count > 0 ? Application.Current.Resources.MergedDictionaries[0] : null;
                theme.MergedDictionaries.Clear();
                if(appTheme != null)
                {
                    appTheme.MergedDictionaries.Clear();
                }
                else
                {
                    appTheme = new ResourceDictionary();
                    Application.Current.Resources.MergedDictionaries.Add(appTheme);
                }
                appTheme.MergedDictionaries.Clear();
                foreach (var uri in newTheme.UriList)
                {
                    theme.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
                    if(uri.ToString().Contains("AvalonDock") && appTheme != null)
                    {
                        appTheme.MergedDictionaries.Add(new ResourceDictionary() {Source = uri});
                    }
                }
                _logger.Log("Theme set to " + name, LogCategory.Info, LogPriority.None);
                _eventAggregator.GetEvent<ThemeChangeEvent>().Publish(newTheme);
            }
            return false;
        }

        public bool AddTheme(ITheme theme)
        {
            if(!ThemeDictionary.ContainsKey(theme.Name))
            {
                //theme.UriList.Add(new Uri("pack://application:,,,/Wide.Core;component/Styles/MenuResource.xaml"));
                ThemeDictionary.Add(theme.Name,theme);
                Themes.Add(theme);
                return true;
            }
            return false;
        }
    }
}
