using System;
using System.Runtime.Serialization;

namespace PocoOrm.Core.Exceptions
{
    public class InterseptException : Exception
    {
        public InterseptException(Exception innerException) : base("See  the inner exception", innerException)
        {
        }

        protected InterseptException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}