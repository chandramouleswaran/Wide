using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Unity;

namespace Wide.Interfaces
{
    public abstract class AbstractMenuItem : AbstractCommandable
    {
        #region Static
        protected static int sepCount = 1;
        #endregion

        #region Members
        protected bool _isChecked;
        protected IUnityContainer _container;
        #endregion

        #region Methods and Properties
        public virtual bool IsSeparator { get; internal set; }

        public virtual ImageSource Icon { get; internal set; }

        public virtual string ToolTip
        {
            get
            {
                string value = this.Header.Replace("_", "");
                if(!string.IsNullOrEmpty(this.InputGestureText))
                {
                    value += " " + InputGestureText;
                }
                return value;
            }
        }

        public virtual string Header { get; internal set; }

        public virtual bool IsCheckable { get; set; }

        public virtual bool IsVisible { get; internal set; }

        public virtual bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; RaisePropertyChanged("IsChecked"); }
        }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return this.Header;
        }

        public override bool Add(AbstractCommandable item)
        {
            if (item.GetType().IsAssignableFrom(typeof(AbstractMenuItem)))
            {
                throw new ArgumentException("Expected a AbstractMenuItem as the argument. Only Menu's can be added within a Menu.");
            }
            return base.Add(item);
        }
        #endregion

        #region CTOR
        public AbstractMenuItem(string header, int priority, ImageSource icon = null, ICommand command = null, KeyGesture gesture = null, bool isCheckable = false)
            : base()
        {
            this.Priority = priority;
            this.IsSeparator = false;
            this.Header = header;
            this.Key = header;
            this.Command = command;
            this.IsCheckable = isCheckable;
            this.Icon = icon;
            if (gesture != null && command != null)
            {
                Application.Current.MainWindow.InputBindings.Add(new KeyBinding(command, gesture));
                this.InputGestureText = gesture.DisplayString;
            }
            if (isCheckable)
            {
                this.IsChecked = false;
            }
            if (this.Header == "SEP")
            {
                this.Key = "SEP" + sepCount.ToString();
                this.Header = "";
                sepCount++;
                this.IsSeparator = true;
            }
        }
        #endregion
    }
}
