using spfgraph.Model.GraphLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace spfgraph.Model.Vizualiztion {
    public class GraphVizBuilder {
        protected StackedGraph domGraph;
        private Graph graph;

        public GraphVizBuilder(Graph graph) {
            this.graph = graph;
        }

        #region Paramentrs

        public int CurrentHeight { get; set; } = 0;
        public int HeightStep { get; set; } = 50;
        public int WidthStep { get; set; } = 60;
        public int StartLeft { get; set; } = 0;

        #endregion

        public ObservableCollection<Element> CreateGraphVizualization(StackedGraph domGraph) {
            this.domGraph = domGraph;
            OptimizeVisualization();
            var elements = CreateElementsToShow();
            return elements;
        }

        protected ObservableCollection<Element> CreateElementsToShow() {
            var nodes = new List<Node>();
            var edges = new List<Edge>();
            var indexByName = new SortedDictionary<int, int>();
            for (int i = 0; i < domGraph.GraphLayers.Count; i++) {
                CurrentHeight += HeightStep;
                int shift = domGraph.GraphLayers[i].Count * WidthStep / 2;
                for (int j = 0; j < domGraph.GraphLayers[i].Count; j++) {
                    var currentWidth = StartLeft + WidthStep * j;
                    var value = domGraph.GraphLayers[i][j];
                    var point = new Point(currentWidth - shift, CurrentHeight);
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

            var graph = new ObservableCollection<Element>();
            foreach (var i in edges)
                graph.Add(i);
            foreach (var i in nodes)
                graph.Add(i);

            return graph;
        }

        internal ObservableCollection<Element> CreateGraphVizualization() {
            throw new NotImplementedException();
        }

        protected void OptimizeVisualization() {
            for (int i = 1; i != domGraph.GraphLayers.Count; ++i) {
                var curLayerCounter = new SortedDictionary<int, int>();
                var curLayerEnterCounter = new SortedDictionary<int, int>();
                var mx = 0;
                for (var index = 0; index != domGraph.GraphLayers[i - 1].Count; ++index) {
                    var value = domGraph.GraphLayers[i - 1][index];
                    for (int j = 0; j != domGraph.AdjacencyList[value].Length; ++j) {
                        int to = domGraph.AdjacencyList[value][j];
                        if (!curLayerCounter.ContainsKey(to)) {
                            curLayerCounter[to] = 0;
                            curLayerEnterCounter[to] = 0;
                        }
                        curLayerCounter[to] += index + 1;
                        curLayerEnterCounter[to] += 1;
                        mx = Math.Max(curLayerCounter[to], mx);
                    }
                }
                domGraph.GraphLayers[i].Sort((a, b) => {
                    double first = (double)curLayerCounter[a] / curLayerEnterCounter[a];
                    double second = (double)curLayerCounter[b] / curLayerEnterCounter[b];
                    if (first == second) return 0;
                    if (first < second) return -1;
                    return 1;
                });
            }
        }
    }
}
