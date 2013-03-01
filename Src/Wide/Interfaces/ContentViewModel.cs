// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Wide.Interfaces.Services;

namespace Wide.Interfaces
{
    /// <summary>
    /// The abstract class which has to be inherited if you want to create a document
    /// </summary>
    public abstract class ContentViewModel : ViewModelBase
    {
        #region Members

        /// <summary>
        /// The static count value for "Untitled" number.
        /// </summary>
        protected static int Count = 1;

        protected ICommandManager _commandManager;

        protected string _contentId = null;
        protected bool _isActive = false;
        protected bool _isSelected = false;
        protected ILoggerService _logger;
        protected string _title = null;
        protected string _tooltip = null;
        protected IWorkspace _workspace;

        #endregion

        #region CTOR

        protected ContentViewModel(AbstractWorkspace workspace, ICommandManager commandManager, ILoggerService logger)
        {
            _workspace = workspace;
            _commandManager = commandManager;
            _logger = logger;
            CloseCommand = new DelegateCommand(CloseDocument);
        }

        #endregion

        #region Property

        public virtual ICommand CloseCommand { get; set; }

        /// <summary>
        /// The content model
        /// </summary>
        public virtual ContentModel Model { get; internal set; }

        /// <summary>
        /// The content view
        /// </summary>
        public virtual UserControl View { get; internal set; }

        /// <summary>
        /// The title of the document
        /// </summary>
        public virtual string Title
        {
            get
            {
                if (Model.IsDirty)
                {
                    return _title + "*";
                }
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// The title of the document
        /// </summary>
        public virtual string Tooltip
        {
            get { return _tooltip; }
            protected set
            {
                if (_tooltip != value)
                {
                    _tooltip = value;
                    RaisePropertyChanged("Tooltip");
                }
            }
        }

        /// <summary>
        /// The image souce that can be used as an icon in the tab
        /// </summary>
        public virtual ImageSource IconSource { get; protected set; }

        /// <summary>
        /// The content ID - unique value for each document
        /// </summary>
        public virtual string ContentId
        {
            get { return _contentId; }
            protected set
            {
                if (_contentId != value)
                {
                    _contentId = value;
                    RaisePropertyChanged("ContentId");
                }
            }
        }

        /// <summary>
        /// Is the document selected
        /// </summary>
        public virtual bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        /// <summary>
        /// Is the document active
        /// </summary>
        public virtual bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    RaisePropertyChanged("IsActive");
                }
            }
        }

        /// <summary>
        /// The content handler which does save and load of the file
        /// </summary>
        public IContentHandler Handler { get; internal set; }

        internal virtual void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RaisePropertyChanged("Title");
        }

        #endregion

        #region Methods

        //Needed for content handlers to restore the layout
        public bool CloseDocument(bool remove)
        {
            var res = MessageBoxResult.Cancel;
            if (Model.IsDirty)
            {
                //means the document is dirty - show a message box and then handle based on the user's selection
                res = MessageBox.Show(string.Format("Save changes for document '{0}'?", Title), "Are you sure?",
                                      MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Yes)
                {
                    Handler.SaveContent(this);
                }
                if (res != MessageBoxResult.Cancel)
                {
                    if (remove)
                    {
                        _workspace.Documents.Remove(this);
                    }
                    return true;
                }
            }
            else
            {
                if (remove)
                {
                    _logger.Log("Closing document " + Model.Location, LogCategory.Info, LogPriority.None);
                    _workspace.Documents.Remove(this);
                    return true;
                }
            }
            return false;
        }

        internal void CloseDocument()
        {
            CloseDocument(true);
        }

        #endregion
    }
}