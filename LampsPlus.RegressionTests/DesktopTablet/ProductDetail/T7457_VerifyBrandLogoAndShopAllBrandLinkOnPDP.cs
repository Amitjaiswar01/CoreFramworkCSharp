using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.RegressionTests.DesktopTablet.ProductDetail;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.Common.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7457_Windows_VerifyBrandLogoAndShopAllBrandLinkOnPDP : T7457_DesktopBase
    {
        public T7457_Windows_VerifyBrandLogoAndShopAllBrandLinkOnPDP(ITestOutputHelper output) : base(output)
        {
        }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyBrandLogoAndShopAllBrandLinkOnPDP(string config) => Validate(config);
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

            Verify.True(ProductDetail.IsProductDetailPage, "The user is not brought to the PDP for the item.");

            Verify.Displayed(ProductDetail.BrandLogo,
                "The brand logo element was expected but not displayed on the screen.");

            Verify.Displayed(ProductDetail.ManufacturerLinkAnchor,
                "The shop all brand link element was expected but not displayed on the screen.");

            var linkText = ProductDetail.ManufacturerLinkAnchor.Text;
            var logoLink = ProductDetail.BrandLogoLink.GetAttribute("href");
            var linkHref = ProductDetail.ManufacturerLinkAnchor.GetAttribute("href");

            Verify.StringContains(linkText, brand.Manufacturer, "Brand name on PDP doesn't match the database.");
            Verify.StringContains(logoLink, brand.Url, "Brand Logo URL doesn't match the database.");
            Verify.StringContains(linkHref, brand.Url, "Shop All link URL doesn't match the database.");
        }
    }
}
