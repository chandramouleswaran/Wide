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
using Microsoft.Practices.Prism.Events;
using Wide.Interfaces.Controls;
using Wide.Interfaces.Events;

namespace Wide.Interfaces.Controls
{
    /// <summary>
    /// Class SaveAsMenuItemViewModel - simple menu implementation with events
    /// </summary>
    public sealed class SaveAsMenuItemViewModel : AbstractMenuItem
    {
        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="command">The command.</param>
        /// <param name="gesture">The gesture.</param>
        /// <param name="isCheckable">if set to <c>true</c> this menu acts as a checkable menu.</param>
        /// <param name="hideDisabled">if set to <c>true</c> this menu is not visible when disabled.</param>
        /// <param name="container">The container.</param>
        public SaveAsMenuItemViewModel(string header, int priority, ImageSource icon = null, ICommand command = null,
                                 KeyGesture gesture = null, bool isCheckable = false, bool hideDisabled = false,
                                 IUnityContainer container = null)
            : base(header, priority, icon, command, gesture, isCheckable, hideDisabled)
        {
            if (container != null)
            {
                IEventAggregator eventAggregator = container.Resolve<IEventAggregator>();
                eventAggregator.GetEvent<ActiveContentChangedEvent>().Subscribe(SaveAs);
            }
        }
        #endregion

        private void SaveAs(ContentViewModel cvm)
        {
            if (cvm != null)
            {
                this.Header = "Save " + cvm.Title + " As...";
            }
            else
            { this.Header = "Save As..."; }
        }
    }
}