using Xunit;
using Xunit.Abstractions;
using LampsPlus.AutomationFramework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.RegressionTests.DesktopTablet.CartOverview.T7736_VerifySaleEndsInCalloutOnCartOverviewPage
{
    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T7736_Windows_VerifySaleEndsInCalloutOnCartOverviewPage : T7736_DesktopBase
    {
        public T7736_Windows_VerifySaleEndsInCalloutOnCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifySaleEndsInCalloutOnCartOverviewPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T7736_Windows_VerifySaleEndsInCalloutOnCartOverviewPageForPro : T7736_DesktopBase
    {
        public T7736_Windows_VerifySaleEndsInCalloutOnCartOverviewPageForPro(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
      public void VerifySaleEndsInCalloutOnCartOverviewPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7736_Mac_VerifySaleEndsInCalloutOnCartOverviewPage : T7736_DesktopBase
    {
        public T7736_Mac_VerifySaleEndsInCalloutOnCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifySaleEndsInCalloutOnCartOverviewPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]
    public class T7736_Mac_VerifySaleEndsInCalloutOnCartOverviewPageForPro : T7736_DesktopBase
    {
        public T7736_Mac_VerifySaleEndsInCalloutOnCartOverviewPageForPro(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void VerifySaleEndsInCalloutOnCartOverviewPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7736_iPad_VerifySaleEndsInCalloutOnCartOverviewPage : T7736_DesktopBase
    {
        public T7736_iPad_VerifySaleEndsInCalloutOnCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifySaleEndsInCalloutOnCartOverviewPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7736_iPad_VerifySaleEndsInCalloutOnCartOverviewPageForPro : T7736_DesktopBase
    {
        public T7736_iPad_VerifySaleEndsInCalloutOnCartOverviewPageForPro(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_PCSI)]
        public void VerifySaleEndsInCalloutOnCartOverviewPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7736_TabletEmulator_VerifySaleEndsInCalloutOnCartOverviewPage : T7736_DesktopBase
    {
        public T7736_TabletEmulator_VerifySaleEndsInCalloutOnCartOverviewPage(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifySaleEndsInCalloutOnCartOverviewPage(string config) => Validate(config);
    }


    //[Collection(LpTraits.BatchGroup.Desktop.CartOverview)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Common.CartOverview)]
    public class T7736_TabletEmulator_VerifySaleEndsInCalloutOnCartOverviewPageForPro : T7736_DesktopBase
    {
        public T7736_TabletEmulator_VerifySaleEndsInCalloutOnCartOverviewPageForPro(ITestOutputHelper output) : base(output) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_PCSI)]
        public void VerifySaleEndsInCalloutOnCartOverviewPage(string config) => Validate(config);
    }

    /// <summary>
    /// Verify Sale Ends In Callout On CartOverview Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9913
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7736
    /// </summary>
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9913"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7736")]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.Keys.Category, LpTraits.BatchGroup.Desktop.CartOverview)]

    public abstract class T7736_DesktopBase : TestsBaseDesktop
    {
        protected T7736_DesktopBase(ITestOutputHelper output) : base(output) { }

        protected virtual void Validate(string config)
        {
            // Arrange: Get Sku with Sale End CallOut
            InitializeFunctionalTest(config);
            var shortSku = ProductActions.GetSkuForSaleEndsInCallout;

            // Act: Add Sku to Cart 
            ProductDetail.AddSingleProductToCart(shortSku);
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            // Assert: Check the Sale Ends In CallOut
            Assert.Equals("Sale Ends in", Cart.GetSaleEndsInCallOut(), "There is no Sale Ends In CallOut");
        }
    }
}
