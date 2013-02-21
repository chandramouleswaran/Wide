using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Wide.Interfaces.Services
{
    public interface ICommandManager
    {
        bool RegisterCommand(string name, ICommand command);
        ICommand GetCommand(string name);
        void Refresh();
    }
}
