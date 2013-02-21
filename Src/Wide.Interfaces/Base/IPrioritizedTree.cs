using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Wide.Interfaces
{
    public interface IPrioritizedTree<T>
    {
        bool Add(T item);
        bool Remove(string key);
        T Get(string key);
        ReadOnlyObservableCollection<T> Children { get; }
        int Priority { get; }
        string Key { get; }
    }
}
