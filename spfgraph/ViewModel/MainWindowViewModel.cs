using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model;

namespace ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        Window window;
        int outerMarginSize = 5;
        int windowRadius = 5;
        GraphVizViewModel graphViz;

        #endregion

        #region Public Propeties
        /// <summary>
        /// Size of the the resize border
        /// </summary>
        public int ResizeBorder { get; set; } = 3;
        public Thickness ResizeBorderThickness {
            get => new Thickness(ResizeBorder + OuterMarginSize);
        }

        public int OuterMarginSize {
            get => window.WindowState == WindowState.Maximized ? 0 : outerMarginSize;
            set {
                outerMarginSize = value;
                OnPropertyChanged(nameof(OuterMarginSize));
            }
        }
        public Thickness OuterMarginSizeThickness {
            get => new Thickness(OuterMarginSize);
        }

        public int WindowRadius {
            get => window.WindowState == WindowState.Maximized ? 0 : windowRadius;
            set {
                windowRadius = value;
                OnPropertyChanged(nameof(WindowRadius));
            }
        }
        public CornerRadius WindowCornerRadius {
            get => new CornerRadius(WindowRadius);
        }

        public int TitleHeight { get; set; } = 18;
        public GridLength TitleHeightGridLength {
            get => new GridLength(TitleHeight + ResizeBorder);
        }

        public GraphVizViewModel GraphViz {
            get => graphViz;
            set {
                graphViz = value;
                OnPropertyChanged(nameof(GraphViz));
            }
        }


        #endregion

        #region Commands

        public RelayCommand CreateGraphToViz;

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window) {
            this.window = window;

            window.StateChanged += (sender, e) => {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };


            GraphViz = new GraphVizViewModel(CreateGraph());

            //var resizer = new WindowResizer(window);
        }

        #endregion


        #region Methods
        Graph CreateGraph() {
            var list = new List<int>[] {
                new List<int> {1, 2},
                new List<int> {2, 3},
                new List<int> {1},
                new List<int> {3},
                new List<int> { }
            };
            var graph = new Graph(list);
            return graph;
        }

        #endregion

    }
}
