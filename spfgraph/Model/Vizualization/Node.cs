using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Model {
    public class Node : Button {
        public Vertex Vertex { get; set; }
        public Point Point { get; set; }

        public Node(Vertex vertex, Point point) {
            Vertex = vertex;
            Point = point;
            Content = Vertex.Value;
        }

        public Node(Node n) : this(new Vertex(n.Vertex.Value), new Point(n.Point.X, n.Point.Y)) { }

        public Node() { }

        public void DrawElement(Canvas canvas) {
            Canvas.SetLeft(this, Point.X);
            Canvas.SetTop(this, Point.Y);
            canvas.Children.Add(this);
        }
    }
}
