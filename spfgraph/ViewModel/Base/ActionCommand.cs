using System;
using System.Windows.Input;

namespace spfgraph.ViewModel.Base {

    /// <summary>
    /// Commad that implement action.
    /// </summary>
    public class ActionCommand : ICommand {

        #region Protected Fields

        Action methodToExecute;
        Func<bool> canExecuteMethod;

        #endregion

        #region Costructor

        public ActionCommand(Action methodToExecute, Func<bool> canExecuteMethod) {
            this.methodToExecute = methodToExecute;
            this.canExecuteMethod = canExecuteMethod;
        }

        #endregion

        #region Implementation Of ICommand Interface

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            return canExecuteMethod();
        }

        public void Execute(object parameter) {
            methodToExecute();
        }

        #endregion

    }
}
