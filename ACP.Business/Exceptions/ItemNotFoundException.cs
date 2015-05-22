using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business
{
    [Serializable] 
    public class ItemNotFoundException : Exception
    { 
        public ItemNotFoundException()
            : base() { }

        public ItemNotFoundException(string message)
            : base(message) { }

        public ItemNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ItemNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public ItemNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ItemNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
