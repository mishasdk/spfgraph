using spfgraph.Model.Exceptions;
using spfgraph.Model.GraphLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace spfgraph.Model.Data {
    /// <summary>
    /// Provides to work with files.
    /// </summary>
    public static class DataProvider {

        #region Public Members

        /// <summary>
        /// Saves graph to png.
        /// </summary>
        /// <param name="filePath">File path for save.</param>
        /// <param name="parameter">Elements to save.</param>
        public static void SaveGraphAsPng(string filePath, object parameter) {
            // Save canvas to png
            var canvas = (ItemsControl)parameter;
            var rect = new Rect(canvas.RenderSize);
            var rtb = new RenderTargetBitmap((int)rect.Right,
              (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(canvas);
            //endcode as PNG
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            //save to memory stream
            var ms = new MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();
            File.WriteAllBytes(filePath, ms.ToArray());
        }

        /// <summary>
        /// Saves dag graph in to text file.
        /// </summary>
        /// <param name="fileName">File path.</param>
        /// <param name="graph">StackedGraph object.</param>
        public static void SaveDagInFile(string fileName, StackedGraph graph) {
            var g = graph.AdjacencyList;
            using (var fs = new FileStream(fileName, FileMode.Create)) {
                using (var writer = new StreamWriter(fs)) {
                    int n = g.Length;
                    writer.WriteLine(n);
                    writer.WriteLine("#");
                    for (int i = 0; i < n; i++) {
                        var sb = new StringBuilder("");
                        sb.Append(i + " ->");
                        for (int j = 0; j < g[i].Length; j++) {
                            sb.Append($" {g[i][j]}");
                        }
                        writer.WriteLine(sb.ToString());
                    }
                    writer.WriteLine("#");
                    foreach (var layer in graph.GraphLayers) {
                        var sb = new StringBuilder("");
                        foreach (var vertex in layer) {
                            sb.Append(vertex  + " ");
                        }
                        writer.WriteLine(sb.ToString());
                    }
                    writer.WriteLine("#");
                    writer.WriteLine(graph.GetGraphFeatures().ToString());
                    writer.WriteLine("#");

                }
            }
        }

        /// <summary>
        /// Read graph from text file.
        /// </summary>
        /// <param name="fileName">File path.</param>
        /// <returns>Graph object.</returns>
        public static Graph ReadGraphFromFile(string fileName) {
            var list = CreateAdjacencyListFromFile(fileName);
            return new Graph(list);
        }

        #endregion

        #region Private Members

        static List<int>[] CreateAdjacencyListFromFile(string filePath) {
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
                var lastIndexOfSeparator = line.LastIndexOf(separator);

                if (indexOfSeparator != lastIndexOfSeparator)
                    throw new DataProviderException("Reading from file error.\n" + $"Wrong format, more than 1 arrow in line: {listReader.CurrentLineIndex}. ");
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

        #endregion

    }
}
