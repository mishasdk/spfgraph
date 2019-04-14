using Model;
using System;
using System.Collections.Generic;

namespace Model {
    public class StackedGraph : Graph {
        public List<List<int>> GraphLayers { get; set; }

        protected StackedGraph() { }
        public StackedGraph(Graph graph) : base(graph) {
            SetGraphLayers();
        }

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

        protected void SetGraphLayers() {
            var verds = new Pair<int>[adjacencyList.Length];
            var q = new Queue<int>();
            var g = adjacencyList;

            for (int i = 0; i < verds.Length; i++)
                verds[i] = new Pair<int>(0, -1);

            for (int i = 0; i < g.Length; i++)
                for (int j = 0; j < g[i].Length; j++)
                    verds[g[i][j]].First++;

            for (int i = 0; i < verds.Length; i++) {
                if (verds[i].First == 0) {
                    q.Enqueue(i);
                    verds[i].Second = 0;
                }
            }

            int amountOfLayers = 1;
            while (q.Count != 0) {
                int v = q.Dequeue();
                for (int i = 0; i < g[v].Length; i++) {
                    int to = g[v][i];
                    verds[to].First--;
                    if (verds[to].First == 0) {
                        q.Enqueue(to);
                        verds[to].Second = verds[v].Second + 1;
                        if (amountOfLayers < verds[to].Second + 1)
                            amountOfLayers++;
                    }
                }
            }

            GraphLayers = new List<List<int>>();
            for (int i = 0; i < amountOfLayers; i++)
                GraphLayers.Add(new List<int>());

            for (int i = 0; i < verds.Length; i++)
                GraphLayers[verds[i].Second].Add(i);
        }

        #region Help Methods

        protected void ConstructSPF() {
            var g = adjacencyList;
            GraphLayers = new List<List<int>>();
            bool[] u = new bool[g.Length];
            for (int i = 0; i < u.Length; i++)
                u[i] = false;

            while (!AllUsed(u)) {
                int[] counter = new int[g.Length];
                GraphLayers.Add(new List<int>());

                for (int i = 0; i < g.Length; i++) {
                    if (!u[i])
                        for (int j = 0; j < g[i].Length; j++)
                            counter[g[i][j]]++;
                }

                for (int i = 0; i < counter.Length; i++)
                    if (!u[i])
                        if (counter[i] == 0) {
                            u[i] = true;
                            GraphLayers[GraphLayers.Count - 1].Add(i);
                        }

                for (int i = 0; i < GraphLayers[GraphLayers.Count - 1].Count; i++)
                    g[i] = new int[0];
            }
        }

        protected bool AllUsed(bool[] u) {
            for (int i = 0; i < u.Length; i++)
                if (!u[i])
                    return false;
            return true;
        }

        #endregion

    }
}
