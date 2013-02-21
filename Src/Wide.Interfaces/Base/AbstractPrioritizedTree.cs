using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Wide.Interfaces
{
    public abstract class AbstractPrioritizedTree<T> : ViewModelBase, IPrioritizedTree<T> 
        where T : IPrioritizedTree<T>
    {
        protected ObservableCollection<T> _children;

        public AbstractPrioritizedTree()
        {
            _children = new ObservableCollection<T>();
        }

        public virtual bool Add(T item)
        {
            _children.Add(item);
            RaisePropertyChanged("Children");
            return true;
        }

        public virtual bool Remove(string key)
        {
            IEnumerable<T> items = _children.Where(f => f.Key == key);
            if (items.Any())
            {
                _children.Remove(items.ElementAt(0));
                RaisePropertyChanged("Children");
                return true;
            }
            return false;
        }

        public virtual T Get(string key)
        {
            IEnumerable<T> items = _children.Where(f => f.Key == key);
            if (items.Any())
            {
                return items.ElementAt(0);
            }
            return default(T);
        }
        
        [Browsable(false)]
        public virtual ReadOnlyObservableCollection<T> Children
        {
            get
            {
                var order = from c in _children
                            orderby c.Priority
                            select c;
                return new ReadOnlyObservableCollection<T>(new ObservableCollection<T>(order.ToList()));
            }
        }

        [Browsable(false)]
        public virtual int Priority { get; protected set; }

        [Browsable(false)]
        public virtual string Key { get; protected set; }
    }
}
