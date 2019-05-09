using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace spfgraph.Converters {

    /// <summary>
    /// Converter for data binding. Gets Model.Visualization.Color object
    /// and returns System.Windows.Media.SolidColorBrush object.
    /// </summary>
    public class ColorToSolidBrushConverter : IValueConverter {

        /// <summary>
        /// Value converter.
        /// </summary>
        /// <param name="value">Color object.</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>SolidColorBrush from <cref="value"></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                var color = (Model.Visualization.Color)value;
                var brush = new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B));
                return brush;

            } catch {
                return new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>NotImplementedException.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

    }
}
