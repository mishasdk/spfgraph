using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
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
