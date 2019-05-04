using spfgraph.Model.Dialog;
using spfgraph.Model.Exceptions;
using spfgraph.Model.Vizualization;
using spfgraph.ViewModel.Base;
using System;
using System.Windows;
using System.Windows.Input;

namespace spfgraph.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Private Fields

        ICommand openCommand;
        ICommand buildGraphCommand;
        ICommand clearDataCommand;

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
                        }},
                    parameter => { return parameter == null ? false : true; }));
        }



        ICommand setColorSchemeFromRadioButton;
        public ICommand SetColorSchemeFromRadioButton {
            get => setColorSchemeFromRadioButton ??
                (setColorSchemeFromRadioButton = new ParametrizedCommand(ExecuteMethod, CanExecuteMethod));
        }

        bool CanExecuteMethod(object parameter)
        {
            return parameter == null ? false : true;
        }

        void ExecuteMethod(object parameter)
        {
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
                        GraphVM = new GraphViewModel(filePath, OptimizeLayout, ColorScheme);
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

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window)
        {
            dialogService = new DefaultDialogService();
            this.window = window;
        }

        #endregion

        #region Methods

        private void ClearData()
        {
            GraphVM = null;
        }

        #endregion

    }
}

