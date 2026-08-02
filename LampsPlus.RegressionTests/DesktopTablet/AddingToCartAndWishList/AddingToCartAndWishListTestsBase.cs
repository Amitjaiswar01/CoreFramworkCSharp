using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.AddingToCartAndWishList
{
    /// <summary>
    /// Base class for Adding to Cart and WishList specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.AddingToCartAndWishlist)]
    // ReSharper disable once InheritdocConsiderUsage
    public class AddingToCartAndWishListTestsBase : TestsBase
    {
        /// <summary>
        /// Test base for AddToCartAndWishList.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public AddingToCartAndWishListTestsBase(ITestOutputHelper output) : base(output) { }
    }
}
