namespace spfgraph.Model.Vizualization {
    public class ParametricColorBuilder : DefaultColorBuilder, IColorBuilder {
        protected Color startColor;
        protected Color endColor;
        protected Color stepColor;

        public int[] VerdsParametrs { get; set; }

        public ParametricColorBuilder() {
            startColor = new Color(25, 25, 30);
            endColor = new Color(218, 112, 214);
        }

        public ParametricColorBuilder(int[] par, int max, int min = 0) : this() {
            VerdsParametrs = par;
            var diff = max - min;
            var stepRed = startColor.R - endColor.R;
            var stepGreen = startColor.G - endColor.G;
            var stepBlue = startColor.B - endColor.B;

            if (diff != 0)
                stepColor = new Color((byte)(stepRed / diff), (byte)(stepGreen / diff), (byte)(stepBlue / diff));
            else
                stepColor = new Color(0, 0, 0);
        }

        public override void SetNodeColor(Node node) {
            var curPar = VerdsParametrs[node.Value];
            var newColor = new Color((byte)(startColor.R + stepColor.R * curPar), (byte)(startColor.G + stepColor.G * curPar), (byte)(startColor.B + stepColor.B * curPar));
            node.NodeColor = newColor;
        }

    }
}
