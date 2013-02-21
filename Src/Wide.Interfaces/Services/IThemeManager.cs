using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Wide.Interfaces.Services
{
    public interface IThemeManager
    {
        /// <summary>
        /// The list of themes registered with the theme manager
        /// </summary>
        ObservableCollection<ITheme> Themes { get; }

        /// <summary>
        /// Adds a theme to the theme manager
        /// </summary>
        /// <param name="theme">The theme to add</param>
        /// <returns>true, if successful</returns>
        bool AddTheme(ITheme theme);

        /// <summary>
        /// Called to set the current theme from the list of themes
        /// </summary>
        /// <param name="name">The name of the theme</param>
        /// <returns>true, if successful</returns>
        bool SetCurrent(string name);
    }
}
