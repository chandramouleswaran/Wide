// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.ComponentModel;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Core.TextDocument
{
    public class TextModel : ContentModel
    {
        private readonly ICommandManager _commandManager;
        protected string OldText;

        public TextModel(ICommandManager commandManager)
        {
            Document = new ICSharpCode.AvalonEdit.Document.TextDocument();
            _commandManager = commandManager;
            Document.PropertyChanged += DocumentPropertyChanged;
            Document.TextChanged += DocumentOnTextChanged;
            OldText = "";
        }

        public override bool IsDirty
        {
            get { return base.IsDirty; }
            set
            {
                base.IsDirty = value;
                if (value == false)
                {
                    OldText = Document.Text;
                }
                else
                {
                    _commandManager.Refresh();
                }
            }
        }

        public ICSharpCode.AvalonEdit.Document.TextDocument Document { get; protected set; }

        private void DocumentOnTextChanged(object sender, EventArgs eventArgs)
        {
            IsDirty = (OldText != Document.Text);
        }

        private void DocumentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Document");
        }
    }
}