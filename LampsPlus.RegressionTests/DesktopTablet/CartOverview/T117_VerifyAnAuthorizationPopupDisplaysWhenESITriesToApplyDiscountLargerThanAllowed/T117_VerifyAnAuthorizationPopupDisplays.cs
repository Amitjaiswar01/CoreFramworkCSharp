using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T117_VerifyAnAuthorizationPopupDisplaysWhenESITriesToApplyDiscountLargerThanAllowed
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T117_Windows_VerifyAnAuthorizationPopupDisplays : T117_DesktopBase
    {
        public T117_Windows_VerifyAnAuthorizationPopupDisplays(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyAnAuthorizationPopupDisplays(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T117_Mac_VerifyAnAuthorizationPopupDisplays : T117_DesktopBase
    {
        public T117_Mac_VerifyAnAuthorizationPopupDisplays(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyAnAuthorizationPopupDisplays(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T117_iPad_VerifyAnAuthorizationPopupDisplays : T117_DesktopBase
    {
        public T117_iPad_VerifyAnAuthorizationPopupDisplays(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyAnAuthorizationPopupDisplays(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T117_TabletEmulator_VerifyAnAuthorizationPopupDisplays : T117_DesktopBase
    {
        public T117_TabletEmulator_VerifyAnAuthorizationPopupDisplays(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyAnAuthorizationPopupDisplays(string config) => Validate(config);
    }


    /// <summary>
    /// Verify An Authorization Popup Displays When ESI Tries To Apply Discount Larger Than Allowed
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9923
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T117
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9923"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T117")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]

    public abstract class T117_DesktopBase : TestsBaseDesktop
    {
        protected T117_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            /*Arrangement:
            Navigate to any Random Sku
            Add to the cart
            */
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetAnySkuWithProductDetailPage;
            ProductDetail.AddSingleProductToCart(shortSku);

            // Act: Apply high percentage discount for MD%
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            var percentageDiscount = 90;
            CsrBlock.ApplyCartLevelDiscount(percentageDiscount);

            // Assert: Check an Authorization Popup Displays
            Assert.True(CsrBlock.IsManualDiscountManagerApprovalFormDisplayed, "Manual discount approval form not displayed.");
        }
    }
}
