namespace spfgraph.Model.Vizualization {
    public class Edge : Element {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public Color EdgeColor { get; set; } = new Color(0, 0, 0);

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
