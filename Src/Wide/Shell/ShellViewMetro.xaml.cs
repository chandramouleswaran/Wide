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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using Wide.Interfaces.Converters;

namespace Wide.Shell
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    internal partial class ShellViewMetro : IShell
    {
        private readonly IUnityContainer _container;
        private IEventAggregator _eventAggregator;
        private ILoggerService _logger;
        private IWorkspace _workspace;
        private ContextMenu _docContextMenu;
        private MultiBinding _itemSourceBinding;

        public ShellViewMetro(IUnityContainer container, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _container = container;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ThemeChangeEvent>().Subscribe(ThemeChanged);
            _docContextMenu = new ContextMenu();
            dockManager.DocumentContextMenu = _docContextMenu;
            _docContextMenu.ContextMenuOpening += _docContextMenu_ContextMenuOpening;
            _docContextMenu.Opened += _docContextMenu_Opened;
            _itemSourceBinding = new MultiBinding();
            _itemSourceBinding.Converter = new DocumentContextMenuMixingConverter();
            var origModel = new Binding(".");
            var docMenus = new Binding("Model.Menus");
            _itemSourceBinding.Bindings.Add(origModel);
            _itemSourceBinding.Bindings.Add(docMenus);
            origModel.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            docMenus.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            _itemSourceBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            _docContextMenu.SetBinding(ContextMenu.ItemsSourceProperty, _itemSourceBinding);
        }

        #region IShell Members

        public void LoadLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
                                                                {
                                                                    var anchorable = e.Model as LayoutAnchorable;
                                                                    var document = e.Model as LayoutDocument;
                                                                    _workspace =
                                                                        _container.Resolve<AbstractWorkspace>();

                                                                    if (anchorable != null)
                                                                    {
                                                                        ToolViewModel model =
                                                                            _workspace.Tools.FirstOrDefault(
                                                                                f => f.ContentId == e.Model.ContentId);
                                                                        if (model != null)
                                                                        {
                                                                            e.Content = model;
                                                                            model.IsVisible = anchorable.IsVisible;
                                                                            model.IsActive = anchorable.IsActive;
                                                                            model.IsSelected = anchorable.IsSelected;
                                                                        }
                                                                        else
                                                                        {
                                                                            e.Cancel = true;
                                                                        }
                                                                    }
                                                                    if (document != null)
                                                                    {
                                                                        var fileService =
                                                                            _container.Resolve<IOpenDocumentService>();
                                                                        ContentViewModel model =
                                                                            fileService.OpenFromID(e.Model.ContentId);
                                                                        if (model != null)
                                                                        {
                                                                            e.Content = model;
                                                                            model.IsActive = document.IsActive;
                                                                            model.IsSelected = document.IsSelected;
                                                                        }
                                                                        else
                                                                        {
                                                                            e.Cancel = true;
                                                                        }
                                                                    }
                                                                };
            try
            {
                layoutSerializer.Deserialize(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "AvalonDock.Layout.config");
            }
            catch (Exception)
            {
            }
        }

        public void SaveLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.Serialize(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "AvalonDock.Layout.config");
        }

        #endregion

        #region Events

        private void _docContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            RefreshMenuBinding();
        }

        private void _docContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            /* When you right click a document - move the focus to that document, so that commands on the context menu
             * which are based on the ActiveDocument work correctly. Example: Save.
             */
            LayoutDocumentItem doc = _docContextMenu.DataContext as LayoutDocumentItem;
            if (doc != null)
            {
                ContentViewModel model = doc.Model as ContentViewModel;
                if (model != null && model != dockManager.ActiveContent)
                {
                    dockManager.ActiveContent = model;
                }
            }
            e.Handled = false;
        }

        private void RefreshMenuBinding()
        {
            MultiBindingExpression b = BindingOperations.GetMultiBindingExpression(_docContextMenu,
                                                                                   ContextMenu.ItemsSourceProperty);
            b.UpdateTarget();
        }

        private void ThemeChanged(ITheme theme)
        {
            //HACK: Reset the context menu or else old menu status is retained and does not theme correctly
            dockManager.DocumentContextMenu = null;
            dockManager.DocumentContextMenu = _docContextMenu;
            _docContextMenu.Style = FindResource("MetroContextMenu") as Style;
            _docContextMenu.ItemContainerStyle = FindResource("MetroMenuStyle") as Style;
        }

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            var workspace = DataContext as IWorkspace;
            if (!workspace.Closing(e))
            {
                e.Cancel = true;
                return;
            }
            _eventAggregator.GetEvent<WindowClosingEvent>().Publish(this);
        }

        private void ContentControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //HACK: Refresh the content control because in AutoHide mode this disappears. Needs to be fixed in AvalonDock.
            ContentControl c = sender as ContentControl;
            if (c != null)
            {
                var backup = c.Content;
                c.Content = null;
                c.Content = backup;
            }
        }

        #endregion

        #region Property

        private ILoggerService Logger
        {
            get
            {
                if (_logger == null)
                    _logger = _container.Resolve<ILoggerService>();

                return _logger;
            }
        }

        #endregion
    }
}