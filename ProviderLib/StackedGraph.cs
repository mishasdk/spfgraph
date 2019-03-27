using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderLib {
    class StackedGraph : Graph {

        public StackedGraph() { }
        public StackedGraph(List<int>[] list) : base(list) { }
        public StackedGraph(Graph graph) : base() { }

        List<int> firstLayer = new List<int>();

        public void InitFirstLayer() {
            var first = from listForVerd in adjacencyList
                        from verd in listForVerd
                        where IsRoot(verd)
                        select verd;
            firstLayer = first.ToList();
        }

        bool IsRoot(int verd) {
            foreach (var line in adjacencyList)
                foreach (var v in line)
                    if (v == verd)
                        return false;
            return true;
        }
    }
}
