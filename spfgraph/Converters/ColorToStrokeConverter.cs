using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace spfgraph.Converters {
    public class ColorToStrokeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var color = (Model.Vizualization.Color)value;
            var brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, color.R, color.G, color.B));
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
