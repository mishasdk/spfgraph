using System.Runtime.Serialization;

namespace spfgraph.Model.Visualization {

    /// <summary>
    /// Class Color.
    /// </summary>
    [DataContract]
    public class Color {

        #region Public Properties

        [DataMember]
        public byte R { get; set; }

        [DataMember]
        public byte G { get; set; }

        [DataMember]
        public byte B { get; set; }

        #endregion

        #region Constructors

        public Color(byte r, byte g, byte b) {
            R = r;
            G = g;
            B = b;
        }
        public Color() { }

        #endregion

        #region Implementation Operations

        /// <summary>
        /// Some basic operations with colors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color operator +(Color a, Color b) => new Color((byte)(a.R + b.R), (byte)(a.G + b.G), (byte)(a.B + b.B));
        public static Color operator -(Color a, Color b) => new Color((byte)(a.R - b.R), (byte)(a.G - b.G), (byte)(a.B - b.B));
        public static Color operator /(Color a, double b) => new Color((byte)(a.R / b), (byte)(a.G / b), (byte)(a.B / b));
        public static Color operator *(Color a, double b) => new Color((byte)(a.R * b), (byte)(a.G * b), (byte)(a.B * b));

        #endregion

    }
}
