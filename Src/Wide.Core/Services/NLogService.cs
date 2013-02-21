using System.Diagnostics;
using System.Reflection;
using Wide.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using NLog;
using Wide.Interfaces.Services;

namespace Wide.Core.Logging
{
    public class NLogService : ILoggerService
    {
        private readonly IEventAggregator _aggregator;
        private static Logger logger = LogManager.GetLogger("Wide");

        private NLogService()
        {
        }

        public NLogService(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        public void Log(string message, LogCategory category, LogPriority priority)
        {
            this.Message = message;
            this.Category = category;
            this.Priority = priority;

            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1); // 0 will be the inner-most method
            MethodBase method = frame.GetMethod();

            logger.Log(LogLevel.Error,method.DeclaringType + ": " + message);

            _aggregator.GetEvent<LogEvent>().Publish(new NLogService(){Message = this.Message, Category = this.Category, Priority = this.Priority});
        }

        public string Message { get; internal set; }
        public LogCategory Category { get; internal set; }
        public LogPriority Priority { get; internal set; }
    }
}
