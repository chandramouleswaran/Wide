using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Wide.Interfaces
{
    public interface IWorkspace
    {
        /// <summary>
        /// The list of documents which are open in the workspace
        /// </summary>
        ObservableCollection<ContentViewModel> Documents { get; set; }
        
        /// <summary>
        /// The list of tools that are available in the workspace
        /// </summary>
        ObservableCollection<ToolViewModel> Tools { get; set; }
        
        /// <summary>
        /// The current document which is active in the workspace
        /// </summary>
        ContentViewModel ActiveDocument { get; set; }

        string Title { get; }

        ImageSource Icon { get; }

        bool Closing();
    }
}
