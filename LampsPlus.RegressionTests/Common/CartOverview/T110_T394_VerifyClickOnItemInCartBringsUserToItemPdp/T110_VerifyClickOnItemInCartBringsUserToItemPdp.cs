using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.CartOverview.T110_T394_VerifyClickOnItemInCartBringsUserToItemPdp
{
    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T110_Windows_VerifyClickOnItemInCartBringsUserToItemPdp : T110_DesktopBase
    {
        public T110_Windows_VerifyClickOnItemInCartBringsUserToItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyClickOnItemInCartBringsUserToItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T110_Mac_VerifyClickOnItemInCartBringsUserToItemPdp : T110_DesktopBase
    {
        public T110_Mac_VerifyClickOnItemInCartBringsUserToItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyClickOnItemInCartBringsUserToItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T110_iPad_VerifyClickOnItemInCartBringsUserToItemPdp : T110_DesktopBase
    {
        public T110_iPad_VerifyClickOnItemInCartBringsUserToItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyClickOnItemInCartBringsUserToItemPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T110_TabletEmulator_VerifyClickOnItemInCartBringsUserToItemPdp : T110_DesktopBase
    {
        public T110_TabletEmulator_VerifyClickOnItemInCartBringsUserToItemPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyClickOnItemInCartBringsUserToItemPdp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that clicking on an item in the cart brings the user to that PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9910
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T110
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9910"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T110")]
    public abstract class T110_DesktopBase : TestsBaseDesktop
    {
        protected T110_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            // Arrange - Add Non-Sale item in Cart and get the total count and shortsku
            InitializeFramework(config);

            var shortSku = ProductActions.GetItemNotOnSale;
            Assert.DatabaseObject(shortSku, "ProductActions.GetProductNotOnSale()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            var cartItemsQty = Cart.GetCountOfAllProductsInCart();
            var cartSkus = Cart.GetListOfCartSkus(Browser.PageUrl, cartItemsQty);

            // Act : Click on product image to navigate to PDP
            Cart.NavigateToPdpViaProductImageInCart();
            Assert.True(ProductDetail.IsCurrentPage, "Page is not PDP page");

            // Assert : The user is re-directed to the product page of the item that was Clicked.
            Assert.Equals(cartSkus[0], ProductDetail.GetProductSku(), "The Product SKU is not matching.");

            Browser.Navigate(Urls.CartOverviewPageUrl);
            Assert.True(Cart.IsCurrentPage, "Current page is not cart page");

            // Act : Click on product Name to navigate to PDP
            Cart.NavigateToPdpViaProductNameInCart();
            Assert.True(ProductDetail.IsCurrentPage, "Page is not PDP page");

            // Assert : The user is re-directed to the product page of the item that was Clicked.
            Assert.Equals(cartSkus[0], ProductDetail.GetProductSku(), "The Product SKU is not matching.");
        }
    }
}
