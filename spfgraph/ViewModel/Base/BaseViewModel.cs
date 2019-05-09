using System.ComponentModel;

namespace spfgraph.ViewModel.Base {

    /// <summary>
    /// Base class for all view models, that 
    /// implements INotifyPropertyChanged.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged {

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
