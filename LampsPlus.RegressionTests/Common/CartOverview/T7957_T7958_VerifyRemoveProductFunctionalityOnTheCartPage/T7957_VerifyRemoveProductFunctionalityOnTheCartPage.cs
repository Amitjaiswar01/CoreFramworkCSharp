using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T7957_T7958_VerifyRemoveProductFunctionalityOnTheCartPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7957_Windows_VerifyTheRemoveItemFunctionalityOnTheCartPage : T7957_DesktopBase
    {
        public T7957_Windows_VerifyTheRemoveItemFunctionalityOnTheCartPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void RemoveItemFunctionality(string config) => Validate(config);
    }

    /// <summary>
    /// Verify the Remove Item Functionality on the Cart Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10697
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7957
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10697"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-7957")]
    public abstract class T7957_DesktopBase : TestsBaseDesktop
    {
        protected T7957_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange: Navigate to homepage and get two random skus 
            InitializeFunctionalTest(config);
            var randomSku1 = ProductActions.GetAnySkuWithProductDetailPage;
            var randomSku2 = ProductActions.GetAnySkuWithProductDetailPage;

            // Act : Add both the skus to the cart 
            ShoppingCartWorkflow.AddMultipleItemsToCart(String.Empty, 2, new List<String> { randomSku1, randomSku2 });
            Assert.True(Cart.IsCurrentPage, "Current page is not cart page");

            // Act : Get the first sku number and and Order total before product removal
            var productSkuListBeforeProductRemoval = Cart.GetListOfAllProductsOnCartPage();
            var firstProductSkuInCart = productSkuListBeforeProductRemoval[0].Sku;
            var secondSkuInCart = productSkuListBeforeProductRemoval[1].Sku;
            var orderTotalBeforeProductRemoval = Cart.GetOrderTotalCost();

            // Act : Remove first item from the cart
            Cart.RemoveSingleItemFromCart();

            // Act : Wait for undo link to be not displayed on Cart page
            Cart.WaitForUndoLinkToDisappear();

            // Act : Get the sku number of remaining sku and Order total after product removal
            var productSkuListAfterProductRemoval = Cart.GetListOfAllProductsOnCartPage();
            var remainingProductSkuInCart = productSkuListAfterProductRemoval[0].Sku;
            var orderTotalAfterProductRemoval = Cart.GetOrderTotalCost();

            // Assert : Verify if the correct product removed from cart and Product total is changed
            Assert.Condition(() => (secondSkuInCart == remainingProductSkuInCart) && (firstProductSkuInCart != remainingProductSkuInCart)," Incorrect product is removed from Cart.");
            Assert.Condition(() => orderTotalBeforeProductRemoval > orderTotalAfterProductRemoval, "The product total is same ");
        }
    }
}