namespace spfgraph.Model.Vizualization {
    public class ParametricColorBuilder : DefaultColorBuilder, IColorBuilder {
        protected Color startColor;
        protected Color endColor;
        protected Color stepColor;

        public int[] VerdsParameters { get; set; }

        public ParametricColorBuilder(Color startColor, Color endColor) {
            this.startColor = startColor;
            this.endColor = endColor;
        }

        public ParametricColorBuilder(Color start, Color end, int[] par, int max, int min = 0) : this(start, end) {
            VerdsParameters = par;
            var diff = max - min;
            var diffColor = endColor - startColor;

            if (diff != 0)
                stepColor = diffColor / diff;
            else
                stepColor = new Color(0, 0, 0);
        }

        public override void SetNodeColor(Node node) {
            var curPar = VerdsParameters[node.Value];
            var newColor = startColor + stepColor * curPar;
            node.NodeColor = newColor;
        }

    }
}
