namespace spfgraph.Model.Vizualization {
    public class ParametricColorBuilder : DefaultColorBuilder, IColorBuilder {
        protected Color startColor;
        protected Color endColor;
        protected Color step;

        public int[] VerdsParametrs { get; set; }

        public ParametricColorBuilder() {
            //startColor = new Color(25, 25, 112);
            //endColor = new Color(218, 112, 214);
            startColor = new Color(25, 25, 30);
            endColor = new Color(218, 112, 214);
        }

        public ParametricColorBuilder(int[] par, int max, int min = 0) : this() {
            VerdsParametrs = par;
            var diff = max - min;
            var sR = startColor.R - endColor.R;
            var sG = startColor.G - endColor.G;
            var sB = startColor.B - endColor.B;

            if (diff != 0)
                step = new Color((byte)(sR / diff), (byte)(sG / diff), (byte)(sB / diff));
            else
                step = new Color(0, 0, 0);
        }

        public override void SetNodeColor(Node node) {
            var curPar = VerdsParametrs[node.Value];
            var newColor = new Color((byte)(startColor.R + step.R * curPar), (byte)(startColor.G + step.G * curPar), (byte)(startColor.B + step.B * curPar));
            node.NodeColor = newColor;
        }

    }
}
