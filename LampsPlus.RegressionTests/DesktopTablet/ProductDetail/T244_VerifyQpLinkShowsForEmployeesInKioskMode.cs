using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Environment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    public class T244_Windows_VerifyQpLinkShowsForEmployeesInKioskMode : T244_DesktopBase
    {
        public T244_Windows_VerifyQpLinkShowsForEmployeesInKioskMode(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void QpLinkShowsForEmployeesInKioskMode(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T244_Mac_VerifyQpLinkShowsForEmployeesInKioskMode : T234_DesktopBase
    {
        public T244_Mac_VerifyQpLinkShowsForEmployeesInKioskMode(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void QpLinkShowsForEmployeesInKioskMode(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T244_iPad_VerifyQpLinkShowsForEmployeesInKioskMode : T234_DesktopBase
    {
        public T244_iPad_VerifyQpLinkShowsForEmployeesInKioskMode(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void QpLinkShowsForEmployeesInKioskMode(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T244_TabletEmulator_VerifyQpLinkShowsForEmployeesInKioskMode : T234_DesktopBase
    {
        public T244_TabletEmulator_VerifyQpLinkShowsForEmployeesInKioskMode(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void QpLinkShowsForEmployeesInKioskMode(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that just the QP link shows for employees logged in kiosk mode.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5203
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T244
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5203"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T244")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    //[Collection(LpTraits.UserRole.EmployeeKiosk)]
    public abstract class T244_DesktopBase : ProductDetailTestsBase
    {
        protected T244_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            var setup = new TestSetup(config) { AccountConfig = { StoreInSessionStoreNumber = "12" } };
            InitializeFramework(config, setup: setup);

            var sku = ProductActions.GetAnySkuWithProductDetailPage;

            Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()");

            ProductDetail.NavigateToProductDetailByShortSku(sku);

            var pdpProductNameWithSku = ProductDetail.ProductNameWithSku;
            var pdpItemPrice = ProductDetail.ItemPriceText.Replace("Price:\r\n", "");

            Assert.Displayed(ProductDetail.QuickPrintLink, "QP Link not displayed on PDP");

            ProductDetail.QuickPrintLink.Click();

            Browser.Wait.ForIframeDomReady(GlobalLocators.IframeModal);
            Browser.SwitchFocusToIframe(GlobalLocators.IframeModal);

            Assert.Equals(pdpItemPrice, ProductDetail.QuickPrintLpModalPrice, "Price not same on the PDP and QP Modal");
            Assert.Equals(pdpProductNameWithSku, ProductDetail.QuickPrintLpModalProductName, "Product Name and SKU not same on the PDP and QP Modal");

            Browser.SwitchToDefaultContent();

            GlobalLocators.LpModalCloseElement.Click();
        }
    }
}
