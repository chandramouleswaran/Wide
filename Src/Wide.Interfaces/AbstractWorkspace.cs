using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Services;

namespace Wide.Interfaces
{
    public abstract class AbstractWorkspace : ViewModelBase, IWorkspace
    {
        private IUnityContainer _container;

        protected ObservableCollection<ContentViewModel> _docs = new ObservableCollection<ContentViewModel>();
        protected ObservableCollection<ToolViewModel> _tools = new ObservableCollection<ToolViewModel>();
        protected ICommandManager _commandManager;
        protected MenuItemViewModel _menus;
        protected IToolbarService _toolbarService;

        public AbstractWorkspace(IUnityContainer container)
        {
            _container = container;
            _docs = new ObservableCollection<ContentViewModel>();
            _tools = new ObservableCollection<ToolViewModel>();
            _menus = _container.Resolve<AbstractMenuItem>() as MenuItemViewModel;
            _menus.PropertyChanged += _menus_PropertyChanged;
            _toolbarService = _container.Resolve<IToolbarService>();
            _commandManager = _container.Resolve<ICommandManager>();
        }


        private void _menus_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Menus");
        }

        public ObservableCollection<ContentViewModel> Documents
        {
            get { return _docs; }
            set { _docs = value; }
        }

        public ObservableCollection<ToolViewModel> Tools
        {
            get { return _tools; }
            set { _tools = value; }
        }

        public IList<AbstractCommandable> Menus
        {
            get
            {
                return _menus.Children;
            }
        }

        public IList<AbstractCommandable> EditToolBar
        {
            get
            {
                return _menus.Get("_Edit").Children;
            }
        }

        private ContentViewModel _activeDocument = null;
        
        public ContentViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    RaisePropertyChanged("ActiveDocument");
                    _commandManager.Refresh();
                    //TODO: Implement pub/sub Active document changed event
                }
            }
        }

        public virtual string Title
        {
            get { return "Wide"; }
        }

        public virtual ImageSource Icon { get; protected set; }

        public virtual bool Closing()
        {
            for (int i = 0; i < Documents.Count; i++)
            {
                ContentViewModel vm = Documents[i];
                if (vm.Model.IsDirty)
                {
                    this.ActiveDocument = vm;
                    if (!vm.CloseDocument(false))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public ToolBarTray ToolBarTray
        {
            get { return _toolbarService.ToolBarTray; }
        }
    }

}
