using System;
using System.IO;

namespace spfgraph.Model.Data {
    /// <summary>
    /// Object for reading adjacency list from text file.
    /// </summary>
    public class AdjacencyListReader : IDisposable {
        StreamReader reader;

        public int AmoutOfVertex { get; set; }
        public int CurrentLineIndex { get; private set; }

        public AdjacencyListReader(string filePath) {
            reader = new StreamReader(filePath);
        }

        /// <summary>
        /// Reads next line from the <cref="reader">.
        /// </summary>
        /// <returns>Read string.</returns>
        public string ReadNextLine() {
            var line = reader.ReadLine();
            CurrentLineIndex++;
            return line;
        }

        /// <summary>
        /// Implementation of IDisposable interface.
        /// </summary>
        public void Dispose() {
            reader.Close();
        }

    }
}
