using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace Wide.Splash
{
    public class Module : IModule
    {
        #region ctors
        public Module(IUnityContainer container_, IEventAggregator eventAggregator_, IShell shell_)
        {
            Container = container_;
            EventAggregator = eventAggregator_;
            Shell = shell_;
        }
        #endregion

        #region Private Properties
        private IUnityContainer Container { get; set; }

        private IEventAggregator EventAggregator { get; set; }

        private IShell Shell { get; set; }

        private AutoResetEvent WaitForCreation { get; set; }
        #endregion

        public void Initialize()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(
                (Action)(() =>
                {
                    Shell.Show();
                    EventAggregator.GetEvent<SplashCloseEvent>().Publish(new SplashCloseEvent());
                }));

            WaitForCreation = new AutoResetEvent(false);

            ThreadStart showSplash =
                () =>
                {
                    Dispatcher.CurrentDispatcher.BeginInvoke(
                    (Action)(() =>
                    {
                        Container.RegisterType<SplashViewModel, SplashViewModel>();
                        ISplashView iSplashView = null;
                        try
                        {
                            //The end user might have set a splash view - try to use that
                            iSplashView = Container.Resolve<ISplashView>();
                        }
                        catch (Exception)
                        {
                            Container.RegisterType<ISplashView, SplashView>();
                            iSplashView = Container.Resolve<ISplashView>();
                        }
                        Window splash = iSplashView as Window;
                        if (splash != null)
                        {
                            EventAggregator.GetEvent<SplashCloseEvent>().Subscribe(
                                e_ => splash.Dispatcher.BeginInvoke((Action) splash.Close),
                                ThreadOption.PublisherThread, true);

                            splash.Show();
                            WaitForCreation.Set();
                        }
                    }));

                    Dispatcher.Run();
                };

            var thread = new Thread(showSplash) { Name = "Splash Thread", IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            WaitForCreation.WaitOne();
        }
    }
}
