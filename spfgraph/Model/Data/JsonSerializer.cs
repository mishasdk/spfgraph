using spfgraph.Model.Visualization;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;

namespace spfgraph.Model.Data {
    
    /// <summary>
    /// Class implements Json serialization
    /// </summary>
    public static class JsonSerializer {

        #region Public Members

        /// <summary>
        /// Json serialization of <cref="collection"> in to the json file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="collection"></param>
        public static void SerializeGraph(string filePath, ObservableCollection<Element> collection) {
            var jsonFormatter = new DataContractJsonSerializer(typeof(ObservableCollection<Element>), new Type[] { typeof(Element), typeof(Node), typeof(Edge), typeof(Color) });
            using (var fs = new FileStream(filePath, FileMode.Create)) {
                jsonFormatter.WriteObject(fs, collection);
            }
        }

        #endregion

    }
}
