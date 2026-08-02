using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T116_VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T116_Windows_VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp : T116_DesktopBase
    {
        public T116_Windows_VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
       public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T116_Mac_VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp : T116_DesktopBase
    {
        public T116_Mac_VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T116_iPad_VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp : T116_DesktopBase
    {
        public T116_iPad_VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T116_TabletEmulator_VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp : T116_DesktopBase
    {
        public T116_TabletEmulator_VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify A PopUp Appears When An Employee Enters A Price Below UMRP
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9922
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T116
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9922"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T116")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    public abstract class T116_DesktopBase : TestsBaseDesktop
    {
        protected T116_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            // Arrange: Select a UMRP Product
            InitializeFunctionalTest(config);

            ShoppingCartWorkflow.EmptyCart();
            var shortSku = ProductActions.GetShortSkuWithUmrp;
            Assert.DatabaseObject(shortSku, "ProductActions.GetShortSkuWithUmrp()");

            // Act : Add a UMRP Product to Cart
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku });

            //Act : Open discount Tooltip and apply discount 
            Cart.ApplyDiscount(10);

            // Assert : Verify UMRP pop-up displays and asks for comment
            Assert.True(Modal.IsModalVisible(), "Modal not Displayed");
            Assert.Displayed(Cart.GetDiscountVendorApprovalComment(), "Discount is greater than UMRP authorized form does not display");
        }
    }
}