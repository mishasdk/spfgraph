using Microsoft.VisualStudio.TestTools.UnitTesting;
using spfgraph.Model.GraphLib;
using System;


namespace Testing {
    [TestClass()]
    public class StackedGraphTests : StackedGraph {
        public StackedGraphTests() { }

        void ShowLayers() {
            Console.WriteLine("Graph Layers");
            for (int i = 0; i < GraphLayers.Count; i++) {
                Console.Write($"{i}:  ");
                for (int j = 0; j < GraphLayers[i].Count; j++)
                    Console.Write(GraphLayers[i][j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
