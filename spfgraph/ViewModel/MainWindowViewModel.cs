using spfgraph.Model.Data;
using spfgraph.Model.Dialog;
using spfgraph.Model.Exceptions;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace spfgraph.ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        RelayCommand openCommand;
        RelayCommand buildGraphCommand;
        RelayCommand clearDataCommand;

        ObservableCollection<Element> elementsToViz;
        IDialogService dialogService;
        string filePath;
        double canvasWidth;
        LayoutTypes selectedLayoutType;
        Window window;

        #endregion

        #region Public Propeties

        public LayoutTypes SelectedLayoutType {
            get => selectedLayoutType;
            set {
                selectedLayoutType = value;
                OnPropertyChanged(nameof(SelectedLayoutType));
            }
        }

        public double CanvasWidth {
            get => canvasWidth;
            set {
                canvasWidth = value;
                OnPropertyChanged(nameof(CanvasWidth));
            }
        }

        public ObservableCollection<Element> ElementsToViz {
            get => elementsToViz;
            set {
                elementsToViz = value;
                OnPropertyChanged(nameof(ElementsToViz));
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
                        var builder = new StackedGraphBuilder() {
                            LayoutType = SelectedLayoutType
                        };
                        var graph = DataProvider.ReadGraphFromFile(FilePath);
                        var dagGraph = builder.ConstructSpf(graph);

                        var graphViz = new GraphVizBuilder();
                        ElementsToViz = graphViz.CreateGraphVizualization(dagGraph);

                    } catch (GraphErrorException ex) {
                        dialogService.ShowMessage(ex.Message);
                    } catch (DataProviderException ex) {
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
            ElementsToViz = null;

        }

        #endregion

    }
}

