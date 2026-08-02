using Automation.FrameworkTests.Tests.Framework.Utilities;
using Automation.FrameworkTests.Utilities;

using Xunit;
using Xunit.Abstractions;

namespace Automation.FrameworkTests.Tests
{
    /// <summary>
    /// Integration tests to prove the Browser class is working as expected.
    /// </summary>
    [Trait(Traits.Unit, "Browser")]
    public class BrowserTests : BrowserBase
    {
        /// <inheritdoc />
        public BrowserTests(ITestOutputHelper output) : base(output, "BrowserTests", false, false) { }

        /// <summary>
        /// Verify the browser and driver are not disposed when the test completes.
        /// NOTE: The browser and driver should be closed manually.
        /// </summary>
        [Trait(Traits.Category, Traits.Unit)]
        [SkippableFact]
        public void VerifyBrowserAndDriverAreNotClosedAfterTest()
        {
            // This test is configured in the test class object construction.
        }
    }
}
