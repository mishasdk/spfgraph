using spfgraph.Model.Data;
using spfgraph.Model.Dialog;
using spfgraph.Model.Exceptions;
using spfgraph.Model.GraphLib;
using spfgraph.Model.Visualization;
using spfgraph.ViewModel.Base;
using System;
using System.Windows.Forms;
using System.Windows.Input;

namespace spfgraph.ViewModel {

    /// <summary>
    /// Class, that encapsulates general logic of application.
    /// </summary>
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        ICommand choosePathForFileCommand;
        ICommand buildGraphCommand;
        ICommand clearDataCommand;
        ICommand saveGraphInTextFileCommand;
        ICommand exportToJsonCommand;
        ICommand exportToPngCommand;
        ICommand openHtmlCommand;

        ICommand setOptimizeLayoutFromRadioButton;
        ICommand setLayoutAlgorithmFromRadioButton;
        ICommand setColorSchemeFromRadioButton;
        ICommand setStartColorCommand;
        ICommand setEndColorCommand;
        ICommand setDefaultColor;

        IDialogService dialogService;
        ColorSchemeTypes colorScheme;
        OptimizeVisualizationTypes optimizeLayout;
        LayoutAlgorithmTypes layoutAlgorithm;
        BackgroundTypes backgroundType;

        string filePath;
        double canvasWidth;
        Color startColor;
        Color endColor;
        GraphViewModel graphVM;

        #endregion

        #region Public Propeties

        public BackgroundTypes BackgroundType {
            get => backgroundType;
            set {
                backgroundType = value;
                OnPropertyChanged(nameof(BackgroundType));
            }
        }

        public LayoutAlgorithmTypes LayoutAlgorithm {
            get => layoutAlgorithm;
            set {
                layoutAlgorithm = value;
                OnPropertyChanged(nameof(LayoutAlgorithm));
            }
        }

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

        public ICommand ChoosePathForFileCommand {
            get => choosePathForFileCommand ??
                (choosePathForFileCommand = new RelayCommand(() => {
                    try {
                        dialogService.Filter = DefaultDialogService.TextEdgFilter;
                        if (dialogService.OpenFileDialog()) {
                            FilePath = dialogService.FilePath;
                            GraphVM = null;
                        }
                    } catch (Exception ex) when (ex is ParserException || ex is DataProviderException) {
                        dialogService.ShowMessage(ex.Message);
                    }
                }));
        }

        public ICommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new ActionCommand(() => {
                    try {
                        GraphVM = new GraphViewModel() {
                            StartColor = StartColor,
                            EndColor = EndColor,
                            OptimizeLayout = OptimizeLayout,
                            LayoutAlgorithm = LayoutAlgorithm,
                            ColorScheme = ColorScheme,
                            FilePath = FilePath,
                            BackgroundType = BackgroundType,
                        };

                        GraphVM.CreateSPF();
                    } catch (OutOfMemoryException) {
                        GraphVM = null;
                        dialogService.ShowMessage("Out of memory. Can’t create graph.");
                    } catch (Exception ex) when (ex is ParserException || ex is DataProviderException) {
                        GraphVM = null;
                        dialogService.ShowMessage(ex.Message);
                    } catch (Exception ex) {
                        GraphVM = null;
                        dialogService.ShowMessage("Critical error.\n" + ex.Message);
                    }

                }, IsFilePathExists));
        }

        public ICommand ClearDataCommand {
            get => clearDataCommand ??
                (clearDataCommand = new ActionCommand(() => {
                    try {
                        var dialogResult = dialogService.AlertDialog("Are you shure? \n" + "All current data will be disappeared.");
                        if (dialogResult == DialogResult.OK)
                            ClearData();
                    } catch (Exception ex) {
                        dialogService.ShowMessage("Clear data error.\n" + ex.Message);
                    }
                }, IsFilePathExists));
        }

        public ICommand SaveGraphInTextFileCommand {
            get => saveGraphInTextFileCommand ??
                (saveGraphInTextFileCommand = new ActionCommand(() => {
                    try {
                        dialogService.Filter = DefaultDialogService.TextFilter;
                        if (!dialogService.SaveFileDialog())
                            return;
                        var filePath = dialogService.FilePath;
                        DataProvider.SaveDagInFile(filePath, GraphVM.DagGraph);
                    } catch (Exception ex) {
                        dialogService.AlertDialog(ex.Message);
                    }
                }, IsGraphVMExists));
        }

        public ICommand ExportToJsonCommand {
            get => exportToJsonCommand ??
                (exportToJsonCommand = new ActionCommand(() => {
                    try {
                        dialogService.Filter = DefaultDialogService.JsonFilter;
                        if (!dialogService.SaveFileDialog())
                            return;
                        JsonSerializer.SerializeGraph(dialogService.FilePath, GraphVM.ElementsToViz);
                    } catch (Exception ex) {
                        dialogService.AlertDialog(ex.Message);
                    }
                }, IsGraphVMExists));
        }

        public ICommand ExportToPngCommand {
            get => exportToPngCommand ??
                (exportToPngCommand = new ParametricActionCommand(
                    (parameter) => {
                        try {
                            dialogService.Filter = DefaultDialogService.PngFilter;
                            if (!dialogService.SaveFileDialog())
                                return;
                            DataProvider.SaveGraphAsPng(dialogService.FilePath, parameter);
                        } catch (UnauthorizedAccessException ex) {
                            dialogService.ShowMessage("Export to png error.\n" + "Нou do not have enough access rights.\n" + ex.Message);
                        } catch (Exception ex) {
                            dialogService.ShowMessage("Export to png error.\n" + ex.Message);
                        }
                    },
                    IsGraphVMExists));
        }

        public ICommand OpenHtmlCommand {
            get => openHtmlCommand ??
                (openHtmlCommand = new ActionCommand(() => {
                    try {
                        DataProvider.OpenHtmlGraph(GraphVM.ElementsToViz);
                    } catch (Exception ex) {
                        dialogService.ShowMessage("Open graph in browser error.\n" + ex.Message);
                    }
                }, IsGraphVMExists));
        }

        #region Layout Parameters Commands

        ICommand setBackgroundTypesFromRadioButton;
        public ICommand SetBackgroundTypesFromRadioButton {
            get => setBackgroundTypesFromRadioButton ??
                (setBackgroundTypesFromRadioButton = new ParametrizedCommand(parameter => {
                    var str = (string)parameter;
                    switch (str) {
                        case "Number of layers":
                            BackgroundType = BackgroundTypes.DottedLines;
                            break;
                        case "None":
                            BackgroundType = BackgroundTypes.None;
                            break;
                    }
                    RebuildGraph();
                }, parameter => parameter != null));
        }

        public ICommand SetColorSchemeFromRadioButton {
            get => setColorSchemeFromRadioButton ??
                (setColorSchemeFromRadioButton = new ParametrizedCommand(parameter => {
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
                    RebuildGraph();
                }, parameter => parameter != null));
        }

        public ICommand SetLayoutAlgorithmFromRadioButton {
            get => setLayoutAlgorithmFromRadioButton ??
                (setLayoutAlgorithmFromRadioButton = new ParametrizedCommand(parameter => {
                    var str = (string)parameter;
                    switch (str) {
                        case "Optimal":
                            LayoutAlgorithm = LayoutAlgorithmTypes.TheShortestHeight;
                            break;
                        case "Straight Pass":
                            LayoutAlgorithm = LayoutAlgorithmTypes.StraightPass;
                            break;
                        case "Reversed Pass":
                            LayoutAlgorithm = LayoutAlgorithmTypes.ReversePass;
                            break;
                    }
                    RebuildGraph();
                }, parameter => parameter != null));
        }

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
                        RebuildGraph();
                    },
                    parameter => { return parameter == null ? false : true; }));
        }

        public ICommand SetStartColorCommand {
            get => setStartColorCommand ??
                (setStartColorCommand = new RelayCommand(() => {
                    try {
                        var color = dialogService.GetColor();
                        if (color != null) {
                            StartColor = color;
                            RebuildGraph();
                        }
                    } catch (Exception ex) {
                        dialogService.ShowMessage("Set start color error.\n" + ex.Message);
                    }
                }));
        }

        public ICommand SetEndColorCommand {
            get => setEndColorCommand ??
                (setEndColorCommand = new RelayCommand(() => {
                    try {
                        var color = dialogService.GetColor();
                        if (color != null) {
                            EndColor = color;
                            RebuildGraph();
                        }
                    } catch (Exception ex) {
                        dialogService.ShowMessage("Set end color error.\n" + ex.Message);
                    }
                }));
        }

        public ICommand SetDefaultColors {
            get => setDefaultColor ??
                (setDefaultColor = new RelayCommand(() => {
                    try {
                        StartColor = new Color(25, 25, 30);
                        EndColor = new Color(218, 112, 214);
                        RebuildGraph();
                    } catch (Exception ex) {
                        dialogService.ShowMessage("Set default colors error.\n" + ex.Message);
                    }
                }));
        }

        #endregion

        #endregion

        #region Constructor

        public MainWindowViewModel() {
            dialogService = new DefaultDialogService();
            SetDefaultColors.Execute(this);
        }

        #endregion

        #region Methods

        private void ClearData() {
            GraphVM = null;
            FilePath = null;
        }

        bool IsFilePathExists() => FilePath != null;

        bool IsGraphVMExists() => GraphVM != null;

        void RebuildGraph() {
            if (GraphVM == null)
                return;
            buildGraphCommand.Execute(this);
        }

        #endregion

    }
}

