using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T7959_T7960_VerifyTheRemoveItemFunctionalityMessageOnTheCartPage
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7960_iPhone_VerifyTheRemoveItemFunctionalityMessageOnTheCartPage : T7960_MobileBase
    {
        public T7960_iPhone_VerifyTheRemoveItemFunctionalityMessageOnTheCartPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "Bug - LP-62923")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void RemoveItemFunctionalityMessage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7960_Emulator_VerifyTheRemoveItemFunctionalityMessageOnTheCartPage : T7960_MobileBase
    {
        public T7960_Emulator_VerifyTheRemoveItemFunctionalityMessageOnTheCartPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void RemoveItemFunctionalityMessage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7960_AndroidPhone_VerifyTheRemoveItemFunctionalityMessageOnTheCartPage : T7960_MobileBase
    {
        public T7960_AndroidPhone_VerifyTheRemoveItemFunctionalityMessageOnTheCartPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void RemoveItemFunctionalityMessage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the Remove Item Functionality Message on the Cart Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10849
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7960
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10849"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-7960")]
    public abstract class T7960_MobileBase : TestsBaseMobile
    {
        protected T7960_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange: Navigate to homepage and get two random skus 
            InitializeFunctionalTest(config);
            var randomSku1 = ProductActions.GetAnySkuWithProductDetailPage;
            var randomSku2 = ProductActions.GetAnySkuWithProductDetailPage;

            // Act : Add both the skus to the cart 
            ShoppingCartWorkflow.AddMultipleItemsToCart(String.Empty, 2, new List<String> { randomSku1, randomSku2 });
            Assert.True(Cart.IsCurrentPage, "Current page is not cart page");

            // Act : Remove first item from the cart
            var cartProductName = Cart.GetProductNameOnCart();
            var completeUndoMessage = "\"" + cartProductName + "\"" + "has been removed from your cart";
            Cart.RemoveSingleItemFromCart();
            var completeCartUndoMessage = Cart.UndoMessageProductName();

            //Assert : Remove message is displayed on the cart page
            Assert.Equals(completeUndoMessage, completeCartUndoMessage, "Remove item message is not displayed");

            // Act : Wait for undo link to be disappear on Cart page
            Cart.WaitForUndoLinkToDisappear();

            // Assert: Remove message is not displayed on the cart page
            Assert.True(Cart.AfterUndoMessageToDisappear(), "Remove item message is displayed");
        }
    }
}