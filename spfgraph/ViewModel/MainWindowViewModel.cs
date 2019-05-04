using spfgraph.Model.Dialog;
using spfgraph.Model.Exceptions;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Windows;
using System.Windows.Input;

namespace spfgraph.ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        RelayCommand openCommand;
        RelayCommand buildGraphCommand;
        RelayCommand clearDataCommand;

        IDialogService dialogService;
        string filePath;
        double canvasWidth;
        Window window;
        GraphViewModel graphVM;
        ColorSchemeTypes colorScheme;
        OptimizeVisualizationTypes optimizeLayout;

        #endregion

        #region Public Propeties

        public OptimizeVisualizationTypes OptimizeLayout {
            get => optimizeLayout;
            set {
                optimizeLayout = value;
                OnPropertyChanged(nameof(OptimizeLayout));
            }
        }

        public ColorSchemeTypes ColorScheme {
            get => colorScheme;
            set {
                colorScheme = value;
                OnPropertyChanged(nameof(ColorScheme));
            }
        }

        public GraphViewModel GraphVM {
            get => graphVM;
            set {
                graphVM = value;
                OnPropertyChanged(nameof(GraphVM));
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

        ICommand setColorSchemeFromRadioButton;
        public ICommand SetColorSchemeFromRadioButton {
            get => setColorSchemeFromRadioButton ??
                (setColorSchemeFromRadioButton = new ParametrizedCommand(ExecuteMethod, CanExecuteMethod));
        }

        bool CanExecuteMethod(object parameter) {
            return parameter == null ? false : true;
        }

        void ExecuteMethod(object parameter) {
            var str = (string)parameter;
            switch (str) {
                case "In Degree":
                    ColorScheme = ColorSchemeTypes.InDegree;
                    break;
                case "Out Degree":
                    ColorScheme = ColorSchemeTypes.OutDegree;
                    break;
                case "Sum Degree":
                    ColorScheme = ColorSchemeTypes.SumDegree;
                    break;
                case "Default":
                    ColorScheme = ColorSchemeTypes.None;
                    break;
            }
        }


        public RelayCommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {
                    if (FilePath == null)
                        return;
                    try {
                        GraphVM = new GraphViewModel(filePath, OptimizeLayout, ColorScheme);
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

        RelayCommand showColorType;
        public RelayCommand ShowColorType {
            get => showColorType ??
                (showColorType = new RelayCommand(() => {
                    MessageBox.Show(ColorScheme.ToString());
                }));
        }

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window) {
            dialogService = new DefaultDialogService();
            this.window = window;

            OptimizeLayout = OptimizeVisualizationTypes.MinimizeCrosses;
            ColorScheme = ColorSchemeTypes.InDegree;
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

