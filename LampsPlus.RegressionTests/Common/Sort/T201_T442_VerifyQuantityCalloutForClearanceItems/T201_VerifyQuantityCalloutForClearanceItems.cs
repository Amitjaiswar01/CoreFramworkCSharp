using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.Common.Sort.T201_T442_VerifyQuantityCalloutForClearanceItems
{
    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T201_Windows_VerifyQuantityCalloutForClearanceItems : T201_DesktopBase
    {
        public T201_Windows_VerifyQuantityCalloutForClearanceItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void QuantityCalloutForClearanceItems(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T201_Mac_VerifyQuantityCalloutForClearanceItems : T201_DesktopBase
    {
        public T201_Mac_VerifyQuantityCalloutForClearanceItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void QuantityCalloutForClearanceItems(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T201_iPad_VerifyQuantityCalloutForClearanceItems : T201_DesktopBase
    {
        public T201_iPad_VerifyQuantityCalloutForClearanceItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void QuantityCalloutForClearanceItems(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.Sort)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T201_TabletEmulator_VerifyQuantityCalloutForClearanceItems : T201_DesktopBase
    {
        public T201_TabletEmulator_VerifyQuantityCalloutForClearanceItems(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void QuantityCalloutForClearanceItems(string config) => Validate(config);
    }


    /// <summary>
    /// Verify that the QTY callout for clearance items is displayed on the Sort page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10083
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T201
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10083"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T201")]
    public abstract class T201_DesktopBase : TestsBaseDesktop
    {
        protected T201_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            //Arrange : User is on Homepage
            InitializeFunctionalTest(config);

            //Act : Navigate to Clearance Sort page
            Browser.Navigate(Urls.ClearanceViewPageUrl);

            //Assert : Verify 'Qty Left' callout displays on the Sort page
            Assert.True(Sort.IsQtyLeftCalloutPresent, "Qty Left callout is not displayed on the Sort page");

            var qtyLeftSort = Sort.GetQtyLeftValue();

            //Act : Locate a product with the 'Qty Left' callout and navigate to its Pdp
            var shortSku = Sort.GetSkuWithCallout(Sort.GetQtyLeftCallout());
            Browser.NavigateToPdp(shortSku);

            var shortSkuOnPdp = ProductDetail.GetProductSku();
            var qtyLeftDb = ProductActions.GetQuantityLeft(shortSkuOnPdp);

            //Assert : Verify Qty Left callout value does not match with value in Database
            Assert.Equals(qtyLeftDb, qtyLeftSort, "Qty Left callout value does not match with value in Database");
        }
    }
}