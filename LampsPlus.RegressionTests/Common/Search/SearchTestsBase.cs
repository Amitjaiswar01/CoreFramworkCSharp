using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;

namespace LampsPlus.RegressionTests.Common.Search
{
    /// <summary>
    /// Base class for Search specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.Search)]
    public class SearchTestsBase : TestsBase
    {
        /// <summary>
        /// Common functionality to for Search tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public SearchTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
