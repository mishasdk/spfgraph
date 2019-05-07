using Microsoft.Win32;
using System.Windows;

namespace spfgraph.Model.Dialog {
    public class DefaultDialogService : IDialogService {

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
    }
}
