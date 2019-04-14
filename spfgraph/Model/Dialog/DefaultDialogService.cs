using Microsoft.Win32;
using System.Windows;

namespace Model {
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

        public void ShowMessage(string message) {
            MessageBox.Show(message, "Message", MessageBoxButton.OK);
        }
    }
}
