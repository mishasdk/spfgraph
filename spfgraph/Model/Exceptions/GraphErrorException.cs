using System;

namespace spfgraph.Model.Exceptions {

    /// <summary>
    /// Custom exception for graph errors.
    /// </summary>
    public class GraphErrorException : Exception {

        #region Custom Exceptions

        public GraphErrorException() { }
        public GraphErrorException(string message) : base(message) { }
        public GraphErrorException(string message, Exception inner) : base(message, inner) { }
        protected GraphErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        #endregion

    }
}

