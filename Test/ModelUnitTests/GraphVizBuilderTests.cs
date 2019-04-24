using Microsoft.VisualStudio.TestTools.UnitTesting;
using spfgraph.Model.Data;
using spfgraph.Model.Vizualization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Testing.ModelUnitTests {
    [TestClass()]
    public class GraphVizBuilderTests : GraphVizBuilder {

        [TestMethod()]
        public void BuildingViz_01() {
            var g = new int[][] {
                new int[] {2, 5 ,6 },
                new int[] { 3},
                new int[] {4 },
                new int[] {8 },
                new int[] { 7 },
                new int[] { 7},
                new int[] { 8},
                new int[] { },
            };

            for (int i = 0; i < g.Length; i++)
                for (int j = 0; j < g[i].Length; j++)
                    g[i][j]--;

            var mst = new List<Pair<int>>();

            // Building mst
            BuildMst(g, mst);

            foreach (var edge in mst)
                Console.WriteLine($"Edge: {edge.First + 1}  {edge.Second + 1}");

        }

        [TestMethod()]
        public void BuildingViz_02() {
            var g = new int[][] {
                new int[] {2, 5 ,6 },
                new int[] {3},
                new int[] {4},
                new int[] {8},
                new int[] {7},
                new int[] {7},
                new int[] {8},
                new int[] { },
            };
            for (int i = 0; i < g.Length; i++)
                for (int j = 0; j < g[i].Length; j++)
                    g[i][j]--;
            var mst = new List<Pair<int>>();

            // Building mst
            BuildMst(g, mst);

            var u = new bool[g.Length];
            var edge = mst[0];
            var res = CountCutValue(edge, g);

            Console.WriteLine(res);
        }

    }
}
