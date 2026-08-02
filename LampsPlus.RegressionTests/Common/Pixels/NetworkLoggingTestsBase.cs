using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Environment;

namespace LampsPlus.RegressionTests.Common.Pixels
{
    /// <summary>
    /// Base class for Network Logging tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    public class NetworkLoggingTestsBase : TestsBase
    {
        /// <summary>
        /// Common functionality to for Pixel tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        /// <param name="config">Test configuration config string.</param>
        public NetworkLoggingTestsBase(ITestOutputHelper output, string config) : base(output)
        {
            var setup = new TestSetup(config) { IsNetworkLoggingTest = true };

            InitializeFramework(config, setup: setup);
        }

        protected int GetVisualProductsCountDesktop()
        {
            var visualProductBreakpointYCoordinate = 838;

            var productCount = Sort.ProductContainersList.Where((product, index) => {
                var location = Sort.DisplayedProductAtIndex(index).Location;
                return location.Y < visualProductBreakpointYCoordinate;
            }).Count();

            Log.Message($"Total count of products is: {productCount}");

            return productCount;
        }
    }
}

