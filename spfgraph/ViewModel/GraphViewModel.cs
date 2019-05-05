using spfgraph.Model.Data;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;

namespace spfgraph.ViewModel
{
    public class GraphViewModel : BaseViewModel {
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

        #endregion

        public GraphViewModel(string filePath, OptimizeVisualizationTypes optimizeLayout, ColorSchemeTypes colorScheme) {
            var graph = DataProvider.ReadGraphFromFile(filePath);
            var builder = new StackedGraphBuilder() {
                LayoutType = LayoutAlgorithmTypes.TheShortestHeigth
            };
            var dagGraph = builder.ConstructSpf(graph);

            // Create GraphVizBuilder
            var graphVizBuilder = new GraphVizBuilder() {
                ColorScheme = colorScheme,
                OptimizeLayout = optimizeLayout,
            };
            ElementsToViz = graphVizBuilder.CreateGraphVizualization(dagGraph);
            features = dagGraph.GetGraphFeatures();
        }


        #region Commands

        RelayCommand exportToJsonCommand;
        public RelayCommand ExportToJsonCommand {
            get => exportToJsonCommand ??
                (exportToJsonCommand = new RelayCommand(() => {
                    var jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Element>), new Type[] { typeof(Element), typeof(Node), typeof(Edge), typeof(Color) });
                    using (var fs = new FileStream("elementsCollection.json", FileMode.Create)) {
                        jsonFormatter.WriteObject(fs, ElementsToViz);
                    }

                }));
        }

       
        #endregion

    }
}
