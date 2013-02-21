using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Services;

namespace Wide.Tools.Logger
{
    class LoggerViewModel : ToolViewModel
    {
        private LoggerModel _model;
        private LoggerView _view;
        private IWorkspace _workspace;
        private IUnityContainer _container;
        private IEventAggregator _aggregator;

        public LoggerViewModel(IUnityContainer container, AbstractWorkspace workspace)
        {
            _workspace = workspace;
            _container = container;
            this.Name = "Logger";
            this.Title = "Logger";
            this.ContentId = "Logger";
            _model = new LoggerModel();
            this.Model = _model;
            this.IsVisible = false;

            _view = new LoggerView();
            _view.DataContext = _model;
            this.View = _view;

            _aggregator = _container.Resolve<IEventAggregator>();
            _aggregator.GetEvent<LogEvent>().Subscribe(AddLog);
        }

        private void AddLog(ILoggerService logger)
        {
            _model.AddLog(logger);
        }
    }
}
