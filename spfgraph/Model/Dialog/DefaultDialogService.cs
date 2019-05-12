using spfgraph.Model.Visualization;
using System.Windows.Forms;

namespace spfgraph.Model.Dialog {

    /// <summary>
    /// Message dialog.
    /// </summary>
    public class DefaultDialogService : IDialogService {

        public static string TextFilter = "text files (*.txt) | *.txt";
        public static string PngFilter = "png files (*.png) | *.png";
        public static string JsonFilter = "json files (*.json) | *.json";

        #region Implementation the IDialogService

        public string FilePath { get; set; }
        public string Filter { get; set; }

        public bool OpenFileDialog() {
            using (var fd = new OpenFileDialog()) {
                fd.Filter = Filter;
                if (fd.ShowDialog() == DialogResult.OK) {
                    FilePath = fd.FileName;
                    return true;
                } else
                    return false;
            }
        }

        public DialogResult AlertDialog(string message) {
            return MessageBox.Show(message, "Alert", MessageBoxButtons.OKCancel);
        }

        public void ShowMessage(string message) {
            MessageBox.Show(message, "Message", MessageBoxButtons.OK);
        }

        public bool SaveFileDialog() {
            using (var sd = new SaveFileDialog()) {
                sd.Filter = Filter;
                if (sd.ShowDialog() == DialogResult.OK) {
                    FilePath = sd.FileName;
                    return true;
                } else
                    return false;
            }
        }

        public Color GetColor() {
            using (var colorDialog = new ColorDialog()) {
                if (colorDialog.ShowDialog() == DialogResult.OK) {
                    var color = colorDialog.Color;
                    return new Color(color.R, color.G, color.B);
                } else
                    return null;
            }
        }

        #endregion
    }
}
