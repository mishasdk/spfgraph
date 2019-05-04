using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace spfgraph.ViewModel.Base {
    public class ParametrizedCommand : ICommand {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }

        public void Execute(object parameter) {
            throw new NotImplementedException();
        }

        Func<object> func;
    }
}
