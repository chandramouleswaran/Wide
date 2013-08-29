using System;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Unity;

namespace Wide.Interfaces
{
    public abstract class AbstractToolbar : AbstractMenuItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolbarViewModel"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="command">The command.</param>
        /// <param name="isCheckable">if set to <c>true</c> does nothing in the case of toolbar - default value is false.</param>
        /// <param name="container">The container.</param>
        /// <exception cref="System.ArgumentException">Header cannot be SEP for a Toolbar</exception>
        protected AbstractToolbar(string header, int priority, ImageSource icon = null, ICommand command = null,
                                  bool isCheckable = false, IUnityContainer container = null)
            : base(header, priority, icon, command, null, isCheckable)
        {
            Priority = priority;
            IsSeparator = false;
            Header = header;
            Key = header;
            Command = command;
            IsCheckable = isCheckable;
            Icon = icon;
            if (isCheckable)
            {
                IsChecked = false;
            }
            if (Header == "SEP")
            {
                throw new ArgumentException("Header cannot be SEP for a Toolbar");
            }
        }

        /// <summary>
        /// Gets or sets the band number for the toolbar in the toolbar tray.
        /// </summary>
        /// <value>The band.</value>
        public int Band { get; set; }

        /// <summary>
        /// Gets or sets the band index of the toolbar in the toolbar tray.
        /// </summary>
        /// <value>The index of the band.</value>
        public int BandIndex { get; set; }
    }
}