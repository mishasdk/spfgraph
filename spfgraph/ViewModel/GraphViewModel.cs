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
        ObservableCollection<Element> elementsToViz;
        GraphFeatures features;

        public StackedGraph DagGraph { get; set; }
        public ObservableCollection<Element> ElementsToViz {
            get => elementsToViz;
            set {
                elementsToViz = value;
                OnPropertyChanged(nameof(ElementsToViz));
            }
        }

        #region Graph Features

        public int GraphHeight {
            get => features.Height;
        }

        public int GraphWidth {
            get => features.Width;
        }

        public string GraphAvrgWidth {
            get => $"{features.AvrgWidth:f2}";
        }

        public string GraphIrregular {
            get => $"{features.Irregular:f2}";
        }

        public string GraphAvrgDev {
            get => $"{features.AvrgDeviation:f2}";
        }

        public double CanvasWidth {
            get => 60 * GraphWidth - 20;
        }

        public double CanvasHeight {
            get => 60 * GraphHeight + 20;
        }

        #endregion

        public GraphViewModel(string filePath, OptimizeVisualizationTypes optimizeLayout, ColorSchemeTypes colorScheme, LayoutAlgorithmTypes layoutAlgorithm, Color startColor, Color endColor) {
            var graph = DataProvider.ReadGraphFromFile(filePath);
            var builder = new StackedGraphBuilder() {
                LayoutType = layoutAlgorithm
            };
            DagGraph = builder.ConstructSpf(graph);
            features = DagGraph.Features;

            // Create GraphVizBuilder
            var graphVizBuilder = new GraphVizBuilder() {
                ColorScheme = colorScheme,
                OptimizeLayout = optimizeLayout,
                StartLeft = GraphWidth * 60 / 2,
                StartColor = startColor,
                EndColor = endColor
            };
            ElementsToViz = graphVizBuilder.CreateGraphVizualization(DagGraph);
        }

    }
}
