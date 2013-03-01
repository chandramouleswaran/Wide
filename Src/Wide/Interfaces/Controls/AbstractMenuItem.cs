// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Windows;
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

        protected IUnityContainer _container;
        protected bool _isChecked;

        #endregion

        #region Methods and Properties

        public virtual bool IsSeparator { get; internal set; }

        public virtual ImageSource Icon { get; internal set; }

        public virtual string ToolTip
        {
            get
            {
                string value = Header.Replace("_", "");
                if (!string.IsNullOrEmpty(InputGestureText))
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
            set
            {
                _isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return Header;
        }

        public override bool Add(AbstractCommandable item)
        {
            if (item.GetType().IsAssignableFrom(typeof (AbstractMenuItem)))
            {
                throw new ArgumentException(
                    "Expected a AbstractMenuItem as the argument. Only Menu's can be added within a Menu.");
            }
            return base.Add(item);
        }

        #endregion

        #region CTOR

        public AbstractMenuItem(string header, int priority, ImageSource icon = null, ICommand command = null,
                                KeyGesture gesture = null, bool isCheckable = false)
        {
            Priority = priority;
            IsSeparator = false;
            Header = header;
            Key = header;
            Command = command;
            IsCheckable = isCheckable;
            Icon = icon;
            if (gesture != null && command != null)
            {
                Application.Current.MainWindow.InputBindings.Add(new KeyBinding(command, gesture));
                InputGestureText = gesture.DisplayString;
            }
            if (isCheckable)
            {
                IsChecked = false;
            }
            if (Header == "SEP")
            {
                Key = "SEP" + sepCount.ToString();
                Header = "";
                sepCount++;
                IsSeparator = true;
            }
        }

        #endregion
    }
}