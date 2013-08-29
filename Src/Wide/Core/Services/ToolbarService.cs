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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Xceed.Wpf.AvalonDock.Converters;

namespace Wide.Core.Services
{
    /// <summary>
    /// The Wide tool bar service
    /// </summary>
    internal sealed class ToolbarService : AbstractToolbar, IToolbarService
    {
        private static BoolToVisibilityConverter btv = new BoolToVisibilityConverter();

        public ToolbarService():base("$MAIN$",0)
        {
            
        }

        /// <summary>
        /// The toolbar tray which will be used in the application
        /// </summary>
        public ToolBarTray ToolBarTray
        {
            get
            {
                var tray = new ToolBarTray();
                tray.ContextMenu = new ContextMenu();
                tray.ContextMenu.ItemsSource = _children;
                IAddChild child = tray;
                foreach (var node in this.Children)
                {
                    var value = node as AbstractToolbar;
                    if (value != null)
                    {
                        var tb = new ToolBar();
                        var t = Application.Current.MainWindow.FindResource("toolBarItemTemplateSelector") as DataTemplateSelector;
                        tb.SetValue(ItemsControl.ItemTemplateSelectorProperty, t);
                        //Need to set these by binding
                        tb.Band = value.Band;
                        tb.BandIndex = value.BandIndex;

                        tb.ItemsSource = value.Children;
                        
                        var visibilityBinding = new Binding("IsChecked")
                        {
                            Converter = btv
                        };
                        value.IsCheckable = true;
                        value.IsChecked = true;
                        visibilityBinding.Source = value;
                        tb.SetBinding(ToolBar.VisibilityProperty, visibilityBinding);
                        child.AddChild(tb);
                    }
                }
                tray.ContextMenu.ItemContainerStyle = Application.Current.MainWindow.FindResource("ToolbarContextMenu") as Style;
                return tray;
            }
        }
    }
}