using spfgraph.Model.Data;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;

namespace spfgraph.ViewModel {
    public class GraphViewModel : BaseViewModel {
        ObservableCollection<Element> elementsToViz;
        GraphFeatures features;

        public ObservableCollection<Element> ElementsToViz {
            get => elementsToViz;
            set {
                elementsToViz = value;
                OnPropertyChanged(nameof(ElementsToViz));
            }
        }

        public int GraphHeight {
            get => features.Height;
        }

        public int GraphWidth {
            get => features.Width;
        }

        public string GraphAvrgWidth {
            get => $"{features.AvrgWidth:f3}";
        }

        public int GraphIrregular {
            get => features.Irregular;
        }

        public GraphViewModel(string filePath) {
            ReadGraphAndCreateVizElements(filePath);
        }

        private void ReadGraphAndCreateVizElements(string filePath) {
            var graph = DataProvider.ReadGraphFromFile(filePath);
            var builder = new StackedGraphBuilder() {
                LayoutType = LayoutTypes.TheShortestHeigth
            };
            var dagGraph = builder.ConstructSpf(graph);
            var graphVizBuilder = new GraphVizBuilder();
            ElementsToViz = graphVizBuilder.CreateGraphVizualization(dagGraph);
            features = dagGraph.GetGraphFeatures();
        }

        RelayCommand exportToJsonCommand;
        public RelayCommand ExportToJsonCommand {
            get => exportToJsonCommand ??
                (exportToJsonCommand = new RelayCommand(() => {
                    var jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Element>), new Type[] { typeof(Element), typeof(Node), typeof(Edge), typeof(Color) });
                    using (var fs = new FileStream("../../../elementsCollection.json", FileMode.Create)) {
                        jsonFormatter.WriteObject(fs, ElementsToViz);
                    }

                }));


        }
    }
}
