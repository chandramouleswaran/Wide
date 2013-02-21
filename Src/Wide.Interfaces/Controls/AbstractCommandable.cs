using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Wide.Interfaces
{
    public class AbstractCommandable : AbstractPrioritizedTree<AbstractCommandable>, ICommandable
    {
        #region CTOR
        public AbstractCommandable() : base() {}
        #endregion

        #region ICommandable
        public virtual ICommand Command { get; internal set; }

        public virtual object CommandParameter { get; set; }

        public string InputGestureText { get; internal set; }
        #endregion
    }
}
