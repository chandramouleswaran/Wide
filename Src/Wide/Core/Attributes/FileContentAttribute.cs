using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wide.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class FileContentAttribute : Attribute
    {
        public FileContentAttribute(string display, string extension, int priority)
        {
            this.Display = display;
            this.Extension = extension;
            this.Priority = priority;
        }

        public string Display { get; private set; }

        public string Extension { get; private set; }

        public int Priority { get; private set; }
    }
}
