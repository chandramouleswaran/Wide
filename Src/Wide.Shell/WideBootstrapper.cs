using System.Threading;
using System.Windows.Controls;
using AvalonDock;
using AvalonDock.Layout;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Services;

namespace Wide.Shell
{
    public class WideBootstrapper : UnityBootstrapper
    {
        private bool _isMetro;

        public WideBootstrapper(bool isMetro = true)
        {
            _isMetro = isMetro;
        }

        //If you want your own splash window - inherit from the bootstrapper and register type ISplashView
        protected override void InitializeModules()
        {
            if(!HideSplashWindow)
            {
                IModule splashModule = Container.Resolve<Wide.Splash.Module>();
                splashModule.Initialize();
            }

            base.InitializeModules();
            Application.Current.MainWindow.DataContext = Container.Resolve<AbstractWorkspace>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            DirectoryModuleCatalog catalog = new DirectoryModuleCatalog() {ModulePath = @".\Internal"};            
            return catalog;
        }

        protected override void ConfigureContainer()
        {
            //Create an instance of the workspace
            if(_isMetro)
            {
                //Use MahApps Metro window
                Container.RegisterType<IShell, ShellViewMetro>(new ContainerControlledLifetimeManager());
            }
            else
            {
                //Use regular window
                Container.RegisterType<IShell, ShellView>(new ContainerControlledLifetimeManager());
            }
            base.ConfigureContainer();
        }

        protected override System.Windows.DependencyObject CreateShell()
        {
            return (DependencyObject)Container.Resolve<IShell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
        }

        public bool HideSplashWindow { get; set; }
    }
}
