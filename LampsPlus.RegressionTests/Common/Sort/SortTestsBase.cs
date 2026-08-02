using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.Sort
{
    /// <summary>
    /// Base class for Sort specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.Sort)]
    public class SortTestsBase : TestsBase
    {
        /// <summary>
        /// Common functionality to for Sort tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public SortTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
