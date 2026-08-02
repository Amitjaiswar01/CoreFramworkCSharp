using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.Sort.T207_VerifyMoreOptionsCallOut
{
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    public class T207_Windows_VerifyMoreOptionsCallOut : T207_DesktopBase
    {
        public T207_Windows_VerifyMoreOptionsCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void VerifyMoreOptionsCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T207_Mac_VerifyMoreOptionsCallOut : T207_DesktopBase
    {
        public T207_Mac_VerifyMoreOptionsCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyMoreOptionsCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T207_iPad_VerifyMoreOptionsCallOut : T207_DesktopBase
    {
        public T207_iPad_VerifyMoreOptionsCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyMoreOptionsCallOut(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.Sort)]
    public class T207_TabletEmulator_VerifyMoreOptionsCallOut : T207_DesktopBase
    {
        public T207_TabletEmulator_VerifyMoreOptionsCallOut(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyMoreOptionsCallOut(string config) => Validate(config);
    }


    /// <summary>
    /// Verify the 'More Options' callout is displayed on the Sort page in the proper circumstances.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10093
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T207
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10093"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T207")]

    public abstract class T207_DesktopBase : TestsBaseDesktop
    {
        protected T207_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Ceiling Fans page
            Browser.Navigate(Urls.CeilingFansUrl);

            //Act : Locate a product with the 'More Options' callout
            var shortSku = Sort.GetSkuWithCallout(Sort.GetMoreOptionsCallout());

            //Act : Execute query using one of the product Skus from the Sort page
            var dbMoreOptionItem = ProductActions.GetMoreOptionItem(shortSku);

            //Assert : Verify the 'callout' column from the database query has the value 'More Options' for selected Sku
            Assert.Equals("More Options", dbMoreOptionItem.Callout, "Database column does not contain More Options callout.");
        }
    }
}