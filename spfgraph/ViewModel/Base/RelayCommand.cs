using System;
using System.Windows.Input;

namespace spfgraph.ViewModel.Base {
    public class RelayCommand : ICommand {
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        Action action;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            action();
        }

        public RelayCommand(Action method) {
            action = method;
        }
    }
}
