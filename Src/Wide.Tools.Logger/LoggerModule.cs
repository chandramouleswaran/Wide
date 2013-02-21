using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Services;

namespace Wide.Tools.Logger
{
    public class LoggerModule : IModule
    {
        private readonly IUnityContainer _container;

        public LoggerModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            EventAggregator.GetEvent<SplashMessageUpdateEvent>().Publish(new SplashMessageUpdateEvent { Message = "Loading Logger Module" });
            _container.RegisterType<LoggerViewModel>();
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            workspace.Tools.Add(_container.Resolve<LoggerViewModel>());
        }

        private IEventAggregator EventAggregator
        {
            get { return _container.Resolve<IEventAggregator>(); }
        }
    }
}
