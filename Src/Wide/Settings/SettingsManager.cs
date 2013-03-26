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

namespace Wide.Settings
{
    /// <summary>
    /// Interface ISettingsManager
    /// </summary>
    public interface ISettingsManager
    {
        /// <summary>
        /// Gets the node with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>AbstractSettings.</returns>
        AbstractSettings Get(string key);

        /// <summary>
        /// Adds the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns><c>true</c> if successfully added, <c>false</c> otherwise</returns>
        bool Add(AbstractSettings settings);
    }

    /// <summary>
    /// Class WideSettingsManager
    /// </summary>
    internal class WideSettingsManager : AbstractSettings, ISettingsManager
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
        /// Loads this settings.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Don't need to load a WideSettingsManager</exception>
        public override void Load()
        {
            throw new NotImplementedException("Don't need to load a WideSettingsManager");
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
    }
}