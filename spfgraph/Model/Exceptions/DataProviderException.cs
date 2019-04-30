﻿using System;

namespace spfgraph.Model.Exceptions {

    public class DataProviderException : Exception {
        public DataProviderException() { }
        public DataProviderException(string message) : base(message) { }
        public DataProviderException(string message, Exception inner) : base(message, inner) { }
        public DataProviderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
