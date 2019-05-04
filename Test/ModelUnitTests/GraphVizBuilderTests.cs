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

            // Building mst
            var mst = BuildMst(g);

            for (int i = 0; i < mst.Count; i++) {
                var u = new bool[g.Length];

                Dfs_MarkAdjacencyVertices(mst, u, mst[i].Second, mst[i].First);

                for (int j = 0; j < g.Length; j++)
                    if (u[j])
                        Console.Write($"{j + 1} ");

                Console.WriteLine($"Edge: {mst[i].First + 1}  {mst[i].Second + 1}");

            }
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

            var mst = BuildMst(g);

            for (int i = 0; i < mst.Count; i++) {
                var n = CountCutValue(g, mst, mst[i]);
                Console.WriteLine(n);
            }
        }

    }
}
