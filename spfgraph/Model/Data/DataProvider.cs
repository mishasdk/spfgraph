using System;
using System.Collections.Generic;
using System.IO;

namespace Model {
    public static class DataProvider {

        public static List<int>[] CreateAdjacencyListFromFile(string filePath) {
            List<int>[] adjacencyList;
            using (var listReader = new AdjacencyListReader(filePath)) {
                SetAmoutOfVertex(listReader);
                CheckForSharp(listReader);
                adjacencyList = ReadAdjacencyListWith(listReader);
                CheckForSharp(listReader);
            }
            return adjacencyList;
        }

        static List<int>[] ReadAdjacencyListWith(AdjacencyListReader listReader) {
            // TODO: Code refactoring.
            var adjacencyList = new List<int>[listReader.AmoutOfVertex];
            var separator = "->";
            for (int i = 0; i < adjacencyList.Length; i++) {
                adjacencyList[i] = new List<int>();
                var line = listReader.ReadNextLine();

                if (line == null)
                    throw new DataProviderException("Reading from file error.\n" + $"Wrong file format, empty line {listReader.CurrentLineIndex}.");
                else if (line == "#")
                    throw new DataProviderException("Reading from file error.\n" + $"Invalid amount of verds line {listReader.CurrentLineIndex}.");

                var indexOfSeparator = line.IndexOf(separator);
                if (indexOfSeparator == 0)
                    throw new DataProviderException("Reading from file error.\n" + $"No verd's value in line: {listReader.CurrentLineIndex}. ");
                else if (indexOfSeparator == -1)
                    throw new DataProviderException("Reading from file error.\n" + $"Wrong format, no arrow in line: {listReader.CurrentLineIndex}. ");

                var splitedLine = line.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                Array.ForEach(splitedLine, x => x.Trim());

                if (splitedLine.Length > 2) {
                    throw new DataProviderException("Reading from file error.\n" + $"Wrong format, more than 1 arrow in line: {listReader.CurrentLineIndex}. ");
                } else if (splitedLine[0] == "")
                    throw new DataProviderException("Reading from file error.\n" + $"No verd's value in line: {listReader.CurrentLineIndex}. ");

                var verd = ReadIntFromString(splitedLine[0], listReader.CurrentLineIndex);
                if (verd != i)
                    throw new DataProviderException("Reading from file error.\n" + $"Invalid verd's order, they should be ordered by ascending in line: {listReader.CurrentLineIndex}. ");

                if (splitedLine.Length == 2) {
                    // Parsing line after arrow sign.
                    var secondLine = splitedLine[1];
                    if (secondLine != "") {
                        var verdsLine = secondLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (verdsLine.Length != 0) {
                            for (int j = 0; j < verdsLine.Length; j++) {
                                int v = ReadIntFromString(verdsLine[j], listReader.CurrentLineIndex);
                                if (v > adjacencyList.Length - 1)
                                    throw new DataProviderException("Reading from file error.\n" + $"Invalid value of the verd in line: {listReader.CurrentLineIndex}.");
                                adjacencyList[i].Add(v);
                            }
                        }
                    }
                }
            }
            return adjacencyList;
        }

        static void SetAmoutOfVertex(AdjacencyListReader listReader) {
            var line = listReader.ReadNextLine();
            var readResult = int.TryParse(line, out int verdsNumber);
            if (readResult == false || verdsNumber < 1)
                throw new DataProviderException("Reading from file error.\n" + $"Invalid amount of verds, line: {listReader.CurrentLineIndex}.");
            listReader.AmoutOfVertex = verdsNumber;
        }

        static void CheckForSharp(AdjacencyListReader listReader) {
            var line = listReader.ReadNextLine();
            if (line != "#")
                throw new DataProviderException("Reading from file error.\n" + $"Wrong file format, no sharp in line: {listReader.CurrentLineIndex}. ");
        }

        static int ReadIntFromString(string str, int CurrentLineIndex) {
            bool readResult = int.TryParse(str, out int verd);
            if (readResult == false || verd < 0)
                throw new DataProviderException("Reading from file error.\n" + $"Invalid value for verd {CurrentLineIndex}. ");
            return verd;
        }

    }
}
