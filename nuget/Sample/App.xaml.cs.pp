using System.Windows;

namespace $rootnamespace$
{
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
