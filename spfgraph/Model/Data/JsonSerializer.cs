using spfgraph.Model.Vizualization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace spfgraph.Model.Data {
    public static class JsonSerializer {

        public static void Serialize(string filePath, ObservableCollection<Element> collection) {
            var jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Element>), new Type[] { typeof(Element), typeof(Node), typeof(Edge), typeof(Color) });
            using (var fs = new FileStream(filePath, FileMode.Create)) {
                jsonFormatter.WriteObject(fs, collection);
            }
        }
    }
}
