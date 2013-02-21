using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Unity;

namespace Wide.Interfaces
{
    // A toolbar basically has the same commands as the one's in the menu
    public sealed class ToolbarViewModel : AbstractMenuItem
    {
        #region CTOR
        public ToolbarViewModel(string header, int priority, ImageSource icon = null, ICommand command = null, bool isCheckable = false, IUnityContainer container = null) 
            : base(header,priority,icon,command,null,isCheckable)
        {
            this.Priority = priority;
            this.IsSeparator = false;
            this.Header = header;
            this.Key = header;
            this.Command = command;
            this.IsCheckable = isCheckable;
            this.Icon = icon;
            if (isCheckable)
            {
                this.IsChecked = false;
            }
            if (this.Header == "SEP")
            {
                throw new ArgumentException("Header cannot be SEP for a Toolbar");
            }
        }
        #endregion

        #region Properties
        public int Band { get; set; }

        public int BandIndex { get; set; }
        #endregion
    }
}