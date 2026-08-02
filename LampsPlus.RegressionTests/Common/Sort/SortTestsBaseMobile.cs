using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;

namespace LampsPlus.RegressionTests.Common.Sort
{
    /// <summary>
    /// Base class for Sort specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.Sort)]
    public class SortTestsBaseMobile : TestsBaseMobile
    {
        /// <summary>
        /// Common functionality to for Sort tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public SortTestsBaseMobile(ITestOutputHelper output) : base(output) { }
    }
}
