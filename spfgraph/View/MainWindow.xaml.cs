using System.Windows;
//using System.Drawing;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace View {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DrawEllips();
        }

        void DrawEllips() {
            Ellipse e = new Ellipse();
            e.Width = 100;
            e.Height = 60;
            e.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            canvas.Children.Add(e);
        }


    }
}
