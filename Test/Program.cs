using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProviderLib;

namespace ConsoleApp {
    class Program {
        static void Main(string[] args) {
            string path = "input.txt";
            var g = DataProvider.CreateAdjacencyListFromFile(path);
            var graph = new Graph(g);
            var spfGraph = new StackedGraph(graph);

            Console.WriteLine(spfGraph);

            //spfGraph.InitFirstLayer();
            //var fl = spfGraph.GetFirtsLayer();

            //foreach (var v in fl)
            //    Console.Write(v + "  ");
            //Console.WriteLine();

            Console.ReadKey();
        }
    }
}
