using System;
using System.ComponentModel;
using ICSharpCode.AvalonEdit.Document;
using Wide.Interfaces;
using Wide.Interfaces.Services;

namespace Wide.Core
{
    public class TextModel : ContentModel
    {
        protected string _oldText;
        private ICommandManager _commandManager;

        public TextModel(ICommandManager commandManager)
        {
            Document = new TextDocument();
            _commandManager = commandManager;
            Document.PropertyChanged += Document_PropertyChanged;
            Document.TextChanged += DocumentOnTextChanged;
            _oldText = "";
        }

        private void DocumentOnTextChanged(object sender, EventArgs eventArgs)
        {
            IsDirty = (_oldText != Document.Text);
        }

        void Document_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Document");
        }

        public override bool IsDirty
        {
            get
            {
                return base.IsDirty;
            }
            set
            {
                base.IsDirty = value;
                if(value == false)
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
    }
}