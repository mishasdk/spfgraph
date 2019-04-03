using Model;
using System;
using System.Collections.Generic;

namespace Model {
    public class StackedGraph : Graph {

        #region Privates Fields

        List<List<int>> graphLayers;
        List<int> firstLayer = new List<int>();
        List<int> lastLayer = new List<int>();

        #endregion

        #region Constructor

        public StackedGraph(Graph graph) : base(graph) {
            InitLayers();
        }

        #endregion

        #region Privates Methods

        void InitLayers() {
            InitFirstLayer();
            InitLastLayer();
        }

        void InitLastLayer() {
            for (int i = 0; i < adjacencyList.Length; i++)
                if (adjacencyList[i].Length == 0)
                    lastLayer.Add(i);
        }

        void InitFirstLayer() {
            int[] counter = new int[adjacencyList.Length];
            for (int i = 0; i < adjacencyList.Length; i++)
                for (int j = 0; j < adjacencyList[i].Length; j++)
                    counter[adjacencyList[i][j]]++;
            for (int i = 0; i < counter.Length; i++)
                if (counter[i] == 0)
                    firstLayer.Add(i);
        }
        void ConstructSPF() {
            // Constructing parallel stacked form.
            graphLayers = new List<List<int>> { firstLayer };
            // TODO: Write algo.
            throw new NotImplementedException();
        }

        #endregion

        // Functions for tests.
        public List<int> GetFirtsLayer() => firstLayer;
        public List<int> GetLastLayer() => lastLayer;
    }
}
