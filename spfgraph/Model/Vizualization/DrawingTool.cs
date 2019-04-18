using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using Model;
using View;
using System.Collections.ObjectModel;

namespace Model {
    public class DrawingTool {
      
        public DrawingTool() {
          
        }

        public void DrawGraph(Graph g) {
            var domGraph = new StackedGraph(g);
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

            var graph = new ObservableCollection<Element>();

            foreach (var i in edges)
                graph.Add(i);

            foreach (var i in nodes)
                graph.Add(i);


        }
    }
}
