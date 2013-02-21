using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wide.Core.Logging;
using Wide.Core.Services;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System.Windows.Controls;
using Wide.Interfaces.Themes;
using CommandManager = Wide.Core.Services.CommandManager;

namespace Wide.Core
{
    public class CoreModule : IModule
    {
        private readonly IUnityContainer _container;

        public CoreModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            EventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent { Message = "Loading Core Module" });            
            _container.RegisterType<TextViewModel>();
            _container.RegisterType<TextModel>();
            _container.RegisterType<TextView>();
            _container.RegisterType<AllFileHandler>();

            _container.RegisterType<IOpenFileService, OpenFileService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ICommandManager, CommandManager>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IContentHandlerRegistry, ContentHandlerRegistry>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IThemeManager, ThemeManager>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ILoggerService, NLogService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IToolbarService, ToolbarService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<AbstractMenuItem, MenuItemViewModel>(new ContainerControlledLifetimeManager(), new InjectionConstructor(new InjectionParameter(typeof(string), "$MAIN$"), new InjectionParameter(typeof(int), 1), new InjectionParameter(typeof(ImageSource), null), new InjectionParameter(typeof(ICommand), null), new InjectionParameter(typeof(KeyGesture), null), new InjectionParameter(typeof(bool), false), new InjectionParameter(typeof(IUnityContainer), this._container)));
            _container.RegisterType<ToolbarViewModel>(new InjectionConstructor(new InjectionParameter(typeof(string), "$MAIN$"), new InjectionParameter(typeof(int), 1), new InjectionParameter(typeof(ImageSource), null), new InjectionParameter(typeof(ICommand), null), new InjectionParameter(typeof(bool), false), new InjectionParameter(typeof(IUnityContainer), this._container)));

            //Register a default file opener
            IContentHandlerRegistry registry = _container.Resolve<IContentHandlerRegistry>();
            registry.Register(_container.Resolve<AllFileHandler>());

            AppCommands();
            AppTheme();
            //AppMenu();
            //AppToolbar();

            //Try resolving a workspace
            IWorkspace workspace;
            try
            {
                workspace = _container.Resolve<AbstractWorkspace>();
            }
            catch
            {
                _container.RegisterType<AbstractWorkspace, Workspace>(new ContainerControlledLifetimeManager());
            }
        }

        private void AppCommands()
        {
            ICommandManager manager = _container.Resolve<ICommandManager>();

            //TODO: Check if you can hook up to the Workspace.ActiveDocument.CloseCommand
            DelegateCommand closeCommand = new DelegateCommand(CloseDocument, CanExecuteCloseDocument);
            manager.RegisterCommand("CLOSE", closeCommand);
        }

        #region Commands
        private bool CanExecuteCloseDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            return workspace.ActiveDocument != null;
        }

        private void CloseDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            MessageBoxResult res = MessageBoxResult.Cancel;
            if (workspace.ActiveDocument.Model.IsDirty)
            {
                //means the document is dirty - show a message box and then handle based on the user's selection
                res = MessageBox.Show(string.Format("Save changes for document '{0}'?", workspace.ActiveDocument.Title), "Are you sure?", MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Yes)
                {
                    workspace.ActiveDocument.Handler.SaveContent(workspace.ActiveDocument);
                }
                if (res != MessageBoxResult.Cancel)
                {
                    workspace.Documents.Remove(workspace.ActiveDocument);
                }
            }
            else
            {
                workspace.Documents.Remove(workspace.ActiveDocument);
            }

        }
        #endregion

        private void AppTheme()
        {
            IThemeManager manager = _container.Resolve<IThemeManager>();
            //manager.AddTheme(new DefaultTheme());
            //manager.AddTheme(new DarkTheme());
        }

        private IEventAggregator EventAggregator
        {
            get { return _container.Resolve<IEventAggregator>(); }
        }
    }
}
