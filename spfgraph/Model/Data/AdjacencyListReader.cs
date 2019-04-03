using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Model {
    public class AdjacencyListReader : IDisposable {

        #region Public Properties

        public int AmoutOfVertex { get; set; }
        public int CurrentLineIndex { get; private set; }
        public StreamReader reader;

        #endregion

        public AdjacencyListReader(string filePath) {
            reader = new StreamReader(filePath);
        }


        public void Dispose() {
            reader.Close();
        }

        public string ReadNextLine() {
            var line = reader.ReadLine();
            CurrentLineIndex++;
            return line;
        }
    }
}
