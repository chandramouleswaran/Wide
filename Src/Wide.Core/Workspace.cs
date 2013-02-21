using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Microsoft.Practices.Unity;

namespace Wide.Core
{
    public class Workspace : AbstractWorkspace
    {
        public Workspace(IUnityContainer container) : base(container)
        {
        }
    }
}
