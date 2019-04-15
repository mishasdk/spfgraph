using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Model {
    public class Node : RoundButton {
        public int Value { get; set; }
        public Node(int value) {
            Content = value.ToString();
        }

        public Node() { }

    }
}
