using Model;
using QuickGraph;
using System;
using View;
using System.Windows;
using GraphSharp.Controls;
using System.Windows.Controls;

namespace ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        RelayCommand openCommand;
        RelayCommand buildGraphCommand;
        RelayCommand clearAllCurrentData;

        DrawingTool drawingTool;
        Window window;
        IDialogService dialogService;
        string filePath;
        IBidirectionalGraph<object, IEdge<object>> graphToShow;

        #endregion

        #region Public Propeties

        public IBidirectionalGraph<object, IEdge<object>> GraphToShow {
            get => graphToShow;
            set {
                graphToShow = value;
                OnPropertyChanged(nameof(GraphToShow));
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
        RelayCommand drawPicture;
        public RelayCommand DrawPicture {
            get => drawPicture ??
                (drawPicture = new RelayCommand(() => {
                    var graph = new Graph(DataProvider.CreateAdjacencyListFromFile(FilePath));
                    drawingTool.DrawGraph(graph);
                }));
        }

        public RelayCommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {
                    GraphToShow = null;
                    if (FilePath == "" || FilePath == null)
                        return;
                    CreateGraphForShow();
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

        public RelayCommand ClearAllCurrentDataCommand {
            get => clearAllCurrentData ??
                (clearAllCurrentData = new RelayCommand(() => {
                    if (GraphToShow == null)
                        return;
                    var dialogResult = dialogService.AlertDialog("All unsaved data will be deleted. ");
                    if (dialogResult == MessageBoxResult.OK) {
                        ClearData();
                    }
                }));
        }

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window, Canvas canvas) {
            this.window = window;
            dialogService = new DefaultDialogService();


            drawingTool = new DrawingTool(canvas);
        }

        #endregion

        #region Methods

        private void ClearData() {
            FilePath = null;
            GraphToShow = null;
        }

        void CreateGraphForShow() {
            try {
                var g = GraphReader.ReadGraphFromFile(FilePath);
                var builder = new GraphBuilder(g);
                GraphToShow = builder.CeateBidirectionalGraphToViz();
            } catch (DataProviderException ex) {
                dialogService.ShowMessage(ex.Message);
            }
        }

        #endregion

    }
}

