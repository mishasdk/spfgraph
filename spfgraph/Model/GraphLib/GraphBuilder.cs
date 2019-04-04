using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public static class GraphBuilder {

        public static StackedGraph StackedFromGraph(Graph graph) {
            if (Algorithms.IsGraphСyclic(graph))
                throw new GraphErrorException("Build graph error." + "Stacked graph can't be cyclic.");

            return new StackedGraph(graph);
        }

        private static bool IsGraphCyclic(Graph graph) {
            return Algorithms.IsGraphСyclic(graph);
        }
    }
}
