using spfgraph.Model.Exceptions;
using System.Collections.Generic;

namespace spfgraph.Model.GraphLib {
    public class StackedGraph : Graph {
        GraphFeatures graphFeatures;

        public List<List<int>> GraphLayers { get; set; }
        public GraphFeatures GraphFeatures {
            get => graphFeatures ?? (graphFeatures = GetGraphFeatures());
        }

        #region Constructors

        protected StackedGraph() { }
        public StackedGraph(Graph graph) : base(graph) {
            if (Algorithms.IsGraphСyclic(graph))
                throw new GraphErrorException("Can't create stacked graph, it's can't be cyclic.");
        }   

        #endregion

        #region Help Methods

        /// <summary>
        /// Gets a specification of StackedGraph
        /// </summary>
        /// <returns></returns>
        public GraphFeatures GetGraphFeatures() {
            var features = new GraphFeatures();
            features.Height = GraphLayers.Count;
            int maxCount = 0;
            for (int i = 0; i < GraphLayers.Count; i++)
                if (maxCount < GraphLayers[i].Count)
                    maxCount = GraphLayers[i].Count;
            features.Width = maxCount;
            features.AvrgWidth = (double)AdjacencyList.Length / GraphLayers.Count;
            return features;
        }

        /// <summary>
        /// Construct stacked parallel form of current graph
        /// </summary>
        protected void SetGraphLayers() {
            GraphLayers = Algorithms.TheShortestPathLayout(AdjacencyList);
        }

        #endregion

    }
}
