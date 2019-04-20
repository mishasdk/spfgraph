using Model;
using QuickGraph;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        RelayCommand openCommand;
        RelayCommand buildGraphCommand;
        RelayCommand clearDataCommand;

        ObservableCollection<Element> graphToViz;
        IDialogService dialogService;
        string filePath;
        Window window;
        double canvasWidth;

        #endregion

        #region Public Propeties

        public double CanvasWidth {
            get => canvasWidth;
            set {
                canvasWidth = value;
                OnPropertyChanged(nameof(CanvasWidth));
            }
        }

        public ObservableCollection<Element> GraphToViz {
            get => graphToViz;
            set {
                graphToViz = value;
                OnPropertyChanged(nameof(GraphToViz));
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

        RelayCommand showWidthCommand;
        public RelayCommand ShowWidthCommand {
            get => showWidthCommand ??
                (showWidthCommand = new RelayCommand(() => {
                    dialogService.ShowMessage(CanvasWidth.ToString());
                }));
        }

        public RelayCommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {
                    if (FilePath == null)
                        return;
                    try {
                        var graph = new Graph(DataProvider.CreateAdjacencyListFromFile(FilePath));
                        var builder = new GraphVizBuilder(graph);
                        GraphToViz = builder.CreateGraphVizualization();
                    }
                    catch (GraphErrorException ex) {
                        dialogService.ShowMessage(ex.Message);
                    }
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

        public RelayCommand ClearDataCommand {
            get => clearDataCommand ??
                (clearDataCommand = new RelayCommand(() => {
                    ClearData();
                }));
        }

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window) {
            dialogService = new DefaultDialogService();
            this.window = window;

        }

        #endregion

        #region Methods

        private void ClearData() {
            FilePath = null;
            GraphToViz = null;

        }

        #endregion

    }
}

