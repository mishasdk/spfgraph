using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spfgraph.Model.Vizualization {
    public class ColorBuilder {
        protected Color startColor;
        protected Color endColor;
        protected int maxBound;
        protected Color step;

        public int[] VerdsParametrs { get; set; }


        public ColorBuilder() {
            startColor = new Color(100, 10, 200);
            endColor = new Color(40, 100, 30);
        }

        public ColorBuilder(int[] par, int max) : this() {
            VerdsParametrs = par;
            maxBound = max;

            var sR = startColor.R - endColor.R;
            var sG = startColor.G - endColor.G;
            var sB = startColor.B - endColor.B;

            if (maxBound != 0)
                step = new Color((byte)(sR / maxBound), (byte)(sG / maxBound), (byte)(sB / maxBound));
            else
                step = new Color(0 ,0 ,0);
        }

        public void SetNodeColor(Node node) {
            var curPar = VerdsParametrs[node.Value];
            var newColor = new Color((byte)(startColor.R + step.R * curPar), (byte)(startColor.G + step.G * curPar), (byte)(startColor.B + step.B * curPar));
            node.NodeColor = newColor;
        }

    }
}
