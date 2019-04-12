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
        int windowRadius = 5;
        GraphVizViewModel graphViz;
        IDialogService dialogService;
        string filePath;

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

        #endregion

        #region Commands

        RelayCommand openCommand;
        public RelayCommand OpenCommand {
            get => openCommand ??
                (openCommand = new RelayCommand(() => {
                    try {
                        if (dialogService.OpenFileDialog()) {
                            FilePath = dialogService.FilePath;
                            dialogService.ShowMessage("File opened\n" + $"Path: {FilePath}");
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
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };


            //ConstructGraphToShow(CreateGraph());

            var resizer = new WindowResizer(window);
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
