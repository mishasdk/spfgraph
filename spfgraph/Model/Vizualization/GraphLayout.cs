using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    /// <summary>
    /// Class that represent spfgraph in window
    /// </summary>
    public class GraphLayout {

        public List<Layout> Layouts { get; set; }

        public GraphLayout() {
            Layouts = new List<Layout>();
        }

        public void AddLayout(Layout layout) {
            Layouts.Add(layout);
        }

        public void AddLayoutRange(IEnumerable<Layout> layout) {
            Layouts.AddRange(layout);
        }

        public void DrawGraph() {
            throw new NotImplementedException();
        }
    }
}
