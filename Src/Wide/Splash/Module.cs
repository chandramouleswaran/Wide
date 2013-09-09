#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Events;

namespace Wide.Splash
{
    [Module(ModuleName = "Wide.Splash")]
    internal class SplashModule : IModule
    {
        #region ctors

        public SplashModule(IUnityContainer container_, IEventAggregator eventAggregator_, IShell shell_)
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

        #region IModule Members

        public void Initialize()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(
                (Action) (() =>
                              {
                                  Shell.Show();
                                  EventAggregator.GetEvent<SplashCloseEvent>().Publish(new SplashCloseEvent());
                              }));

            WaitForCreation = new AutoResetEvent(false);

            ThreadStart showSplash =
                () =>
                    {
                        Dispatcher.CurrentDispatcher.BeginInvoke(
                            (Action) (() =>
                                          {
                                              Container.RegisterType<SplashViewModel, SplashViewModel>();
                                              ISplashView iSplashView;
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
                                              var splash = iSplashView as Window;
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

            var thread = new Thread(showSplash) {Name = "Splash Thread", IsBackground = true};
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            WaitForCreation.WaitOne();
        }

        #endregion
    }
}