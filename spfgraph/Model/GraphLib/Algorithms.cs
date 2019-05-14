using spfgraph.Model.Data;
using spfgraph.Model.Exceptions;
using System.Collections.Generic;

namespace spfgraph.Model.GraphLib {

    /// <summary>
    /// Class that implements some algorithms
    /// for work with graphs.
    /// </summary>
    public class Algorithms {

        #region Check For Cyclic

        /// <summary>
        /// Check graph for presence of cycles.
        /// </summary>
        /// <param name="graph">Graph for check.</param>
        /// <returns>True, if cycles presence, otherwise false.</returns>
        public static bool IsGraphСyclic(Graph graph) {
            var g = graph.AdjacencyList;
            var color = new int[g.Length];
            bool result = false;

            for (int i = 0; i < g.Length; i++) {
                if (color[i] == 0) {
                    result = CheckForCyclicDFS(g, color, i);
                    if (result)
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// DFS for check cyclic.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="color"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        protected static bool CheckForCyclicDFS(int[][] g, int[] color, int v) {
            color[v] = 1;
            var result = false;
            for (int i = 0; i < g[v].Length; i++) {
                int to = g[v][i];
                if (color[to] == 0) {
                    result = CheckForCyclicDFS(g, color, to);
                } else if (color[to] == 1) {
                    result = true;
                }
                if (result)
                    return true;
            }
            color[v] = 2;
            return result;
        }

        #endregion

        #region The Shortest Path Layout

        /// <summary>
        /// Layouts verices on the graph layers 
        /// to minimize graph's height.
        /// </summary>
        /// <param name="adjacencyList"></param>
        /// <returns></returns>
        public static List<List<int>> TheShortestPathLayout(int[][] adjacencyList) {
            var firstLayoutList = StraightPass(adjacencyList);
            var secondLayoutList = ReversePass(adjacencyList);

            if (secondLayoutList.Count != firstLayoutList.Count)
                throw new AlgorithmException("Wrong height of tree");

            int maxWidth1 = 0;
            int maxWidth2 = 0;
            for (int i = 0; i < firstLayoutList.Count; i++) {
                if (maxWidth1 < firstLayoutList[i].Count)
                    maxWidth1 = firstLayoutList[i].Count;
                if (maxWidth2 < secondLayoutList[i].Count)
                    maxWidth2 = secondLayoutList[i].Count;
            }

            // Choose which one who has less maxWidth
            if (maxWidth1 <= maxWidth2)
                return firstLayoutList;
            else
                return secondLayoutList;
        }

        public static List<List<int>> StraightPass(int[][] adjacencyList) {
            var g = adjacencyList;
            var verds = new Pair<int>[g.Length];
            var q = new Queue<int>();
            var amountOfLayers = 1;

            // First is outdegree number, second number of layout
            for (int i = 0; i < verds.Length; i++)
                verds[i] = new Pair<int>(0, -1);

            // Outdegree counting
            for (int i = 0; i < g.Length; i++)
                for (int j = 0; j < g[i].Length; j++)
                    verds[g[i][j]].First++;

            // Adding in queue vertices with indegree == 0
            for (int i = 0; i < verds.Length; i++) {
                if (verds[i].First == 0) {
                    verds[i].Second = 0;
                    q.Enqueue(i);
                }
            }

            while (q.Count != 0) {
                int v = q.Dequeue();
                for (int i = 0; i < g[v].Length; i++) {
                    int to = g[v][i];
                    verds[to].First--;
                    if (verds[to].First == 0) {
                        q.Enqueue(to);
                        verds[to].Second = verds[v].Second + 1;
                        if (amountOfLayers <= verds[to].Second)
                            amountOfLayers++;
                    }
                }
            }

            var graphLayers = new List<List<int>>();
            for (int i = 0; i < amountOfLayers; i++)
                graphLayers.Add(new List<int>());
            for (int i = 0; i < verds.Length; i++)
                graphLayers[verds[i].Second].Add(i);
            return graphLayers;
        }

        public static List<List<int>> ReversePass(int[][] adjacencyList) {
            var g = adjacencyList;
            var reversedAdjacencyList = new List<int>[g.Length];
            var reversedList = new int[g.Length][];

            for (int i = 0; i < g.Length; i++)
                reversedAdjacencyList[i] = new List<int>();

            // Fill list with reversed edges from adjacencyList
            for (int i = 0; i < g.Length; i++) {
                for (int j = 0; j < g[i].Length; j++)
                    reversedAdjacencyList[g[i][j]].Add(i);
            }

            for (int i = 0; i < reversedAdjacencyList.Length; i++) {
                reversedList[i] = new int[reversedAdjacencyList[i].Count];
                for (int j = 0; j < reversedList[i].Length; j++)
                    reversedList[i][j] = reversedAdjacencyList[i][j];
            }

            var graphLayers = StraightPass(reversedList);
            graphLayers.Reverse();
            return graphLayers;
        }

        #endregion

    }
}
