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
            domGraph = optimizeVisualization(domGraph);
            var nodes = new List<Node>();
            var edges = new List<Edge>();
            int currentHeight = 0;
            int startLeft = 100;
            var widthStep = 60;
            var heightStep = 50;
            var indexByName = new SortedDictionary<int, int>();
            for (int i = 0; i < domGraph.GraphLayers.Count; i++) {
                currentHeight += heightStep;
                int shift = domGraph.GraphLayers[i].Count * widthStep / 2;
                for (int j = 0; j < domGraph.GraphLayers[i].Count; j++) {
                    var currentWidth = startLeft + widthStep * j;
                    var value = domGraph.GraphLayers[i][j];
                    var point = new Point(currentWidth - shift, currentHeight);
                    var node = new Node((int)point.X, (int)point.Y, value);
                    nodes.Add(node);
                    indexByName[domGraph.GraphLayers[i][j]] = nodes.Count - 1;
                }
            }

            for (int i = 0; i < domGraph.AdjacencyList.Length; i++)
                for (int j = 0; j < domGraph.AdjacencyList[i].Length; j++) {
                    int to = domGraph.AdjacencyList[i][j];
                    edges.Add(new Edge(nodes[indexByName[i]], nodes[indexByName[to]]));
                }

            foreach (var i in edges)
                graph.Add(i);

            foreach (var i in nodes)
                graph.Add(i);

            return graph;
        }

        public StackedGraph optimizeVisualization(StackedGraph graph) {
            var tmp = new StackedGraph(graph);

            for (int i = 1; i != tmp.GraphLayers.Count; ++i) {
                var curLayerCounter = new SortedDictionary<int, int>();
                var curLayerEnterCounter = new SortedDictionary<int, int>();
                var mx = 0;
                for (var index = 0; index != tmp.GraphLayers[i - 1].Count; ++index) {
                    var value = tmp.GraphLayers[i - 1][index];
                    for (int j = 0; j != tmp.AdjacencyList[value].Length; ++j) {
                        int to = tmp.AdjacencyList[value][j];
                        if (!curLayerCounter.ContainsKey(to)) {
                            curLayerCounter[to] = 0;
                            curLayerEnterCounter[to] = 0;
                        }
                        curLayerCounter[to] += index + 1;
                        curLayerEnterCounter[to] += 1;
                        mx = Math.Max(curLayerCounter[to], mx);
                    }
                }
                tmp.GraphLayers[i].Sort((a, b) => {
                    double first = (double)curLayerCounter[a] / curLayerEnterCounter[a];
                    double second = (double)curLayerCounter[b] / curLayerEnterCounter[b];
                    if (first == second) return 0;
                    if (first < second) return -1;
                    return 1;
                });
            }

            return tmp;
        }
    }
}
