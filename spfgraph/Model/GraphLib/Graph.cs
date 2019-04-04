using System;
using System.Collections.Generic;

namespace Model {
    public class Graph {

        #region Properties

        protected int[][] adjacencyList;
        public int[][] AdjacencyList {
            get => adjacencyList;
        }

        #endregion

        #region Constructors

        public Graph(List<int>[] list) { adjacencyList = Proceed(list); }
        public Graph(Graph gr) : this(gr.adjacencyList) { }
        public Graph(int[][] list) {
            var newList = new int[list.Length][];
            for (int i = 0; i < newList.Length; i++) {
                var line = list[i];
                newList[i] = new int[line.Length];
                Array.Copy(line, newList[i], line.Length);
            }
            adjacencyList = newList;
        }

        #endregion

        #region Methods

        // Transforms List<int>[] adjacency list to int[][].
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

        public override string ToString() {
            var mes = "";
            for (int i = 0; i < adjacencyList.Length; i++) {
                var line = adjacencyList[i];
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
