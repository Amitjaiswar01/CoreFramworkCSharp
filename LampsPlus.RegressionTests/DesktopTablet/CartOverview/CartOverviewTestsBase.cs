using System.Collections.Generic;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview
{
    /// <summary>
    /// Base class for Shopping Cart specific tests.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Regression)]
    [Trait(LpTraits.Keys.Feature, LpTraits.RegressionFeatureTags.CartOverview)]
    public class ShoppingCartTestsBase : TestsBase
    {
        /// <summary>
        /// Common functionality to for CartOverview tests.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public ShoppingCartTestsBase(ITestOutputHelper output) : base(output) { }

        /// <summary>
        /// Verify the given product lists are equal.
        /// </summary>
        /// <param name="list1">Expected list of products to compare.</param>
        /// <param name="list2">Actual list of products to compare.</param>
        public void VerifyListsAreEqual(List<ProductModel> list1, List<ProductModel> list2)
        {
            var areSame = true;

            for (var index = 0; index < list1.Count; index++)
            {
                areSame &= list1[index].Name == list2[index].Name &&
                           list1[index].Sku == list2[index].Sku &&
                           list1[index].Quantity == list2[index].Quantity &&
                           list1[index].Price == list2[index].Price &&
                           list1[index].Total == list2[index].Total;
            }

            Assert.True(areSame, "The provided lists are not the same.");
        }
    }
}
