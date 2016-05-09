using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quality.WebUI.Exceptions
{
    [global::System.Serializable]
    public class ScopeNotSelectedException : Exception
    {
        public ScopeNotSelectedException() { }
        public ScopeNotSelectedException(string message) : base(message) { }
        public ScopeNotSelectedException(string message, Exception inner) : base(message, inner) { }

        protected ScopeNotSelectedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
