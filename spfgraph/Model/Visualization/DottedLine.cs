using System.Runtime.Serialization;

namespace spfgraph.Model.Visualization {
    /// <summary>
    /// Class, that encapsulates dotted line's
    /// data.
    /// </summary>
    [DataContract]
    public class DottedLine : Element {

        #region Public Properties
        [DataMember]
        public int X1 { get; private set; }
        [DataMember]
        public int Y1 { get; private set; }
        [DataMember]
        public int X2 { get; private set; }
        [DataMember]
        public int Y2 { get; private set; }
        [DataMember]
        public int Value { get; private set; }
        [DataMember]
        public int ShiftY { get; private set; }
        [DataMember]
        public int ShiftX { get; private set; }

        #endregion

        #region Constructor

        public DottedLine(int x1, int y1, int x2, int y2, int value) {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Value = value;
            ShiftY = Y1 + 11;
            ShiftX = X1 - 20;
        }

        #endregion

    }
}
