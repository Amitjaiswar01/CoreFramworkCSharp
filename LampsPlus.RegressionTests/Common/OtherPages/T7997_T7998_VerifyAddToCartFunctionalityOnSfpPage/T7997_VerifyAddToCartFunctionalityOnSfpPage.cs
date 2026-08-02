using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.OtherPages.T7997_T7998_VerifyAddToCartFunctionalityOnSfpPage
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7997_Windows_VerifyAddToCartFunctionalityOnSfpPage : T7997_DesktopBase
    {
        public T7997_Windows_VerifyAddToCartFunctionalityOnSfpPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyAddToCartFunctionalityOnSfpPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7997_Mac_VerifyAddToCartFunctionalityOnSfpPage : T7997_DesktopBase
    {
        public T7997_Mac_VerifyAddToCartFunctionalityOnSfpPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyAddToCartFunctionalityOnSfpPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7997_iPad_VerifyAddToCartFunctionalityOnSfpPage : T7997_DesktopBase
    {
        public T7997_iPad_VerifyAddToCartFunctionalityOnSfpPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyAddToCartFunctionalityOnSfpPage(string config) => Validate(config);
    }


    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.OtherPages)]
    public class T7997_TabletEmulator_VerifyAddToCartFunctionalityOnSfpPage : T7997_DesktopBase
    {
        public T7997_TabletEmulator_VerifyAddToCartFunctionalityOnSfpPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyAddToCartFunctionalityOnSfpPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify Add to Cart Functionality on SFP Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10905
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7997
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10905"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7997")]
    public abstract class T7997_DesktopBase : TestsBaseDesktop
    {
        protected T7997_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        { 
            //Arrange: User has identified an SKU using the query
            InitializeFunctionalTest(config);
            var sku = ProductActions.GetSkuThatQualifiesForReviews;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuThatQualifiesForReviews");

            //Act: Navigate to SFP Page using SKU returned from query
            ProductDetail.NavigateToPlaPageByShortSku(sku);

            //Act: Click on Add to Cart Button
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            //Assert: The item is added to the cart
            var cartSku = Cart.GetListOfCartSkus(Browser.PageUrl, 1)[0];
            Assert.Equals(cartSku, sku, "Product in Cart does not match Product on Sfp Page");
        }
    }
}