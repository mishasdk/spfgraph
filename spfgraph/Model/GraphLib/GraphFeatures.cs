using System;

namespace spfgraph.Model.GraphLib {
    public class GraphFeatures {
        public int Height { get; set; }
        public int Width { get; set; }
        public double AvrgWidth { get; set; }
        public double Irregular { get; set; }
        public double AvrgDeviation { get; set; }

        public GraphFeatures() { }

        #region Overrided Methods

        public override bool Equals(object obj) {
            var feature = (GraphFeatures)obj;
            if (feature.Height == Height && feature.Width == Width && Math.Abs(AvrgWidth - feature.AvrgWidth) <= 0.0001 && Irregular == feature.Irregular)
                return true;
            else
                return false;
        }

        public override string ToString() {
            return $"Height: {Height}, Width: {Width}, AvrgWidth: {AvrgWidth}, Irregular: {Irregular}";
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion

    }
}
