using spfgraph.Model.Vizualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spfgraph.Model.Dialog {
    public class ColorDialogService {

        public Color GetColor() {
            using (var colorDialog = new ColorDialog()) {
                if (colorDialog.ShowDialog() == DialogResult.OK) {
                    var color = colorDialog.Color;
                    return new Color(color.R, color.G, color.B);
                } else
                    return null;
            }
        }

    }
}
