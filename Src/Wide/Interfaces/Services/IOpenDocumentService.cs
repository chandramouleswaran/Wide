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
    /// Interface IOpenDocumentService - used to open a file
    /// </summary>
    public interface IOpenDocumentService
    {
        /// <summary>
        /// Opens the document from the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>ContentViewModel.</returns>
        ContentViewModel Open(object location = null);

        /// <summary>
        /// Opens from content from an ID.
        /// </summary>
        /// <param name="contentID">The content ID.</param>
        /// <param name="makeActive">if set to <c>true</c> makes the new document as the active document.</param>
        /// <returns>ContentViewModel.</returns>
        ContentViewModel OpenFromID(string contentID, bool makeActive = false);
    }
}