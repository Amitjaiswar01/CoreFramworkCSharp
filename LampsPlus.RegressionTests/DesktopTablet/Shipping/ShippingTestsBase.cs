using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.Shipping
{
    /// <summary>
    /// Base class for Shipping Info specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.Shipping)]
    public class ShippingInfoTestsBase : TestsBase
    {
        /// <summary>
        /// Common functionality to for Shipping Info tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ShippingInfoTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
