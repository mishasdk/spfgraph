using Microsoft.Win32;
using spfgraph.Model.Visualization;
using System.Windows;

namespace spfgraph.Model.Dialog {

    /// <summary>
    /// Message dialog.
    /// </summary>
    public class DefaultDialogService : IDialogService {

        #region Implementation the IDialogService

        public string FilePath { get; set; }

        public bool OpenFileDialog() {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog() == true) {
                FilePath = fd.FileName;
                return true;
            } else
                return false;
        }

        public MessageBoxResult AlertDialog(string message) {
            return MessageBox.Show(message, "Alert", MessageBoxButton.OKCancel);
        }

        public void ShowMessage(string message) {
            MessageBox.Show(message, "Message", MessageBoxButton.OK);
        }

        public bool SaveFileDialog() {
            var sd = new SaveFileDialog();
            if (sd.ShowDialog() == true) {
                FilePath = sd.FileName;
                return true;
            } else
                return false;
        }

        public Color GetColor() {
            using (var colorDialog = new System.Windows.Forms.ColorDialog()) {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    var color = colorDialog.Color;
                    return new Color(color.R, color.G, color.B);
                } else
                    return null;
            }
        }

        #endregion
    }
}
