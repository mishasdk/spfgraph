using spfgraph.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace spfgraph.Model.Data {
    /// <summary>
    /// Parser for edg format
    /// </summary>
    public class EdgParser : IParser {

        public int[][] ReadAdjacencyListFromFile(string filePath) {
            List<int>[] adjacencyList = null;

            using (var reader = new AdjacencyListReader(filePath)) {
                try {
                    reader.AmoutOfVertex = GetAmountOfVertices(filePath);
                    adjacencyList = CreateAdjacencyList(reader);
                } catch {
                    throw new ParserException("Parsing .edg file error.\n" + $"Error in line {reader.CurrentLineIndex + 1}");
                }
            }
            return Proceed(adjacencyList);
        }


        static int GetAmountOfVertices(string filePath) {
            using (var reader = new StreamReader(filePath)) {
                var firstLine = reader.ReadLine();
                var splittedFirstLine = firstLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var n = int.Parse(splittedFirstLine[0]);
                var set = new HashSet<int>();
                for (int i = 0; i < n; i++) {
                    var line = reader.ReadLine();
                    var splittedLine = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var a = int.Parse(splittedLine[0]) - 100;
                    var b = int.Parse(splittedLine[1]) - 100;
                    set.Add(a);
                    set.Add(b);
                }
                return set.Count;
            }
        }

        static List<int>[] CreateAdjacencyList(AdjacencyListReader reader) {
            var adjacencyList = new List<int>[reader.AmoutOfVertex];
            for (int i = 0; i < adjacencyList.Length; i++)
                adjacencyList[i] = new List<int>();
            var firstLine = reader.ReadNextLine();
            var splittedFirstLine = firstLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var n = int.Parse(splittedFirstLine[0]);
            for (int i = 0; i < n; i++) {
                var line = reader.ReadNextLine();
                var splitedLine = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var u = int.Parse(splitedLine[0]) - 100;
                var v = int.Parse(splitedLine[1]) - 100;
                adjacencyList[u].Add(v);
            }
            return adjacencyList;
        }

        int[][] Proceed(List<int>[] adjacencyList) {
            int[][] list = new int[adjacencyList.Length][];
            for (int i = 0; i < adjacencyList.Length; i++) {
                list[i] = new int[adjacencyList[i].Count];
                for (int j = 0; j < list[i].Length; j++) {
                    list[i][j] = adjacencyList[i][j];
                }
            }
            return list;
        }

    }
}
