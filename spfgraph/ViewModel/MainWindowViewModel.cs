using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model;
using QuickGraph;

namespace ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        Window window;
        int outerMarginSize = 5;
        int resizeBorder = 3;
        GraphVizViewModel graphViz;
        IDialogService dialogService;
        string filePath;
        WindowResizer windowResizer;
        WindowDockPosition windowDock = WindowDockPosition.Undocked;
        Thickness innerContentPadding = new Thickness(4, 0, 4, 4);

        #endregion

        #region Public Propeties
        /// <summary>
        /// Size of the the resize border
        /// </summary>
        public int ResizeBorder {
            get => window.WindowState == WindowState.Maximized ? 0 : resizeBorder;
        }
        public Thickness ResizeBorderThickness {
            get => new Thickness(ResizeBorder + OuterMarginSize);
        }

        public int OuterMarginSize {
            get {
                switch (windowDock) {
                    case WindowDockPosition.BottomLeft:
                    case WindowDockPosition.BottomRight:
                    case WindowDockPosition.Right:
                    case WindowDockPosition.Left:
                    case WindowDockPosition.TopLeft:
                    case WindowDockPosition.TopRight: return 0;
                }
                return window.WindowState == WindowState.Maximized ? 0 : outerMarginSize;
            }
            set {
                outerMarginSize = value;
                OnPropertyChanged(nameof(OuterMarginSize));
            }
        }
        public Thickness OuterMarginSizeThickness {
            get => new Thickness(OuterMarginSize);
        }

        public int TitleHeight { get; set; } = 18;
        public GridLength TitleHeightGridLength {
            get => window.WindowState == WindowState.Maximized ? new GridLength(TitleHeight) : new GridLength(TitleHeight + ResizeBorder);
        }

        public GraphVizViewModel GraphViz {
            get => graphViz;
            set {
                graphViz = value;
                OnPropertyChanged(nameof(GraphViz));
            }
        }

        IBidirectionalGraph<object, IEdge<object>> graphToShow;
        public IBidirectionalGraph<object, IEdge<object>> GraphToShow {
            get => graphToShow;
            set {
                graphToShow = value;
                OnPropertyChanged(nameof(GraphToShow));
            }
        }

        public string FilePath {
            get => filePath;
            set {
                filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        public Thickness InnerContentPadding {
            get => window.WindowState == WindowState.Maximized ? new Thickness(0) : innerContentPadding;
        }

        bool BeingMoved { get; set; } = false;

        #endregion

        #region Commands

        RelayCommand openCommand;
        public RelayCommand OpenCommand {
            get => openCommand ??
                (openCommand = new RelayCommand(() => {
                    try {
                        if (dialogService.OpenFileDialog()) {
                            FilePath = dialogService.FilePath;
                        }
                    } catch (Exception ex) {
                        dialogService.ShowMessage(ex.Message);
                    }
                }));
        }

        RelayCommand buildGraphCommand;
        public RelayCommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {
                    if (FilePath == "" || FilePath == null)
                        return;
                    var g = CreateGraph(FilePath);
                    ConstructGraphToShow(g);
                }));
        }

        RelayCommand closeWindowCommand;
        public RelayCommand CloseWindowCommand {
            get => closeWindowCommand ??
                (closeWindowCommand = new RelayCommand(() => {
                    window.Close();
                }));
        }

        RelayCommand expandWindowCommand;
        public RelayCommand ExpandWindowCommand {
            get => expandWindowCommand ??
                (expandWindowCommand = new RelayCommand(() => {
                    window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                }));
        }

        RelayCommand hideWindowCommand;
        public RelayCommand HideWindowCommand {
            get => hideWindowCommand ??
                (hideWindowCommand = new RelayCommand(() => {
                    window.WindowState = WindowState.Minimized;
                }));
        }

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window) {
            this.window = window;
            dialogService = new DefaultDialogService();

            window.StateChanged += (sender, e) => {
                WindowResized();
            };

            windowResizer = new WindowResizer(window);

            // Listen out for dock changes
            windowResizer.WindowDockChanged += (dock) => {
                // Store last position
                windowDock = dock;

                // Fire off resize events
                WindowResized();
            };

            // On window being moved/dragged
            windowResizer.WindowStartedMove += () => {
                // Update being moved flag
                BeingMoved = true;
            };

            // Fix dropping an undocked window at top which should be positioned at the
            // very top of screen
            windowResizer.WindowFinishedMove += () => {
                // Update being moved flag
                BeingMoved = false;

                // Check for moved to top of window and not at an edge
                if (windowDock == WindowDockPosition.Undocked && window.Top == windowResizer.CurrentScreenSize.Top)
                    // If so, move it to the true top (the border size)
                    window.Top = -OuterMarginSizeThickness.Top;
            };

        }

        #endregion

        #region Methods

        private void WindowResized() {
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(InnerContentPadding));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(TitleHeightGridLength));
        }

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

        Graph CreateGraph(string fileName) {
            var adjacencyList = DataProvider.CreateAdjacencyListFromFile(fileName);
            var graph = new Graph(adjacencyList);
            return graph;
        }

        void ConstructGraphToShow(Graph graph) {
            var g = new BidirectionalGraph<object, IEdge<object>>();
            int[][] adjacencyList = graph.AdjacencyList;

            var ver = new List<string>();
            for (int i = 0; i < adjacencyList.Length; i++)
                ver.Add(i.ToString());
            g.AddVertexRange(ver);

            var edg = new List<Edge<object>>();
            for (int i = 0; i < adjacencyList.Length; i++)
                for (int j = 0; j < adjacencyList[i].Length; j++) {
                    int v = i;
                    int to = adjacencyList[i][j];
                    edg.Add(new Edge<object>(ver[v], ver[to]));
                }
            g.AddEdgeRange(edg);

            GraphToShow = g;
        }

        #endregion
    }
}

