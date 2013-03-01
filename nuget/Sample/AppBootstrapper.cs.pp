using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Shell;

namespace $rootnamespace$
{
    internal class AppBootstrapper : WideBootstrapper
    {
        public AppBootstrapper(bool isMetro = false)
            : base(isMetro)
        {
        }

        protected override void InitializeModules()
        {
            //Register your splash view or else the default splash will load
            //Container.RegisterType<ISplashView, AppSplash>();
			
            //Register your workspace here - if you have any
            Container.RegisterType<AbstractWorkspace, IconWorkspace>(new ContainerControlledLifetimeManager());

            // You can also override the logger service. Currently, NLog is used.
            // Since the config file is there in the output location, text files should be available in the Logs folder.

            //Initialize the original bootstrapper which will load modules from the probing path. Check app.config for probing path.
            base.InitializeModules();

            //Load the required stuff
            //NOTE: this should probably be done from other modules. This is used only for demo purpose.
            var loader = new Loader(Container);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new DirectoryModuleCatalog {ModulePath = "."};
            return catalog;
        }
    }
}