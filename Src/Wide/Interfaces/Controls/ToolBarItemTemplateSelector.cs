#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Wide.Interfaces.Controls
{
    /// <summary>
    /// Class ToolBarItemTemplateSelector
    /// </summary>
    internal class ToolBarItemTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the button template.
        /// </summary>
        /// <value>The button template.</value>
        public DataTemplate ButtonTemplate { get; set; }

        /// <summary>
        /// Gets or sets the combo box template.
        /// </summary>
        /// <value>The combo box template.</value>
        public DataTemplate ComboBoxTemplate { get; set; }

        /// <summary>
        /// Gets or sets the separator template.
        /// </summary>
        /// <value>The separator template.</value>
        public DataTemplate SeparatorTemplate { get; set; }

        /// <summary>
        /// When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.
        /// Here, it looks at the item definition and determines if the toolbar item needs to be a button or combo box or a separator.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a <see cref="T:System.Windows.DataTemplate" /> or null. The default value is null.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var toolBarItem = item as AbstractMenuItem;
            if (toolBarItem != null && !toolBarItem.IsSeparator)
            {
                if (toolBarItem.Children.Count > 0)
                    return ComboBoxTemplate;

                return ButtonTemplate;
            }
            return SeparatorTemplate;
        }
    }
}