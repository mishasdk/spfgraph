using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;


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

        [TestMethod()]
        public void SetGraphLayers_01() {
            int[][] list = new int[][] {
                new int[] {},
            };
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void SetGraphLayers_02() {
            int[][] list = new int[][] {
                new int[] {},
                new int[] {0},
                new int[] {7, 0},
                new int[] {2},
                new int[] {8, 6},
                new int[] {4, 3, 7 },
                new int[] {1, 7},
                new int[] {0},
                new int[] {1},
            };
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void SetGraphLayer_03() {
            int[][] list = new int[][] {
                new int[] {5, 2, 1 },
                new int[] {5 },
                new int[] {3, 4},
                new int[] {5 },
                new int[] {5},
                new int[] { },
            };
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);

        }

        [TestMethod()]
        public void SetGraphLayer_04() {
            int[][] list = new int[][] {
                new int[] { },
                new int[] {0},
                new int[] {0},
                new int[] {0},
                new int[] {1, 5, 3, 2},
                new int[] {0},
            };
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void SetGraphLayer_05() {
            int[][] list = new int[][] {
                new int[] {},
                new int[] {0},
                new int[] {3},
                new int[] {0, 1, 4},
                new int[] {0},

            };
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void SetGraphLayer_06() {
            int[][] list = new int[][] {
                new int[] {3, 1},
                new int[] {},
                new int[] {1, 4},
                new int[] {4, 2},
                new int[] {1},
            };
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void MultipleSourceTest_01() {
            int[][] list = new int[][] {
                new int[] { 3, 7 },
                new int[] { 3 },
                new int[] { 4 },
                new int[] { 6, 7 },
                new int[] { 5 },
                new int[] { 6, 8 },
                new int[] { 7, 8 },
                new int[] { },
                new int[] { },
            };
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void MultipleSinkTest_01() {
            int[][] list = new int[][] {
                new int[] { 1, 2 },
                new int[] { 4, 3 },
                new int[] { 5 },
                new int[] { 9, 8 },
                new int[] { 8, 6 },
                new int[] { 6, 8, 7 },
                new int[] { },
                new int[] { },
                new int[] { },
                new int[] { },
            };
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void MultipleSourceAndSinkTest_01() {
            int[][] list = new int[][] {
                new int[] { 3 },
                new int[] { 2 },
                new int[] { 4, 5, 6 },
                new int[] { 5, 6, 7 },
                new int[] { },
                new int[] { },
                new int[] { },
                new int[] { },
            };
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);
        }

        [TestMethod()]
        public void DenseGraph_01() {
            var size = 100;
            int[][] list = new int[size][];
            for (var i = 0; i < size; ++i) {
                list[i] = new int[size - i - 1];
                for (var j = i + 1; j < size; ++j) {
                    list[i][j - i - 1] = j;
                }
            }
            adjacencyList = list;

            SetGraphLayers();
            var actual = graphLayers;
            var expected = TestHelper.StupidLayerConstructor(AdjacencyList);

            var testResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(testResult);
        }

        void ShowLayers() {
            Console.WriteLine("Graph Layers");
            for (int i = 0; i < graphLayers.Count; i++) {
                Console.Write($"{i}:  ");
                for (int j = 0; j < graphLayers[i].Count; j++)
                    Console.Write(graphLayers[i][j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
