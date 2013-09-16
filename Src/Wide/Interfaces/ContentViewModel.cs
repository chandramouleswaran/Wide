#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Wide.Interfaces.Controls;
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

        /// <summary>
        /// The model
        /// </summary>
        protected ContentModel _model;

        /// <summary>
        /// The command manager
        /// </summary>
        protected ICommandManager _commandManager;

        /// <summary>
        /// The content id of the document
        /// </summary>
        protected string _contentId = null;

        /// <summary>
        /// Is the document active
        /// </summary>
        protected bool _isActive = false;

        /// <summary>
        /// Is the document selected
        /// </summary>
        protected bool _isSelected = false;

        /// <summary>
        /// The logger instance
        /// </summary>
        protected ILoggerService _logger;

        /// <summary>
        /// The title of the document
        /// </summary>
        protected string _title = null;

        /// <summary>
        /// The tool tip to display on the document
        /// </summary>
        protected string _tooltip = null;

        /// <summary>
        /// The workspace instance
        /// </summary>
        protected IWorkspace _workspace;

        /// <summary>
        /// The menu service
        /// </summary>
        protected IMenuService _menuService;

        #endregion

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentViewModel"/> class.
        /// </summary>
        /// <param name="workspace">The injected workspace.</param>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="logger">The injected logger.</param>
        protected ContentViewModel(AbstractWorkspace workspace, ICommandManager commandManager, ILoggerService logger,
                                   IMenuService menuService)
        {
            _workspace = workspace;
            _commandManager = commandManager;
            _logger = logger;
            _menuService = menuService;
            CloseCommand = new DelegateCommand<object>(Close, CanClose);
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the close command.
        /// </summary>
        /// <value>The close command.</value>
        public virtual ICommand CloseCommand { get; protected internal set; }

        /// <summary>
        /// The content model
        /// </summary>
        /// <value>The model.</value>
        public virtual ContentModel Model
        {
            get { return _model; }
            protected internal set
            {
                if (_model != null)
                {
                    _model.PropertyChanged -= Model_PropertyChanged;
                }
                if (value != null)
                {
                    _model = value;
                    _model.PropertyChanged += Model_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// The content view
        /// </summary>
        /// <value>The view.</value>
        public virtual UserControl View { get; protected internal set; }

        /// <summary>
        /// The content menu that should be available for the document pane
        /// </summary>
        /// <value>The view.</value>
        public IReadOnlyCollection<AbstractMenuItem> Menus
        {
            get
            {
                AbstractMenuItem item = _menuService.Get("_File").Get("_Save") as AbstractMenuItem;
                List<AbstractMenuItem> items = new List<AbstractMenuItem>();
                items.Add(item);
                return items.AsReadOnly();
            }
        }

        /// <summary>
        /// The title of the document
        /// </summary>
        /// <value>The title.</value>
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
        /// The tool tip of the document
        /// </summary>
        /// <value>The tool tip.</value>
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
        /// The image source that can be used as an icon in the tab
        /// </summary>
        /// <value>The icon source.</value>
        public virtual ImageSource IconSource { get; protected internal set; }

        /// <summary>
        /// The content ID - unique value for each document
        /// </summary>
        /// <value>The content id.</value>
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
        /// <value><c>true</c> if this document is selected; otherwise, <c>false</c>.</value>
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
        /// <value><c>true</c> if this document is active; otherwise, <c>false</c>.</value>
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
        /// <value>The handler.</value>
        public IContentHandler Handler { get; protected internal set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance can close.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if this instance can close; otherwise, <c>false</c>.</returns>
        protected virtual bool CanClose(object obj)
        {
            return (obj != null)
                       ? _commandManager.GetCommand("CLOSE").CanExecute(obj)
                       : _commandManager.GetCommand("CLOSE").CanExecute(this);
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected virtual void Close(object obj)
        {
            if (obj != null)
            {
                _commandManager.GetCommand("CLOSE").Execute(obj);
            }
            else
            {
                _commandManager.GetCommand("CLOSE").Execute(this);
            }
        }


        protected virtual void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Model");
            RaisePropertyChanged("Title");
            RaisePropertyChanged("ContentId");
            RaisePropertyChanged("Tooltip");
            RaisePropertyChanged("IsSelected");
            RaisePropertyChanged("IsActive");
        }

        #endregion
    }
}