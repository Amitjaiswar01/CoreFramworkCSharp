using Xunit;
using Xunit.Abstractions;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T204_T444_VerifyClearancePriceAndFlag
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T204_Windows_VerifyClearancePriceAndFlag : T204_DesktopBase
    {
        public T204_Windows_VerifyClearancePriceAndFlag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void ClearancePriceAndFlag(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T204_Mac_VerifyClearancePriceAndFlag : T204_DesktopBase
    {
        public T204_Mac_VerifyClearancePriceAndFlag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void ClearancePriceAndFlag(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T204_iPad_VerifyClearancePriceAndFlag : T204_DesktopBase
    {
        public T204_iPad_VerifyClearancePriceAndFlag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void ClearancePriceAndFlag(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    public class T204_TabletEmulator_VerifyClearancePriceAndFlag : T204_DesktopBase
    {
        public T204_TabletEmulator_VerifyClearancePriceAndFlag(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void ClearancePriceAndFlag(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that a product marked as a 'Clearance' item has correct flag and price in database.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10077
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T204
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10077"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T204")]
    public abstract class T204_DesktopBase : TestsBaseDesktop
    {
        protected T204_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Clearance Page
            Browser.Navigate(Urls.ClearancePageUrl);

            //Assert : Verify the Price and the word 'Clearance' is in red
            Assert.True(Sort.DoesTextColorMatches("rgba(153, 0, 0, 1)"), "The price and the word 'Clearance' text is not in red.");

            //Act : Notedown the Shortsku and its Price on site
            var sku = Sort.GetSkuWithCallout(Sort.GetSaleCallout());
            var productDetails = Sort.GetContentsOf(sku);

            var priceOnSite = TextActions.GetPriceTextOnly(productDetails.Price);

            //Act : Notedown the product details from Database
            var clearancePriceEntity = ProductActions.GetClearancePriceByShortsku(sku);

            //Assert : Verify Price on site matches with RetailPriceInternet in DB, and ClearanceFlag is 1
            Assert.Equals(priceOnSite, string.Format("{0:0.00}", clearancePriceEntity.RetailPriceInternet).Trim(), "Product price is not identical on Site and in database");
            Assert.True(clearancePriceEntity.IsClearance, "Clearance Flag value is not 1");
        }
    }
}