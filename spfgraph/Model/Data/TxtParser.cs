using spfgraph.Model.Exceptions;
using System;
using System.Collections.Generic;

namespace spfgraph.Model.Data {
    public class TxtParser : IParser {

        /// <summary>
        /// Creates adjacency list of graph using 
        /// text file.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <returns>Adjacency list.</returns>
        public int[][] ReadAdjacencyListFromFile(string filePath) {
            List<int>[] adjacencyList;
            using (var listReader = new AdjacencyListReader(filePath)) {
                SetAmoutOfVertex(listReader);
                CheckForSharp(listReader);
                adjacencyList = ReadAdjacencyListWith(listReader);
                CheckForSharp(listReader);
            }
            return Proceed(adjacencyList);
        }

        #region Private Members

        List<int>[] ReadAdjacencyListWith(AdjacencyListReader listReader) {
            var adjacencyList = new List<int>[listReader.AmoutOfVertex];
            var separator = "->";
            for (int i = 0; i < adjacencyList.Length; i++) {
                adjacencyList[i] = new List<int>();
                var line = listReader.ReadNextLine();

                if (line == null)
                    throw new ParserException("Reading from file error.\n" + $"Wrong file format, empty line {listReader.CurrentLineIndex}.");
                else if (line == "#")
                    throw new ParserException("Reading from file error.\n" + $"Invalid amount of verds line {listReader.CurrentLineIndex}.");

                var indexOfSeparator = line.IndexOf(separator);
                var lastIndexOfSeparator = line.LastIndexOf(separator);

                if (indexOfSeparator != lastIndexOfSeparator)
                    throw new ParserException("Reading from file error.\n" + $"Wrong format, more than 1 arrow in line: {listReader.CurrentLineIndex}. ");
                if (indexOfSeparator == 0)
                    throw new ParserException("Reading from file error.\n" + $"No verd's value in line: {listReader.CurrentLineIndex}. ");
                else if (indexOfSeparator == -1)
                    throw new ParserException("Reading from file error.\n" + $"Wrong format, no arrow in line: {listReader.CurrentLineIndex}. ");

                var splitedLine = line.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                Array.ForEach(splitedLine, x => x.Trim());

                if (splitedLine.Length > 2) {
                    throw new ParserException("Reading from file error.\n" + $"Wrong format, more than 1 arrow in line: {listReader.CurrentLineIndex}. ");
                } else if (splitedLine[0] == "")
                    throw new ParserException("Reading from file error.\n" + $"No verd's value in line: {listReader.CurrentLineIndex}. ");

                var verd = ReadIntFromString(splitedLine[0], listReader.CurrentLineIndex);
                if (verd != i)
                    throw new ParserException("Reading from file error.\n" + $"Invalid verd's order, they should be ordered by ascending in line: {listReader.CurrentLineIndex}. ");

                if (splitedLine.Length == 2) {
                    // Parsing line after arrow sign.
                    var secondLine = splitedLine[1];
                    if (secondLine != "") {
                        var verdsLine = secondLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (verdsLine.Length != 0) {
                            for (int j = 0; j < verdsLine.Length; j++) {
                                int v = ReadIntFromString(verdsLine[j], listReader.CurrentLineIndex);
                                if (v > adjacencyList.Length - 1)
                                    throw new ParserException("Reading from file error.\n" + $"Invalid value of the verd in line: {listReader.CurrentLineIndex}.");
                                adjacencyList[i].Add(v);
                            }
                        }
                    }
                }
            }
            return adjacencyList;
        }

        void SetAmoutOfVertex(AdjacencyListReader listReader) {
            var line = listReader.ReadNextLine();
            var readResult = int.TryParse(line, out int verdsNumber);
            if (readResult == false || verdsNumber < 1)
                throw new ParserException("Reading from file error.\n" + $"Invalid amount of verds, line: {listReader.CurrentLineIndex}.");
            listReader.AmoutOfVertex = verdsNumber;
        }

        void CheckForSharp(AdjacencyListReader listReader) {
            var line = listReader.ReadNextLine();
            if (line != "#")
                throw new ParserException("Reading from file error.\n" + $"Wrong file format, no sharp in line: {listReader.CurrentLineIndex}. ");
        }

        int ReadIntFromString(string str, int CurrentLineIndex) {
            bool readResult = int.TryParse(str, out int verd);
            if (readResult == false || verd < 0)
                throw new ParserException("Reading from file error.\n" + $"Invalid value for verd {CurrentLineIndex}. ");
            return verd;
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

        #endregion

    }
}
