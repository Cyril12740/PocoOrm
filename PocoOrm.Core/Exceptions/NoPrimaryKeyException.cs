using System;
using System.Runtime.Serialization;

namespace PocoOrm.Core.Exceptions
{
    public class NoPrimaryKeyException : Exception
    {
        public NoPrimaryKeyException()
        {
        }

        public NoPrimaryKeyException(string message) : base(message)
        {
        }

        public NoPrimaryKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoPrimaryKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}