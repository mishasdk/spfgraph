using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class GraphVizBuilder {
        public Graph Graph { get; set; }
        string GraphLayoutAlgorithmType { get; set; }

        public GraphVizBuilder(Graph g) {
            Graph = g;
        }

        public BiderectionalGraph ConstructGraphToShow() {

            throw new NotImplementedException();
        }
    }
}
