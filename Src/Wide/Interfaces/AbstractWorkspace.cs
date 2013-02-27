// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Services;

namespace Wide.Interfaces
{
    public abstract class AbstractWorkspace : ViewModelBase, IWorkspace
    {
        private readonly IUnityContainer _container;
        private ContentViewModel _activeDocument;

        protected ICommandManager _commandManager;
        protected ObservableCollection<ContentViewModel> _docs = new ObservableCollection<ContentViewModel>();
        protected MenuItemViewModel _menus;
        protected IToolbarService _toolbarService;
        protected ObservableCollection<ToolViewModel> _tools = new ObservableCollection<ToolViewModel>();

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


        public IList<AbstractCommandable> Menus
        {
            get { return _menus.Children; }
        }

        public IList<AbstractCommandable> EditToolBar
        {
            get { return _menus.Get("_Edit").Children; }
        }

        public ToolBarTray ToolBarTray
        {
            get { return _toolbarService.ToolBarTray; }
        }

        #region IWorkspace Members

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
                    ActiveDocument = vm;
                    if (!vm.CloseDocument(false))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        private void _menus_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Menus");
        }
    }
}