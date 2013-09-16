#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Configuration;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Wide.Interfaces.Controls;
using Wide.Interfaces.Events;
using Wide.Interfaces.Settings;
using Wide.Interfaces.Services;
using System.Collections.Generic;

namespace Wide.Core.Settings
{
    internal class ToolbarPositionSettings : AbstractSettings, IToolbarPositionSettings
    {
        private AbstractToolbar _toolTray;
        private Dictionary<string, ToolbarSettingItem> _loadDict;

        public ToolbarPositionSettings(IEventAggregator eventAggregator, IToolbarService toolbarService)
        {
            eventAggregator.GetEvent<WindowClosingEvent>().Subscribe(SaveToolbarPositions);
            _toolTray = toolbarService as AbstractToolbar;
            _loadDict = new Dictionary<string, ToolbarSettingItem>();

            if (this.Toolbars != null && this.Toolbars.Count > 0)
            {
                foreach (var setting in this.Toolbars)
                {
                    _loadDict[setting.Header] = setting;
                }

                for (int i = 0; i < _toolTray.Children.Count; i++)
                {
                    AbstractToolbar tb = _toolTray.Children[i] as AbstractToolbar;
                    if (_loadDict.ContainsKey(tb.Header))
                    {
                        ToolbarSettingItem item = _loadDict[tb.Header];
                        tb.Band = item.Band;
                        tb.BandIndex = item.BandIndex;
                        tb.IsChecked = item.IsChecked;
                        tb.Refresh();
                    }
                }
            }
        }

        private void SaveToolbarPositions(Window window)
        {
            this.Toolbars = new List<ToolbarSettingItem>();
            for (int i = 0; i < _toolTray.Children.Count; i++)
            {
                AbstractToolbar tb = _toolTray.Children[i] as AbstractToolbar;
                Toolbars.Add(new ToolbarSettingItem(tb));
            }
            Save();
        }

        [UserScopedSetting()]
        public List<ToolbarSettingItem> Toolbars
        {
            get
            {
                if ((List<ToolbarSettingItem>) this["Toolbars"] == null)
                    this["Toolbars"] = new List<ToolbarSettingItem>();
                return (List<ToolbarSettingItem>) this["Toolbars"];
            }
            set { this["Toolbars"] = value; }
        }
    }
}