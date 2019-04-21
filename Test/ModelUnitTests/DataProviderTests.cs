using Microsoft.VisualStudio.TestTools.UnitTesting;
using spfgraph.Model.Data;
using spfgraph.Model.Exceptions;
using System.Collections.Generic;


namespace Testing {
    [TestClass()]
    public class DataProviderTests {
        string testSourceFolderPath = "../../TestSource/";

        [TestMethod()]
        public void CreateListFrom_testList_01() {
            var filePath = "\\testList_01.txt";
            var expected = new List<int>[] {
                new List<int> {1, 2},
                new List<int> {2, 3},
                new List<int> {1},
                new List<int> {3},
                new List<int> { }
            };

            ReadListFromFileAssert(filePath, expected);
        }

        [TestMethod()]
        public void CreateListFrom_testList_01_01() {
            var filePath = "\\testList_01_01.txt";
            var expected = new List<int>[] {
                new List<int> {},
            };

            ReadListFromFileAssert(filePath, expected);
        }

        [TestMethod()]
        public void CreateListFrom_testList_02() {
            var filePath = "\\testList_02.txt";
            var expected = new List<int>[] {
                new List<int> {1},
                new List<int> {0, 2},
                new List<int> {1, 0},
                new List<int> {0}
            };

            ReadListFromFileAssert(filePath, expected);
        }

        [TestMethod()]
        public void ReadGraphFromFile_testList_01() {
            var filePath = "\\testList_01.txt";
            var expected = new int[][] {
                new int[] {1, 2},
                new int[] {2, 3},
                new int[] {1},
                new int[] {3},
                new int[]  { }
            };

            ReadGraphFromFileAssert(filePath, expected);
        }

        [TestMethod()]
        public void ReadGraphFromFile_testList_01_01() {
            var filePath = "\\testList_01_01.txt";
            var expected = new int[][] {
                new int[] {},
            };

            ReadGraphFromFileAssert(filePath, expected);
        }

        [TestMethod()]
        public void ReadGraphFromFile_testList_02() {
            var filePath = "\\testList_02.txt";
            var expected = new int[][] {
                new int[] {1},
                new int[] {0, 2},
                new int[] {1, 0},
                new int[] {0}
            };

            ReadGraphFromFileAssert(filePath, expected);
        }


        [TestMethod()]
        [ExpectedException(typeof(DataProviderException))]
        public void CreateListFrom_testList_03() {
            var filePath = "\\testList_03.txt";
            CheckForPresenseException(filePath);
        }

        [TestMethod()]
        [ExpectedException(typeof(DataProviderException))]
        public void CreateListFrom_testList_04() {
            var filePath = "\\testList_04.txt";
            CheckForPresenseException(filePath);
        }

        [TestMethod()]
        [ExpectedException(typeof(DataProviderException))]
        public void CreateListFrom_testList_05_EmptyFile() {
            var filePath = "\\testList_05.txt";
            CheckForPresenseException(filePath);

        }

        [TestMethod()]
        [ExpectedException(typeof(DataProviderException))]
        public void CreateListFrom_testList_06_WrongOrder() {
            var filePath = "\\testList_06.txt";
            CheckForPresenseException(filePath);
        }

        [TestMethod()]
        [ExpectedException(typeof(DataProviderException))]
        public void WrongVertexIndex_testList_07() {
            var filepath = "\\testList_07.txt";
            CheckForPresenseException(filepath);
        }

        void ReadListFromFileAssert(string filePath, List<int>[] expected) {
            string path = testSourceFolderPath + filePath;
            var actual = DataProvider.CreateAdjacencyListFromFile(path);
            var compareResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(compareResult);
        }

        private void ReadGraphFromFileAssert(string filePath, int[][] expected) {
            string path = testSourceFolderPath + filePath;
            var actual = DataProvider.ReadGraphFromFile(path).AdjacencyList;
            var compareResult = TestHelper.CheckListsForIdentity(expected, actual);
            Assert.IsTrue(compareResult);
        }

        void CheckForPresenseException(string filePath) {
            string path = testSourceFolderPath + filePath;
            DataProvider.CreateAdjacencyListFromFile(path);
        }
    }
}
