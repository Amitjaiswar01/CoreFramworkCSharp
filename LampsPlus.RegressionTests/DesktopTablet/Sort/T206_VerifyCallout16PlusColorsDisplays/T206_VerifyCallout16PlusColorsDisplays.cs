using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Sort.T206_VerifyCallout16PlusColorsDisplays
{
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T206_Windows_VerifyCallout16PlusColorsDisplays : T206_DesktopBase
    {
        public T206_Windows_VerifyCallout16PlusColorsDisplays(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyCallout16PlusColorsDisplays(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T206_Mac_VerifyCallout16PlusColorsDisplays : T206_DesktopBase
    {
        public T206_Mac_VerifyCallout16PlusColorsDisplays(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyCallout16PlusColorsDisplays(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T206_iPad_VerifyCallout16PlusColorsDisplays : T206_DesktopBase
    {
        public T206_iPad_VerifyCallout16PlusColorsDisplays(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyCallout16PlusColorsDisplays(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T206_TabletEmulator_VerifyCallout16PlusColorsDisplays : T206_DesktopBase
    {
        public T206_TabletEmulator_VerifyCallout16PlusColorsDisplays(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyCallout16PlusColorsDisplays(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the '16+ Colors' callout is displayed on the Sort page in the proper circumstances.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10092
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T206
    /// </summary>      
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10092"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T206")]
    public abstract class T206_DesktopBase : TestsBaseDesktop
    {
        protected T206_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Sort page
            Browser.Navigate(Urls.BrittoManufacturerWith16ColorsCalloutUrl);

            //Act : Locate a product with ' 16+ colors' callout
            var sku = Sort.GetSkuWithCallout(Sort.Get16PlusColorsCallout());
            var productDetails = Sort.GetContentsOf(sku);

            //Act : Execute query using one of the product SKUs from the Sort page
            var db16PlusColorsItem = ProductActions.Get16PlusColorItem(sku);

            //Assert : Verify '16+ Colors' callout displays on the Sort page for selected item
            Assert.Equals("16+ COLORS", Sort.Get16PlusColorsCalloutLabel(), "16+ Colors callout is not displayed for the selected item.");

            //Assert : Verify Sku and callout match the values from the the database for selected Sku
            Assert.Equals(sku, db16PlusColorsItem.ShortSku, "Selected product ShortSku does not match on site and database");
            Assert.Equals(Sort.Get16PlusColorsCalloutLabel(), db16PlusColorsItem.Callout.ToUpper(), "Callout text does not match on site and database");
        }
    }
}