using spfgraph.Model.Visualization;
using System.Windows.Forms;

namespace spfgraph.Model.Dialog {

    /// <summary>
    /// Interface of the message dialog.
    /// </summary>
    public interface IDialogService {

        /// <summary>
        /// File path property.
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Filter
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// Open file dialog, that init <cref="FilePath">.
        /// </summary>
        /// <returns></returns>
        bool OpenFileDialog();

        /// <summary>
        /// AlertDialog with some buttons.
        /// </summary>
        /// <param name="message">Message to show.</param>
        /// <returns>MessageBoxResult object.</returns>
        DialogResult AlertDialog(string message);

        /// <summary>
        /// Shows message to user.
        /// </summary>
        /// <param name="message">Message to show.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Save file dialog, that init <cref="FilePath">
        /// </summary>
        /// <returns></returns>
        bool SaveFileDialog();

        /// <summary>
        /// Raises color dialog.
        /// </summary>
        /// <returns>Choosen color object.</returns>
        Color GetColor();
    }
}
