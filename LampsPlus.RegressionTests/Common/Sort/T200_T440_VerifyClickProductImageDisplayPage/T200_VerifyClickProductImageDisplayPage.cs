using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T200_T440_VerifyClickProductImageDisplayPage
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T200_Windows_VerifyClickProductImageDisplayPage : T200_DesktopBase
    {
        public T200_Windows_VerifyClickProductImageDisplayPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void ClickProductImageDisplayPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T200_Mac_VerifyClickProductImageDisplayPage : T200_DesktopBase
    {
        public T200_Mac_VerifyClickProductImageDisplayPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ClickProductImageDisplayPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T200_iPad_VerifyClickProductImageDisplayPage : T200_DesktopBase
    {
        public T200_iPad_VerifyClickProductImageDisplayPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ClickProductImageDisplayPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T200_TabletEmulator_VerifyClickProductImageDisplayPage : T200_DesktopBase
    {
        public T200_TabletEmulator_VerifyClickProductImageDisplayPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void ClickProductImageDisplayPage(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that clicking a product image on the sort page displays the PDP for the product.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10075
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T200
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10075"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T200")]
    public abstract class T200_DesktopBase : TestsBaseDesktop
    {
        protected T200_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : Navigate to a Sort page
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.OutdoorLightingSortUrl);

            //Act : Make a note of Price and ProductName of any product on Sort page
            var sku = Sort.GetNonSaleProductFromSort();
            var productDetails = Sort.GetContentsOf(sku);

            var productName = TextActions.RegexNoTabsAndNewLines(productDetails.Name);
            var productPrice = TextActions.GetOnlyPriceFromString(productDetails.Price);

            //Act : Navigate to Pdp of the product noted above
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            //Assert : Verify PDP shows the same product's name, and price, as the sort page
            Assert.Equals(TextActions.NormalizeWhitespace(productName), ProductDetail.GetProductName(), "Product Sku does not match.");
            Assert.Equals(TextActions.NormalizeWhitespace(productPrice), ProductDetail.GetProductPriceOnPdp(), "Product Price does not match.");
        }
    }
}