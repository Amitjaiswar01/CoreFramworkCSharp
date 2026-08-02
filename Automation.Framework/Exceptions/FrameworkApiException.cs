using System;
using System.Runtime.Serialization;

namespace Automation.Framework.Exceptions
{
    public class FrameworkApiException : Exception
    {
        public FrameworkApiException()
        {
        }

        public FrameworkApiException(string message) : base(message)
        {
        }

        public FrameworkApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FrameworkApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
