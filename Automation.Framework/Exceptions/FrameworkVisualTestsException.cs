using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Exceptions
{
    public class FrameworkVisualTestsException : Exception
    {
        public FrameworkVisualTestsException()
        {
        }

        public FrameworkVisualTestsException(string message) : base(message)
        {
        }

        public FrameworkVisualTestsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FrameworkVisualTestsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
