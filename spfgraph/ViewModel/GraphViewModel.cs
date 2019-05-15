using spfgraph.Model.Data;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Visualization;
using spfgraph.ViewModel.Base;
using System.Collections.ObjectModel;

namespace spfgraph.ViewModel {

    /// <summary>
    /// Class, that encapsulates logic of graph
    /// visualization.
    /// </summary>
    public class GraphViewModel : BaseViewModel {

        #region Private Properties

        ObservableCollection<Element> elementsToViz;
        GraphFeatures features;
        string graphAvrgWidth;
        string graphIrregular;
        string graphAvrgDev;
        int graphHeight;
        int graphWidth;
        int canvasWidth;
        int canvasHeight;

        #endregion

        #region Public Properties

        public StackedGraph DagGraph { get; private set; }
        public OptimizeVisualizationTypes OptimizeLayout { get; set; }
        public ColorSchemeTypes ColorScheme { get; set; }
        public LayoutAlgorithmTypes LayoutAlgorithm { get; set; }
        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public string FilePath { get; set; }
        public BackgroundTypes BackgroundType { get; set; }

        public ObservableCollection<Element> ElementsToViz {
            get => elementsToViz;
            set {
                elementsToViz = value;
                OnPropertyChanged(nameof(ElementsToViz));
            }
        }

        public int GraphHeight {
            get => graphHeight;
            set {
                graphHeight = value;
                OnPropertyChanged(nameof(GraphHeight));
            }
        }

        public int GraphWidth {
            get => graphWidth;
            set {
                graphWidth = value;
                OnPropertyChanged(nameof(GraphWidth));
            }
        }

        public string GraphAvrgWidth {
            get => $"{graphAvrgWidth:f2}";
            set {
                graphAvrgWidth = value;
                OnPropertyChanged(nameof(GraphAvrgWidth));
            }
        }

        public string GraphIrregular {
            get => $"{graphIrregular:f2}";
            set {
                graphIrregular = value;
                OnPropertyChanged(nameof(GraphIrregular));
            }
        }

        public string GraphAvrgDev {
            get => $"{graphAvrgDev:f2}";
            set {
                graphAvrgDev = value;
                OnPropertyChanged(nameof(GraphAvrgDev));
            }
        }

        public int CanvasWidth {
            get => canvasWidth;
            set {
                canvasWidth = value;
                OnPropertyChanged(nameof(CanvasWidth));
            }
        }

        public int CanvasHeight {
            get => canvasHeight;
            set {
                canvasHeight = value;
                OnPropertyChanged(nameof(CanvasHeight));
            }
        }

        #endregion

        #region Public Methods

        public void CreateSPF() {
            CreateDagGraph();
            SetGraphViewModelProperties();
            CreateElementsToViz();
        }

        #endregion

        #region Private Methods

        void CreateDagGraph() {
            var graph = DataProvider.ReadGraphFromFile(FilePath);
            var graphBuilder = new StackedGraphBuilder() {
                LayoutType = LayoutAlgorithm
            };
            DagGraph = graphBuilder.ConstructSpf(graph);
        }

        void SetGraphViewModelProperties() {
            features = DagGraph.Features;
            GraphHeight = features.Height;
            GraphWidth = features.Width;
            GraphAvrgWidth = $"{features.AvrgWidth:f2}";
            GraphIrregular = $"{features.Irregular:f2}";
            GraphAvrgDev = $"{features.AvrgDeviation:f2}";
            CanvasHeight = 60 * GraphHeight + 20;
            CanvasWidth = 60 * GraphWidth + 150;
        }

        void CreateElementsToViz() {
            // Create GraphVizBuilder
            var graphVizBuilder = new GraphVizBuilder() {
                ColorScheme = ColorScheme,
                OptimizeLayout = OptimizeLayout,
                StartLeft = CanvasWidth / 2,
                StartColor = StartColor,
                EndColor = EndColor,
                CanvasWidth = CanvasWidth,
                HeightStep = 60,
                WidthStep = 60,
                StartHeight = 20,
                BackgroundType = BackgroundType
            };
            ElementsToViz = graphVizBuilder.CreateGraphVizualization(DagGraph);
        }

        #endregion

    }
}
