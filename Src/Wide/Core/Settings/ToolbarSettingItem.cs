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
using System.ComponentModel;
using Wide.Interfaces.Controls;

namespace Wide.Core.Settings
{
    [Serializable]
    [Browsable(false)]
    public sealed class ToolbarSettingItem : IToolbar
    {
        public ToolbarSettingItem()
        {
        }

        public ToolbarSettingItem(IToolbar toolbar)
        {
            this.BandIndex = toolbar.BandIndex;
            this.Band = toolbar.Band;
            this.Header = toolbar.Header;
            this.IsChecked = toolbar.IsChecked;
        }

        public int Band { get; set; }

        public int BandIndex { get; set; }

        public string Header { get; set; }

        public bool IsChecked { get; set; }

        public override bool Equals(object obj)
        {
            ToolbarSettingItem item = obj as ToolbarSettingItem;
            return (item != null) && Header.Equals(item.Header);
        }

        public override int GetHashCode()
        {
            return Header.GetHashCode();
        }

        public override string ToString()
        {
            return Header.ToString();
        }
    }
}