using System;

namespace Model {
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
