#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.ComponentModel;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Wide.Interfaces.Settings
{
    /// <summary>
    /// Class AbstractSettings
    /// </summary>
    public abstract class AbstractSettingsItem : AbstractPrioritizedTree<AbstractSettingsItem>
    {
        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractSettings"/> class.
        /// </summary>
        protected AbstractSettingsItem(string title, AbstractSettings settings) : base()
        {
            this.Title = title;
            this.Key = title;
            this._appSettings = settings;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title of the setting.
        /// </summary>
        /// <value>The title.</value>
        [Browsable(false)]
        public string Title { get; protected set; }

        /// <summary>
        /// Gets the view that displays the setting.
        /// </summary>
        /// <value>The view.</value>
        [Browsable(false)]
        public virtual ContentControl View
        {
            get
            {
                if (_appSettings != null)
                {
                    var p = ContentControl.Content as PropertyGrid;
                    p.SelectedObject = _appSettings;
                    p.SelectedObjectName = "";
                    p.SelectedObjectTypeName = this.Title;
                    return ContentControl;
                }
                if (Children.Count > 0)
                {
                    return Children[0].View;
                }
                return null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets this instance with the default values for settings.
        /// </summary>
        public virtual void Reset()
        {
            foreach (AbstractSettingsItem settings in Children)
            {
                settings.Reset();
            }
            if (_appSettings != null)
            {
                //The app settings should use reload. Reset with set it to "defaults" whereas we need to read from the settings file instead of loading defaults.
                _appSettings.Reload();
            }
        }

        /// <summary>
        /// Saves this instance and the children of the settings. In your own implementation - call base.Save() after you save your settings.
        /// </summary>
        public virtual void Save()
        {
            foreach (AbstractSettingsItem settings in Children)
            {
                settings.Save();
            }
            if (_appSettings != null)
            {
                _appSettings.Save();
            }
        }

        #endregion

        #region Static

        //Singleton which can be reused
        private static readonly ContentControl ContentControl = new ContentControl()
                                                                    {
                                                                        Content =
                                                                            new PropertyGrid
                                                                                {ShowSearchBox = false}
                                                                    };

        #endregion

        #region Members

        protected AbstractSettings _appSettings;

        #endregion
    }
}