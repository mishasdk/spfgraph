using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace spfgraph.Converters {

    /// <summary>
    /// Converter for data binding. Gets some value and 
    /// transform it to Visibility.
    /// </summary>
    public class NullRefToVisibilityConverter : IValueConverter {

        #region Public Members

        /// <summary>
        /// Value converter.
        /// </summary>
        /// <param name="value">Reference of object</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>Visiblity.Hidden if <cref="value"> is null, instead Visiblity.Visible.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null ? Visibility.Hidden : Visibility.Visible;
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

        #endregion

    }
}
