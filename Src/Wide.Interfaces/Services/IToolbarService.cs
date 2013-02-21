using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Wide.Interfaces.Services
{
    public interface IToolbarService
    {
        bool Add(ToolbarViewModel item);
        bool Remove(string key);
        ToolbarViewModel Get(string key);
        ToolBarTray ToolBarTray { get; }
    }
}
