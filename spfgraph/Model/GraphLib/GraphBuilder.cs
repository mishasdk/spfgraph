using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;

namespace Model {
    public class GraphBuilder {
        Graph graph { get; set; }
        BidirectionalGraph<object, IEdge<object>> graphToViz;

        public GraphBuilder(Graph graph) {
            this.graph = graph;
            graphToViz = new BidirectionalGraph<object, IEdge<object>>();
        }

        public BidirectionalGraph<object, IEdge<object>> CeateBidirectionalGraphToViz() {
            AddVertices();
            AddEdges();
            return graphToViz;
        }

        void AddVertices() {
            var ver = new List<string>();
            for (int i = 0; i < graph.AdjacencyList.Length; i++)
                ver.Add(i.ToString());
            graphToViz.AddVertexRange(ver);
        }

        void AddEdges() {
            var edg = new List<Edge<object>>();
            for (int i = 0; i < graph.AdjacencyList.Length; i++) {
                int v = i;
                for (int j = 0; j < graph.AdjacencyList[i].Length; j++) {
                    int to = graph.AdjacencyList[i][j];
                    edg.Add(new Edge<object>(v.ToString(), to.ToString()));
                }
            }
            graphToViz.AddEdgeRange(edg);
        }
    }
}
