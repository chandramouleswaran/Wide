// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Diagnostics;
using System.Reflection;
using Microsoft.Practices.Prism.Events;
using NLog;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    internal class NLogService : ILoggerService
    {
        private static readonly Logger Logger = LogManager.GetLogger("Wide");
        private readonly IEventAggregator _aggregator;

        private NLogService()
        {
        }

        public NLogService(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        #region ILoggerService Members

        public void Log(string message, LogCategory category, LogPriority priority)
        {
            Message = message;
            Category = category;
            Priority = priority;

            var trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1); // 0 will be the inner-most method
            MethodBase method = frame.GetMethod();

            Logger.Log(LogLevel.Error, method.DeclaringType + ": " + message);

            _aggregator.GetEvent<LogEvent>().Publish(new NLogService
                                                         {Message = Message, Category = Category, Priority = Priority});
        }

        public string Message { get; internal set; }
        public LogCategory Category { get; internal set; }
        public LogPriority Priority { get; internal set; }

        #endregion
    }
}