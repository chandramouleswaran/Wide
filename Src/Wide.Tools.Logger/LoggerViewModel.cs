#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;

namespace Wide.Tools.Logger
{
    internal class LoggerViewModel : ToolViewModel
    {
        private readonly IEventAggregator _aggregator;
        private readonly IUnityContainer _container;
        private readonly LoggerModel _model;
        private readonly LoggerView _view;
        private IWorkspace _workspace;

        public LoggerViewModel(IUnityContainer container, AbstractWorkspace workspace)
        {
            _workspace = workspace;
            _container = container;
            Name = "Logger";
            Title = "Logger";
            ContentId = "Logger";
            _model = new LoggerModel();
            Model = _model;
            IsVisible = false;

            _view = new LoggerView();
            _view.DataContext = _model;
            View = _view;

            _aggregator = _container.Resolve<IEventAggregator>();
            _aggregator.GetEvent<LogEvent>().Subscribe(AddLog);
        }

        private void AddLog(ILoggerService logger)
        {
            _model.AddLog(logger);
        }

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Bottom; }
        }
    }
}