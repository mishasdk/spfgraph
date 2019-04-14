using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public static class GraphReader {

        public static Graph ReadGraphFromFile(string filePath) {
            Graph graph;
            var list = DataProvider.CreateAdjacencyListFromFile(filePath);
            graph = new Graph(list);
            try {
                graph = StackedFromGraph(graph);
            } catch (GraphErrorException) {
                ;
            }
            return graph;
        }

        static Graph StackedFromGraph(Graph graph) {
            if (Algorithms.IsGraphСyclic(graph))
                return graph;
                //throw new GraphErrorException("Build graph error." + "Stacked graph can't be cyclic.");
            return new StackedGraph(graph);
        }

        static bool IsGraphCyclic(Graph graph) {
            return Algorithms.IsGraphСyclic(graph);
        }

    }
}
