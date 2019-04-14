namespace Model {
    public interface IDialogService {
        string FilePath { get; set; }
        bool OpenFileDialog();
        void ShowMessage(string message);
    }
}
