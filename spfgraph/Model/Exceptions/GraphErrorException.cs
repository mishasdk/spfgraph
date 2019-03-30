using System;

namespace Model.Excepitons {

    public class GraphErrorException : Exception {
        public GraphErrorException() { }
        public GraphErrorException(string message) : base(message) { }
        public GraphErrorException(string message, Exception inner) : base(message, inner) { }
        protected GraphErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

