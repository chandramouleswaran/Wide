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
namespace Wide.Interfaces.Services
{
    /// <summary>
    /// Interface IContentHandlerRegistry
    /// </summary>
    public interface IContentHandlerRegistry
    {
        /// <summary>
        /// Register a content handler with the registry
        /// </summary>
        /// <param name="handler">The content handler</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise</returns>
        bool Register(IContentHandler handler);

        /// <summary>
        /// Unregisters a content handler
        /// </summary>
        /// <param name="handler">The handler to remove</param>
        /// <returns><c>true</c> if successfully unregistered, <c>false</c> otherwise</returns>
        bool Unregister(IContentHandler handler);

        /// <summary>
        /// Returns a content view model for the specified object which needs to be displayed as a document
        /// The object could be anything - based on the handlers, a content view model is returned
        /// </summary>
        /// <param name="info">The object which needs to be displayed as a document in Wide</param>
        /// <returns>The content view model for the given info</returns>
        ContentViewModel GetViewModel(object info);

        /// <summary>
        /// Returns a content view model for the specified contentID which needs to be displayed as a document
        /// The contentID is the ID used in AvalonDock
        /// </summary>
        /// <param name="contentId">The contentID which needs to be displayed as a document in Wide</param>
        /// <returns>The content view model for the given info</returns>
        ContentViewModel GetViewModelFromContentId(string contentId);

        /// <summary>
        /// Gets the command which provides the option to create a new document.
        /// </summary>
        /// <value>The new command.</value>
        ICommand NewCommand { get; }
    }
}