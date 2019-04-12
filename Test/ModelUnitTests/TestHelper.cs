using System;
using System.Collections.Generic;

namespace Testing {
    public class TestHelper {

        static public List<List<int>> StupidLayerConstructor(int[][] adjacencyList) {
            var g = adjacencyList;
            var graphLayers = new List<List<int>>();
            bool[] u = new bool[g.Length];
            for (int i = 0; i < u.Length; i++)
                u[i] = false;

            while (!AllUsed(u)) {
                int[] counter = new int[g.Length];
                graphLayers.Add(new List<int>());

                for (int i = 0; i < g.Length; i++) {
                    if (!u[i])
                        for (int j = 0; j < g[i].Length; j++)
                            counter[g[i][j]]++;
                }

                for (int i = 0; i < counter.Length; i++)
                    if (!u[i])
                        if (counter[i] == 0) {
                            u[i] = true;
                            graphLayers[graphLayers.Count - 1].Add(i);
                        }

                for (int i = 0; i < graphLayers[graphLayers.Count - 1].Count; i++)
                    g[i] = new int[0];
            }
            return graphLayers;
        }

        static bool AllUsed(bool[] u) {
            for (int i = 0; i < u.Length; i++)
                if (!u[i])
                    return false;
            return true;
        }


        public static bool CheckListsForIdentity(List<int>[] expected, List<int>[] actual) {
            try {
                if (expected.Length != actual.Length)
                    return false;
                for (int i = 0; i < expected.Length; i++) {
                    if (expected[i].Count != actual[i].Count)
                        return false;
                    for (int j = 0; j < expected[i].Count; j++)
                        if (expected[i][j] != actual[i][j])
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
                Console.WriteLine("actual: ");
                foreach (var i in actual) {
                    foreach (var j in i)
                        Console.Write(j + " ");
                    Console.WriteLine();
                }
            }
        }

        public static bool CheckListsForIdentity(List<List<int>> expected, List<List<int>> actual) {
            try {
                if (expected.Count != actual.Count)
                    return false;
                for (int i = 0; i < expected.Count; i++) {
                    if (expected[i].Count != actual[i].Count)
                        return false;
                    for (int j = 0; j < expected[i].Count; j++)
                        if (expected[i][j] != actual[i][j])
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
                Console.WriteLine("actual: ");
                foreach (var i in actual) {
                    foreach (var j in i)
                        Console.Write(j + " ");
                    Console.WriteLine();
                }
            }
        }
    }
}
