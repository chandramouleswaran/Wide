using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wide.Interfaces;
using Microsoft.Practices.Prism.Events;

namespace Wide.Interfaces.Events
{
    public class OpenContentEvent : CompositePresentationEvent<ContentViewModel>
    {
    }
}
