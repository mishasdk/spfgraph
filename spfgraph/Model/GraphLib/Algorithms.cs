namespace Model.GraphLib {
    public class Algorithms {

        public static bool IsGraphСyclic(Graph graph) {
            var g = graph.List;
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

        static bool CheckForCyclicDFS(int[][] g, int[] color, int v) {
            color[v] = 1;
            var result = false;
            for (int i = 0; i < g[v].Length; i++) {
                int to = g[v][i];
                if (color[to] == 0) {
                    result = CheckForCyclicDFS(g, color, to);    
                    color[to] = 2;
                } else if (color[to] == 1) {
                    result = true;
                }
                if (result)
                    return true;
            }
            return result;
        }
    }

}
