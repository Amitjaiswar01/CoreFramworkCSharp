using System;
using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T7762_VerifyCartLinkFunctionalityonCart
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    //[Collection(LpTraits.UserRole.Employee)]
    public class T7762_Windows_VerifyCartLinkFunctionalityOnCart : T7762_DesktopBase
    {
        public T7762_Windows_VerifyCartLinkFunctionalityOnCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyCartLinkFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7762_Mac_VerifyCartLinkFunctionalityOnCart : T7762_DesktopBase
    {
        public T7762_Mac_VerifyCartLinkFunctionalityOnCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyCartLinkFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7762_iPad_VerifyCartLinkFunctionalityOnCart : T7762_DesktopBase
    {
        public T7762_iPad_VerifyCartLinkFunctionalityOnCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyCartLinkFunctionality(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7762_TabletEmulator_VerifyCartLinkFunctionalityOnCart : T7762_DesktopBase
    {
        public T7762_TabletEmulator_VerifyCartLinkFunctionalityOnCart(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyCartLinkFunctionality(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the 'Cart Link' functionality works as expected
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9914
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7762
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop),
     Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen),
     Trait(LpTraits.Keys.Category, LpTraits.Categories.CRUD)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9914"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7762")]
    public abstract class T7762_DesktopBase : TestsBaseDesktop
    {
        protected T7762_DesktopBase(ITestOutputHelper output) : base(output)
        {
        }

        protected void Validate(string config)
        {
            InitializeFunctionalTest(config);

            // Arrange : Add an contemporary floor lamp to cart
            var numberOfProductsToAddToCart = 1;
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, numberOfProductsToAddToCart);
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            // Act : Change the item quantity  
            var qty = "3";
            var qtyEmployee = Convert.ToInt32(qty);
            Cart.ChangeItemQuantity(qty);

            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            // Act : Get the details of product added to cart by employee
            var cartItemsQty = Cart.GetCountOfAllProductsInCart();
            var cartskus = Cart.GetListOfCartSkus(Browser.PageUrl, cartItemsQty);

            // Act : Open cart link modal and get the cartlink text
            Cart.OpenCartLinkModal();
            var CartlinkDetails = Cart.GetCartLinkDetails();

            // Asset : Verify the cartcopylink changed to copied
            Assert.True(CartlinkDetails[0].CaseInsensitiveContains("COPIED!"), "The cart link is not copied");

            Modal.CloseLpModal();
            HeaderFooter.SignOut();

            // Act : Get any sku with product detail page and add it to cart
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            var shortSkuQty = 1;
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(shortSku));
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            // Act : Navigate to cartlink copied from employee user session and  Get all the cart details for Assertation
            Browser.Navigate(CartlinkDetails[1]);
            var cartMergedSkus = Cart.GetListOfAllProductsOnCartPage();

            // Assert : Check the employee sku and quantity and User sku and quantity added to cart
            Assert.Equals( cartskus[0], cartMergedSkus[0].Sku, "Employee added sku not matching");
            Assert.Equals(qtyEmployee, cartMergedSkus[0].Quantity, "Employee added sku quantity not matching");
            Assert.Equals(shortSku, cartMergedSkus[1].Sku, "Anonymous user added sku not matching");
            Assert.Equals(shortSkuQty, cartMergedSkus[1].Quantity, "Anonymous user added Skuqty not matching");
        }
    }
}

