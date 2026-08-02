using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    //[Collection(LpTraits.UserRole.Employee)]
    public class T259_Windows_VerifyInvInfoForEmpCust : T259_DesktopBase
    {
        public T259_Windows_VerifyInvInfoForEmpCust(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyInvInfoShown(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    public class T259_Windows_Kiosk_VerifyInvInfoForEmpCust : T259_DesktopBase
    {
        public T259_Windows_Kiosk_VerifyInvInfoForEmpCust(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void VerifyInvInfoShown(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T259_Mac_VerifyInvInfoForEmpCust : T259_DesktopBase
    {
        public T259_Mac_VerifyInvInfoForEmpCust(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyInvInfoShown(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.OrderHistory)]
    public class T259_Mac_Kiosk_VerifyInvInfoForEmpCust : T259_DesktopBase
    {
        public T259_Mac_Kiosk_VerifyInvInfoForEmpCust(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void VerifyInvInfoShown(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Employee)]
    public class T259_iPad_VerifyInvInfoForEmpCust : T259_DesktopBase
    {
        public T259_iPad_VerifyInvInfoForEmpCust(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyInvInfoShown(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    public class T259_TabletEmulator_VerifyInvInfoForEmpCust : T259_DesktopBase
    {
        public T259_TabletEmulator_VerifyInvInfoForEmpCust(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI)]
        public void VerifyInvInfoShown(string config) => Validate(config);
    }
    

    /// <summary>
    /// Verify that inventory info and SKU Status should display for ESI and SIS.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5441
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T259
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5441"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T259")]
    public abstract class T259_DesktopBase : ProductDetailTestsBase
    { 
        protected T259_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var productWithSkuStatus = ProductActions.GetProductWithSkuStatus();

            Assert.DatabaseObject(productWithSkuStatus, "ProductActions.GetProductWithSkuStatus()");

            ProductDetail.NavigateToProductDetailByShortSku(productWithSkuStatus.ShortSku);

            Assert.Displayed(ProductDetail.LongSkuElement, "Long Sku Element should be displayed");
            Assert.Displayed(ProductDetail.StoreInventoryElement, "Store Inventory Element should be displayed");
            Assert.True(ProductDetail.CsInfo.Text.Contains(ProductDetail.SkuStatusLabel), "Sku Status Element is not displayed");
            Assert.Equals(productWithSkuStatus.SkuStatus.Trim(), ProductDetail.SkuStatusValue().Trim(), "Incorrect SKU status displayed.");
        }
    }
}
