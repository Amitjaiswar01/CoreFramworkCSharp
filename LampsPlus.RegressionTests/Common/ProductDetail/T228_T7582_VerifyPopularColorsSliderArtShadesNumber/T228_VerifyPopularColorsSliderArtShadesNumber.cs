using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ProductDetail.T228_T7582_VerifyPopularColorsSliderArtShadesNumber
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    public class T228_Windows_VerifyPopColorsSliderArtShadesNum : T228_DesktopBase
    {
        public T228_Windows_VerifyPopColorsSliderArtShadesNum(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void PopColorsSliderArtShadesNum(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T228_Mac_VerifyPopColorsSliderArtShadesNum : T228_DesktopBase
    {
        public T228_Mac_VerifyPopColorsSliderArtShadesNum(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void PopColorsSliderArtShadesNum(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T228_iPad_VerifyPopColorsSliderArtShadesNum : T228_DesktopBase
    {
        public T228_iPad_VerifyPopColorsSliderArtShadesNum(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void PopColorsSliderArtShadesNum(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T228_TabletEmulator_VerifyPopColorsSliderArtShadesNum : T228_DesktopBase
    {
        public T228_TabletEmulator_VerifyPopColorsSliderArtShadesNum(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void PopColorsSliderArtShadesNum(string config) => Validate(config);
    }


    /// <summary>
    /// Verifies the product color art shades number.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5231
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T228 
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8793"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T228")]
    public abstract class T228_DesktopBase : TestsBaseDesktop
    {
        protected T228_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            //Arrange: Run the query to find a product which has a 'Popular Colors' slider.
            InitializeFunctionalTest(config);
            var popularProduct = ProductActions.GetSkuPopularProduct();
            Assert.DatabaseObject(popularProduct, "ProductActions.GetSkuPopularProduct()");

            //Act: Navigate by ShortSKU that was returned from the query results 
            ProductDetail.NavigateToProductDetailByShortSku(popularProduct.ShortSku);

            /*Assert
            Once the PDP loads, click through the slider options for 'Popular Colors' and get total on how many there are.
            Compare this value to the value in the 'PatternIDTotal' column from the database.
            */
            Assert.Equals(popularProduct.PatternIdTotal, ProductDetailMcp.GetPopularColorsCount(), "The total does not match."); 
        }
    }
}