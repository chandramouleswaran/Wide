#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

namespace Wide.Interfaces.Services
{
    /// <summary>
    /// Enum LogCategory
    /// </summary>
    public enum LogCategory
    {
        Debug,
        Exception,
        Info,
        Warn,
        Error
    }

    /// <summary>
    /// Enum LogPriority
    /// </summary>
    public enum LogPriority
    {
        None,
        Low,
        Medium,
        High
    }

    /// <summary>
    /// Interface ILoggerService - used for logging in the application
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Gets the message which just got logged.
        /// </summary>
        /// <value>The message.</value>
        string Message { get; }

        /// <summary>
        /// Gets the category of logging.
        /// </summary>
        /// <value>The category.</value>
        LogCategory Category { get; }

        /// <summary>
        /// Gets the priority of logging.
        /// </summary>
        /// <value>The priority.</value>
        LogPriority Priority { get; }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The logging category.</param>
        /// <param name="priority">The logging priority.</param>
        void Log(string message, LogCategory category, LogPriority priority);
    }
}