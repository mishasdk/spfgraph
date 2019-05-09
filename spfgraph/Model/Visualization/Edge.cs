using System.Runtime.Serialization;

namespace spfgraph.Model.Visualization {

    /// <summary>
    /// Class, that encapsulates graph's edge data.
    /// </summary>
    [DataContract]
    public class Edge : Element {

        #region Public Properties

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

        #endregion

        #region Constructors

        public Edge(Node source, Node target) {
            X1 = source.X;
            Y1 = source.Y;
            X2 = target.X;
            Y2 = target.Y;
        }

        public Edge(Node source, Node target, Color color) : this(source, target) {
            EdgeColor = color;
        }

        #endregion

    }
}
