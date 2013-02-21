using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;

namespace Wide.Interfaces.Events
{
    public class SplashMessageUpdateEvent : CompositePresentationEvent<SplashMessageUpdateEvent>
    {
        public string Message { get; set; }
    }
}
