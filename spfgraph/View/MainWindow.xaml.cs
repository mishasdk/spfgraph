using System.Windows;
using ViewModel;

namespace View {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        #region Constructor

        public MainWindow() {
            InitializeComponent();

            DataContext = new MainWindowViewModel(this);
        }

        #endregion

    }
}
