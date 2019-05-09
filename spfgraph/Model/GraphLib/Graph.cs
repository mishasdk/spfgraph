using System;
using System.Collections.Generic;

namespace spfgraph.Model.GraphLib {

    /// <summary>
    /// Class, that encapsulates all graph's data.
    /// </summary>
    public class Graph {

        #region Public Properties

        public int[][] AdjacencyList { get; private set; }

        #endregion

        #region Constructors

        public Graph(List<int>[] list) { AdjacencyList = Proceed(list); }
        public Graph(Graph gr) : this(gr.AdjacencyList) { }
        public Graph(int[][] list) { AdjacencyList = Copy(list); }
        protected Graph() { }

        #endregion

        #region Help Methods  

        // Transforms List<int>[] adjacency list to int[][]
        protected static int[][] Proceed(List<int>[] list) {
            int[][] newList = new int[list.Length][];
            for (int i = 0; i < newList.Length; i++) {
                var line = list[i];
                newList[i] = new int[line.Count];
                for (int j = 0; j < line.Count; j++)
                    newList[i][j] = line[j];
            }
            return newList;
        }

        protected static int[][] Copy(int[][] list) {
            var newList = new int[list.Length][];
            for (int i = 0; i < newList.Length; i++) {
                var line = list[i];
                newList[i] = new int[line.Length];
                Array.Copy(line, newList[i], line.Length);
            }
            return newList;
        }

        public override string ToString() {
            var mes = "";
            for (int i = 0; i < AdjacencyList.Length; i++) {
                var line = AdjacencyList[i];
                mes += $"{i} -> ";
                for (int j = 0; j < line.Length; j++) {
                    mes += $"{line[j]} ";
                }
                mes += "\n";
            }
            return mes;
        }

        #endregion  

    }
}
