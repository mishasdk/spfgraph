using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace spfgraph.Converters {
    public class ColorToSolidBrushConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                var color = (Model.Visualization.Color)value;
                var brush = new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B));
                return brush;

            } catch {
                return new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
