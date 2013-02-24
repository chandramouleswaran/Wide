using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wide.Interfaces;
using Wide.Shell;
using Microsoft.Practices.Unity;
using Wide.Splash;

namespace VS2010TestApp
{
    class AppBootstrapper : WideBootstrapper
    {
        public AppBootstrapper(bool isMetro = true) : base(isMetro)
        {
        }

        protected override void InitializeModules()
        {
            //Register your splash view or else the default splash will load
            Container.RegisterType<ISplashView, AppSplash>();

            //Register your workspace here - if you have any
            Container.RegisterType<AbstractWorkspace, IconWorkspace>(new ContainerControlledLifetimeManager());

            // You can also override the logger service. Currently, NLog is used.
            // Since the config file is there in the output location, text files should be available in the Logs folder.

            //Initialize the original bootstrapper which will load modules from the probing path. Check app.config for probing path.
            base.InitializeModules();

            //Load the required stuff
            //NOTE: this should probably be done from other modules. This is used only for demo purpose.
            Loader loader = new Loader(Container);
        }
    }
}
