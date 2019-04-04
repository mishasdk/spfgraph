using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;

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
    }
}
