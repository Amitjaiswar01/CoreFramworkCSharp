using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.ProductDetail.T221_T455_VerifyFreeShippingOnProduct
{
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T221_Windows_VerifyFreeShippingOnProduct : T221_DesktopBase
    {
        public T221_Windows_VerifyFreeShippingOnProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void FreeShippingOnProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T221_Mac_VerifyFreeShippingOnProduct : T221_DesktopBase
    {
        public T221_Mac_VerifyFreeShippingOnProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void FreeShippingOnProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T221_iPad_VerifyFreeShippingOnProduct : T221_DesktopBase
    {
        public T221_iPad_VerifyFreeShippingOnProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void FreeShippingOnProduct(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    public class T221_TabletEmulator_VerifyFreeShippingOnProduct : T221_DesktopBase
    {
        public T221_TabletEmulator_VerifyFreeShippingOnProduct(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void FreeShippingOnProduct(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that all items with the 'Free Shipping' attribute persist to the PDP page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-5055
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T221
    /// </summary>
    //[Collection(LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.ProductDetail)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-5055"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T221")]
    public abstract class T221_DesktopBase : TestsBaseDesktop
    {
        protected T221_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            /*Arrange
            User is on the following page: https://www.lampsplus.com/products/chandeliers/style_crystal/
            */
            InitializeFunctionalTest(config, Urls.CrystalChandeliersUrl);

            /*Act
            Navigate to Free Shipping Sort sub-page
            */
            var url = Browser.PageUrl;
            Browser.Navigate(url + Sort.FreeShippingUrlFragmentString);
            Assert.True(Sort.IsCurrentPage, "Current page is not Sort page");

            /*Act
            Get Free Shipping products skus
            */
            var numberOfGivenProducts = 3;
            var productLinks = Sort.GetLinksForGivenNumberOfProductsOnSortPage(numberOfGivenProducts);
            var freeShippingProductsSkus = ProductDetail.GetFreeShippingProductsSkus(productLinks);

            /*Assert
            The database returns a result for the given SKU.
            */
            foreach (var sku in freeShippingProductsSkus)
            {
                var freeShippingSkuData = ProductActions.GetFreeShippingSkuData(sku);

                Assert.DatabaseObject(freeShippingSkuData, "ProductActions.GetFreeShippingSkuData(sku)");
                Assert.False(string.IsNullOrWhiteSpace(freeShippingSkuData.ShortSku), "Related Item Sku Should Exist");
            }
        }
    }
}