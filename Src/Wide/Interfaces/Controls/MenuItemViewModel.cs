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

namespace Wide.Interfaces.Controls
{
    /// <summary>
    /// Class MenuItemViewModel - simple menu implementation which can be reused by apps
    /// </summary>
    public sealed class MenuItemViewModel : AbstractMenuItem
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
        public MenuItemViewModel(string header, int priority, ImageSource icon = null, ICommand command = null,
                                 KeyGesture gesture = null, bool isCheckable = false, bool hideDisabled = false,
                                 IUnityContainer container = null)
            : base(header, priority, icon, command, gesture, isCheckable, hideDisabled)
        {
        }

        #endregion

        #region Static

        /// <summary>
        /// Creates an instance of a menu acting as a separator with a priority
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <returns>AbstractMenuItem.</returns>
        public static AbstractMenuItem Separator(int priority)
        {
            return new MenuItemViewModel("SEP", priority);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checkable.
        /// </summary>
        /// <value><c>true</c> if this instance is checkable; otherwise, <c>false</c>.</value>
        public new bool IsCheckable
        {
            get { return base.IsCheckable; }
            set { base.IsCheckable = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public new bool IsChecked
        {
            get { return base.IsChecked; }
            set { base.IsChecked = value; }
        }

        #endregion
    }
}