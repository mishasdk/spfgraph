using System;
using System.IO;

namespace Model {
    public class AdjacencyListReader : IDisposable {
        StreamReader reader;

        public int AmoutOfVertex { get; set; }
        public int CurrentLineIndex { get; private set; }

        public AdjacencyListReader(string filePath) {
            reader = new StreamReader(filePath);
        }

        public string ReadNextLine() {
            var line = reader.ReadLine();
            CurrentLineIndex++;
            return line;
        }

        public void Dispose() {
            reader.Close();
        }

    }
}
