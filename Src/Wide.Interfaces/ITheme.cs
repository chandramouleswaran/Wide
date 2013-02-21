using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wide.Interfaces
{
    /// <summary>
    /// You can register a theme adding it to the list of themes in theme manager
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// Lists of valid URIs which will be loaded in the theme dictionary
        /// </summary>
        IList<Uri> UriList { get; }

        /// <summary>
        /// The name of the theme
        /// </summary>
        string Name { get; }
    }
}
