using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces.Controls;

namespace Wide.Core.Settings
{
    [Serializable]
    public class ToolbarSettingItem : IToolbar
    {
        public ToolbarSettingItem()
        {
        }
        
        public ToolbarSettingItem(IToolbar toolbar)
        {
            this.BandIndex = toolbar.BandIndex;
            this.Band = toolbar.Band;
            this.Header = toolbar.Header;
        }

        public int Band { get; set; }

        public int BandIndex { get; set; }

        public string Header { get; set; }

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
