using System;
using System.Runtime.Serialization;

namespace Purchase.Core.ApplicationServices
{
    public class ApplicationServiceException : Exception
    {
        public ApplicationServiceException()
        {
        }

        public ApplicationServiceException(string message) : base(message)
        {
        }

        public ApplicationServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApplicationServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
