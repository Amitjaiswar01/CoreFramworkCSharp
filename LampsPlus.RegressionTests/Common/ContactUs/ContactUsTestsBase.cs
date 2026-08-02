using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.ContactUs
{
    /// <summary>
    /// Base class for Contact Us specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.ContactUs)]
    public class ContactUsTestsBase : TestsBase
    {
        /// <summary>
        /// Test base for Contact Us tests to check if the tests are against the production database.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ContactUsTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
