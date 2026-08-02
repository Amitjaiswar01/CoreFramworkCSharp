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

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7566_T7567_VerifyLayoutOfCallToOrder
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7566_Window_VerifyLayoutOfCallToOrder : T7566_DesktopBase
    {
        public T7566_Window_VerifyLayoutOfCallToOrder(ITestOutputHelper output, T7566_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfCallToOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7566_Mac_VerifyLayoutOfCallToOrder : T7566_DesktopBase
    {
        public T7566_Mac_VerifyLayoutOfCallToOrder(ITestOutputHelper output, T7566_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfCallToOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7566_iPad_VerifyLayoutOfCallToOrder : T7566_DesktopBase
    {
        public T7566_iPad_VerifyLayoutOfCallToOrder(ITestOutputHelper output, T7566_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfCallToOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7566_TabletEmulator_VerifyLayoutOfCallToOrder : T7566_DesktopBase
    {
        public T7566_TabletEmulator_VerifyLayoutOfCallToOrder(ITestOutputHelper output, T7566_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfCallToOrder(string config) => Validate(Validate, config);
    }


    public class T7566_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7566_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetCallToOrderSku;
        }
    }


    /// <summary>
    /// Verify the layout of the page for Call To Order products.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9840
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7566
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9840"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7566")]
    public abstract class T7566_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7566_SharedSkus_Fixture>
    {
        protected readonly T7566_SharedSkus_Fixture Fixture;

        protected T7566_DesktopBase(ITestOutputHelper output, T7566_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            //Arrange: User has identified a qualifying SKU.
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetCallToOrderSku");
            var shortSku = Fixture.ShortSku;

            //Act: Navigate to the page https://www.lampsplus.com/sfp/<SKU> using the SKU from the pre-conditions.
            Browser.Navigate(Urls.ProductFullPageBaseUrl + shortSku);
            Assert.True(ProductDetail.IsCallCustomerServiceBlockVisible, "User is not on a PDP with an unavailable SKU.");

            //Act: Capture a screenshot of the entire page and ignore the Similar Design SKUs.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { SortFullPageCertona.IgnoreSimilarDesignsContainer() }, true);

            //Act: Navigate to the page https://www.lampsplus.com/products/table-lamps/?sfp=<SKU>
            Browser.Navigate(Urls.PlaTableLampsSfpUrl + Fixture.ShortSku);
            Assert.True(ProductDetail.IsCallCustomerServiceBlockVisible, "User is not on a PDP with an unavailable SKU.");

            //Act: Capture a screenshot of the visible screen with the Sort SKUs ignored.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Sort.IgnoreSortResultProduct() });

            //Act: Navigate to the page https://www.lampsplus.com/products/<SKU>
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> {ProductDetail.IgnoreMoreYouMayLikeSection() }, offset:15);
        }
    }
}
