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
using System.Linq;

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

        #region IPrioritizedTree<T> Members

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
                IOrderedEnumerable<T> order = from c in _children
                                              orderby c.Priority
                                              select c;
                return new ReadOnlyObservableCollection<T>(new ObservableCollection<T>(order.ToList()));
            }
        }

        [Browsable(false)]
        public virtual int Priority { get; protected set; }

        [Browsable(false)]
        public virtual string Key { get; protected set; }

        #endregion
    }
}