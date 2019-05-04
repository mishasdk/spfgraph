using spfgraph.Model.Vizualization;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace spfgraph.Converters {
    public class IsCheckedToColorScheme : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var rb = (RadioButton)value;
            var type = rb.Content.ToString();
            switch (type) {
                case ("In Degree"):
                    return ColorSchemeTypes.InDegree;
                case ("Out Degree"):
                    return ColorSchemeTypes.OutDegree;
                case ("Sum Degree"):
                    return ColorSchemeTypes.SumDegree;
                case ("Default"):
                    return ColorSchemeTypes.None;
                default:
                    return ColorSchemeTypes.None;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
