using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace spfgraph.Converters {
    public class ColorToLighterConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var b = ((SolidColorBrush)value).Color;
            return new SolidColorBrush(Color.FromRgb((byte)(b.R + (255 - b.R) / 2), (byte)(b.G + (255 - b.G) / 2), (byte)(b.B + (255 - b.B) / 2)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
