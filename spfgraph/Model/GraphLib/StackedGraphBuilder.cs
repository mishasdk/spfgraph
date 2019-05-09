using System;

namespace spfgraph.Model.GraphLib {
    
    /// <summary>
    /// Class, that encapsulates all logic
    /// of creation StackGraph object.
    /// </summary>
    public class StackedGraphBuilder {
        protected StackedGraph dagGraph;

        public LayoutAlgorithmTypes LayoutType { get; set; }

        #region Build Parallel Form

        public StackedGraph ConstructSpf(Graph graph) {
            dagGraph = new StackedGraph(graph);

            switch (LayoutType) {
                case LayoutAlgorithmTypes.TheShortestHeigth:
                    ConstructTheShortestHeigth();
                    break;
                default:
                    throw new Exception("Need to choose layout type.");
            }

            return dagGraph;
        }

        protected void ConstructTheShortestHeigth() {
            dagGraph.GraphLayers = Algorithms.TheShortestPathLayout(dagGraph.AdjacencyList);
        }

        #endregion

    }
}
