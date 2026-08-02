using System;
using System.Runtime.Serialization;

namespace Automation.Framework.Exceptions
{
    public class FrameworkCookieException : Exception
    {
        public FrameworkCookieException()
        {
        }

        public FrameworkCookieException(string message) : base(message)
        {
        }

        public FrameworkCookieException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FrameworkCookieException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
