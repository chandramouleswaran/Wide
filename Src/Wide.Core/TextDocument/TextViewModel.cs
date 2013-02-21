using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wide.Interfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Services;

namespace Wide.Core
{
    internal class TextViewModel : ContentViewModel
    {
        public TextViewModel(AbstractWorkspace workspace, ICommandManager commandManager, ILoggerService logger):base(workspace, commandManager, logger)
        {
        }
        
        public override string Tooltip
        {
            get { return Model.Location as String; }
            protected set { base.Tooltip = value; }
        }

        public override string ContentId
        {
            get { return "FILE:##:" + Tooltip; }
        }
    }
}
