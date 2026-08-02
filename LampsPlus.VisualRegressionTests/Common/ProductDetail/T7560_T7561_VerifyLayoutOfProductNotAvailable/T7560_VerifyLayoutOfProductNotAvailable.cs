using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7560_T7561_VerifyLayoutOfProductNotAvailable
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7560_Window_VerifyLayoutOfProductNotAvailable : T7560_DesktopBase
    {
        public T7560_Window_VerifyLayoutOfProductNotAvailable(ITestOutputHelper output, T7560_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfProductNotAvailable(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7560_Mac_VerifyLayoutOfProductNotAvailable : T7560_DesktopBase
    {
        public T7560_Mac_VerifyLayoutOfProductNotAvailable(ITestOutputHelper output, T7560_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfProductNotAvailable(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7560_iPad_VerifyLayoutOfProductNotAvailable : T7560_DesktopBase
    {
        public T7560_iPad_VerifyLayoutOfProductNotAvailable(ITestOutputHelper output, T7560_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfProductNotAvailable(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7560_TabletEmulator_VerifyLayoutOfProductNotAvailable : T7560_DesktopBase
    {
        public T7560_TabletEmulator_VerifyLayoutOfProductNotAvailable(ITestOutputHelper output, T7560_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfProductNotAvailable(string config) => Validate(Validate, config);
    }


    public class T7560_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public List<Dictionary<string, string>> Url { get; }

        public T7560_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetProductNotAvailableShortSku;
            Url = SortActions.GetSortWithNoActiveAbTest();
        }
    }


    /// <summary>
    /// Verify the layout of the page for Product Not Available SKUs.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9841
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7560
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9841"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7560")]
    public abstract class T7560_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7560_SharedSkus_Fixture>
    {
        protected readonly T7560_SharedSkus_Fixture Fixture;

        protected T7560_DesktopBase(ITestOutputHelper output, T7560_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a qualifying SKU.
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetProductNotAvailableShortSku");
            var shortSku = Fixture.ShortSku;

            //Act: Using the SKU from the query in the pre-conditions, navigate to the following page: https://www.lampsplus.com/sfp/<SKU>
            Browser.Navigate(Urls.ProductFullPageBaseUrl + shortSku);
            Assert.True(ProductDetail.IsCallCustomerServiceBlockVisible, "User is not on a PDP with an unavailable SKU.");

            //Act: Capture a screenshot of the entire page and ignore the Certona Designs container.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { SortFullPageCertona.IgnoreSimilarDesignsContainer() }, true, true);

            //Act: Navigate to any Sort page and add the following to the end of the URL: ?sfp=<SKU>
            Browser.Navigate($"https://{Fixture.Url[0]["Url"]}?sfp={Fixture.ShortSku}");
            Assert.True(ProductDetail.IsCallCustomerServiceBlockVisible, "User is not on a PDP with an unavailable SKU.");

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: avigate to the following URL: https://www.lampsplus.com/products/<SKU>
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on a PDP.");

            //Act: Capture a screenshot of the visible screen while ignoring the 'More You May Like' section.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreMoreYouMayLikeSection() });
        }
    }
}
