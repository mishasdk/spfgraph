using Model.Excepitons;
using System;
using System.Collections.Generic;

namespace Model.GraphLib {
    public class StackedGraph : Graph {

        public StackedGraph(Graph graph) : base(graph) {
            InitLayers();
            CheckGraphForCorrectSPF();
        }

        private void InitLayers() {
            InitFirstLayer();
            InitLastLayer();
        }

        List<List<int>> graphLayers;
        List<int> firstLayer = new List<int>();
        List<int> lastLayer = new List<int>();

        public void InitLastLayer() {
            for (int i = 0; i < adjacencyList.Length; i++)
                if (adjacencyList[i].Length == 0)
                    lastLayer.Add(i);
        }

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
            bool graphCyclic = Algorithms.IsGraphСyclic(this);
            if (graphCyclic)
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
        public List<int> GetLastLayer() => lastLayer;
    }
}
