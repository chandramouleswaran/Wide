#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Windows.Controls;
using Wide.Interfaces.Controls;

namespace Wide.Interfaces.Services
{
    /// <summary>
    /// Interface IToolbarService - the application's toolbar tray is returned by this service
    /// </summary>
    public interface IToolbarService
    {
        /// <summary>
        /// Gets the tool bar tray of the application.
        /// </summary>
        /// <value>The tool bar tray.</value>
        ToolBarTray ToolBarTray { get; }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if successfully added, <c>false</c> otherwise</returns>
        string Add(AbstractCommandable item);

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="GUID">The GUID.</param>
        /// <returns><c>true</c> if successfully removed, <c>false</c> otherwise</returns>
        bool Remove(string GUID);

        /// <summary>
        /// Gets the specified toolbar using the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>AbstractCommandable.</returns>
        AbstractCommandable Get(string key);

        /// <summary>
        /// Gets the right click menu.
        /// </summary>
        /// <value>The right click menu.</value>
        AbstractMenuItem RightClickMenu { get; }
    }
}