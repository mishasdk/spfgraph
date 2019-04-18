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

        IDialogService dialogService;
        string filePath;

        #endregion

        #region Public Propeties

        ObservableCollection<Element> graphToViz;
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

        public RelayCommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {
                    if (FilePath == null)
                        return;
                    var graph = new Graph(DataProvider.CreateAdjacencyListFromFile(FilePath));
                    var builder = new GraphVizBuilder(graph);
                    GraphToViz = builder.CreateGraphVizualization();
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

        public MainWindowViewModel() {
            graphToViz = new ObservableCollection<Element>();
            dialogService = new DefaultDialogService();
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

