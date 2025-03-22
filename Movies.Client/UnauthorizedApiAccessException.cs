using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Client
{
    [Serializable]
    public class UnauthorizedApiAccessException : Exception
    {
        public UnauthorizedApiAccessException()
        {
        }

        public UnauthorizedApiAccessException(string? message) : base(message)
        {
        }

        public UnauthorizedApiAccessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedApiAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}