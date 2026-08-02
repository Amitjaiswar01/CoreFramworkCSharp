using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7457_Windows_VerifyBrandLogoAndShopAllBrandLinkOnPdp : T7457_DesktopBase
    {
        public T7457_Windows_VerifyBrandLogoAndShopAllBrandLinkOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyBrandLogoAndShopAllBrandLinkOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T7457_Mac_VerifyBrandLogoAndShopAllBrandLinkOnPdp : T7457_DesktopBase
    {
        public T7457_Mac_VerifyBrandLogoAndShopAllBrandLinkOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyBrandLogoAndShopAllBrandLinkOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T7457_iPad_VerifyBrandLogoAndShopAllBrandLinkOnPdp : T7457_DesktopBase
    {
        public T7457_iPad_VerifyBrandLogoAndShopAllBrandLinkOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyBrandLogoAndShopAllBrandLinkOnPdp(string config) => Validate(config);
    }


    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T7457_TabletEmulator_VerifyBrandLogoAndShopAllBrandLinkOnPdp : T7457_DesktopBase
    {
        public T7457_TabletEmulator_VerifyBrandLogoAndShopAllBrandLinkOnPdp(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyBrandLogoAndShopAllBrandLinkOnPdp(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the Brand Logo and 'Shop all brand' link are displayed on the PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8034
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7457
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8034"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7457")]
    public abstract class T7457_DesktopBase : ProductDetailTestsBase
    {
        protected T7457_DesktopBase(ITestOutputHelper output) : base(output)
        {
        }

        protected void Validate(string config)
        {
            InitializeFramework(config);

            var brand = ProductActions.GetRandomBrandInfo();

            ProductDetail.NavigateToProductDetailByShortSku(brand.ShortSku);

            Assert.True(ProductDetail.IsProductDetailPage, "The user is not brought to the PDP for the item.");

            Assert.Displayed(ProductDetail.BrandLogo,
                "The brand logo element was expected but not displayed on the screen.");

            var logoLink = ProductDetail.BrandLogoLink.GetAttribute("href");

            Assert.StringContains(logoLink, brand.Url, "Brand Logo URL doesn't match the database.");
        }
    }
}
