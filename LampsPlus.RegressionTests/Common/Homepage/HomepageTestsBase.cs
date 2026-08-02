using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;

using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.Homepage
{
    /// <summary>
    /// Base class for Homepage specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.Homepage)]
    public class HomepageTestsBase : TestsBase
    {
        /// <summary>
        /// Base class for Homepage specific tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public HomepageTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
