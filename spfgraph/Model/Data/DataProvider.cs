using spfgraph.Model.Exceptions;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Visualization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace spfgraph.Model.Data {
    /// <summary>
    /// Provides to work with files.
    /// </summary>
    public class DataProvider {

        #region Public Members

        /// <summary>
        /// Opens serialized json graph in browser
        /// using js and html.
        /// </summary>
        public static void OpenHtmlGraph(ObservableCollection<Element> collection) {
            JsonSerializer.SerializeGraph("Resources\\elementsCollection.json", collection);
            using (var sr = new StreamReader("Resources\\elementsCollection.json")) {
                using (var fs = new FileStream("Resources\\elementsCollection.js", FileMode.Create)) {
                    using (var sw = new StreamWriter(fs)) {
                        var str1 = "data = ";
                        var str2 = sr.ReadLine();
                        sw.WriteLine(str1 + str2);
                    }
                }
            }
            System.Diagnostics.Process.Start("Resources\\htmlGraph.html");
        }

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
                            sb.Append(vertex + " ");
                        }
                        writer.WriteLine(sb.ToString());
                    }
                    writer.WriteLine("#");
                    writer.WriteLine(graph.Features.ToString());
                    writer.WriteLine("#");

                }
            }
        }

        /// <summary>
        /// Read graph from text file.
        /// </summary>
        /// <param name="filePath">File path.</param>
        /// <returns>Graph object.</returns>
        public static Graph ReadGraphFromFile(string filePath) {
            var format = GetFileFormat(filePath);
            var parser = GetParser(format);
            var list =  parser.ReadAdjacencyListFromFile(filePath);
            return new Graph(list);
        }

        #endregion

        #region Private Methods

        static IParser GetParser(string format) {
            switch (format) {
                case "txt":
                    return new TxtParser();
                case "edg":
                    return new EdgParser();
                default:
                    throw new DataProviderException("Read from file error.\n" + "This file format is not supported.");
            }
        }

        static string GetFileFormat(string filePath) {
            var index = filePath.LastIndexOf('.') + 1;
            var format = filePath.Substring(index);
            return format;  
        }

        #endregion

    }

}