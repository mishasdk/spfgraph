using spfgraph.Model.Data;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var graph = DataProvider.ReadGraphFromFile(filePath);
            var builder = new StackedGraphBuilder() {
                LayoutType = LayoutTypes.TheShortestHeigth
            };
            var dagGraph = builder.ConstructSpf(graph);
            var graphVizBuilder = new GraphVizBuilder();

            features = dagGraph.GetGraphFeatures();
           ElementsToViz = graphVizBuilder.CreateGraphVizualization(dagGraph);
        }
    }
}
