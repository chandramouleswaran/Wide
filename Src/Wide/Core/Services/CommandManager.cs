// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Wide.Interfaces.Services;

namespace Wide.Core.Services
{
    internal sealed class CommandManager : ICommandManager
    {
        private readonly Dictionary<string, ICommand> _commands;

        public CommandManager()
        {
            _commands = new Dictionary<string, ICommand>();
        }

        #region ICommandManager Members

        public bool RegisterCommand(string name, ICommand command)
        {
            if (_commands.ContainsKey(name))
                throw new Exception("Commmand " + name + " already exists !");

            _commands.Add(name, command);
            return true;
        }

        public ICommand GetCommand(string name)
        {
            if (_commands.ContainsKey(name))
                return _commands[name];
            return null;
        }


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