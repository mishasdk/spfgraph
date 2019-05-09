using System;
using System.Windows.Input;

namespace spfgraph.ViewModel.Base {

    /// <summary>
    /// Simple action command.
    /// </summary>
    public class RelayCommand : ICommand {

        #region Private Fields

        Action action;

        #endregion

        #region Constructor

        public RelayCommand(Action method) {
            action = method;
        }

        #endregion

        #region ICommand Implementation

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            action();

        }
        #endregion

    }
}
