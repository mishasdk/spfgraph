using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

namespace ProviderLib {
    public static class DataProvider {

        public static List<int>[] CreateAdjacencyListFromFile(string filePath) {
            List<int>[] adjacencyList;
            using (var reader = new StreamReader(filePath)) {
                var n = ReadNumberOfVerds(reader);
                adjacencyList = new List<int>[n];
                CheckForSharp(reader);
                ReadAdjacencyList(adjacencyList, reader);
                CheckForSharp(reader);
            }
            return adjacencyList;
        }

        static void ReadAdjacencyList(List<int>[] adjacencyList, StreamReader reader) {
            // TODO: Code refactoring.
            var separator = "->";
            for (int i = 0; i < adjacencyList.Length; i++) {
                adjacencyList[i] = new List<int>();
                var line = reader.ReadLine();

                if (line == null)
                    throw new DataProviderException("Reading from file error.\n" + "Wrong file format.");
                else if (line == "#")
                    throw new DataProviderException("Reading from file error.\n" + "Invalid amount of verds.");

                var indexOfSeparator = line.IndexOf(separator);
                if (indexOfSeparator == 0)
                    throw new DataProviderException("Reading from file error.\n" + $"No verd's value in line: {i + 3}. ");
                else if (indexOfSeparator == -1)
                    throw new DataProviderException("Reading from file error.\n" + $"Wrong format, no arrow in line: {i + 3}. ");

                var splitedLine = line.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                Array.ForEach(splitedLine, x => x.Trim());

                if (splitedLine.Length > 2) {
                    throw new DataProviderException("Reading from file error.\n" + $"Wrong format, more than 1 arrow in line: {i + 3}. ");
                } else if (splitedLine[0] == "")
                    throw new DataProviderException("Reading from file error.\n" + $"No verd's value in line: {i + 3}. ");

                var verd = ReadIntFromString(splitedLine[0]);
                if (verd != i)
                    throw new DataProviderException("Reading from file error.\n" + $"Invalid verd's order, they should be ordered by ascending in line: {i + 3}. ");

                if (splitedLine.Length == 2) {
                    // Parsing line after arrow sign.
                    var secondLine = splitedLine[1];
                    if (secondLine != "") {
                        var verdsLine = secondLine.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (verdsLine.Length != 0) {
                            for (int j = 0; j < verdsLine.Length; j++) {
                                int v = ReadIntFromString(verdsLine[j]);
                                if (v > adjacencyList.Length - 1)
                                    throw new DataProviderException("Reading from file error.\n" + $"Invalid value of the verd in line: {i + 3}.");
                                adjacencyList[i].Add(v);
                            }
                        }
                    }
                }
            }

        }

        static void CheckForSharp(StreamReader reader) {
            var line = reader.ReadLine();
            if (line != "#")
                throw new DataProviderException("Reading from file error.\n" + "Wrong file format, no sharp. ");
        }

        static int ReadIntFromString(string str) {
            bool readResult = int.TryParse(str, out int verd);
            if (readResult == false || verd < 0)
                throw new DataProviderException("Reading from file error.\n" + "Invalid value for verd. ");
            return verd;
        }

        static int ReadNumberOfVerds(StreamReader reader) {
            var line = reader.ReadLine();
            var readResult = int.TryParse(line, out int verdsNumber);
            if (readResult == false || verdsNumber < 1)
                throw new DataProviderException("Reading from file error.\n" + "Invalid amount of verds.");
            return verdsNumber;
        }

    }
}
