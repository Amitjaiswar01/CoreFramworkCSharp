using System;
using System.Runtime.Serialization;

namespace LampsPlus.AutomationFramework.Exceptions
{
    public class EnvironmentNotFoundException : Exception
    {
        public EnvironmentNotFoundException() { }

        public EnvironmentNotFoundException(string message) : base(message) { }

        public EnvironmentNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        protected EnvironmentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
