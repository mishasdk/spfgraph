using System;
using System.Windows.Input;

namespace spfgraph.ViewModel.Base {

    /// <summary>
    /// Command with parameters.
    /// </summary>
    public class ParametrizedCommand : ICommand {

        #region Protected Fields

        Action<object> executeMethod;
        Func<object, bool> canExecuteMethod;

        #endregion

        #region Contructor

        public ParametrizedCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod) {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        #endregion

        #region ICommand Implementation

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

        #endregion

    }
}
