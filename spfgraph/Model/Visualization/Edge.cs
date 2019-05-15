using System;
using System.Runtime.Serialization;
using System.Windows;

namespace spfgraph.Model.Visualization {

    /// <summary>
    /// Class, that encapsulates graph's edge data.
    /// </summary>
    [DataContract]
    public class Edge : Element {

        #region Public Properties

        [DataMember]
        public Color EdgeColor { get; set; }

        [DataMember]
        public double X1 { get; set; }
        [DataMember]
        public double Y1 { get; set; }

        [DataMember]
        public double X2 { get; set; }
        [DataMember]
        public double Y2 { get; set; }

        [DataMember]
        public double X3 { get; set; }
        [DataMember]
        public double Y3 { get; set; }

        [DataMember]
        public double X4 { get; set; }
        [DataMember]
        public double Y4 { get; set; }

        #endregion

        #region Constructors

        public Edge(Node source, Node target) {
            X1 = source.X;
            Y1 = source.Y;
            X2 = target.X;
            Y2 = target.Y;
            var v = new Vector(X2 - X1, Y2 - Y1);
            v = v / v.Length * (v.Length - 18.5);
            X2 = X1 + v.X;
            Y2 = Y1 + v.Y;
            SetArrow();
        }

        public Edge(Node source, Node target, Color color) : this(source, target) {
            EdgeColor = color;
        }

        #endregion

        #region Methods

        void SetArrow() {
            var r = 10;
            var t = new Vector(X1 - X2, Y1 - Y2);
            var g = t / t.Length * r;
            var a = RotateVector(g, 25);
            var b = RotateVector(g, -25);

            X3 = a.X + X2;
            Y3 = a.Y + Y2;

            X4 = b.X + X2;
            Y4 = b.Y + Y2;
        }

        Vector RotateVector(Vector v, double angle) {
            var rad = angle * Math.PI / 180;
            return new Vector(Math.Cos(rad) * v.X - Math.Sin(rad) * v.Y, Math.Sin(rad) * v.X + Math.Cos(rad) * v.Y);
        }

        #endregion

    }
}
