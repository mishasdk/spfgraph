using spfgraph.ViewModel.Base;
using spfgraph.Model.GraphLib;

namespace spfgraph.ViewModel {
    public class GraphFeaturesViewModel : BaseViewModel {
        int heigth;
        public string Heigth {
            get => heigth.ToString();
            set {
                heigth = int.Parse(value);
                OnPropertyChanged(nameof(Heigth));
            }
        }

        //public int Width {
        //    get => features.Width;
        //}

        //public double AvrgWidth {
        //    get => features.AvrgWidth;
        //}

        public GraphFeaturesViewModel(GraphFeatures features) {
            Heigth = features.Height.ToString();
        }

    }
}
