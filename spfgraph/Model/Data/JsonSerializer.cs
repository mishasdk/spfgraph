using spfgraph.Model.Visualization;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;

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
