#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Wide.Interfaces
{
    public interface IWorkspace
    {
        /// <summary>
        /// The list of documents which are open in the workspace
        /// </summary>
        ObservableCollection<ContentViewModel> Documents { get; set; }

        /// <summary>
        /// The list of tools that are available in the workspace
        /// </summary>
        ObservableCollection<ToolViewModel> Tools { get; set; }

        /// <summary>
        /// The current document which is active in the workspace
        /// </summary>
        ContentViewModel ActiveDocument { get; set; }

        /// <summary>
        /// Gets the title of the application.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; }

        /// <summary>
        /// Gets the icon of the application.
        /// </summary>
        /// <value>The icon.</value>
        ImageSource Icon { get; }

        /// <summary>
        /// Closing this instance.
        /// </summary>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        /// <returns><c>true</c> if the application is closing, <c>false</c> otherwise</returns>
        bool Closing(CancelEventArgs e);
    }
}