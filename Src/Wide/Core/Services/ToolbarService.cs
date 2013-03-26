#region License
// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    /// <summary>
    /// The Wide tool bar service
    /// </summary>
    internal sealed class ToolbarService : IToolbarService
    {
        /// <summary>
        /// The dictionary of toolbars
        /// </summary>
        private static readonly Dictionary<string, ToolbarViewModel> ToolbarDictionary = new Dictionary<string, ToolbarViewModel>();

        #region IToolbarService Members
        /// <summary>
        /// Adds a new toolbar to the application
        /// </summary>
        /// <param name="item">The toolbar to add</param>
        /// <returns>true, if successful - false, otherwise</returns>
        public bool Add(ToolbarViewModel item)
        {
            if (!ToolbarDictionary.ContainsKey(item.Key))
            {
                ToolbarDictionary.Add(item.Key, item);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Removes a toolbar from the application
        /// </summary>
        /// <param name="key">The key of the toolbar to remove</param>
        /// <returns>true, if successful - false, otherwise</returns>
        public bool Remove(string key)
        {
            if (ToolbarDictionary.ContainsKey(key))
            {
                ToolbarDictionary.Remove(key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the toolbar with this key
        /// </summary>
        /// <param name="key">The key of the toolbar</param>
        /// <returns>A ToolbarViewModel or null if not found</returns>
        public ToolbarViewModel Get(string key)
        {
            if (ToolbarDictionary.ContainsKey(key))
            {
                return ToolbarDictionary[key];
            }
            return null;
        }

        /// <summary>
        /// The toolbar tray which will be used in the application
        /// </summary>
        public ToolBarTray ToolBarTray
        {
            get
            {
                var tray = new ToolBarTray();
                IAddChild child = tray;
                foreach (var valuePair in ToolbarDictionary)
                {
                    var tb = new ToolBar();
                    var t =
                        Application.Current.MainWindow.FindResource("toolBarItemTemplateSelector") as
                        DataTemplateSelector;
                    tb.SetValue(ItemsControl.ItemTemplateSelectorProperty, t);
                    //Need to set these by binding
                    tb.Band = valuePair.Value.Band;
                    tb.BandIndex = valuePair.Value.BandIndex;

                    tb.ItemsSource = valuePair.Value.Children;
                    child.AddChild(tb);
                }
                return tray;
            }
        }

        #endregion
    }
}