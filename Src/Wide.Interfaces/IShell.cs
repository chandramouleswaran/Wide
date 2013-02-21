using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wide.Interfaces.Services;

namespace Wide.Interfaces
{
    public interface IShell
    {
        void Show();
        void LoadLayout();
        void SaveLayout();
    }
}
