using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.CartOverview.T7341_VerifyLayoutOfMoreDetailsDrawerOnCartOverviewPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7341_Windows_VerifyLayoutOfMoreDetailsDrawerOnCartOverviewPage : T7341_DesktopBase
    {
        public T7341_Windows_VerifyLayoutOfMoreDetailsDrawerOnCartOverviewPage(ITestOutputHelper output, T7341_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SIS_ESI)]
        public void VerifyLayoutOfMoreDetailsDrawer (string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7341_Mac_VerifyLayoutOfMoreDetailsDrawerOnCartOverviewPage : T7341_DesktopBase
    {
        public T7341_Mac_VerifyLayoutOfMoreDetailsDrawerOnCartOverviewPage(ITestOutputHelper output, T7341_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SIS_ESI)]
        public void VerifyLayoutOfMoreDetailsDrawer(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7341_iPad_VerifyLayoutOfMoreDetailsDrawerOnCartOverviewPage : T7341_DesktopBase
    {
        public T7341_iPad_VerifyLayoutOfMoreDetailsDrawerOnCartOverviewPage(ITestOutputHelper output, T7341_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SIS_ESI)]
        public void VerifyLayoutOfMoreDetailsDrawer(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7341_TabletEmulator_VerifyLayoutOfMoreDetailsDrawerOnCartOverviewPage : T7341_DesktopBase
    {
        public T7341_TabletEmulator_VerifyLayoutOfMoreDetailsDrawerOnCartOverviewPage(ITestOutputHelper output, T7341_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SIS_ESI)]
        public void VerifyLayoutOfMoreDetailsDrawer(string config) => Validate(Validate, config);
    }

    public class T7341_SharedSku_Fixture : FixtureBase
    {
        public string  ShortSku { get; }

        public T7341_SharedSku_Fixture()
        {
           ShortSku = ProductActions.GetSkuBetweenTenAndTwentyDollars;
        }
    }


    // <summary>
    /// Verify the layout of the More Details drawer on the Cart Overview page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9785
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7341
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9785"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7341")]
    public abstract class T7341_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7341_SharedSku_Fixture>
    {
        protected readonly T7341_SharedSku_Fixture Fixture;

        protected T7341_DesktopBase(ITestOutputHelper output, T7341_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            // Arrange: Get the sku that has value of less than $20 and navigate to cart page
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku, "ProductionActions.GetSkuBetweenTenAndTwentyDollars()");
            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);
            Assert.True(ProductDetail.IsCurrentPage, "Current Page is not the Product Detail page");
            ProductDetail.AddToCart();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart Page");

            // Act: Open More Details drawer on cart page
            Cart.OpenMoreDetailsDrawer();
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart Page");

            // Act: Capture the screenshot of the page by ignoring the cart id
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> {Cart.IgnoreCartId() }, true);
        }
    }
}
