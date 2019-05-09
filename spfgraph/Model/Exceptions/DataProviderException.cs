using System;

namespace spfgraph.Model.Exceptions {

    /// <summary>
    /// Custom exception for DataProvider errors.
    /// </summary>
    public class DataProviderException : Exception {

        #region Default Constructors

        public DataProviderException() { }
        public DataProviderException(string message) : base(message) { }
        public DataProviderException(string message, Exception inner) : base(message, inner) { }
        public DataProviderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        #endregion

    }
}
