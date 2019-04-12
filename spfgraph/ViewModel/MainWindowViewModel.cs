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
        IDialogService dialogService;
        string filePath;

        #endregion

        #region Public Propeties

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
        RelayCommand buildGraphCommand;
        public RelayCommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {
                    if (FilePath == "" || FilePath == null)
                        return;
                    CreateGraphForShow();
                }));
        }

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

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window) {
            this.window = window;
            dialogService = new DefaultDialogService();

        }

        #endregion

        #region Methods

        void CreateGraphForShow() {
            try {
                var g = GraphReader.ReadGraphFromFile(FilePath);
                if (g.GetType() != typeof(StackedGraph)) {
                    dialogService.ShowMessage("Graph can't transform into spf, it's cyclic.");
                }

                var builder = new GraphBuilder(g);
                GraphToShow = builder.CeateBidirectionalGraphToViz();
            } catch (DataProviderException ex) {
                dialogService.ShowMessage(ex.Message);
            }
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

