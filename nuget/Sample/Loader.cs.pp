using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace $rootnamespace$
{
    class Loader
    {
        private IUnityContainer _container;

        public Loader(IUnityContainer container)
        {
            _container = container;
            LoadCommands();
            LoadMenus();
            LoadToolbar();
        }


        private void LoadMenus()
        {
            ICommandManager manager = _container.Resolve<ICommandManager>();
            AbstractMenuItem vm = _container.Resolve<AbstractMenuItem>();
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();

            vm.Add(new MenuItemViewModel("_File", 1));

            vm.Get("_File").Add((new MenuItemViewModel("_Open", 4, null, manager.GetCommand("OPEN"), new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl + O"))));
            vm.Get("_File").Add(new MenuItemViewModel("_Save", 5, null, manager.GetCommand("SAVE"), new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S")));
            vm.Get("_File").Add(new MenuItemViewModel("Close", 8, null, manager.GetCommand("CLOSE"), new KeyGesture(Key.F4, ModifierKeys.Control, "Ctrl + F4")));


            vm.Add(new MenuItemViewModel("_Edit", 2));
            vm.Get("_Edit").Add(new MenuItemViewModel("_Undo", 1, null, ApplicationCommands.Undo));
            vm.Get("_Edit").Add(new MenuItemViewModel("_Redo", 2, null, ApplicationCommands.Redo));
            vm.Get("_Edit").Add(MenuItemViewModel.Separator(15));
            vm.Get("_Edit").Add(new MenuItemViewModel("Cut", 20, new BitmapImage(new Uri(@"pack://application:,,,/$rootnamespace$;component/Icons/Cut_6523.png")), ApplicationCommands.Cut));
            vm.Get("_Edit").Add(new MenuItemViewModel("Copy", 21, new BitmapImage(new Uri(@"pack://application:,,,/$rootnamespace$;component/Icons/Copy_6524.png")), ApplicationCommands.Copy));
            vm.Get("_Edit").Add(new MenuItemViewModel("_Paste", 22, new BitmapImage(new Uri(@"pack://application:,,,/$rootnamespace$;component/Icons/Paste_6520.png")), ApplicationCommands.Paste));

            vm.Add(new MenuItemViewModel("_View", 3));
            
            ToolViewModel logger = workspace.Tools.First(f => f.ContentId == "Logger");
            if (logger != null)
                vm.Get("_View").Add(new MenuItemViewModel("_Logger", 1, null, manager.GetCommand("LOGSHOW")) { IsCheckable = true, IsChecked = logger.IsVisible });
        }

        private void LoadToolbar()
        {
            IToolbarService service = _container.Resolve<IToolbarService>();
            AbstractMenuItem vm = _container.Resolve<AbstractMenuItem>();
            //Parameter overrides are used to set parameter value when you are resolving using the container
            ToolbarViewModel mainToolbar = _container.Resolve<ToolbarViewModel>(new ParameterOverride("header", "mainToolbar"));

            mainToolbar.Add(vm.Get("_Edit").Get("Cut"));
            mainToolbar.Add(vm.Get("_Edit").Get("Copy"));
            mainToolbar.Add(vm.Get("_Edit").Get("_Paste"));
            mainToolbar.Band = 0;
            mainToolbar.BandIndex = 1;
            service.Add(mainToolbar);
        }

        private void LoadCommands()
        {
            ICommandManager manager = _container.Resolve<ICommandManager>();

            //throw new NotImplementedException();
            DelegateCommand openCommand = new DelegateCommand(OpenModule);
            DelegateCommand saveCommand = new DelegateCommand(SaveDocument, CanExecuteSaveDocument);
            DelegateCommand loggerCommand = new DelegateCommand(ToggleLogger);


            manager.RegisterCommand("OPEN", openCommand);
            manager.RegisterCommand("SAVE", saveCommand);
            manager.RegisterCommand("LOGSHOW", loggerCommand);
        }

        private void ToggleLogger()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            AbstractMenuItem vm = _container.Resolve<AbstractMenuItem>();
            ToolViewModel logger = workspace.Tools.First(f => f.ContentId == "Logger");
            if (logger != null)
            {
                logger.IsVisible = !logger.IsVisible;
                AbstractMenuItem mi = vm.Get("_View").Get("_Logger") as AbstractMenuItem;
                mi.IsChecked = logger.IsVisible;
            }
        }

        #region Commands
        #region Open
        private void OpenModule()
        {
            IOpenFileService service = _container.Resolve<IOpenFileService>();
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
