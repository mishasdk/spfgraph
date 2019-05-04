using spfgraph.Model.GraphLib;
using spfgraph.Model.Vizualization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace spfgraph.Converters {
    public class IsCheckedToLayoutType : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var rb = (RadioButton)value;
            var type = rb.Content.ToString();
            switch (type) {
                case "Minimize":
                    return OptimizeVisualizationTypes.MinimizeCrosses;
                case "Default":
                    return OptimizeVisualizationTypes.None;
                default:
                    return OptimizeVisualizationTypes.None;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
