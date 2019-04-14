using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model;
using QuickGraph;

namespace ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        RelayCommand openCommand;
        RelayCommand buildGraphCommand;
        RelayCommand clearAllCurrentData;


        Window window;
        IDialogService dialogService;
        string filePath;
        IBidirectionalGraph<object, IEdge<object>> graphToShow;

        #endregion

        #region Public Propeties

        public IBidirectionalGraph<object, IEdge<object>> GraphToShow {
            get => graphToShow;
            set {
                graphToShow = value;
                OnPropertyChanged(nameof(GraphToShow));
            }
        }

        public string FilePath {
            get => filePath;
            set {
                filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        #endregion

        #region Commands

        public RelayCommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {
                    if (FilePath == "" || FilePath == null)
                        return;
                    CreateGraphForShow();
                }));
        }

        public RelayCommand OpenCommand {
            get => openCommand ??
                (openCommand = new RelayCommand(() => {
                    try {
                        if (dialogService.OpenFileDialog()) {
                            FilePath = dialogService.FilePath;
                        }
                    } catch (Exception ex) {
                        dialogService.ShowMessage(ex.Message);
                    }
                }));
        }

        public RelayCommand ClearAllCurrentDataCommand {
            get => clearAllCurrentData ??
                (clearAllCurrentData = new RelayCommand(() => {
                    if (GraphToShow == null)
                        return;
                    var dialogResult = dialogService.AlertDialog("All unsaved data will be deleted. ");
                    if (dialogResult == MessageBoxResult.OK) {
                        ClearData();
                    }
                }));
        }

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window) {
            this.window = window;
            dialogService = new DefaultDialogService();

        }

        #endregion

        #region Methods

        private void ClearData() {
            FilePath = null;
            GraphToShow = null;
        }

        void CreateGraphForShow() {
            try {
                var g = GraphReader.ReadGraphFromFile(FilePath);
                var builder = new GraphBuilder(g);
                GraphToShow = builder.CeateBidirectionalGraphToViz();
            } catch (DataProviderException ex) {
                dialogService.ShowMessage(ex.Message);
            }
        }

        #endregion
    }
}

