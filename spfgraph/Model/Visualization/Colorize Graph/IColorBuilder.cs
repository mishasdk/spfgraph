namespace spfgraph.Model.Visualization {

    /// <summary>
    /// Interface for color builder, which 
    /// colorizes garph's nodes.
    /// </summary>
    public interface IColorBuilder {
        void SetNodeColor(Node node);
    }
}
