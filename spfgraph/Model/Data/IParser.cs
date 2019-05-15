using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spfgraph.Model.Data {
    public interface IParser {
        int[][] ReadAdjacencyListFromFile(string filePath);
    }
}
