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
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Wide.Interfaces.Settings
{
    public abstract class AbstractSettings : ApplicationSettingsBase, IDataErrorInfo
    {
        #region Member

        protected Dictionary<string, object> Backup;

        #endregion

        #region CTOR

        protected AbstractSettings()
        {
            Backup = new Dictionary<string, object>();
        }

        #endregion

        #region Methods

        protected virtual void UpdateBackup([CallerMemberName] string propertyName = "")
        {
            if (propertyName == null || string.IsNullOrEmpty(propertyName) == true)
            {
                throw new ArgumentException("Error handling null property for object");
            }

            if (Backup.ContainsKey(propertyName))
                Backup.Remove(propertyName);
            Backup.Add(propertyName, this[propertyName]);
        }

        protected virtual void ApplyDefault([CallerMemberName] string propertyName = "")
        {
            if (propertyName == null || string.IsNullOrEmpty(propertyName) == true)
            {
                throw new ArgumentException("Error handling null property for object");
            }

            PropertyInfo prop = this.GetType().GetProperty(propertyName);
            if (prop.GetCustomAttributes(true).Length > 0)
            {
                object[] defaultValueAttribute = prop.GetCustomAttributes(typeof (DefaultValueAttribute), true);
                if (defaultValueAttribute != null)
                {
                    DefaultValueAttribute defaultValue = defaultValueAttribute[0] as DefaultValueAttribute;
                    if (defaultValue != null)
                    {
                        prop.SetValue(this, defaultValue.Value, null);
                    }
                }
            }
        }

        #endregion

        #region IDataErrorInfo

        [Browsable(false)]
        public virtual string Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get { return null; }
        }

        #endregion
    }
}