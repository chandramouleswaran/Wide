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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Controls;
using Wide.Interfaces.Events;
using Wide.Interfaces.Services;

namespace Wide.Interfaces
{
    /// <summary>
    /// Class AbstractWorkspace
    /// </summary>
    public abstract class AbstractWorkspace : ViewModelBase, IWorkspace
    {
        #region Fields

        /// <summary>
        /// The injected container
        /// </summary>
        protected readonly IUnityContainer _container;

        /// <summary>
        /// The injected event aggregator
        /// </summary>
        protected readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The active document
        /// </summary>
        private ContentViewModel _activeDocument;

        /// <summary>
        /// The injected command manager
        /// </summary>
        protected ICommandManager _commandManager;

        /// <summary>
        /// The list of documents
        /// </summary>
        protected ObservableCollection<ContentViewModel> _docs = new ObservableCollection<ContentViewModel>();

        /// <summary>
        /// The menu service
        /// </summary>
        protected MenuItemViewModel _menus;

        /// <summary>
        /// The toolbar service
        /// </summary>
        protected AbstractToolbar _toolbarService;

        /// <summary>
        /// The status bar service
        /// </summary>
        protected IStatusbarService _statusbarService;

        /// <summary>
        /// The list of tools
        /// </summary>
        protected ObservableCollection<ToolViewModel> _tools = new ObservableCollection<ToolViewModel>();

        #endregion

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractWorkspace" /> class.
        /// </summary>
        /// <param name="container">The injected container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        protected AbstractWorkspace(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _docs = new ObservableCollection<ContentViewModel>();
            _docs.CollectionChanged += Docs_CollectionChanged;
            _tools = new ObservableCollection<ToolViewModel>();
            _menus = _container.Resolve<IMenuService>() as MenuItemViewModel;
            _menus.PropertyChanged += _menus_PropertyChanged;
            _toolbarService = _container.Resolve<IToolbarService>() as AbstractToolbar;
            _statusbarService = _container.Resolve<IStatusbarService>();
            _commandManager = _container.Resolve<ICommandManager>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <value>The menu.</value>
        public IList<AbstractCommandable> Menus
        {
            get { return _menus.Children; }
        }

        /// <summary>
        /// Gets the tool bar tray.
        /// </summary>
        /// <value>The tool bar tray.</value>
        public ToolBarTray ToolBarTray
        {
            get { return (_toolbarService as IToolbarService).ToolBarTray; }
        }

        public IStatusbarService StatusBar
        {
            get { return _statusbarService; }
        }

        #endregion

        #region IWorkspace Members

        /// <summary>
        /// The list of documents which are open in the workspace
        /// </summary>
        /// <value>The documents.</value>
        public virtual ObservableCollection<ContentViewModel> Documents
        {
            get { return _docs; }
            set { _docs = value; }
        }

        /// <summary>
        /// The list of tools that are available in the workspace
        /// </summary>
        /// <value>The tools.</value>
        public virtual ObservableCollection<ToolViewModel> Tools
        {
            get { return _tools; }
            set { _tools = value; }
        }

        /// <summary>
        /// The current document which is active in the workspace
        /// </summary>
        /// <value>The active document.</value>
        public virtual ContentViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    RaisePropertyChanged("ActiveDocument");
                    _commandManager.Refresh();
                    _menus.Refresh();
                    _eventAggregator.GetEvent<ActiveContentChangedEvent>().Publish(_activeDocument);
                }
            }
        }

        /// <summary>
        /// Gets the title of the application.
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title
        {
            get { return "Wide"; }
        }

        /// <summary>
        /// Gets the icon of the application.
        /// </summary>
        /// <value>The icon.</value>
        public virtual ImageSource Icon { get; protected set; }

        /// <summary>
        /// Closing this instance.
        /// </summary>
        /// <param name="e">The <see cref="CancelEventArgs" /> instance containing the event data.</param>
        /// <returns><c>true</c> if the application is closing, <c>false</c> otherwise</returns>
        public virtual bool Closing(CancelEventArgs e)
        {
            for (int i = 0; i < Documents.Count; i++)
            {
                ContentViewModel vm = Documents[i];
                if (vm.Model.IsDirty)
                {
                    ActiveDocument = vm;

                    //Execute the close command
                    vm.CloseCommand.Execute(e);

                    //If canceled
                    if (e.Cancel == true)
                    {
                        return false;
                    }

                    //If it was a new view model with no location to save, we have removed the view model from documents - so reduce the count
                    if(vm.Model.Location == null)
                    {
                        i--;
                    }
                }
            }
            return true;
        }

        #endregion

        /// <summary>
        /// Handles the PropertyChanged event of the menu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void _menus_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Menus");
        }


        protected void Docs_CollectionChanged(object sender,
                                              System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= ModelChangedEventHandler;
            }

            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += ModelChangedEventHandler;
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (_docs.Count == 0)
                {
                    this.ActiveDocument = null;
                }
            }
        }

        /// <summary>
        /// The changed event handler when a property on the model changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void ModelChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            _commandManager.Refresh();
            _menus.Refresh();
            _toolbarService.Refresh();
        }
    }
}