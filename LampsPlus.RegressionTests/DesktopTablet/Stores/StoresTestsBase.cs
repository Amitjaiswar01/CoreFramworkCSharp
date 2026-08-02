using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.Stores
{
    /// <summary>
    /// Base class for Stores specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.Stores)]
    public class StoresTestsBase : TestsBase
    {
        /// <summary>
        /// Common functionality to for Stores tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public StoresTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
