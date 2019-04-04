﻿using Model;
using System;
using System.Collections.Generic;

namespace Model {
    public class StackedGraph : Graph {

        #region Privates Fields

        protected List<List<int>> graphLayers;
        List<int> firstLayer = new List<int>();
        List<int> lastLayer = new List<int>();

        #endregion

        #region Constructor

        public StackedGraph(Graph graph) : base(graph) {
            InitLayers();
        }
        protected StackedGraph() { }

        #endregion

        #region Privates Methods

        void InitLayers() {
            InitFirstLayer();
            InitLastLayer();
        }

        void InitLastLayer() {
            for (int i = 0; i < adjacencyList.Length; i++)
                if (adjacencyList[i].Length == 0)
                    lastLayer.Add(i);
        }

        void InitFirstLayer() {
            int[] counter = new int[adjacencyList.Length];
            for (int i = 0; i < adjacencyList.Length; i++)
                for (int j = 0; j < adjacencyList[i].Length; j++)
                    counter[adjacencyList[i][j]]++;
            for (int i = 0; i < counter.Length; i++)
                if (counter[i] == 0)
                    firstLayer.Add(i);
        }

        protected void ConstructSPF() {
            var g = Proceed(adjacencyList);
            graphLayers = new List<List<int>>();
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

        }

        protected bool AllUsed(bool[] u) {
            for (int i = 0; i < u.Length; i++)
                if (!u[i])
                    return false;
            return true;
        }

        #endregion

        // Functions for tests.
        public List<int> GetFirtsLayer() => firstLayer;
        public List<int> GetLastLayer() => lastLayer;
    }
}
