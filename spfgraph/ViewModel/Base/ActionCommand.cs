using System;
using System.Windows.Input;

namespace spfgraph.ViewModel.Base {
    public class ActionCommand : ICommand {

        Action methodToExecute;
        Func<bool> canExecuteMethod;

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

        public ActionCommand(Action methodToExecute, Func<bool> canExecuteMethod) {
            this.methodToExecute = methodToExecute;
            this.canExecuteMethod = canExecuteMethod;
        }
    }
}
