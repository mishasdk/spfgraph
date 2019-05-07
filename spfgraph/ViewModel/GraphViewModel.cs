using spfgraph.Model.Data;
using spfgraph.Model.Dialog;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Input;

namespace spfgraph.ViewModel {
    public class GraphViewModel : BaseViewModel {
        public StackedGraph DagGraph { get; set; }

        ObservableCollection<Element> elementsToViz;
        public ObservableCollection<Element> ElementsToViz {
            get => elementsToViz;
            set {
                elementsToViz = value;
                OnPropertyChanged(nameof(ElementsToViz));
            }
        }

        #region Graph Features

        GraphFeatures features;

        public int GraphHeight {
            get => features.Height;
        }

        public int GraphWidth {
            get => features.Width;
        }

        public string GraphAvrgWidth {
            get => $"{features.AvrgWidth:f2}";
        }

        public int GraphIrregular {
            get => features.Irregular;
        }

        public double CanvasWidth {
            get => 60 * GraphWidth - 20;
        }

        public double CanvasHeight {
            get => 60 * GraphHeight + 20;
        }

        #endregion

        public GraphViewModel(string filePath, OptimizeVisualizationTypes optimizeLayout, ColorSchemeTypes colorScheme, Color startColor, Color endColor) {
            var graph = DataProvider.ReadGraphFromFile(filePath);
            var builder = new StackedGraphBuilder() {
                LayoutType = LayoutAlgorithmTypes.TheShortestHeigth
            };
            DagGraph = builder.ConstructSpf(graph);
            features = DagGraph.GetGraphFeatures();

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
