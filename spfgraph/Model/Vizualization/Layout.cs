using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Model {
    public class Layout {
        public List<Node> Nodes { get; set; }

        public Layout() {
            Nodes = new List<Node>();
        }

        public void AddVertex(Node v) {
            Nodes.Add(v);
        }

        public void AddVertexRange(IEnumerable<Node> v) {
            Nodes.AddRange(v);
        }

        public void DrawElement(Canvas canvas) {
            foreach (var v in Nodes)
                v.DrawElement(canvas);
        }

        //public void Swap(int index1, int index2) {
        //    var v1 = new Node(Nodes[index2]);
        //    var v2 = new Node(Nodes[index1]);

        //    Nodes[index1] = v1;
        //    Nodes[index2] = v2;
        //}


    }
}
