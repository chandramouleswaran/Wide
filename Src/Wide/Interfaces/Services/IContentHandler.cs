#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

namespace Wide.Interfaces.Services
{
    /// <summary>
    /// Interface IContentHandler
    /// </summary>
    public interface IContentHandler
    {
        /// <summary>
        /// Creates a new content.
        /// </summary>
        /// <param name="parameter">The parameter needed to create a new content.</param>
        /// <returns>ContentViewModel.</returns>
        ContentViewModel NewContent(object parameter);

        /// <summary>
        /// Opens the content.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns>ContentViewModel.</returns>
        ContentViewModel OpenContent(object info);

        /// <summary>
        /// Opens the content from id.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <returns>ContentViewModel.</returns>
        ContentViewModel OpenContentFromId(string contentId);

        /// <summary>
        /// Saves the content.
        /// </summary>
        /// <param name="contentViewModel">The content view model.</param>
        /// <param name="saveAs">if set to <c>true</c> the document needs to be saved as - you need to implement logic for saving the document.</param>
        /// <returns><c>true</c> if successfully saved, <c>false</c> otherwise</returns>
        bool SaveContent(ContentViewModel contentViewModel, bool saveAs = false);

        /// <summary>
        /// Validates the content from id.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <returns><c>true</c> if this handler is able to open the contentID, <c>false</c> otherwise</returns>
        bool ValidateContentFromId(string contentId);

        /// <summary>
        /// Validates the type of the content.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns><c>true</c> if this handler is able to open info, <c>false</c> otherwise</returns>
        bool ValidateContentType(object info);
    }
}