using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderLib {

    public class DataProviderException : Exception {
        public DataProviderException() { }
        public DataProviderException(string message) : base(message) { }
        public DataProviderException(string message, Exception inner) : base(message, inner) { }
        protected DataProviderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
