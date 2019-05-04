using System.Runtime.Serialization;

namespace spfgraph.Model.Vizualization {
    [DataContract]
    public class Edge : Element {
        [DataMember]
        public int X1 { get; set; }

        [DataMember]
        public int Y1 { get; set; }

        [DataMember]
        public int X2 { get; set; }

        [DataMember]
        public int Y2 { get; set; }

        [DataMember]
        public Color EdgeColor { get; set; }

        public Edge(Node source, Node target) {
            X1 = source.X;
            Y1 = source.Y;
            X2 = target.X;
            Y2 = target.Y;
        }

        public Edge(Node source, Node target, Color color) : this(source, target) {
            EdgeColor = color;
        }
    }
}
