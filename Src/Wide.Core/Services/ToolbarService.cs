using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    internal sealed class ToolbarService : IToolbarService
    {
        private IUnityContainer _container;
        private static Dictionary<string, ToolbarViewModel> ToolbarDictionary = new Dictionary<string, ToolbarViewModel>();

        public ToolbarService(IUnityContainer container)
        {
            this._container = container;
        }

        public bool Add(ToolbarViewModel item)
        {
            if (!ToolbarDictionary.ContainsKey(item.Key))
            {
                ToolbarDictionary.Add(item.Key, item);
                return true;
            }
            return false;
        }

        public bool Remove(string key)
        {
            if (ToolbarDictionary.ContainsKey(key))
            {
                ToolbarDictionary.Remove(key);
                return true;
            }
            return false;
        }

        public ToolbarViewModel Get(string key)
        {
            if(ToolbarDictionary.ContainsKey(key))
            {
                return ToolbarDictionary[key];
            }
            return null;
        }

        public ToolBarTray ToolBarTray
        {
            get
            {
                ToolBarTray tray = new ToolBarTray();
                IAddChild child = tray;
                foreach (KeyValuePair<string, ToolbarViewModel> valuePair in ToolbarDictionary)
                {
                    ToolBar tb = new ToolBar();
                    DataTemplateSelector t = Application.Current.MainWindow.FindResource("toolBarItemTemplateSelector") as DataTemplateSelector;
                    tb.SetValue(ToolBar.ItemTemplateSelectorProperty, t);
                    //Need to set these by binding
                    tb.Band = valuePair.Value.Band;
                    tb.BandIndex = valuePair.Value.BandIndex;

                    tb.ItemsSource = valuePair.Value.Children;
                    child.AddChild(tb);
                }
                return tray;
            }
        }
    }
}
