using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces.Services;

namespace Wide.Interfaces
{
    public interface ITool
    {
        PaneLocation PreferredLocation { get; }
        double PreferredWidth { get; }
        double PreferredHeight { get; }

        bool IsVisible { get; set; }
    }
}
