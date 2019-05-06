using spfgraph.Model.Dialog;
using spfgraph.Model.Exceptions;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Windows;
using System.Windows.Input;

namespace spfgraph.ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        ICommand openCommand;
        ICommand buildGraphCommand;
        ICommand clearDataCommand;

        IDialogService dialogService;
        ColorDialogService colorDialog;
        string filePath;
        double canvasWidth;
        Color startColor;
        Color endColor;
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

        public Color StartColor {
            get => startColor;
            set {
                startColor = value;
                OnPropertyChanged(nameof(StartColor));
            }
        }

        public Color EndColor {
            get => endColor;
            set {
                endColor = value;
                OnPropertyChanged(nameof(EndColor));
            }
        }

        #endregion

        #region Commands

        ICommand setOptimizeLayoutFromRadioButton;
        public ICommand SetOptimizeLayotFromRadioButton {
            get => setOptimizeLayoutFromRadioButton ??
                (setOptimizeLayoutFromRadioButton = new ParametrizedCommand(
                    parameter => {
                        var str = (string)parameter;
                        switch (str) {
                            case "Minimize Crosses":
                                OptimizeLayout = OptimizeVisualizationTypes.MinimizeCrosses;
                                break;
                            case "Default":
                                OptimizeLayout = OptimizeVisualizationTypes.None;
                                break;
                        }
                    },
                    parameter => { return parameter == null ? false : true; }));
        }

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

        public ICommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {
                    if (FilePath == null)
                        return;
                    try {
                        GraphVM = new GraphViewModel(filePath, OptimizeLayout, ColorScheme, StartColor, EndColor);
                    } catch (GraphErrorException ex) {
                        dialogService.ShowMessage(ex.Message);
                    } catch (DataProviderException ex) {
                        dialogService.ShowMessage(ex.Message);
                    }
                }));
        }

        public ICommand OpenCommand {
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

        public ICommand ClearDataCommand {
            get => clearDataCommand ??
                (clearDataCommand = new RelayCommand(() => {
                    ClearData();
                }));
        }

        ICommand showColorType;
        public ICommand ShowColorType {
            get => showColorType ??
                (showColorType = new RelayCommand(() => {
                    MessageBox.Show(ColorScheme.ToString());
                }));
        }

        ICommand showLayoutType;
        public ICommand ShowLayoutType {
            get => showLayoutType ??
                (showLayoutType = new RelayCommand(() => {
                    MessageBox.Show(OptimizeLayout.ToString());
                }));
        }

        ICommand setStartColorCommand;
        public ICommand SetStartColorCommand {
            get => setStartColorCommand ??
                (setStartColorCommand = new RelayCommand(() => {
                    var color = colorDialog.GetColor();
                    if (color != null) {
                        StartColor = color;
                    }
                }));
        }

        ICommand setEndColorCommand;
        public ICommand SetEndColorCommand {
            get => setEndColorCommand ??
                (setEndColorCommand = new RelayCommand(() => {
                    var color = colorDialog.GetColor();
                    if (color != null) {
                        EndColor = color;
                    }
                }));
        }

        ICommand setDefaultColor;
        public ICommand SetDefaultColor {
            get => setDefaultColor ??
                (setDefaultColor = new RelayCommand(() => {
                    StartColor = new Color(25, 25, 30);
                    EndColor = new Color(218, 112, 214);
                }));
        }

        ICommand openHtmlCommand;
        public ICommand OpenHtmlCommand {
            get => openHtmlCommand ??
                (openHtmlCommand = new RelayCommand(() => {
                    try {
                        System.Diagnostics.Process.Start("demo.html");
                    } catch {
                    }

                }));

        }

        #endregion

        #region Constructor

        public MainWindowViewModel() {
            dialogService = new DefaultDialogService();
            colorDialog = new ColorDialogService();

            StartColor = new Color(25, 25, 30);
            EndColor = new Color(218, 112, 214);
        }

        #endregion

        #region Methods

        private void ClearData() {
            GraphVM = null;
        }

        #endregion

    }
}

