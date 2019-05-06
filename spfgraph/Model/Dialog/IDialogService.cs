using System.Windows;

namespace spfgraph.Model.Dialog {
    public interface IDialogService {
        string FilePath { get; set; }
        bool OpenFileDialog();
        MessageBoxResult AlertDialog(string message);
        void ShowMessage(string message);
        bool SaveFileDialog();
    }
}
