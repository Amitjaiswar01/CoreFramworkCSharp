using System;
using System.Runtime.Serialization;

namespace Automation.Framework.Exceptions
{
    public class FrameworkWaitException : Exception
    {
        public FrameworkWaitException()
        {
        }

        public FrameworkWaitException(string message) : base(message)
        {
        }

        public FrameworkWaitException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FrameworkWaitException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
