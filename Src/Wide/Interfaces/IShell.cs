#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

namespace Wide.Interfaces
{
    /// <summary>
    /// Interface IShell
    /// </summary>
    public interface IShell
    {
        /// <summary>
        /// Closes this instance of the shell.
        /// </summary>
        void Close();

        /// <summary>
        /// Shows this instance of the shell
        /// </summary>
        void Show();

        /// <summary>
        /// Loads the layout of the shell from previous run.
        /// </summary>
        void LoadLayout();

        /// <summary>
        /// Saves the layout of the shell.
        /// </summary>
        void SaveLayout();


        /// <summary>
        /// Gets the top.
        /// </summary>
        /// <value>The top.</value>
        double Top { get; }

        /// <summary>
        /// Gets the left.
        /// </summary>
        /// <value>The left.</value>
        double Left { get; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        double Width { get; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        double Height { get; }
    }
}