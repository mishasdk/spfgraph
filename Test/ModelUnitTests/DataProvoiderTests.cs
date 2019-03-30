using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Data;
using System;
using System.Collections.Generic;
using Testing.Properties;
using Model.Excepitons;


namespace Testing.ModelUnitTests {
    [TestClass()]
    public class DataProvoiderTests {

        [TestMethod()]
        public void CreateListFrom_testList_01() {
            var str = "\\testList_01.txt";
            string path = Resource1.TestSource + str;
            var expected = new List<int>[] {
                new List<int> {1, 2},
                new List<int> {2, 3},
                new List<int> {1},
                new List<int> {3},
                new List<int> { }
            };

            var actual = DataProvider.CreateAdjacencyListFromFile(path);
            var compareResult = CheckListsForIdentity(expected, actual);

            Assert.IsTrue(compareResult);
        }

        [TestMethod()]
        public void CreateListFrom_testList_02() {
            var str = "\\testList_02.txt";
            string path = Resource1.TestSource + str;
            var expected = new List<int>[] {
                new List<int> {1},
                new List<int> {0, 2},
                new List<int> {1, 0},
                new List<int> {0}
            };

            var actual = DataProvider.CreateAdjacencyListFromFile(path);
            var compareResult = CheckListsForIdentity(expected, actual);

            Assert.IsTrue(compareResult);
        }

        [TestMethod()]
        public void CreateListFrom_testList_03() {
            var str = "\\testList_03.txt";
            string path = Resource1.TestSource + str;
            Assert.ThrowsException<DataProviderException>(() => DataProvider.CreateAdjacencyListFromFile(path));
        }

        [TestMethod()]
        public void CreateListFrom_testList_04() {
            var str = "\\testList_04.txt";
            string path = Resource1.TestSource + str;
            Assert.ThrowsException<DataProviderException>(() => DataProvider.CreateAdjacencyListFromFile(path));
        }

        [TestMethod()]
        public void CreateListFrom_testList_05_EmptyFile() {
            var str = "\\testList_05.txt";
            string path = Resource1.TestSource + str;
            Assert.ThrowsException<DataProviderException>(() => DataProvider.CreateAdjacencyListFromFile(path));
        }
        [TestMethod()]
        public void CreateListFrom_testList_05_WrongOrder() {
            var str = "\\testList_06.txt";
            string path = Resource1.TestSource + str;
            Assert.ThrowsException<DataProviderException>(() => DataProvider.CreateAdjacencyListFromFile(path));
        }



        static bool CheckListsForIdentity(List<int>[] expected, List<int>[] output) {
            try {
                if (expected.Length != output.Length)
                    return false;
                for (int i = 0; i < expected.Length; i++) {
                    if (expected[i].Count != output[i].Count)
                        return false;
                    for (int j = 0; j < expected[i].Count; j++)
                        if (expected[i][j] != output[i][j])
                            return false;
                }
                return true;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            } finally {
                Console.WriteLine("Expected: ");
                foreach (var i in expected) {
                    foreach (var j in i)
                        Console.Write(j + " ");
                    Console.WriteLine();
                }
                Console.WriteLine("Output: ");
                foreach (var i in output) {
                    foreach (var j in i)
                        Console.Write(j + " ");
                    Console.WriteLine();
                }
            }
        }
    }

    //[TestClass()]
    //public class DataProviderTest {

    //}

}
