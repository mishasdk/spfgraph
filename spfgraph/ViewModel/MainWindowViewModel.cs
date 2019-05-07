using spfgraph.Model.Data;
using spfgraph.Model.Dialog;
using spfgraph.Model.Exceptions;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Input;

namespace spfgraph.ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        ICommand choosePathForFileCommand;
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

        public ICommand ChoosePathForFileCommand {
            get => choosePathForFileCommand ??
                (choosePathForFileCommand = new RelayCommand(() => {
                    try {
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
                }, IsFilePathExist));
        }

        public ICommand ClearDataCommand {
            get => clearDataCommand ??
                (clearDataCommand = new ActionCommand(() => {
                    ClearData();
                }, IsFilePathExist));
        }

        ICommand exportToJsonCommand;
        public ICommand ExportToJsonCommand {
            get => exportToJsonCommand ??
                (exportToJsonCommand = new ActionCommand(() => {
                    try {
                        var jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Element>), new Type[] { typeof(Element), typeof(Node), typeof(Edge), typeof(Color) });
                        using (var fs = new FileStream("elementsCollection.json", FileMode.Create)) {
                            jsonFormatter.WriteObject(fs, GraphVM.ElementsToViz);
                        }
                    } catch {

                    }

                }, IsGraphVMExist));
        }

        ICommand saveDagInFile;
        public ICommand SaveDagInFile {
            get => saveDagInFile ??
                (saveDagInFile = new ActionCommand(() => {
                    try {
                        var saveDialog = new DefaultDialogService();
                        if (!saveDialog.SaveFileDialog())
                            return;

                        var filePath = saveDialog.TargetPath;
                        DataProvider.SaveDagInFile(filePath, GraphVM.DagGraph);
                    } catch {

                    }
                }, IsGraphVMExist));
        }


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
                }, parameter => parameter != null));
        }

        #region Color Commands

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

        #endregion

        ICommand openHtmlCommand;
        public ICommand OpenHtmlCommand {
            get => openHtmlCommand ??
                (openHtmlCommand = new ActionCommand(() => {
                    try {
                        System.Diagnostics.Process.Start("demo.html");
                    } catch {
                    }

                }, IsGraphVMExist));
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
            FilePath = null;
        }

        bool IsFilePathExist() => FilePath != null;

        private bool IsGraphVMExist() => GraphVM != null;

        #endregion

    }
}

