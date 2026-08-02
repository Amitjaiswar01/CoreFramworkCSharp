using System;
using Xunit;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Mobile.CartOverview.T7755_VerifyOrderSummarySectionContainsCorrectInformation
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
    public class T7755_iPhone_VerifyOrderSummarySectionContainsCorrectInformation : T7755_MobileBase
    {
        public T7755_iPhone_VerifyOrderSummarySectionContainsCorrectInformation(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyOrderSummarySectionContainsCorrectInformation(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
    public class T7755_Emulator_VerifyOrderSummarySectionContainsCorrectInformation : T7755_MobileBase
    {
        public T7755_Emulator_VerifyOrderSummarySectionContainsCorrectInformation(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyOrderSummarySectionContainsCorrectInformation(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Order Summary Section Contains Correct Information
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9937
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7755
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9937"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7755")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]

    public abstract class T7755_MobileBase : TestsBaseMobile
    {
        protected T7755_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            //Arrange: Navigate to Any PDP 
            InitializeFramework(config);
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage()");
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            //Act : Note the Price of the item  and Add the Product to Cart
            var price = ProductDetail.GetProductPriceText();
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "User is Not on Cart Page");

            //Act : Note the Price, Quantity, Name, Sku of the product in Cart
            var cartProducts = Cart.GetListOfAllProductsOnCartPage();
            var cartProductPrice = cartProducts[0].Price.Replace("$", string.Empty); 
            var cartProductQty = Convert.ToString(cartProducts[0].Quantity);

            //Act : Proceed to Shipping Page and Open Order Summary Drawer
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "User is not on Shipping Page");

            OrderSummaryBlock.OpenOrderSummaryDrawer();

            //Assert : Verify Product Name, Sku, Quantity, Price in order summary block matches with that in the Cart  
            Assert.Equals(cartProducts[0].Name, OrderSummaryBlock.GetProductName(), "Product Name does not match  with that on Cart Page");
            Assert.Equals(cartProducts[0].Sku, OrderSummaryBlock.GetShortSku(), "Product Sku does not match  with that on Cart Page");
            Assert.Equals(cartProductQty, OrderSummaryBlock.GetProductQuantity(), "Product Quantity does not match  with that on Cart Page");
            Assert.Equals(cartProductPrice, OrderSummaryBlock.GetProductPrice(), "Product Price does not match with that on Cart Page");
            Assert.Equals(price, OrderSummaryBlock.GetProductPrice(), "Product Price does not match with that on Product Detail Page");

            /*Act :
             Enter Shipping address and Proceed to Payment Page
             Open Order Summary section on Payment Page 
             */
            OrderSummaryBlock.CloseOrderSummaryDrawer();
            Assert.True(Shipping.IsCurrentPage, "User is not on Shipping Page");

            CustomerAddressInformation.EnterShippingAddress(Address);
            Shipping.ProceedToPayment();
            Assert.True(Payment.IsCurrentPage, "User is Not on Payment Page");

            OrderSummaryBlock.OpenOrderSummaryDrawer();

            //Assert : Verify Product Name, Sku, Quantity, Price in order summary block matches with that in the Cart
            Assert.Equals(cartProducts[0].Name, OrderSummaryBlock.GetProductName(), "Product Name does not match with that on Cart Page");
            Assert.Equals(cartProducts[0].Sku, OrderSummaryBlock.GetShortSku(), "Product Sku does not match with that on Cart Page");
            Assert.Equals(cartProductQty, OrderSummaryBlock.GetProductQuantity(), "Product Quantity does not match with that on Cart Page");
            Assert.Equals(cartProductPrice, OrderSummaryBlock.GetProductPrice(), "Product Price does not match with that on Cart Page");
            Assert.Equals(price, OrderSummaryBlock.GetProductPrice(), "Product Price does not match with that on Product Detail Page");
        }
    }
}
