using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Model {
    public class Edge {
        public Point Source { get; set; }
        public Point Target { get; set; }

        public Edge(Node source, Node target) {
            Source = source.Point;
            Target = target.Point;
        }

        public Line GetVizualizationOfEdge() {
            return new Line {
                X1 = Source.X,
                Y1 = Source.Y,
                X2 = Target.X,
                Y2 = Target.Y,
                Stroke = new SolidColorBrush(Color.FromRgb(60, 60, 60)),
            };
        }
    }
}
