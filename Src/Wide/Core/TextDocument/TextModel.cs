// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.ComponentModel;
using ICSharpCode.AvalonEdit.Document;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Core
{
    public class TextModel : ContentModel
    {
        private readonly ICommandManager _commandManager;
        protected string _oldText;

        public TextModel(ICommandManager commandManager)
        {
            Document = new TextDocument();
            _commandManager = commandManager;
            Document.PropertyChanged += Document_PropertyChanged;
            Document.TextChanged += DocumentOnTextChanged;
            _oldText = "";
        }

        public override bool IsDirty
        {
            get { return base.IsDirty; }
            set
            {
                base.IsDirty = value;
                if (value == false)
                {
                    _oldText = Document.Text;
                }
                else
                {
                    _commandManager.Refresh();
                }
            }
        }

        public TextDocument Document { get; protected set; }

        private void DocumentOnTextChanged(object sender, EventArgs eventArgs)
        {
            IsDirty = (_oldText != Document.Text);
        }

        private void Document_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Document");
        }
    }
}