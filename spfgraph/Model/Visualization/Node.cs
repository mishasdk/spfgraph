using System.Runtime.Serialization;

namespace spfgraph.Model.Visualization {

    /// <summary>
    /// Class, that encapsulates all graph's node data.
    /// </summary>
    [DataContract]
    public class Node : Element {

        #region Public Properties

        [DataMember]
        public int Value { get; set; }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public Color NodeColor { get; set; }

        #endregion

        #region Constructors

        public Node(int x, int y, int value) {
            Value = value;
            X = x;
            Y = y;
        }
        public Node(int x, int y, int value, Color color) : this(x, y, value) {
            NodeColor = color;
        }
        public Node(Node n) : this(n.X, n.Y, n.Value) { }

        #endregion

    }
}
