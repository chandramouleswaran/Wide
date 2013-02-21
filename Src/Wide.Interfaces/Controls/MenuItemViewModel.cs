using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Unity;
using Wide.Interfaces;
using System.Windows;

namespace Wide.Interfaces
{
    public sealed class MenuItemViewModel : AbstractMenuItem
    {
        #region CTOR
        public MenuItemViewModel(string header, int priority, ImageSource icon = null, ICommand command = null, KeyGesture gesture = null, bool isCheckable = false, IUnityContainer container = null)
            :base(header,priority,icon,command,gesture,isCheckable)
        {

        }
        #endregion

        #region Static
        public static  AbstractMenuItem Separator(int priority)
        {
            return new MenuItemViewModel("SEP",priority);
        }
        #endregion
    }
}
