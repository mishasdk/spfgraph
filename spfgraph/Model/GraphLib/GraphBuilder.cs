using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace Model {
    public class StackedGraphBuilder {
        StackedGraph dagGraph;
        public LayoutTypes LayoutType { get; set; }

        #region Build Parallel Form

        public StackedGraph ConstructSpf(Graph graph) {
            dagGraph = new StackedGraph(graph);

            switch (LayoutType) {
                case LayoutTypes.TheShortestHeigth:
                    ConstructTheShortestHeigth();
                    break;
                default:
                    throw new Exception("Need to choose layout type.");
            }

            return dagGraph;
        }

        void ConstructTheShortestHeigth() {
            dagGraph.GraphLayers = Algorithms.TheShortestPathLayout(dagGraph.AdjacencyList);
        }

        #endregion

    }
}
