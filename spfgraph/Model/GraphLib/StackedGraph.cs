using spfgraph.Model.Exceptions;
using System;
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
            var features = new GraphFeatures {
                Height = GraphLayers.Count
            };
            int maxW = 0, minW = 10000;
            for (int i = 0; i < GraphLayers.Count; i++) {
                if (maxW < GraphLayers[i].Count)
                    maxW = GraphLayers[i].Count;
                if (minW > GraphLayers[i].Count)
                    minW = GraphLayers[i].Count;
            }
            features.Width = maxW;
            features.AvrgWidth = (double)AdjacencyList.Length / GraphLayers.Count;
            features.Irregular = maxW / ((double)minW);

            double dev = 0;
            for (int i = 0; i < GraphLayers.Count; i++) {
                dev += (features.AvrgWidth - GraphLayers[i].Count) * (features.AvrgWidth - GraphLayers[i].Count);
            }
            dev = Math.Sqrt(dev / GraphLayers.Count);
            features.AvrgDeviation = dev;
            return features;
        }

        #endregion

    }
}
