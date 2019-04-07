using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        #region Private Fields

        Window window;
        //int resizeBorder = 6;
        int outerMarginSize = 5;
        int windowRadius = 10;

        #endregion

        #region Public Propeties
        /// <summary>
        /// Size of the the resize border
        /// </summary>
        public int ResizeBorder { get; set; } = 3;
        public Thickness ResizeBorderThickness {
            get => new Thickness(ResizeBorder + OuterMarginSize);
        }

        public int OuterMarginSize {
            get => window.WindowState == WindowState.Maximized ? 0 : outerMarginSize;
            set {
                outerMarginSize = value;
            }
        }
        public Thickness OuterMarginSizeThickness {
            get => new Thickness(OuterMarginSize);
        }

        public int WindowRadius {
            get => window.WindowState == WindowState.Maximized ? 0 : windowRadius;
            set {
                windowRadius = value;
            }
        }
        public CornerRadius WindowCornerRadius {
            get => new CornerRadius(WindowRadius);
        }
    
        #endregion


        #region Constructor

        public MainWindowViewModel(Window window) {
            this.window = window;

            window.StateChanged += (sender, e) => {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(windowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

        }

        #endregion

    }
}
