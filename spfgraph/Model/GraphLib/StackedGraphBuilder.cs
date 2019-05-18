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
                case LayoutAlgorithmTypes.TheShortestHeight:
                    ConstructTheShortestHeigth();
                    break;
                case LayoutAlgorithmTypes.StraightPass:
                    ConstructStraightPass();
                    break;
                case LayoutAlgorithmTypes.ReversePass:
                    ConstructReversePass();
                    break;
                default:
                    throw new Exception("Need to choose layout type.");
            }

            return dagGraph;
        }

        private void ConstructReversePass() {
            dagGraph.GraphLayers = Algorithms.ReversePass(dagGraph.AdjacencyList);
        }

        private void ConstructStraightPass() {
            dagGraph.GraphLayers = Algorithms.StraightPass(dagGraph.AdjacencyList);
        }

        protected void ConstructTheShortestHeigth() {
            dagGraph.GraphLayers = Algorithms.OptimalLayoutByWidth(dagGraph.AdjacencyList);
        }

        #endregion

    }
}
