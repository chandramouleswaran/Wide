#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Controls;

namespace Wide.Interfaces.Controls
{
    /// <summary>
    /// Class ToolbarViewModel
    /// </summary>
    public sealed class ToolbarViewModel : AbstractToolbar
    {
        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolbarViewModel"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="command">The command.</param>
        /// <param name="isCheckable">if set to <c>true</c> does nothing in the case of toolbar - default value is false.</param>
        /// <param name="container">The container.</param>
        /// <exception cref="System.ArgumentException">Header cannot be SEP for a Toolbar</exception>
        public ToolbarViewModel(string header, int priority, ImageSource icon = null, ICommand command = null,
                                bool isCheckable = false, IUnityContainer container = null)
            : base(header, priority, icon, command, isCheckable, container)
        {
        }

        #endregion
    }
}