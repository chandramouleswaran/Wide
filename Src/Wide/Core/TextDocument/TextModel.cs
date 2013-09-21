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
using System.ComponentModel;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Core.TextDocument
{
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class TextModel : ContentModel
    {
        /// <summary>
        /// The command manager
        /// </summary>
        protected readonly ICommandManager _commandManager;

        /// <summary>
        /// The menu service
        /// </summary>
        protected readonly IMenuService _menuService;

        /// <summary>
        /// The old text which was last saved
        /// </summary>
        protected string OldText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public TextModel(ICommandManager commandManager, IMenuService menuService)
        {
            Document = new ICSharpCode.AvalonEdit.Document.TextDocument();
            _commandManager = commandManager;
            _menuService = menuService;
            Document.PropertyChanged += DocumentPropertyChanged;
            Document.TextChanged += DocumentOnTextChanged;
            OldText = "";
        }

        /// <summary>
        /// Is the document dirty - does it need to be saved?
        /// </summary>
        /// <value><c>true</c> if this instance is dirty; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public override bool IsDirty
        {
            get { return base.IsDirty; }
            protected internal set
            {
                base.IsDirty = value;
                if (value == false)
                {
                    OldText = Document.Text;
                }
            }
        }

        /// <summary>
        /// Gets or sets the AvalonEdit's text document.
        /// </summary>
        /// <value>The document.</value>
        [Browsable(false)]
        public ICSharpCode.AvalonEdit.Document.TextDocument Document { get; protected set; }

        /// <summary>
        /// Documents the on text changed.
        /// </summary>
        /// <param name="sender">The sender - a text editor instance.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DocumentOnTextChanged(object sender, EventArgs eventArgs)
        {
            IsDirty = (OldText != Document.Text);
        }

        /// <summary>
        /// Documents the property changed.
        /// </summary>
        /// <param name="sender">The sender - a text model instance.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void DocumentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Document");
        }

        internal void SetLocation(object location)
        {
            this.Location = location;
            RaisePropertyChanged("Location");
        }
    }
}