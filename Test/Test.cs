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
            var sg = CreateStackedGraph(g);

            ShowGraph(sg);
            ShowFirstLayer(sg);
            ShowLastLayer(sg);

            Console.ReadKey();
        }

        static Graph CreateGraphFromFile() {
            var path = "input.txt";
            var g = DataProvider.CreateAdjacencyListFromFile(path);
            var graph = new Graph(g);
            return graph;
        }

        static StackedGraph CreateStackedGraph(Graph g) {
            return new StackedGraph(g);
        }

        static void ShowGraph(Graph g) {
            Console.WriteLine("spf graph");
            Console.WriteLine(g);
        }
        
        static void ShowFirstLayer(StackedGraph g) {
            Console.Write("First layer: ");
            var a = g.GetFirtsLayer();
            foreach (var i in a )
                Console.Write(i + " ");
            Console.WriteLine();
        }

        static void ShowLastLayer(StackedGraph g) {
            Console.Write("Last layer: ");
            var a = g.GetLastLayer();
            foreach (var i in a)
                Console.Write(i + " ");
            Console.WriteLine();
        }

    }
}
