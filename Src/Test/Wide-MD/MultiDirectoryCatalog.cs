#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Collections.Generic;
using Microsoft.Practices.Prism.Modularity;

namespace WideMD
{
    /// <summary>
    /// Allows our shell to probe multiple directories for module assemblies
    /// </summary>
    public class MultipleDirectoryModuleCatalog : DirectoryModuleCatalog
    {
        private readonly IList<string> _pathsToProbe;

        /// <summary>
        /// Initializes a new instance of the MultipleDirectoryModuleCatalog class.
        /// </summary>
        /// <param name="pathsToProbe">An IList of paths to probe for modules.</param>
        public MultipleDirectoryModuleCatalog(IList<string> pathsToProbe)
        {
            _pathsToProbe = pathsToProbe;
        }

        /// <summary>
        /// Provides multiple-path loading of modules over the default <see cref="DirectoryModuleCatalog.InnerLoad"/> method.
        /// </summary>
        protected override void InnerLoad()
        {
            foreach (string path in _pathsToProbe)
            {
                ModulePath = path;
                base.InnerLoad();
            }
        }
    }
}