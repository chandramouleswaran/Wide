#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Collections.Generic;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Shell;
using Wide.Splash;

namespace WideMD
{
    internal class MDBootstrapper : WideBootstrapper
    {
        public MDBootstrapper(bool isMetro = true)
            : base(isMetro)
        {
        }

        protected override void InitializeModules()
        {
            //Register your splash view or else the default splash will load
            Container.RegisterType<ISplashView, AppSplash>();

            //Register your workspace here - if you have any
            Container.RegisterType<AbstractWorkspace, MDWorkspace>(new ContainerControlledLifetimeManager());

            // You can also override the logger service. Currently, NLog is used.
            // Since the config file is there in the output location, text files should be available in the Logs folder.

            //Initialize the original bootstrapper which will load modules from the probing path. Check app.config for probing path.
            base.InitializeModules();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new MultipleDirectoryModuleCatalog(new List<string>() {@".", @".\External", @".\Internal"});
            return catalog;
        }
    }
}