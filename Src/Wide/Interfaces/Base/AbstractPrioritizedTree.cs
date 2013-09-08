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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Wide.Interfaces
{
    /// <summary>
    /// Class AbstractPrioritizedTree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractPrioritizedTree<T> : ViewModelBase, IPrioritizedTree<T>
        where T : AbstractPrioritizedTree<T>
    {
        /// <summary>
        /// The children of this tree node
        /// </summary>
        protected ObservableCollection<T> _children;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPrioritizedTree{T}"/> class.
        /// </summary>
        protected AbstractPrioritizedTree()
        {
            _children = new ObservableCollection<T>();
        }

        #region IPrioritizedTree<T> Members

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if successfully added, <c>false</c> otherwise</returns>
        public virtual string Add(T item)
        {
            _children.Add(item);
            item.GuidString = Guid.NewGuid().ToString();
            RaisePropertyChanged("Children");
            return item.GuidString;
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="GuidString">The unique GUID set for the menu available for the creator.</param>
        /// <returns><c>true</c> if successfully removed, <c>false</c> otherwise</returns>
        public virtual bool Remove(string GuidString)
        {
            IEnumerable<T> items = _children.Where(f => f.GuidString == GuidString);
            if (items.Any())
            {
                _children.Remove(items.ElementAt(0));
                RaisePropertyChanged("Children");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the node with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>`0.</returns>
        public virtual T Get(string key)
        {
            IEnumerable<T> items = _children.Where(f => f.Key == key);
            if (items.Any())
            {
                return items.ElementAt(0);
            }
            return default(T);
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>The children.</value>
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

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        [Browsable(false)]
        public virtual int Priority { get; protected set; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        [Browsable(false)]
        public virtual string Key { get; protected set; }

        protected string GuidString { get; set; }

        #endregion
    }
}