using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spfgraph.Model.Exceptions {
    public class AlgorithmException : Exception {
        public AlgorithmException() { }
        public AlgorithmException(string message) : base(message) { }
        public AlgorithmException(string message, Exception inner) : base(message, inner) { }
        protected AlgorithmException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
