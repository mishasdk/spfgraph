using spfgraph.Model.GraphLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace spfgraph.Model.Visualization {
    public class GraphVizBuilder {
        protected StackedGraph dagGraph;
        protected IColorBuilder colorBuilder;

        public Color EndColor { get; set; }
        public Color StartColor { get; set; }

        public OptimizeVisualizationTypes OptimizeLayout { get; set; }
        public ColorSchemeTypes ColorScheme { get; set; }

        #region Location Paramenters

        public int StartHeight { get; set; } = 20;
        public int HeightStep { get; set; } = 60;
        public int WidthStep { get; set; } = 60;
        public int StartLeft { get; set; } = 0;

        #endregion

        public GraphVizBuilder() { }

        public ObservableCollection<Element> CreateGraphVizualization(StackedGraph dagGraph) {
            this.dagGraph = dagGraph;
            UseOptimizeLayoutAlgorithm();
            SetColorScheme();
            return CreateElementsToShow();
        }

        #region Optimize Layout

        private void UseOptimizeLayoutAlgorithm() {
            switch (OptimizeLayout) {
                case (OptimizeVisualizationTypes.MinimizeCrosses):
                    MinimizeCrosses();
                    break;
                case (OptimizeVisualizationTypes.None):
                default:
                    // Nothing
                    break;
            }
        }

        protected void MinimizeCrosses() {
            for (int i = 1; i != dagGraph.GraphLayers.Count; ++i) {
                var curLayerCounter = new SortedDictionary<int, int>();
                var curLayerEnterCounter = new SortedDictionary<int, int>();
                for (var index = 0; index != dagGraph.GraphLayers[i - 1].Count; ++index) {
                    var value = dagGraph.GraphLayers[i - 1][index];
                    for (int j = 0; j != dagGraph.AdjacencyList[value].Length; ++j) {
                        int to = dagGraph.AdjacencyList[value][j];
                        if (!curLayerCounter.ContainsKey(to)) {
                            curLayerCounter[to] = 0;
                            curLayerEnterCounter[to] = 0;
                        }
                        curLayerCounter[to] += index + 1;
                        curLayerEnterCounter[to] += 1;
                    }
                }
                dagGraph.GraphLayers[i].Sort((a, b) => {
                    double first = 0.0;
                    if (curLayerCounter.ContainsKey(a))
                        first = (double)curLayerCounter[a] / curLayerEnterCounter[a];
                    double second = 0.0;
                    if (curLayerCounter.ContainsKey(b))
                        second = (double)curLayerCounter[b] / curLayerEnterCounter[b];
                    if (first == second) return 0;
                    if (first < second) return -1;
                    return 1;
                });
            }
        }

        #endregion

        #region Color Scheme Constructing

        protected void SetColorScheme() {
            switch (ColorScheme) {
                case (ColorSchemeTypes.InDegree):
                    InDegreeColorScheme();
                    break;
                case (ColorSchemeTypes.OutDegree):
                    OutDegreeColorScheme();
                    break;
                case (ColorSchemeTypes.SumDegree):
                    SumDegreeColorScheme();
                    break;
                case (ColorSchemeTypes.None):
                    NoneDegreeColorScheme();
                    break;
            }
        }

        private void NoneDegreeColorScheme() {
            colorBuilder = new DefaultColorBuilder();
        }

        protected void InDegreeColorScheme() {
            var inDegree = InDegreeArrayCount();

            var maxInDegree = 0;
            for (int i = 0; i < inDegree.Length; i++)
                if (inDegree[i] > maxInDegree)
                    maxInDegree = inDegree[i];
            colorBuilder = new ParametricColorBuilder(StartColor, EndColor, inDegree, maxInDegree);
        }

        protected void OutDegreeColorScheme() {
            var outDegree = OutDegreeArrayCount();
            var maxOutDegree = 0;

            for (int i = 0; i < outDegree.Length; i++)
                if (outDegree[i] > maxOutDegree)
                    maxOutDegree = outDegree[i];
            colorBuilder = new ParametricColorBuilder(StartColor, EndColor, outDegree, maxOutDegree);
        }

        protected void SumDegreeColorScheme() {
            var sumDegree = SumDegreeArrayCount();
            int max = -1, min = int.MaxValue;

            for (int i = 0; i < sumDegree.Length; i++) {
                if (max < sumDegree[i])
                    max = sumDegree[i];
                if (min > sumDegree[i])
                    min = sumDegree[i];
            }

            colorBuilder = new ParametricColorBuilder(StartColor, EndColor, sumDegree, max, min);
        }

        protected int[] InDegreeArrayCount() {
            var g = dagGraph.AdjacencyList;
            var inDegree = new int[g.Length];

            for (int i = 0; i < g.Length; i++)
                for (int j = 0; j < g[i].Length; j++)
                    inDegree[g[i][j]]++;
            return inDegree;
        }

        protected int[] OutDegreeArrayCount() {
            var g = dagGraph.AdjacencyList;
            var outDegree = new int[g.Length];

            for (int i = 0; i < g.Length; i++)
                outDegree[i] = g[i].Length;
            return outDegree;
        }

        protected int[] SumDegreeArrayCount() {
            var g = dagGraph.AdjacencyList;
            var inDegree = InDegreeArrayCount();
            var outDegree = OutDegreeArrayCount();
            var sumDegree = new int[g.Length];

            for (int i = 0; i < g.Length; i++) {
                sumDegree[i] = inDegree[i] + outDegree[i];
            }
            return sumDegree;
        }

        #endregion

        protected ObservableCollection<Element> CreateElementsToShow() {
            var nodes = new List<Node>();
            var edges = new List<Edge>();
            var indexByName = new SortedDictionary<int, int>();
            var currentHeight = StartHeight;

            // Creating nodes of graph
            for (int i = 0; i < dagGraph.GraphLayers.Count; i++) {
                int shift = dagGraph.GraphLayers[i].Count * WidthStep / 2;
                for (int j = 0; j < dagGraph.GraphLayers[i].Count; j++) {
                    var currentWidth = StartLeft + WidthStep * j;
                    var value = dagGraph.GraphLayers[i][j];
                    var point = new Point(currentWidth - shift, currentHeight);
                    var node = new Node((int)point.X, (int)point.Y, value);
                    nodes.Add(node);
                    indexByName[dagGraph.GraphLayers[i][j]] = nodes.Count - 1;
                }
                currentHeight += HeightStep;
            }

            // Set node colors
            ColorizeNodes(nodes);

            // Creating edges
            for (int i = 0; i < dagGraph.AdjacencyList.Length; i++)
                for (int j = 0; j < dagGraph.AdjacencyList[i].Length; j++) {
                    int to = dagGraph.AdjacencyList[i][j];
                    // Set edges colors
                    var source = nodes[indexByName[i]];
                    var target = nodes[indexByName[to]];
                    var edgeColor = nodes[indexByName[i]].NodeColor;
                    var edge = new Edge(source, target, edgeColor);
                    edges.Add(edge);
                }

            // Creating output colection of elementss
            var elementsCollection = new ObservableCollection<Element>();
            foreach (var i in edges)
                elementsCollection.Add(i);
            foreach (var node in nodes)
                elementsCollection.Add(node);

            return elementsCollection;
        }

        private void ColorizeNodes(List<Node> nodes) {
            foreach (var node in nodes)
                colorBuilder.SetNodeColor(node);

        }

    }
}
