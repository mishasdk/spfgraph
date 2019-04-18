using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Model {
    public class GraphVizBuilder {
        public Graph Graph { get; set; }

        public GraphVizBuilder(Graph g) {
            Graph = g;
        }

        public ObservableCollection<Element> CreateGraphVizualization() {
            var graph = new ObservableCollection<Element>();
            var domGraph = new StackedGraph(Graph);
            var nodes = new List<Node>();
            var edges = new List<Edge>();
            int currentHeight = 0;
            int startLeft = 100;
            var widthStep = 60;
            var heightStep = 50;
            for (int i = 0; i < domGraph.GraphLayers.Count; i++) {
                currentHeight += heightStep;
                int shift = domGraph.GraphLayers[i].Count * widthStep / 2;
                for (int j = 0; j < domGraph.GraphLayers[i].Count; j++) {
                    var currentWidth = startLeft + widthStep * j;
                    var value = domGraph.GraphLayers[i][j];
                    var point = new Point(currentWidth - shift, currentHeight);
                    var node = new Node((int)point.X, (int)point.Y, value);
                    nodes.Add(node);
                }
            }

            for (int i = 0; i < domGraph.AdjacencyList.Length; i++)
                for (int j = 0; j < domGraph.AdjacencyList[i].Length; j++) {
                    int to = domGraph.AdjacencyList[i][j];
                    edges.Add(new Edge(nodes[i], nodes[to]));
                }

            foreach (var i in edges)
                graph.Add(i);

            foreach (var i in nodes)
                graph.Add(i);

            return graph;
        }
    }
}
