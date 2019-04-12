using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel {
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
