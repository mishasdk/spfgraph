using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderLib {

    public class GraphErrorException : Exception {
        public GraphErrorException() { }
        public GraphErrorException(string message) : base(message) { }
        public GraphErrorException(string message, Exception inner) : base(message, inner) { }
        protected GraphErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

