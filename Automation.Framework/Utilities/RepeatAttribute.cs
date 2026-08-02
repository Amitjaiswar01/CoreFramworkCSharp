using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Xunit.Sdk;

namespace Automation.Framework.Utilities
{
    /// <summary> 
    /// Custom test attribute for running tests multiple times.
    /// Code found: https://stackoverflow.com/questions/31873778/xunit-test-fact-multiple-times
    /// </summary> 
    public class RepeatAttribute : DataAttribute
    {
        private readonly int _count;

        /// <summary>
        /// Custom test attribute for running tests multiple times.
        /// </summary>
        /// <param name="count">Number of times to execute the test.</param>
        public RepeatAttribute(int count)
        {
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Repeat count must be greater than 0.");
            }

            _count = count;
        }

        /// <summary> 
        /// Execute a test a specified number of times. 
        /// </summary> 
        /// <param name="testMethod">Method to execute.</param> 
        /// <returns></returns> 
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            foreach (var iterationNumber in Enumerable.Range(start: 1, count: this._count))
            {
                yield return new object[] { iterationNumber };
            }
        }
    }
}
