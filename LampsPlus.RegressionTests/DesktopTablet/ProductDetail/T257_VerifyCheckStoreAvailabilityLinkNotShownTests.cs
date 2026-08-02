using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T257_Windows_VerifyCheckStoreAvailabilityLinkNotShown : T257_DesktopBase
    {
        public T257_Windows_VerifyCheckStoreAvailabilityLinkNotShown(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_UNSI)]
        public void CheckStoreAvailabilityLinkNotShown(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T257_TabletEmulator_VerifyCheckStoreAvailabilityLinkNotShown : T257_DesktopBase
    {
        public T257_TabletEmulator_VerifyCheckStoreAvailabilityLinkNotShown(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_UNSI)]
        public void CheckStoreAvailabilityLinkNotShown(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the 'Check Store Availability' link is NOT shown for certain roles.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5274
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T257
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5274"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T257")]
    //[Collection(LpTraits.UserRole.CustomerKiosk)]
    public abstract class T257_DesktopBase : ProductDetailTestsBase
    {
        protected T257_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var shortSku = ProductActions.GetItemsThatHaveCheckStoreAvailabilityLinkOnProductDetailPage();

            Assert.DatabaseObject(shortSku, "ProductActions.GetItemsThatHaveCheckStoreAvailabilityLinkOnProductDetailPage()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            CookieUtility.EnterStoreInSessionMode();
          
            Assert.False(ProductDetail.IsCheckStoreAvailabilityLinkVisible , "Failed - Check Store Availability Link Is Displayed");
        }
    }
}
