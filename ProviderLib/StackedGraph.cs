using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderLib {
    class StackedGraph : Graph {

        public StackedGraph() { }
        public StackedGraph(List<int>[] list) : base(list) { }
        public StackedGraph(Graph graph) : base(graph) {
            throw new NotImplementedException();
        }

        List<int> firstLayer = new List<int>();

        public void InitFirstLayer() {
            int[] counter = new int[adjacencyList.Length];
            for (int i = 0; i < adjacencyList.Length; i++) {
                for (int j = 0; j < adjacencyList[i].Length; j++) {
                    counter[adjacencyList[i][j]]++;
                }
            }
        }

        
    }
}
