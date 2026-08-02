using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T121_VerifyEmployeeWithoutProductMarginRoleCantSeeMarginInfo
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T121_Windows_VerifyEmployeeCantSeeMarginInfo : T121_DesktopBase
    {
        public T121_Windows_VerifyEmployeeCantSeeMarginInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
       public void VerifyEmployeeWithoutProductMarginRoleCannotSeeInfoFromTheEditLink(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T121_Mac_VerifyEmployeeCantSeeMarginInfo : T121_DesktopBase
    {
        public T121_Mac_VerifyEmployeeCantSeeMarginInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T121_iPad_VerifyEmployeeCantSeeMarginInfo : T121_DesktopBase
    {
        public T121_iPad_VerifyEmployeeCantSeeMarginInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T121_TabletEmulator_VerifyEmployeeCantSeeMarginInfo : T121_DesktopBase
    {
        public T121_TabletEmulator_VerifyEmployeeCantSeeMarginInfo(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyAPopUpIsDisplayedWhenAnEmployeeEntersAPriceBelowUmrp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify a user without the Product Margin role cannot see that info from the 'edit' link
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9927
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T121
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9927"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T121")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]

    public abstract class T121_DesktopBase : TestsBaseDesktop
    {
        protected T121_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrange :
            Login With ESI user role without Product Margin role
            Add a Product to Cart
             */
            InitializeFunctionalTest(config);
            ShoppingCartWorkflow.EmptyCart();
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            Assert.DatabaseObject(shortSku, "ProductActions.GetAnySkuWithProductDetailPage");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = shortSku});
            Assert.True(Cart.IsCurrentPage, "User is Not on Cart Page");

            //Act : Click on Edit link to Open the Discount Tooltip
            Cart.OpenDiscountTooltip();

            //Assert : Verify that the Margin Info is Not Shown
            Assert.NotDisplayed(Cart.GetTextMarginField(), "Text Margin Field is Displayed");
            Assert.True(Cart.IsTextMarginFieldEmpty(), "Text Margin Field is Not Null");
        }
    }
}
