using System.Runtime.Serialization;

namespace spfgraph.Model.Vizualization {
    [DataContract]
    public class Color {
        [DataMember]
        public byte R { get; set; }

        [DataMember]
        public byte G { get; set; }

        [DataMember]
        public byte B { get; set; }

        public Color(byte r, byte g, byte b) {
            R = r;
            G = g;
            B = b;
        }

    }
}
