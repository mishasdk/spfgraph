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
            string path = testSourceFolderPath + filePath;
            var expected = new List<int>[] {
                new List<int> {1, 2},
                new List<int> {2, 3},
                new List<int> {1},
                new List<int> {3},
                new List<int> { }
            };

            var actual = DataProvider.CreateAdjacencyListFromFile(path);
            var compareResult = TestHelper.CheckListsForIdentity(expected, actual);

            Assert.IsTrue(compareResult);
        }

        [TestMethod()]
        public void CreateListFrom_testList_02() {
            var filePath = "\\testList_02.txt";
            string path = testSourceFolderPath + filePath;
            var expected = new List<int>[] {
                new List<int> {1},
                new List<int> {0, 2},
                new List<int> {1, 0},
                new List<int> {0}
            };

            var actual = DataProvider.CreateAdjacencyListFromFile(path);
            var compareResult = TestHelper.CheckListsForIdentity(expected, actual);

            Assert.IsTrue(compareResult);
        }

        [TestMethod()]
        public void CreateListFrom_testList_03() {
            var filePath = "\\testList_03.txt";
            string path = testSourceFolderPath + filePath;
            Assert.ThrowsException<DataProviderException>(() => DataProvider.CreateAdjacencyListFromFile(path));
        }

        [TestMethod()]
        public void CreateListFrom_testList_04() {
            var filePath = "\\testList_04.txt";
            string path = testSourceFolderPath + filePath;
            Assert.ThrowsException<DataProviderException>(() => DataProvider.CreateAdjacencyListFromFile(path));
        }

        [TestMethod()]
        public void CreateListFrom_testList_05_EmptyFile() {
            var filePath = "\\testList_05.txt";
            string path = testSourceFolderPath + filePath;
            Assert.ThrowsException<DataProviderException>(() => DataProvider.CreateAdjacencyListFromFile(path));
        }

        [TestMethod()]
        public void CreateListFrom_testList_06_WrongOrder() {
            var filePath = "\\testList_06.txt";
            string path = testSourceFolderPath + filePath;
            Assert.ThrowsException<DataProviderException>(() => DataProvider.CreateAdjacencyListFromFile(path));
        }

        [TestMethod()]
        [ExpectedException(typeof(DataProviderException))]
        public void WrongVertexIndex_testList_07() {
            var filepath = "\\testList_07.txt";
            string path = testSourceFolderPath + filepath;
            DataProvider.CreateAdjacencyListFromFile(path);
        }

        
    }
}
