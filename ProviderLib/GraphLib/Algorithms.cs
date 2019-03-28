using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderLib {
    public class Algorithms {
        public static bool IsGraphСyclical(Graph graph) {
            var g = graph.List;
            var color = new int[g.Length];
            bool result = false;

            for (int i = 0; i < g.Length; i++) {
                if (color[i] == 0)
                    CheckForCycleDFS(g, color, i, ref result);
            }

            return result;
        }

        static void CheckForCycleDFS(int[][] g, int[] color, int v, ref bool result) {
            if (result == true)
                return;
            color[v] = 1;
            for (int i = 0; i < g[v].Length; i++) {
                int to = g[v][i];
                if (color[to] == 0) {
                    CheckForCycleDFS(g, color, to, ref result);
                    color[to] = 2;
                } else if (color[to] == 1) {
                    result = true;
                }
            }
        }


    }

}
