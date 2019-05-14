namespace spfgraph.Model.Visualization {
    /// <summary>
    /// Class, that encapsulates dotted line's
    /// data.
    /// </summary>
    public class DottedLine : Element {

        #region Public Properties

        public int X1 { get; private set; }
        public int Y1 { get; private set; }
        public int X2 { get; private set; }
        public int Y2 { get; private set; }
        public int Value { get; private set; }
        public int ShiftY { get; private set; }
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
