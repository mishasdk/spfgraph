using System.Windows;

namespace Model {
    public interface IDialogService {
        string FilePath { get; set; }
        bool OpenFileDialog();
        MessageBoxResult AlertDialog(string message);
        void ShowMessage(string message);
    }
}
