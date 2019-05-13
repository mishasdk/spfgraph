using spfgraph.Model.Data;
using spfgraph.Model.Dialog;
using spfgraph.Model.Exceptions;
using spfgraph.Model.Visualization;
using spfgraph.ViewModel.Base;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
        ICommand setColorSchemeFromRadioButton;
        ICommand setStartColorCommand;
        ICommand setEndColorCommand;
        ICommand setDefaultColor;

        IDialogService dialogService;
        ColorSchemeTypes colorScheme;
        OptimizeVisualizationTypes optimizeLayout;
        string filePath;
        double canvasWidth;
        Color startColor;
        Color endColor;
        GraphViewModel graphVM;

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

        public ICommand ChoosePathForFileCommand {
            get => choosePathForFileCommand ??
                (choosePathForFileCommand = new RelayCommand(() => {
                    try {
                        dialogService.Filter = DefaultDialogService.TextFilter;
                        if (dialogService.OpenFileDialog()) {
                            FilePath = dialogService.FilePath;
                        }
                    } catch (Exception ex) {
                        dialogService.ShowMessage(ex.Message);
                    }
                }));
        }

        public ICommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new ActionCommand(() => {
                    try {
                        GraphVM = new GraphViewModel(filePath, OptimizeLayout, ColorScheme, StartColor, EndColor);
                    } catch (GraphErrorException ex) {
                        dialogService.ShowMessage(ex.Message);
                    } catch (DataProviderException ex) {
                        dialogService.ShowMessage(ex.Message);
                    } catch (Exception ex) {
                        dialogService.ShowMessage(ex.Message);
                    }
                }, IsFilePathExists));
        }

        public ICommand ClearDataCommand {
            get => clearDataCommand ??
                (clearDataCommand = new ActionCommand(() => {
                    ClearData();
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
                        dialogService.Filter = DefaultDialogService.PngFilter;
                        if (!dialogService.SaveFileDialog())
                            return;

                        DataProvider.SaveGraphAsPng(dialogService.FilePath, parameter);
                    },
                    IsGraphVMExists));
        }

        public ICommand OpenHtmlCommand {
            get => openHtmlCommand ??
                (openHtmlCommand = new ActionCommand(() => {
                    try {
                        JsonSerializer.SerializeGraph("Resources\\elementsCollection.json", GraphVM.ElementsToViz);
                        using (var sr = new StreamReader("Resources\\elementsCollection.json")) {
                            using (var fs = new FileStream("Resources\\elementsCollection.js", FileMode.Create)) {
                                using (var sw = new StreamWriter(fs)) {
                                    var str1 = "data = ";
                                    var str2 = sr.ReadLine();
                                    sw.WriteLine(str1 + str2);
                                }
                            }
                        }
                        System.Diagnostics.Process.Start("Resources\\htmlGraph.html");
                    } catch {
                    }

                }, IsGraphVMExists));
        }

        #region Layout Parameters Commands

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
                    var color = dialogService.GetColor();
                    if (color != null) {
                        StartColor = color;
                        RebuildGraph();
                    }
                }));
        }

        public ICommand SetEndColorCommand {
            get => setEndColorCommand ??
                (setEndColorCommand = new RelayCommand(() => {
                    var color = dialogService.GetColor();
                    if (color != null) {
                        EndColor = color;
                        RebuildGraph();
                    }
                }));
        }

        public ICommand SetDefaultColors {
            get => setDefaultColor ??
                (setDefaultColor = new RelayCommand(() => {
                    StartColor = new Color(25, 25, 30);
                    EndColor = new Color(218, 112, 214);
                    RebuildGraph();
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

