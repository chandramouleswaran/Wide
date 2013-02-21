using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Wide.Interfaces
{
    internal interface ICommandable
    {
        ICommand Command { get; }
        object CommandParameter { get; set; }
        string InputGestureText { get; }
    }
}
