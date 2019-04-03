using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace spfgraph.Model {
    public class AdjacencyReader : IDisposable {

        #region Public Properties

        public int AmoutOfVertex { get; }
        public int CurrentLine { get; } = 1;
        public StreamReader Reader { get; }

        #endregion

        public AdjacencyReader(string filePath) {
            Reader = new StreamReader(filePath);
        }

        public void Dispose() {
            Reader.Close();
        }

        public string ReadNextLine() {
            var line = Reader.ReadLine();
            return line;
        }
    }
}
