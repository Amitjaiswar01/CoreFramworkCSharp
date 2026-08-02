using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T133_VerifyMoreInfoLinkDisplaysForStoreInSession
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T133_Windows_VerifyMoreInfoLinkDisplayedWhenStoreInSession : T133_DesktopBase
    {
        public T133_Windows_VerifyMoreInfoLinkDisplayedWhenStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void VerifyMoreInfoLinkDisplayedWhenStoreInSession(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Mobile.CartOverview)]
    public class T133_TabletEmulator_VerifyMoreInfoLinkDisplayedWhenStoreInSession : T133_DesktopBase
    {
        public T133_TabletEmulator_VerifyMoreInfoLinkDisplayedWhenStoreInSession(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI)]
        public void VerifyMoreInfoLinkDisplayedWhenStoreInSession(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that "more info" link is displayed when store is in session.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9934
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T133  
    /// </summary>
    public abstract class T133_DesktopBase : TestsBaseDesktop
    {
        protected T133_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange: User has identified SKU with Inventory Details and navigated to PDP
            InitializeFunctionalTest(config);
            Skip.IfNot(DevEnvInformation.DatabaseSymbol == "P", "This test can only be executed against DBCLUST.");

            var productWithWarehouseInventory = ProductActions.GetProductWithWarehouseInventory();
            Assert.DatabaseObject(productWithWarehouseInventory, "ProductActions.GetProductWithWarehouseInventory()");
            
            ProductDetail.NavigateToProductDetailByShortSku(productWithWarehouseInventory.ShortSku);
            Assert.True(ProductDetail.IsCurrentPage,"Current Page is not on Product Detail Page.");

            var productDetailInventory = ProductDetail.GetProductInventory().Split('O')[0].TrimEnd().Split('|')[2].TrimEnd();
            
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            //Act: Click on More Details Link on Cart Page
            Cart.OpenMoreDetailsDrawer();

            //Act: Get Product Inventory on Cart Page
            var cartDetailsInventory = Cart.GetProductInventoryDetails().Split('|')[2].TrimEnd();

            //Assert: Verify Inventory on cart page matches with database
            var warehouseInventory = Cart.GetProductInventoryDetails().EndsWith($"99&LP: {productWithWarehouseInventory.WarehouseInventory}");
            Assert.True(warehouseInventory, "Inventory does not match database");

            //Assert: Verify Inventory on cart page and on PDP are same
            Assert.Equals(productDetailInventory, cartDetailsInventory, "Inventory value does not match on PDP and cart");
        }
    }
}