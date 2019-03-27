using System.Collections.Generic;
using System.IO;

namespace ProviderLib {
    public static class DataProvider {

        public static List<int>[] CreateAdjacencyListFromFile(string filePath) {
            List<int>[] adjacencyList;
            using (var reader = new StreamReader(filePath)) {
                int n = int.Parse(reader.ReadLine());
                adjacencyList = new List<int>[n];
                for (int i = 0; i < n; i++) {
                    adjacencyList[i] = new List<int>();
                    var line = reader.ReadLine().Trim();
                    var splitedLine = line.Split(' ');
                    for (int j = 0; j < splitedLine.Length; j++) {
                        var verd = int.Parse(splitedLine[j]);
                        verd--;
                        adjacencyList[i].Add(verd);
                    }
                }
            }
            return adjacencyList;
        }
    }

}
