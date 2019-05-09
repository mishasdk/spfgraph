using System;

namespace spfgraph.Model.Exceptions {

    /// <summary>
    /// Custom exception for algorithms errors.
    /// </summary>
    public class AlgorithmException : Exception {

        #region Default Contructors

        public AlgorithmException() { }
        public AlgorithmException(string message) : base(message) { }
        public AlgorithmException(string message, Exception inner) : base(message, inner) { }
        protected AlgorithmException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        #endregion

    }
}
