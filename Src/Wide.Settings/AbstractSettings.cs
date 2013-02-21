using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Wide.Interfaces;
using Wide.Interfaces.Utils;
using Wide.Interfaces.Utils;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Wide.Settings
{
    public abstract class AbstractSettings : AbstractPrioritizedTree<AbstractSettings>, ICloneable
    {
        protected AbstractSettings() : base()
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
                ContentControl c = new ContentControl();
                PropertyGrid propertyGrid = new PropertyGrid();
                propertyGrid.ShowSearchBox = false;
                propertyGrid.SelectedObject = this;
                c.Content = propertyGrid;
                return c; 
            }
        }

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

        public abstract object Clone();

        protected ObservableCollection<AbstractSettings> ChildClone()
        {
            ObservableCollection<AbstractSettings> newChildren =
                    this.Children.Clone<AbstractSettings>().ToObservableCollection();
            return newChildren;
        }
    }
}
