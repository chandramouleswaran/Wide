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
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;
using Wide.Interfaces.Themes;

namespace WideMD.Core
{
    [Module(ModuleName = "WideMD.Core")]
    [ModuleDependency("Wide.Tools.Logger")]
    public class CoreModule : IModule
    {
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;

        public CoreModule(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Loading Core Module"});
            LoadTheme();
            LoadCommands();
            LoadMenus();
            RegisterParts();
        }

        private void RegisterParts()
        {
            _container.RegisterType<MDHandler>();
            _container.RegisterType<MDViewModel>();

            IContentHandler handler = _container.Resolve<MDHandler>();
            _container.Resolve<IContentHandlerRegistry>().Register(handler);
        }

        private void LoadTheme()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Themes.."});
            var manager = _container.Resolve<IThemeManager>();
            manager.AddTheme(new LightTheme());
            manager.AddTheme(new DarkTheme());
            manager.SetCurrent("Dark");
        }

        private void LoadCommands()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Commands.."});
            var manager = _container.Resolve<ICommandManager>();

            //throw new NotImplementedException();
            var openCommand = new DelegateCommand(OpenModule);
            var saveCommand = new DelegateCommand(SaveDocument, CanExecuteSaveDocument);
            var themeCommand = new DelegateCommand<string>(ThemeChangeCommand);
            var loggerCommand = new DelegateCommand(ToggleLogger);


            manager.RegisterCommand("OPEN", openCommand);
            manager.RegisterCommand("SAVE", saveCommand);
            manager.RegisterCommand("LOGSHOW", loggerCommand);
            manager.RegisterCommand("THEMECHANGE", themeCommand);
        }

        private void LoadMenus()
        {
            _eventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent
                                                                              {Message = "Menus.."});
            var manager = _container.Resolve<ICommandManager>();
            var vm = _container.Resolve<AbstractMenuItem>();
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            ToolViewModel logger = workspace.Tools.First(f => f.ContentId == "Logger");

            vm.Add(new MenuItemViewModel("_File", 1));

            vm.Get("_File").Add(
                (new MenuItemViewModel("_Open", 4,
                                       new BitmapImage(
                                           new Uri(
                                               @"pack://application:,,,/WideMD.Core;component/Icons/OpenFileDialog_692.png")),
                                       manager.GetCommand("OPEN"),
                                       new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl + O"))));
            vm.Get("_File").Add(new MenuItemViewModel("_Save", 5,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/WideMD.Core;component/Icons/Save_6530.png")),
                                                      manager.GetCommand("SAVE"),
                                                      new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S")));
            vm.Get("_File").Add(new MenuItemViewModel("Close", 8, null, manager.GetCommand("CLOSE"),
                                                      new KeyGesture(Key.F4, ModifierKeys.Control, "Ctrl + F4")));


            vm.Add(new MenuItemViewModel("_Edit", 2));
            vm.Get("_Edit").Add(new MenuItemViewModel("_Undo", 1,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/WideMD.Core;component/Icons/Undo_16x.png")),
                                                      ApplicationCommands.Undo));
            vm.Get("_Edit").Add(new MenuItemViewModel("_Redo", 2,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/WideMD.Core;component/Icons/Redo_16x.png")),
                                                      ApplicationCommands.Redo));
            vm.Get("_Edit").Add(MenuItemViewModel.Separator(15));
            vm.Get("_Edit").Add(new MenuItemViewModel("Cut", 20,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/WideMD.Core;component/Icons/Cut_6523.png")),
                                                      ApplicationCommands.Cut));
            vm.Get("_Edit").Add(new MenuItemViewModel("Copy", 21,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/WideMD.Core;component/Icons/Copy_6524.png")),
                                                      ApplicationCommands.Copy));
            vm.Get("_Edit").Add(new MenuItemViewModel("_Paste", 22,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/WideMD.Core;component/Icons/Paste_6520.png")),
                                                      ApplicationCommands.Paste));

            vm.Add(new MenuItemViewModel("_View", 3));

            if (logger != null)
                vm.Get("_View").Add(new MenuItemViewModel("_Logger", 1,
                                                          new BitmapImage(
                                                              new Uri(
                                                                  @"pack://application:,,,/WideMD.Core;component/Icons/Undo_16x.png")),
                                                          manager.GetCommand("LOGSHOW"))
                                        {IsCheckable = true, IsChecked = logger.IsVisible});

            vm.Get("_View").Add(new MenuItemViewModel("Themes", 1));
            vm.Get("_View").Get("Themes").Add(new MenuItemViewModel("Dark", 1, null, manager.GetCommand("THEMECHANGE"))
                                                  {IsCheckable = true, IsChecked = true, CommandParameter = "Dark"});
            vm.Get("_View").Get("Themes").Add(new MenuItemViewModel("Light", 2, null, manager.GetCommand("THEMECHANGE"))
                                                  {IsCheckable = true, IsChecked = false, CommandParameter = "Light"});

            vm.Add(new MenuItemViewModel("_Tools", 4));
            vm.Add(new MenuItemViewModel("_Help", 4));
        }

        #region Commands

        #region Open

        private void OpenModule()
        {
            var service = _container.Resolve<IOpenFileService>();
            service.Open();
        }

        #endregion

        #region Save

        private bool CanExecuteSaveDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            if (workspace.ActiveDocument != null)
            {
                return workspace.ActiveDocument.Model.IsDirty;
            }
            return false;
        }

        private void SaveDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            workspace.ActiveDocument.Handler.SaveContent(workspace.ActiveDocument);
        }

        #endregion

        #region Theme

        private void ThemeChangeCommand(string s)
        {
            var vm = _container.Resolve<AbstractMenuItem>();
            var manager = _container.Resolve<IThemeManager>();

            MenuItemViewModel mvm = vm.Get("_View").Get("Themes").Get(manager.CurrentTheme.Name) as MenuItemViewModel;
            mvm.IsChecked = false;
            manager.SetCurrent(s);
        }

        #endregion

        #region Logger click

        private void ToggleLogger()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            var vm = _container.Resolve<AbstractMenuItem>();
            ToolViewModel logger = workspace.Tools.First(f => f.ContentId == "Logger");
            if (logger != null)
            {
                logger.IsVisible = !logger.IsVisible;
                var mi = vm.Get("_View").Get("_Logger") as AbstractMenuItem;
                mi.IsChecked = logger.IsVisible;
            }
        }

        #endregion

        #endregion
    }
}