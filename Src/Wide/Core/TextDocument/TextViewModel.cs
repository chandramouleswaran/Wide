#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Core.TextDocument
{
    /// <summary>
    /// Class TextViewModel
    /// </summary>
    public class TextViewModel : ContentViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentViewModel" /> class.
        /// </summary>
        /// <param name="workspace">The injected workspace.</param>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="logger">The injected logger.</param>
        /// <param name="menuService">The menu service.</param>
        public TextViewModel(AbstractWorkspace workspace, ICommandManager commandManager, ILoggerService logger,
                             IMenuService menuService)
            : base(workspace, commandManager, logger, menuService)
        {
        }

        /// <summary>
        /// The title of the document
        /// </summary>
        /// <value>The tool tip.</value>
        public override string Tooltip
        {
            get { return Model.Location as String; }
            protected set { base.Tooltip = value; }
        }

        /// <summary>
        /// The content ID - unique value for each document. For TextViewModels, the contentID is "FILE:##:" + location of the file.
        /// </summary>
        /// <value>The content id.</value>
        public override string ContentId
        {
            get { return "FILE:##:" + Tooltip; }
        }
    }
}