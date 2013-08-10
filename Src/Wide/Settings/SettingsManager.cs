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
using System.Windows.Controls;
using Wide.Interfaces;

namespace Wide.Settings
{
    /// <summary>
    /// Class WideSettingsManager
    /// </summary>
    internal class WideSettingsManager : AbstractSettings
    {
        //NOTE: There is a difference between settings manager and each setting - settings manager is like a host for settings and does not need to be saved/loaded.
        //It however needs a clone as the clone will be hosted in the view.

        /// <summary>
        /// Gets the view that displays the setting.
        /// </summary>
        /// <value>The view.</value>
        public override ContentControl View
        {
            get { return null; }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance. All implementation of settings needs to provide this clone.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            //Need a clone
            return null;
        }

        /// <summary>
        /// Resets this instance with the default values for settings.
        /// </summary>
        public override void Reset()
        {
            foreach (AbstractSettings settings in Children)
            {
                settings.Reset();
            }
        }

        /// <summary>
        /// Gets the settings menu.
        /// </summary>
        /// <value>The settings menu.</value>
        public AbstractMenuItem SettingsMenu { get; private set; }
    }
}