using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace spfgraph.ViewModel.Base {
    public class ParametrizedCommand : ICommand {
        Action<object> executeMethod;
        Func<object, bool> canExecuteMethod;

        public ParametrizedCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod) {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            if (canExecuteMethod != null) {
                return canExecuteMethod(parameter);
            }
            return false;
        }

        public void Execute(object parameter) {
            executeMethod(parameter);
        }

    }
}
