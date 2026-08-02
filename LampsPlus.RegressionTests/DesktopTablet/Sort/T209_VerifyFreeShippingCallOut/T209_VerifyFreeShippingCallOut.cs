using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Sort.T209_VerifyFreeShippingCallOut
{
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T209_Windows_VerifyFreeShippingCallOut : T209_DesktopBase
    {
        public T209_Windows_VerifyFreeShippingCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void FreeShippingCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T209_Mac_VerifyFreeShippingCallOut : T209_DesktopBase
    {
        public T209_Mac_VerifyFreeShippingCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void FreeShippingCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T209_iPad_VerifyFreeShippingCallOut : T209_DesktopBase
    {
        public T209_iPad_VerifyFreeShippingCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void FreeShippingCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T209_TabletEmulator_VerifyFreeShippingCallOut : T209_DesktopBase
    {
        public T209_TabletEmulator_VerifyFreeShippingCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void FreeShippingCallOut(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that when 'Free Shipping' applies to a product, the callout is on the Sort page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10094
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T209
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10094"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T209")]
    public abstract class T209_DesktopBase : TestsBaseDesktop
    {
        protected T209_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : Navigate to a Sort page
            InitializeFunctionalTest(config);
            Browser.Navigate(Urls.CrystalChandeliersUrl);

            //Act : From 'Specials' filter select the 'Free Shipping' option
            Sort.ApplyFilters(1, false, new Dictionary<string, string>(){{ "Specials", "Free Shipping" }});
            Assert.True(Sort.IsFreeShippingFilterApplied(), "Free Shipping Filter is not Applied");

            //Act : Execute query using one of the product SKUs from the Sort page
            var sku = Sort.GetSkuWithCallout(Sort.GetShippingCallOut());
            var productDetails = Sort.GetContentsOf(sku);

            var freeShippingProductDb = ProductActions.GetFreeShippingProduct(sku);
            Assert.DatabaseObject(freeShippingProductDb, "ProductActions.GetFreeShippingProduct(firstProductOnFreeShippingSortPage)");

            //Assert : Verify 'Free Shipping & Free Returns' callout displays on the Sort page for selected item
            Assert.Equals(Sort.FreeShippingString, Sort.GetShippingCallOutLabel(), "The Free Shipping callout is not displayed on the Sort page for the selected item.");

            //Assert : Verify SKU, Price, and Product Name matches on site and database
            Assert.Equals(sku, freeShippingProductDb.ShortSku, "Free shipping product sku does not match database.");
            Assert.Equals(productDetails.Price.Replace("$", "").Replace(" Sale", ""), freeShippingProductDb.Price.ToString("0.00"), "Price on the web page does not match the values in the database.");
            Assert.Equals(productDetails.Name, freeShippingProductDb.ProductName.Replace("&quot;", "\""), "Product name does not match product name in database.");
        }
    }
}