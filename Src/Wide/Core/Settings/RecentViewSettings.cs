#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Collections.Generic;
using System.Configuration;
using System.Xml.Serialization;
using Microsoft.Practices.Prism.Commands;
using Wide.Interfaces;
using Wide.Interfaces.Controls;
using Wide.Interfaces.Services;
using Wide.Interfaces.Settings;
using Microsoft.Practices.Unity;

namespace Wide.Core.Settings
{
    internal class RecentViewSettings : AbstractSettings, IRecentViewSettings
    {
        private AbstractMenuItem recentMenu;
        private List<string> menuGuids;
        private DelegateCommand<string> recentOpen;
        private IOpenDocumentService fileService;
        private IUnityContainer _container;

        public RecentViewSettings(IUnityContainer container)
        {
            recentMenu = new MenuItemViewModel("Recentl_y opened..", 100);
            menuGuids = new List<string>();
            recentOpen = new DelegateCommand<string>(ExecuteMethod);
            this._container = container;
        }

        [UserScopedSetting()]
        public List<RecentViewItem> ActualRecentItems
        {
            get
            {
                if ((List<RecentViewItem>) this["ActualRecentItems"] == null)
                    this["ActualRecentItems"] = new List<RecentViewItem>((int) TotalItems);
                return (List<RecentViewItem>) this["ActualRecentItems"];
            }
            set { this["ActualRecentItems"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("10")]
        public uint TotalItems
        {
            get { return (uint) this["TotalItems"]; }
            set
            {
                this["TotalItems"] = value;
                ActualRecentItems.Capacity = (int) value;
                menuGuids.Capacity = (int) value;
            }
        }

        public void Update(ContentViewModel viewModel)
        {
            RecentViewItem item = new RecentViewItem();
            item.ContentID = viewModel.ContentId;
            item.DisplayValue = viewModel.Model.Location.ToString();

            if (ActualRecentItems.Contains(item))
            {
                ActualRecentItems.Remove(item);
            }
            ActualRecentItems.Add(item);
            this.Save();
            RecentMenu.Refresh();
        }

        private void ExecuteMethod(string s)
        {
            if (fileService == null)
            {
                fileService = _container.Resolve<IOpenDocumentService>();
            }
            fileService.OpenFromID(s, true);
        }

        [XmlIgnore]
        public AbstractMenuItem RecentMenu
        {
            get
            {
                int i = RecentItems.Count;
                foreach (string guid in menuGuids)
                {
                    recentMenu.Remove(guid);
                    i--;
                }

                menuGuids.Clear();

                for (i = RecentItems.Count; i > 0; i--)
                {
                    int priority = RecentItems.Count - i + 1;
                    string number = "_" + priority.ToString() + " " + RecentItems[i - 1].DisplayValue;
                    menuGuids.Add(recentMenu.Add(new MenuItemViewModel(number, priority, null, recentOpen, null)
                                                     {CommandParameter = RecentItems[i - 1].ContentID}));
                }
                return recentMenu;
            }
        }

        [XmlIgnore]
        public IReadOnlyList<IRecentViewItem> RecentItems
        {
            get { return this.ActualRecentItems.AsReadOnly(); }
        }
    }
}