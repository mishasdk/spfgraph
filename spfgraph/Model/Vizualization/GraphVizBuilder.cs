﻿using spfgraph.Model.Data;
using spfgraph.Model.GraphLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace spfgraph.Model.Vizualization {
    public class GraphVizBuilder {
        protected StackedGraph domGraph;

        #region Paramentrs

        public int StartHeight { get; set; } = 0;
        public int HeightStep { get; set; } = 60;
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
            var currentHeight = StartHeight;
            for (int i = 0; i < domGraph.GraphLayers.Count; i++) {
                int shift = domGraph.GraphLayers[i].Count * WidthStep / 2;
                for (int j = 0; j < domGraph.GraphLayers[i].Count; j++) {
                    var currentWidth = StartLeft + WidthStep * j;
                    var value = domGraph.GraphLayers[i][j];
                    var point = new Point(currentWidth - shift, currentHeight);
                    var node = new Node((int)point.X, (int)point.Y, value);
                    nodes.Add(node);
                    indexByName[domGraph.GraphLayers[i][j]] = nodes.Count - 1;
                }
                currentHeight += HeightStep;
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

        protected void OptimizeVisualization() {
            for (int i = 1; i != domGraph.GraphLayers.Count; ++i) {
                var curLayerCounter = new SortedDictionary<int, int>();
                var curLayerEnterCounter = new SortedDictionary<int, int>();
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
                    }
                }
                domGraph.GraphLayers[i].Sort((a, b) => {
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

        protected void SugyamaVis(StackedGraph dagGraph) {
            var g = dagGraph.AdjacencyList;
            var dict = new SortedDictionary<Pair<int>, int>();

            // Building mst
            var mst = BuildMst(g);

            // Count cut value
            foreach (var edge in mst) {
                var cutValue = CountCutValue(g, mst, edge);
                dict.Add(edge, cutValue);
            }

            while (IsNegativeCutValue(dict)) {
                var e = FindeNegativeEdge(dict);
                dict.Remove(e);
                var u = new bool[g.Length];
                Dfs_MarkAdjacencyVertices(mst, u, e.Second);

                

                //var b = FindNewEdge(g, mst);
            }
        }

        //private Pair<int> FindNewEdge(int[][] g, List<Pair<int>> mst) {

        //}

        private Pair<int> FindeNegativeEdge(SortedDictionary<Pair<int>, int> dict) {
            foreach (var e in dict)
                if (e.Value < 0)
                    return e.Key;
            return null;
        }

        private bool IsNegativeCutValue(SortedDictionary<Pair<int>, int> dict) {
            foreach (var e in dict)
                if (e.Value < 0)
                    return true;
            return false;
        }

        protected List<Pair<int>> BuildMst(int[][] g) {
            var mst = new List<Pair<int>>();
            var u = new bool[g.Length];
            for (int i = 0; i < g.Length; i++)
                if (!u[i])
                    Dfs_ForMst(g, u, mst, i);
            return mst;
        }

        protected int CountCutValue(int[][] g, List<Pair<int>> mst, Pair<int> edge) {
            int toTail = 0, toHead = 0;
            var u = new bool[g.Length];

            Dfs_MarkAdjacencyVertices(mst, u, edge.Second, edge.First);

            for (int i = 0; i < g.Length; i++)
                for (int j = 0; j < g[i].Length; j++) {
                    int v = i, to = g[i][j];
                    if (u[v] != u[to]) {
                        if (u[v] && !u[to])
                            toHead++;
                        else
                            toTail++;
                    }
                }

            return toTail - toHead;
        }

        protected void Dfs_MarkAdjacencyVertices(List<Pair<int>> mst, bool[] u, int v, int except = -1) {
            u[v] = true;
            for (int i = 0; i < mst.Count; i++)
                if (mst[i].Second == v && mst[i].First != except && !u[mst[i].First])
                    Dfs_MarkAdjacencyVertices(mst, u, mst[i].First);
                else if (mst[i].First == v && !u[mst[i].Second])
                    Dfs_MarkAdjacencyVertices(mst, u, mst[i].Second);
        }


        protected void Dfs_ForMst(int[][] g, bool[] u, List<Pair<int>> mst, int v) {
            u[v] = true;
            for (int i = 0; i < g[v].Length; i++) {
                int to = g[v][i];
                if (!u[to]) {
                    mst.Add(new Pair<int>(v, to));
                    Dfs_ForMst(g, u, mst, to);
                }
            }
        }

    }
}
