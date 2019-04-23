using spfgraph.Model.Data;
using spfgraph.Model.Dialog;
using spfgraph.Model.Exceptions;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace spfgraph.ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        RelayCommand openCommand;
        RelayCommand buildGraphCommand;
        RelayCommand clearDataCommand;

        IDialogService dialogService;
        string filePath;
        double canvasWidth;
        LayoutTypes selectedLayoutType;
        Window window;
        GraphViewModel graphVM;

        #endregion

        #region Public Propeties

        public GraphViewModel GraphVM {
            get => graphVM;
            set {
                graphVM = value;
                OnPropertyChanged(nameof(GraphVM));
            }
        }

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

        public string FilePath {
            get => filePath;
            set {
                filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }

        #endregion

        #region Commands

        RelayCommand showInfoCommand;
        public RelayCommand ShowInfoCommand {
            get => showInfoCommand ??
                (showInfoCommand = new RelayCommand(() => {
                    try {
                        dialogService.ShowMessage($"Heigth: {GraphVM.GraphHeight}");
                    } catch {
                        dialogService.ShowMessage("Empty graphVM");
                    }
                }));
        }

        public RelayCommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {
                    if (FilePath == null)
                        return;
                    try {
                        GraphVM = new GraphViewModel(filePath);
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
            GraphVM = null;

        }

        #endregion

    }
}

