using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProviderLib;

namespace Test {
    class Test {
        static void Main(string[] args) {
            var g = CreateGraphFromFile();
            ShowGraph(g);
            TestDFS(g);

            Console.ReadKey();
        }

        static Graph CreateGraphFromFile() {
            var path = "input.txt";
            var g = DataProvider.CreateAdjacencyListFromFile(path);
            var graph = new Graph(g);
            return graph;
        }

        static void ShowGraph(Graph g) {
            Console.WriteLine(g);
        }

        static void TestDFS(Graph g) {
            bool result = Algorithms.IsGraphСyclical(g);
            Console.WriteLine(result);
        }

    }
}
