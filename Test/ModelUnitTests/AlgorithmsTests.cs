using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Collections.Generic;

namespace Testing {
    [TestClass()]
    public class AlgorithmsTests {
        [TestMethod()]
        public void IsGraphCyclicTest_01() {
            int[][] list = new int[][] {
                new int[] {2, 3},
                new int[] {3},
                new int[] {4},
                new int[] {4},
                new int[] {}
            };
            var gr = new Graph(list);
            var expected = false;

            var actual = Algorithms.IsGraphСyclic(gr);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsGraphCyclicTest_02() {
            int[][] list = new int[][] {
                new int[] {1, 2, 3},
                new int[] {4},
                new int[] {4},
                new int[] {},
                new int[] {3}
            };
            var gr = new Graph(list);
            var expected = false;

            var actual = Algorithms.IsGraphСyclic(gr);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsGraphCyclicTest_03() {
            int[][] list = new int[][] {
                new int[] {1},
                new int[] {0}
            };
            var gr = new Graph(list);
            var expected = true;

            var actual = Algorithms.IsGraphСyclic(gr);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsGraphCyclicTest_04_OneVertex() {
            int[][] list = new int[][] {
                new int[] {},
            };
            var gr = new Graph(list);
            var expected = false;

            var actual = Algorithms.IsGraphСyclic(gr);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsGraphCyclicTest_05() {
            int[][] list = new int[][] {
                new int[] {1},
                new int[] {2},
                new int[] {0}
            };
            var gr = new Graph(list);
            var expected = true;

            var actual = Algorithms.IsGraphСyclic(gr);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void OneVertexCycleTest_01() {
            int[][] list = new int[][] {
                new int[] { 1, 2, 3 },
                new int[] { 2, 3 },
                new int[] { 3 },
                new int[] { 3 }
            };
            var graph = new Graph(list);
            Assert.AreEqual(true, Algorithms.IsGraphСyclic(graph));
        }

        [TestMethod()]
        public void StraightPassAndReversed_01() {
            int[][] List = new int[][] {
                new int[] {5, 2, 1 },
                new int[] {5 },
                new int[] {3, 4},
                new int[] {5 },
                new int[] {5},
                new int[] { },
            };
            CreateTwoDiffLayers(List);
        }

        [TestMethod()]
        public void StraightPassAndReversed_02() {
            int[][] list = new int[][] {
                new int[] { 1, 2, 3 },
                new int[] { 2, 3 },
                new int[] { 3 },
                new int[] { }
            };
            CreateTwoDiffLayers(list);
        }

        [TestMethod()]
        public void StraightPassAndReversed_03() {
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
            CreateTwoDiffLayers(list);
        }

        private static void CreateTwoDiffLayers(int[][] List) {
            List<List<int>> a = Algorithms.StraightPass(List);
            List<List<int>> b = Algorithms.ReversePass(List);

            System.Console.WriteLine("streight");
            TestHelper.ShowGraph(a);
            System.Console.WriteLine("reverse");
            TestHelper.ShowGraph(b);
        }

    }
}
