using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace spfgraph.ViewModel.Base {
    public class ParametricActionCommand : ICommand {

        #region Private Properties

        Action<object> methodToExecute;
        Func<bool> canExecuteMethod;

        #endregion

        #region Constructor

        public ParametricActionCommand(Action<object> action, Func<bool> func) {
            methodToExecute = action;
            canExecuteMethod = func;
        }

        #endregion

        #region ICommand Implementation

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            if (canExecuteMethod != null) {
                if (canExecuteMethod())
                    return true;
            }
            return false;
        }

        public void Execute(object parameter) {
            methodToExecute(parameter);
        }

        #endregion
    }
}
