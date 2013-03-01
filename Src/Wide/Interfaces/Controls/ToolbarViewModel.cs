// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Unity;

namespace Wide.Interfaces
{
    // A toolbar basically has the same commands as the one's in the menu
    public sealed class ToolbarViewModel : AbstractMenuItem
    {
        #region CTOR

        public ToolbarViewModel(string header, int priority, ImageSource icon = null, ICommand command = null,
                                bool isCheckable = false, IUnityContainer container = null)
            : base(header, priority, icon, command, null, isCheckable)
        {
            Priority = priority;
            IsSeparator = false;
            Header = header;
            Key = header;
            Command = command;
            IsCheckable = isCheckable;
            Icon = icon;
            if (isCheckable)
            {
                IsChecked = false;
            }
            if (Header == "SEP")
            {
                throw new ArgumentException("Header cannot be SEP for a Toolbar");
            }
        }

        #endregion

        #region Properties

        public int Band { get; set; }

        public int BandIndex { get; set; }

        #endregion
    }
}