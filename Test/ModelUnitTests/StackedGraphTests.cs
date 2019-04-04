using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;


namespace Testing {
    [TestClass()]
    public class StackedGraphTests : StackedGraph {
        public StackedGraphTests() { }

        [TestMethod()]
        public void ConstructSPFFormTest_01() {
            int[][] list = new int[][] {
                new int[] {5, 2, 1 },
                new int[] {5 },
                new int[] {3, 4},
                new int[] {5 },
                new int[] {5},
                new int[] { },
            };
            adjacencyList = list;

            ConstructSPF();

            ShowLayers();
            Console.WriteLine(this);

            Assert.IsTrue(true);
        }

        void ShowLayers() {
            Console.WriteLine("Graph Layers");
            for (int i = 0; i < graphLayers.Count; i++) {
                Console.Write($"{i}:  ");
                for (int j = 0; j  < graphLayers[i].Count; j++)
                    Console.Write(graphLayers[i][j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
