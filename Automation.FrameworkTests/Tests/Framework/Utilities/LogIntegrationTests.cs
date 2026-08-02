using System.IO;
using Xunit;
using Xunit.Abstractions;

using Automation.FrameworkTests.Utilities;

namespace Automation.FrameworkTests.Tests.Framework.Utilities
{
    /// <summary>
    /// Integration tests to prove the Log class is working as expected.
    /// </summary>
    [Trait(Traits.Category, Traits.Unit), Trait(Traits.Feature, "Log")]
    public class LogIntegrationTests : BrowserBase
    {
        /// <inheritdoc />
        public LogIntegrationTests(ITestOutputHelper output) : base(output, "LogIntegrationTests") { }

        /// <summary>
        /// Check that a using Log.Message outputs to the console.
        /// </summary>
        [SkippableFact]
        public void AssertMessagesAreLoggedInTheConsoleTest()
        {
            var testMessage = "1%bKL' DEFa u"; // Random message to ensure the message is logged.

            Log.Message(testMessage);

            Assert.Contains(testMessage, Log.Output);
        }
    }


    /// <summary>
    /// Integration tests to prove the Log class is working as expected.
    /// </summary>
    [Trait(Traits.Category, Traits.Integration), Trait(Traits.Feature, "Log")]
    public class RealTimeLogIntegrationTests : BrowserBase
    {
        private const string _testName = "RealTimeLogIntegrationTests";
        /// <inheritdoc />
        public RealTimeLogIntegrationTests(ITestOutputHelper output) : base(output, _testName, true) { }

        /// <summary>
        /// Check that a using Log.Message outputs to the console.
        /// </summary>
        [SkippableFact]
        public void AssertLogsAreWrittenInRealTimeTest()
        {
            Browser.Log.ClearLogs();

            Assert.False(File.Exists(Log.TestLogPath), $"File: {Log.TestLogPath} found but not expected");

            var testMessage = "1%bKL' DEFa u"; // Random message to ensure the message is logged.

            Log.Message(testMessage);

            Assert.True(File.Exists(Log.TestLogPath), $"File: {Log.TestLogPath} found but not expected");
        }
    }
}
