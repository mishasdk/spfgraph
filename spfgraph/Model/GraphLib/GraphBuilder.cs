using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public static class GraphBuilder {

        public static StackedGraph StackedFromGraph(Graph graph) {
            if (Algorithms.IsGraphСyclic(graph))
                throw new GraphErrorException("Graph is cyclic.");

            return new StackedGraph(graph);
        }

        private static bool IsGraphCyclic(Graph graph) {
            return Algorithms.IsGraphСyclic(graph);
        }
    }
}
