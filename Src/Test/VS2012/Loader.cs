// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Wide.Interfaces.Themes;

namespace VS2012TestApp
{
    /*
     * NOTE: You don't need a loaded class to load menu/toolbar/commands
     * The idea is to make the modules participate in adding the same. This way,
     * each module can concentrate on a particular area and provide interfaces
     * for other modules which can make use of it.
     * The loader is just used for a demo purpose. I would recommend using modules.
     */

    internal class Loader
    {
        private readonly IUnityContainer _container;

        public Loader(IUnityContainer container)
        {
            _container = container;
            LoadTheme();
            LoadCommands();
            LoadMenus();
            LoadToolbar();
        }

        private void LoadTheme()
        {
            var manager = _container.Resolve<IThemeManager>();
            //manager.AddTheme(new VS2010());
            //manager.SetCurrent("VS2010");
            manager.AddTheme(new LightTheme());
            manager.AddTheme(new DarkTheme());
            manager.SetCurrent("Dark");
        }

        private void LoadMenus()
        {
            var manager = _container.Resolve<ICommandManager>();
            var vm = _container.Resolve<AbstractMenuItem>();
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            ToolViewModel logger = workspace.Tools.First(f => f.ContentId == "Logger");

            vm.Add(new MenuItemViewModel("_File", 1));

            vm.Get("_File").Add(
               (new MenuItemViewModel("_New", 4,
                                      new BitmapImage(
                                          new Uri(
                                              @"pack://application:,,,/VS2012TestApp;component/Icons/OpenFileDialog_692.png")),
                                      manager.GetCommand("NEW"),
                                      new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N"))));
            vm.Get("_File").Add(
                (new MenuItemViewModel("_Open", 4,
                                       new BitmapImage(
                                           new Uri(
                                               @"pack://application:,,,/VS2012TestApp;component/Icons/OpenFileDialog_692.png")),
                                       manager.GetCommand("OPEN"),
                                       new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl + O"))));
            vm.Get("_File").Add(new MenuItemViewModel("_Save", 5,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/VS2012TestApp;component/Icons/Save_6530.png")),
                                                      manager.GetCommand("SAVE"),
                                                      new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S")));
            vm.Get("_File").Add(new MenuItemViewModel("Close", 8, null, manager.GetCommand("CLOSE"),
                                                      new KeyGesture(Key.F4, ModifierKeys.Control, "Ctrl + F4")));


            vm.Add(new MenuItemViewModel("_Edit", 2));
            vm.Get("_Edit").Add(new MenuItemViewModel("_Undo", 1,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/VS2012TestApp;component/Icons/Undo_16x.png")),
                                                      ApplicationCommands.Undo));
            vm.Get("_Edit").Add(new MenuItemViewModel("_Redo", 2,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/VS2012TestApp;component/Icons/Redo_16x.png")),
                                                      ApplicationCommands.Redo));
            vm.Get("_Edit").Add(MenuItemViewModel.Separator(15));
            vm.Get("_Edit").Add(new MenuItemViewModel("Cut", 20,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/VS2012TestApp;component/Icons/Cut_6523.png")),
                                                      ApplicationCommands.Cut));
            vm.Get("_Edit").Add(new MenuItemViewModel("Copy", 21,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/VS2012TestApp;component/Icons/Copy_6524.png")),
                                                      ApplicationCommands.Copy));
            vm.Get("_Edit").Add(new MenuItemViewModel("_Paste", 22,
                                                      new BitmapImage(
                                                          new Uri(
                                                              @"pack://application:,,,/VS2012TestApp;component/Icons/Paste_6520.png")),
                                                      ApplicationCommands.Paste));

            vm.Add(new MenuItemViewModel("_View", 3));

            if (logger != null)
                vm.Get("_View").Add(new MenuItemViewModel("_Logger", 1,
                                                          new BitmapImage(
                                                              new Uri(
                                                                  @"pack://application:,,,/VS2012TestApp;component/Icons/Undo_16x.png")),
                                                          manager.GetCommand("LOGSHOW"))
                                        {IsCheckable = true, IsChecked = logger.IsVisible});

            vm.Get("_View").Add(new MenuItemViewModel("Themes", 1));
            vm.Get("_View").Get("Themes").Add(new MenuItemViewModel("Dark", 1, null, manager.GetCommand("THEMECHANGE")) { IsCheckable = true, IsChecked = true, CommandParameter = "Dark" });
            vm.Get("_View").Get("Themes").Add(new MenuItemViewModel("Light", 2, null, manager.GetCommand("THEMECHANGE")) { IsCheckable = true, IsChecked = false, CommandParameter = "Light"});
        }

        private void LoadToolbar()
        {
            var service = _container.Resolve<IToolbarService>();
            var vm = _container.Resolve<AbstractMenuItem>();
            //Parameter overrides are used to set parameter value when you are resolving using the container
            var mainToolbar = _container.Resolve<ToolbarViewModel>(new ParameterOverride("header", "mainToolbar"));
            var editToolbar = _container.Resolve<ToolbarViewModel>(new ParameterOverride("header", "editToolbar"));

            mainToolbar.Band = 0;
            mainToolbar.BandIndex = 1;
            editToolbar.Add(vm.Get("_Edit").Get("_Undo"));
            editToolbar.Add(MenuItemViewModel.Separator(2));
            editToolbar.Add(vm.Get("_Edit").Get("_Redo"));
            mainToolbar.Add(vm.Get("_Edit").Get("Cut"));
            mainToolbar.Add(vm.Get("_Edit").Get("Copy"));
            mainToolbar.Add(vm.Get("_Edit").Get("_Paste"));
            //mainToolbar.Add(vm.Get("_Edit"));
            mainToolbar.Band = 0;
            mainToolbar.BandIndex = 2;
            service.Add(mainToolbar);
            service.Add(editToolbar);
        }

        private void LoadCommands()
        {
            var manager = _container.Resolve<ICommandManager>();

            //throw new NotImplementedException();
            var openCommand = new DelegateCommand(OpenModule);
            var saveCommand = new DelegateCommand(SaveDocument, CanExecuteSaveDocument);
            var loggerCommand = new DelegateCommand(ToggleLogger);
            var themeCommand = new DelegateCommand<string>(ThemeChangeCommand);


            manager.RegisterCommand("OPEN", openCommand);
            manager.RegisterCommand("SAVE", saveCommand);
            manager.RegisterCommand("LOGSHOW", loggerCommand);
            manager.RegisterCommand("THEMECHANGE", themeCommand);
        }

        private void ThemeChangeCommand(string s)
        {
            var vm = _container.Resolve<AbstractMenuItem>();
            var manager = _container.Resolve<IThemeManager>();

            MenuItemViewModel mvm = vm.Get("_View").Get("Themes").Get(manager.CurrentTheme.Name) as MenuItemViewModel;
            mvm.IsChecked = false;
            manager.SetCurrent(s);
        }

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

        #endregion
    }
}