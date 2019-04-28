namespace spfgraph.Model.Vizualization {
    public class Node : Element {
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Color NodeColor { get; set; }    

        public Node(int x, int y, int value) {
            Value = value;
            X = x;
            Y = y;
        }

        public Node(int x, int y, int value, Color color) : this(x, y, value) {
            NodeColor = color;
        }

        public Node(Node n) : this(n.X, n.Y, n.Value) { }
    }
}
