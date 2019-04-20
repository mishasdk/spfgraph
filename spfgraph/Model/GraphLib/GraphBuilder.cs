using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace Model {
    public class GraphBuilder {
        Graph Graph { get; set; }
        LayoutTypes layoutType;

        public GraphBuilder(LayoutTypes layoutType) {

            this.layoutType = layoutType;

        }

        public void ReadGraphFromFile(string fileName) {
            var list = DataProvider.CreateAdjacencyListFromFile(fileName);
            Graph = new Graph(list);
        }

        public void ConstructSpfForm() {
            switch (layoutType) {
                case LayoutTypes.TheShortestHeigth:
                    ConstructTheShortestHeigth();
                    break;
            }

        }

        private void ConstructTheShortestHeigth() {
            
        }
    }
}
