using System.Linq;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Constants.CityStateCountry;
using LampsPlus.AutomationFramework.Pages.Refactored.Address;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.OrderConfirmation.T557_T568_VerifyElementsOfConfirmationPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T568_iPhone_VerifyElementsOfConfirmationPage : T568_MobileBase
    {
        public T568_iPhone_VerifyElementsOfConfirmationPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T568. Rework - ACD-10910")]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VariousElementsOfConfirmationPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OrderConfirmation)]
    public class T568_Emulator_VerifyElementsOfConfirmationPage : T568_MobileBase
    {
        public T568_Emulator_VerifyElementsOfConfirmationPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VariousElementsOfConfirmationPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the various elements of the Order Confirmation page.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10783
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T568
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10783"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T568")]
    public abstract class T568_MobileBase : TestsBaseMobile
    {
        protected T568_MobileBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: Add an item to the Cart between $10 and $25
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuBetweenTenAndTwentyDollars;
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "T", "This test can only be executed against DBTEST.");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            //Act: Proceed to shipping page & Enter the Shipping Address
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current Page is not an Shipping Page");

            var address = new Address { State = StateCodeListUnitedStates.CA };
            CustomerAddressInformation.EnterShippingAddress(address);

            Shipping.SelectRequiredNoteText();
            Payment.OpenOrderSummaryDropdown();
            ShoppingCartWorkflow.WaitForTaxLabelToUpdate();

            //Assert: The information in the Order Summary block has been noted
            var shippingProductTotalValue = Cart.GetOrderTotalCost();
            var shippingTaxValue = Cart.GetTaxAmount();

            //Act: Return to cart page by Edit Cart Link
            Payment.CloseOrderSummaryDropdown();
            Cart.SelectEditCartLink();
            Assert.True(Cart.IsCurrentPage, "Current Page is not an Cart Page");

            //Assert: The information in the Order Summary block has been noted
            var cartProductTotalValue = Cart.GetOrderTotalCost();
            var cartProductName = Cart.GetListOfAllProductsOnCartPage().First().Name;
            var cartTaxValue = Cart.GetTaxAmount();

            //Act: Follow the Order Flow and Place the order
            ShoppingCartWorkflow.GoToOrderConfirmationFromCartUsingCc();
            Assert.True(OrderConfirmation.IsCurrentPage, "Current Page is not an Order Confirmation");

            var ocProductTotalValue = Cart.GetOrderTotalCost();
            var ocTaxValue = Cart.GetTaxAmount("orderConfirmation");
            var ocProductName = OrderConfirmation.GetOcPageProductName();

            /*Assert:
             Verify Tax Value and Order Total are consistent between the Cart Overview page, Shipping page, and Order Confirmation page
             Verify The billing and shipping addresses are correct
             Verify ordered item is correct
            */
            Assert.True(cartProductTotalValue == shippingProductTotalValue && shippingProductTotalValue == ocProductTotalValue, "The order totals are not consistent between the Cart Overview page, Shipping page and the Order Confirmation page");
            Assert.True(cartTaxValue == shippingTaxValue && shippingTaxValue == ocTaxValue, "The tax value are not consistent between the Cart Overview page, Shipping page and the Order Confirmation page");
            Assert.True(cartProductName == ocProductName, "The ordered items are not correct");
            Assert.True(ShoppingCartWorkflow.VerifyAddress(address, OrderConfirmation.GetOcPageShippingAddress()), "Shipping address is not correct");
            Assert.True(ShoppingCartWorkflow.VerifyAddress(address, OrderConfirmation.GetOcPageBillingAddress()), "Billing address is not correct");
        }
    }
}