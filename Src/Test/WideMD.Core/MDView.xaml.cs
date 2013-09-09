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
using MarkdownSharp;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.Threading;

namespace WideMD.Core
{
    /// <summary>
    /// Interaction logic for MDView.xaml
    /// </summary>
    public partial class MDView : IContentView
    {
        private Markdown _md;
        private IStatusbarService _statusbar;
        private Thread t;

        public MDView(IStatusbarService statusbar)
        {
            _md = new Markdown();
            this._statusbar = statusbar;
            InitializeComponent();
            textEditor.TextArea.Caret.PositionChanged += Caret_PositionChanged;
        }

        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            Update();
        }

        private void textEditor_TextChanged(object sender, EventArgs e)
        {
            var model = this.DataContext as MDModel;
            if (model != null)
            {
                model.SetHtml(_md.Transform(textEditor.Text));
            }
        }

        private void Update()
        {
            _statusbar.LineNumber = textEditor.Document.GetLineByOffset(textEditor.CaretOffset).LineNumber;
            _statusbar.ColPosition = textEditor.TextArea.Caret.VisualColumn + 1;
            _statusbar.CharPosition = textEditor.CaretOffset;
            _statusbar.InsertMode = false;
            if (t == null || !t.IsAlive)
                Run();
        }

        private void Run()
        {
            t = new Thread(SimpleRun);
            t.Start();
        }

        private void SimpleRun(object obj)
        {
            uint i = 0;
            while (i < 1000)
            {
                _statusbar.Progress(true, i, 1000);
                Thread.Sleep(10);
                i++;
            }
            _statusbar.Progress(false, i, 1000);
        }
    }
}