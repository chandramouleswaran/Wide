// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using AvalonDock;
using AvalonDock.Layout;
using AvalonDock.Layout.Serialization;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;

namespace Wide.Shell
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class ShellViewMetro : IShell
    {
        private readonly IUnityContainer _container;
        private IEventAggregator _eventAggregator;
        private ILoggerService _logger;

        public ShellViewMetro(IUnityContainer container, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _container = container;
            _eventAggregator = eventAggregator;
            Loaded += MainWindow_Loaded;
            Unloaded += MainWindow_Unloaded;
        }

        #region IShell Members

        public void LoadLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
                                                                {
                                                                    var anchorable = e.Model as LayoutAnchorable;
                                                                    var document = e.Model as LayoutDocument;
                                                                    IWorkspace workspace =
                                                                        _container.Resolve<AbstractWorkspace>();

                                                                    if (anchorable != null)
                                                                    {
                                                                        ToolViewModel model =
                                                                            workspace.Tools.First(
                                                                                f => f.ContentId == e.Model.ContentId);
                                                                        if (model != null)
                                                                        {
                                                                            e.Content = model;
                                                                            model.IsVisible = anchorable.IsVisible;
                                                                            model.IsActive = anchorable.IsActive;
                                                                            model.IsSelected = anchorable.IsSelected;
                                                                        }
                                                                    }
                                                                    if (document != null)
                                                                    {
                                                                        var fileService =
                                                                            _container.Resolve<IOpenFileService>();
                                                                        ContentViewModel model =
                                                                            fileService.OpenFromID(e.Model.ContentId);
                                                                        if (model != null)
                                                                        {
                                                                            e.Content = model;
                                                                            model.IsActive = document.IsActive;
                                                                            model.IsSelected = document.IsSelected;
                                                                        }
                                                                    }
                                                                };
            try
            {
                layoutSerializer.Deserialize(@".\AvalonDock.Layout.config");
            }
            catch (Exception)
            {
            }
        }

        public void SaveLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.Serialize(@".\AvalonDock.Layout.config");
        }

        #endregion

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveLayout();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLayout();
        }

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            var workspace = DataContext as IWorkspace;
            if (!workspace.Closing())
            {
                e.Cancel = true;
            }
        }

        private void dockManager_ActiveContentChanged(object sender, EventArgs e)
        {
            DockingManager manager = sender as DockingManager;
            ContentViewModel cvm = manager.ActiveContent as ContentViewModel;
            _eventAggregator.GetEvent<ActiveContentChangedEvent>().Publish(cvm);
            if(cvm != null) Logger.Log("Active document changed to " + cvm.Title, LogCategory.Info, LogPriority.None);
        }

        private ILoggerService Logger
        {
            get
            {
                if(_logger == null)
                    _logger = _container.Resolve<ILoggerService>();

                return _logger;
            }
        }
    }
}