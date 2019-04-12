using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model {
    public interface IDialogService {

        string FilePath { get; set; }
        bool OpenFileDialog();
        void ShowMessage(string message);

    }
}
