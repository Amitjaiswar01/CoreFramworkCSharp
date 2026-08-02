using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using xRetry;

namespace LampsPlus.RegressionTests.Common.ProductDetail.T228_T7582_VerifyPopularColorsSliderArtShadesNumber
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7582_iPhone_VerifyPopColorsSliderArtShadesNum : T7582_MobileBase
    {
        public T7582_iPhone_VerifyPopColorsSliderArtShadesNum(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(3)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void PopColorsSliderArtShadesNum(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T7582_Emulator_VerifyPopColorsSliderArtShadesNum : T7582_MobileBase
    {
        public T7582_Emulator_VerifyPopColorsSliderArtShadesNum(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void PopColorsSliderArtShadesNum(string config) => Validate(config);
    }


    /// <summary>
    /// Verifies the product color art shades number.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8793
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7582
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8793"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7582")]
    public abstract class T7582_MobileBase : TestsBaseMobile
    {
        protected T7582_MobileBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            //Arrange: Run the query to find a product which has a 'Popular Colors' slider.
            InitializeFunctionalTest(config);
            var popularProduct = ProductActions.GetSkuPopularProduct();
            Assert.DatabaseObject(popularProduct, "ProductActions.GetSkuPopularProduct()");

            //Act: Navigate by ShortSKU that was returned from the query results 
            ProductDetail.NavigateToProductDetailByShortSku(popularProduct.ShortSku);

            /*Assert
            Once the PDP loads, open the 'Popular Colors' drawer, scroll through the slider options and total how many there are.
            Compare this value to the value in the 'PatternIDTotal' column from the database.
            */
            Assert.Equals(popularProduct.PatternIdTotal, ProductDetailMcp.GetPopularColorsCount(), "The total does not match.");
        }
    }
}