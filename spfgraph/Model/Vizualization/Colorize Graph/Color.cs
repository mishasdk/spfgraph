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
        public Color() { }

        public static Color operator +(Color a, Color b) => new Color((byte)(a.R + b.R), (byte)(a.G + b.G), (byte)(a.B + b.B));
        public static Color operator -(Color a, Color b) => new Color((byte)(a.R - b.R), (byte)(a.G - b.G), (byte)(a.B - b.B));
        public static Color operator /(Color a, double b) => new Color((byte)(a.R / b), (byte)(a.G / b), (byte)(a.B / b));
        public static Color operator *(Color a, double b) => new Color((byte)(a.R * b), (byte)(a.G * b), (byte)(a.B * b));

    }
}
