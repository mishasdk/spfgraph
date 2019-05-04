namespace spfgraph.Model.Vizualization {
    public class DefaultColorBuilder : IColorBuilder {
        Color defaultColor;

        public DefaultColorBuilder() {
            defaultColor = new Color(0, 0, 0);
        }

        public virtual void SetNodeColor(Node node) {
            node.NodeColor = defaultColor;
        }
    }
}