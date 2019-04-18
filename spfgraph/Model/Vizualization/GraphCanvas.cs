using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Model {
    /// <summary>
    /// Class that represent spfgraph in canvas
    /// </summary>
    public class GraphCanvas : Canvas {
        public GraphCanvas() {

        }

        public BiderectionalGraph Graph {
            get => (BiderectionalGraph)GetValue(GraphProperty);
            set {
                SetValue(GraphProperty, value);
            }
        }

        public static readonly DependencyProperty GraphProperty =
            DependencyProperty.Register("GraphToShow", typeof(BiderectionalGraph), typeof(GraphCanvas), new PropertyMetadata(null));
    }

}

