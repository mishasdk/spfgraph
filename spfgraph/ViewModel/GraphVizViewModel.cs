using Model;
using QuickGraph;
using System.Collections.Generic;

namespace ViewModel {
    public class GraphVizViewModel : BaseViewModel {

        #region Private

        IBidirectionalGraph<object, IEdge<object>> graphToShow;
        Graph graph;

        #endregion

        #region Public Properties

        public IBidirectionalGraph<object, IEdge<object>> GraphToShow {
            get => graphToShow;
            set {
                graphToShow = value;
                OnPropertyChanged(nameof(GraphToShow));
            }
        }

        #endregion

        #region Constructor

        public GraphVizViewModel(Graph graph) {
            this.graph = graph;
            ConstructGraphToShow();

        }

        #endregion

        #region Methods

        void ConstructGraphToShow() {
            var g = new BidirectionalGraph<object, IEdge<object>>();
            int[][] adjacencyList = graph.AdjacencyList;

            var ver = new List<string>();
            for (int i = 0; i < adjacencyList.Length; i++)
                ver.Add(i.ToString());
            g.AddVertexRange(ver);

            var edg = new List<Edge<object>>();
            for (int i = 0; i < adjacencyList.Length; i++)
                for (int j = 0; j < adjacencyList[i].Length; j++) {
                    int v = i;
                    int to = adjacencyList[i][j];
                    edg.Add(new Edge<object>(ver[v], ver[to]));
                }
            g.AddEdgeRange(edg);

            GraphToShow = g;
        }

        #endregion
    }
}
