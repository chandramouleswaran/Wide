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
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    /// <summary>
    /// The command manager class where you can register and query for a command
    /// </summary>
    internal sealed class CommandManager : ICommandManager
    {
        /// <summary>
        /// The dictionary which holds all the commands
        /// </summary>
        private readonly Dictionary<string, ICommand> _commands;

        /// <summary>
        /// Command manager constructor
        /// </summary>
        public CommandManager()
        {
            _commands = new Dictionary<string, ICommand>();
        }

        #region ICommandManager Members

        /// <summary>
        /// Register a command with the command manager
        /// </summary>
        /// <param name="name">The name of the command</param>
        /// <param name="command">The command to register</param>
        /// <returns>true if a command is registered, false otherwise</returns>
        public bool RegisterCommand(string name, ICommand command)
        {
            if (_commands.ContainsKey(name))
                throw new Exception("Command " + name + " already exists !");

            _commands.Add(name, command);
            return true;
        }

        /// <summary>
        /// Gets the command if registered with the command manager
        /// </summary>
        /// <param name="name">The name of the command</param>
        /// <returns>The command if available, null otherwise</returns>
        public ICommand GetCommand(string name)
        {
            if (_commands.ContainsKey(name))
                return _commands[name];
            return null;
        }

        /// <summary>
        /// Calls RaiseCanExecuteChanged on all Delegate commands available in the command manager
        /// </summary>
        public void Refresh()
        {
            foreach (var keyValuePair in _commands)
            {
                if (keyValuePair.Value is DelegateCommand)
                {
                    var c = keyValuePair.Value as DelegateCommand;
                    c.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion
    }
}