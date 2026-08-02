using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;

namespace LampsPlus.RegressionTests.DesktopTablet.ProductDetail
{
    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T234_Windows_VerifyHousingOptionItemQualification : T234_DesktopBase
    {
        public T234_Windows_VerifyHousingOptionItemQualification(ITestOutputHelper output) : base(output){ }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyHousingOptionItemQualification(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T234_Mac_VerifyHousingOptionItemQualification : T234_DesktopBase
    {
        public T234_Mac_VerifyHousingOptionItemQualification(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyHousingOptionItemQualification(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T234_iPad_VerifyHousingOptionItemQualification : T234_DesktopBase
    {
        public T234_iPad_VerifyHousingOptionItemQualification(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyHousingOptionItemQualification(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.ProductDetail)]
    public class T234_TabletEmulator_VerifyHousingOptionItemQualification : T234_DesktopBase
    {
        public T234_TabletEmulator_VerifyHousingOptionItemQualification(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyHousingOptionItemQualification(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that an item qualifies to be a Housing Option item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5171
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T234
    /// Investigate: The query in VerifyHousingOptionSkus always returns 0 results.
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5171"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T234")]
    public abstract class T234_DesktopBase : ProductDetailTestsBase
    {
        protected T234_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            InitializeFramework(config);
            var shortSku = ProductActions.GetSkuThatHasHousingOptions;

            Assert.DatabaseObject(shortSku, "ProductActions.GetSkuThatHasHousingOptions()");

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Assert.Equals(ProductDetail.GetTitleSku, shortSku, "Short Sku does not match.");
            Assert.Displayed(ProductDetail.HousingOptionsSectionHeader, "Housing options section is not displayed.");
            Assert.Equals(ProductDetail.HousingOptionsString, ProductDetail.HousingOptionsSectionHeader.Text, "Housing Option header text do not match.");

            VerifyHousingOptionSkus(shortSku);
        }

        private void VerifyHousingOptionSkus(string shortSku)
        {
            var housingOptionsSkus = ProductActions.GetHousingOptionsSkus(shortSku);
            var housingOptionsSkusFromSection = ProductDetail.GetSkusFromHousingOptionsSection;

            Assert.Equals(housingOptionsSkus.Count, housingOptionsSkusFromSection.Count, $"Expected a count of {housingOptionsSkus.Count} but got {housingOptionsSkusFromSection.Count}.");

            for (var i = 0; i < housingOptionsSkusFromSection.Count; i++)
            {
                Assert.Equals(housingOptionsSkus[i].ToLower(), housingOptionsSkusFromSection[i].ToLower(), "The expected count was not found.");
            }
        }
    }
}
