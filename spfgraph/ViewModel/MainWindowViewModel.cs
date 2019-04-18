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
        //RelayCommand clearAllCurrentData;

        Window window;
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
        RelayCommand drawPicture;
        public RelayCommand DrawPicture {
            get => drawPicture ??
                (drawPicture = new RelayCommand(() => {


                }));
        }

        public RelayCommand BuildGraphCommand {
            get => buildGraphCommand ??
                (buildGraphCommand = new RelayCommand(() => {

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

        #endregion

        #region Constructor

        public MainWindowViewModel(Window window) {
            this.window = window;
            graphToViz = new ObservableCollection<Element>();
            dialogService = new DefaultDialogService();
            CreateGraphForShow();
        }

        #endregion

        #region Methods

        private void ClearData() {
            FilePath = null;

        }

        void CreateGraphForShow() {
            var vertices = new Node[] {
                new Node(10, 100, 1),
                new Node(60, 110, 2),
                new Node(110, 120, 3)
            };
            var edges = new Edge[] {
                new Edge(vertices[0], vertices[1]),
                new Edge(vertices[1], vertices[2])
             };

            foreach (var i in edges)
                GraphToViz.Add(i);
            foreach (var i in vertices)
                GraphToViz.Add(i);
        }

        #endregion

    }
}

