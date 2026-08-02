using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Sort.T210_VerifyFreeShippingFreeReturnCallOut
{
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T210_Windows_VerifyFreeShippingFreeReturnCallOut : T210_DesktopBase
    {
        public T210_Windows_VerifyFreeShippingFreeReturnCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyFreeShippingFreeReturnCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T210_Mac_VerifyFreeShippingFreeReturnCallOut : T210_DesktopBase
    {
        public T210_Mac_VerifyFreeShippingFreeReturnCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyFreeShippingFreeReturnCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T210_iPad_VerifyFreeShippingFreeReturnCallOut : T210_DesktopBase
    {
        public T210_iPad_VerifyFreeShippingFreeReturnCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyFreeShippingFreeReturnCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T210_TabletEmulator_VerifyFreeShippingFreeReturnCallOut : T210_DesktopBase
    {
        public T210_TabletEmulator_VerifyFreeShippingFreeReturnCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyFreeShippingFreeReturnCallOut(string config) => Validate(config);
    }


    /// <summary>
    /// Verify when 'Free Shipping & Returns' applies to a product, the callout is on the Sort page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10095
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T210
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10095"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T210")]
    public abstract class T210_DesktopBase : TestsBaseDesktop
    {
        protected T210_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : Navigate to Free Shipping Free Returns Sort Page
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.FreeShippingAndFreeReturnsUrl);
            var nonSaleSku = Sort.GetNonSaleProductFromSort();

             //Act : Execute query using one of the product SKUs from the Sort page
            var productContents = Sort.GetContentsOf(nonSaleSku);
            var freeShippingInfoDb = ProductActions.GetFreeShippingFreeReturnsInformation(nonSaleSku);
            Assert.DatabaseObject(freeShippingInfoDb, "ProductActions.GetFreeShippingFreeReturnsInformation(shortSku)");

            //Assert : Verify 'Free Shipping & Free Returns' callout displays on the Sort page for selected item
            Assert.Equals(Sort.FreeShippingFreeReturnString, Sort.GetFreeShippingFreeReturnLabel().Trim(), "'Free Shipping & Free Returns' callout is not displayed on the Sort page for selected item.");

            //Assert : Verify SKU, Price, and Product Name matches on site and database
            Assert.Equals(nonSaleSku, freeShippingInfoDb.ShortSku, "Free Shipping & Free Returns product sku does not match database.");
            Assert.Equals(productContents.Price.Replace("$", "").Trim(), freeShippingInfoDb.Price.ToString("0.00"), "Price on the web page does not match the values in the database.");
            Assert.Equals(productContents.Name.Trim(), freeShippingInfoDb.ProductName.Replace("&quot;", "\""), "Product name does not match product name in database.");
        }
    }
}