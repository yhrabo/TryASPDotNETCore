using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Purchase.Core.App
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
