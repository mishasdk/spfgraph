using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spfgraph.Model.Vizualization {
    public interface IColorBuilder {
        void SetNodeColor(Node node);
    }
}
