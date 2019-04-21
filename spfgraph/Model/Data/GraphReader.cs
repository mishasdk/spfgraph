using spfgraph.Model.GraphLib;
using System;

namespace spfgraph.Model.Data {
    public static class GraphReader {

        public static Graph ReadGraphFromFile(string filePath) {
            var list = DataProvider.CreateAdjacencyListFromFile(filePath);
            return new Graph(list);

        }

        public static Graph WriteGraphToFile(string filePath) {
            throw new NotImplementedException();
        }

    }
}
