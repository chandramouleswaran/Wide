// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using Wide.Interfaces;
using Wide.Interfaces.Utils;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Wide.Settings
{
    public abstract class AbstractSettings : AbstractPrioritizedTree<AbstractSettings>, ICloneable
    {
        protected AbstractSettings()
        {
            Reset();
        }

        [Browsable(false)]
        public string Title { get; protected set; }

        [Browsable(false)]
        public virtual ContentControl View
        {
            get
            {
                var c = new ContentControl();
                var propertyGrid = new PropertyGrid {ShowSearchBox = false, SelectedObject = this};
                c.Content = propertyGrid;
                return c;
            }
        }

        #region ICloneable Members

        public abstract object Clone();

        #endregion

        public virtual void Reset()
        {
            // Iterate through each property and call ResetValue()
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
                property.ResetValue(this);

            foreach (AbstractSettings settings in Children)
            {
                settings.Reset();
            }
        }

        //Settings needs to override, save and call base.Save()
        public virtual void Save()
        {
            foreach (AbstractSettings settings in Children)
            {
                settings.Save();
            }
        }

        public abstract void Load();

        protected ObservableCollection<AbstractSettings> ChildClone()
        {
            ObservableCollection<AbstractSettings> newChildren =
                Children.Clone().ToObservableCollection();
            return newChildren;
        }
    }
}