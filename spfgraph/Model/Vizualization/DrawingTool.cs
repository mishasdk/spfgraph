using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using Model;
using View;

namespace Model {
    public class DrawingTool {
        Canvas Canvas { get; }

        public DrawingTool(Canvas canvas) {
            Canvas = canvas;
        }

        public void DrawGraph(Graph g) {
            var domGraph = new StackedGraph(g);
            var vertices = new List<Pair<Node, Point>>();
            var edges = new List<Edge>();
            int currentHeight = 0;
            int startLeft = (int)Canvas.ActualWidth / 2;

            for (int i = 0; i < domGraph.GraphLayers.Count; i++) {
                currentHeight += 50;
                for (int j = 0; j < domGraph.GraphLayers[i].Count; j++) {
                    var currentWidth = startLeft + 60 * j;
                    var p = new Point(currentWidth, currentHeight);
                    var node = new Node(domGraph.GraphLayers[i][j]);
                    Canvas.SetLeft(node, p.X);
                    Canvas.SetTop(node, p.Y);
                    vertices.Add(new Pair<Node, Point>(node, p));
                }
            }

            for (int i = 0; i < vertices.Count; i++) {
                for (int j = 0; j < domGraph.AdjacencyList[i].Length; j++) {
                    int to = domGraph.AdjacencyList[i][j];
                    edges.Add(new Edge(vertices[i], vertices[to]));
                }
            }

            foreach (var edge in edges)
                Canvas.Children.Add(edge.GetVizualizationOfEdge());
            foreach (var vertex in vertices)
                Canvas.Children.Add(vertex.First);
        }
    }
}
