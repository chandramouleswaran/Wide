using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wide.Shell;


namespace VS2010TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AppBootstrapper b;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            b = new AppBootstrapper();
            b.Run();
        }
    }
}
