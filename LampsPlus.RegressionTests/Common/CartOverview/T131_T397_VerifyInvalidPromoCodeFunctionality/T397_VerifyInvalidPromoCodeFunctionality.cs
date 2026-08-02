using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T131_T397_VerifyInvalidPromoCodeFunctionality
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T397_iPhone_VerifyInvalidPromoCodeFunctionality : T397_MobileBase
    {
        public T397_iPhone_VerifyInvalidPromoCodeFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyInvalidPromoFunctionalityOnCart(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T397_Emulator_VerifyInvalidPromoCodeFunctionality : T397_MobileBase
    {
        public T397_Emulator_VerifyInvalidPromoCodeFunctionality(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyInvalidPromoFunctionalityOnCart(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that when the user tries to apply an invalid promo code the functionality is correct.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9908
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T397
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9908"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T397")]
    public abstract class T397_MobileBase : TestsBaseMobile
    {
        protected T397_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange : User add the item to the cart.
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetFinialSkuWithMultipleShippingOptions();
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });
            Assert.True(Cart.IsCurrentPage, "Current page is not cart page");

            // Act : Apply invalid promo code  
            var invalidPromoCode = Cart.GetInvalidPromoCodeValue();
            Cart.OpenPromoCodeEntryField();
            Cart.UpdatePromoCode(invalidPromoCode);

            // Assert : Verify the invalid promo code error message
            Assert.Equals(Messages.PromoRelatedMessages.InvalidPromoCodeMessage, Cart.GetInvalidPromoCodeErrorMessage(), "Promo code error message is not correct.");

            // Act : Navigate to shipping page to check if the promo code applied or not
            Cart.CheckOutFromCartPage();
            Assert.True(Shipping.IsCurrentPage, "Current page is not shipping page");
            Shipping.OpenOrderSummaryBlock();

            // Assert : Verify the promo code is not applied on the order summary Block 
            Assert.False(Shipping.GetPromoCodeElementOnOrderSummary().IsInitialized, "Promo code is displayed on Shipping page order summary");
        }
    }
}