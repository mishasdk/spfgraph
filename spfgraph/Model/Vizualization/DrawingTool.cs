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
            var layouts = new List<Layout>();
            var nodes = new List<Node>();
            var edges = new List<Edge>();
            int currentHeight = 0;
            int startLeft = (int)Canvas.ActualWidth / 2;
            var widthStep = 60;
            var heightStep = 50;

            for (int i = 0; i < domGraph.GraphLayers.Count; i++) {
                currentHeight += heightStep;
                int shift = domGraph.GraphLayers[i].Count * widthStep / 2;
                layouts.Add(new Layout());
                for (int j = 0; j < domGraph.GraphLayers[i].Count; j++) {
                    var currentWidth = startLeft + widthStep * j;
                    var value = domGraph.GraphLayers[i][j];
                    var point = new Point(currentWidth - shift, currentHeight);
                    var node = new Node((int)point.X, (int)point.Y, value);
                    nodes.Add(node);
                    layouts[i].AddVertex(node);
                }
            }


            for (int i = 0; i < domGraph.AdjacencyList.Length; i++)
                for (int j = 0; j < domGraph.AdjacencyList[i].Length; j++) {
                    int to = domGraph.AdjacencyList[i][j];
                    edges.Add(new Edge(nodes[i], nodes[to]));
                }

            

        }
    }
}
