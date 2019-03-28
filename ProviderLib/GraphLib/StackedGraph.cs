using System;
using System.Collections.Generic;

namespace ProviderLib {
    public class StackedGraph : Graph {

        public StackedGraph(Graph graph) : base(graph) {
            InitFirstLayer();
            CheckGraphForCorrectSPF();
        }

        List<List<int>> graphLayers;
        List<int> firstLayer = new List<int>();

        public void InitFirstLayer() {
            int[] counter = new int[adjacencyList.Length];
            for (int i = 0; i < adjacencyList.Length; i++)
                for (int j = 0; j < adjacencyList[i].Length; j++)
                    counter[adjacencyList[i][j]]++;
            for (int i = 0; i < counter.Length; i++)
                if (counter[i] == 0)
                    firstLayer.Add(i);
        }

        void CheckGraphForCorrectSPF() {
            bool graphCyclical = Algorithms.IsGraphСyclical(this);
            if (graphCyclical)
                throw new GraphErrorException("Stacked graph can't be cyclical.");
        }

        public void ConstructSPF() {
            // Constructing parallel stacked form.
            graphLayers = new List<List<int>> { firstLayer };
            // TODO: Write algo.
            throw new NotImplementedException();
        }

        // Functions for tests.
        public List<int> GetFirtsLayer() => firstLayer;
    }
}
