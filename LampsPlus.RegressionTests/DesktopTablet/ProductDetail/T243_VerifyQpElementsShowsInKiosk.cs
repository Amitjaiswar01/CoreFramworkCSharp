using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T243_Windows_VerifyQpElementsShowsInKiosk : T243_DesktopBase
    {
        public T243_Windows_VerifyQpElementsShowsInKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void QpElementsShowsInKiosk(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T243_TabletEmulator_VerifyQpElementsShowsInKiosk : T243_DesktopBase
    {
        public T243_TabletEmulator_VerifyQpElementsShowsInKiosk(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI)]
        public void QpElementsShowsInKiosk(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the QP input box and QP link shows in kiosk mode.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5431
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T243
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5431"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T243")]
    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    public abstract class T243_DesktopBase : ProductDetailTestsBase
    {
        protected T243_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);
            var sku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Browser.TakeScreenshot("LP-17231_LP-T243_QP-PDP");

            CookieUtility.EnterStoreInSessionMode();

            Browser.Wait.ForDomReady();

            Assert.Displayed(ProductDetail.QuickPrintInput, "Quick Print Input should be displayed for kiosk store in session.");
            Assert.Displayed(ProductDetail.QuickPrintLink, "Quick Print Link should be displayed for kiosk store in session.");
        }
    }
}
