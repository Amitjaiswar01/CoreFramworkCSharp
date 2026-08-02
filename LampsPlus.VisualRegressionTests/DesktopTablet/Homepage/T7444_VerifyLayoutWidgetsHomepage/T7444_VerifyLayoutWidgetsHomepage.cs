using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.HomePage.T7444_VerifyLayoutWidgetsHomepage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7444_Windows_VerifyLayoutWidgetHomepage : T7444_DesktopBase
    {
        public T7444_Windows_VerifyLayoutWidgetHomepage(ITestOutputHelper output, T7444_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutWidgetOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7444_Mac_VerifyLayoutWidgetsHomepage : T7444_DesktopBase
    {
        public T7444_Mac_VerifyLayoutWidgetsHomepage(ITestOutputHelper output, T7444_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutWidgetOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7444_iPad_VerifyLayoutWidgetsHomepage : T7444_DesktopBase
    {
        public T7444_iPad_VerifyLayoutWidgetsHomepage(ITestOutputHelper output, T7444_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutWidgetOnHomepage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7444_TabletEmulator_VerifyLayoutWidgetsHomepage : T7444_DesktopBase
    {
        public T7444_TabletEmulator_VerifyLayoutWidgetsHomepage(ITestOutputHelper output, T7444_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutWidgetOnHomepage(string config) => Validate(Validate, config);
    }


    public class T7444_SharedProductSku_Fixture : FixtureBase
    {
        public string RandomShortSku1 { get; }
        public string RandomShortSku2 { get; }
        public string RandomShortSku3 { get; }

        public T7444_SharedProductSku_Fixture()
        {
            RandomShortSku1 = ProductActions.GetAnySkuWithProductDetailPage;
            RandomShortSku2 = ProductActions.GetAnySkuWithProductDetailPage;
            RandomShortSku3 = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the Widgets on Homepage
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9804
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7444
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9643"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7844")]
    public abstract class T7444_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7444_SharedProductSku_Fixture>
    {
        protected readonly T7444_SharedProductSku_Fixture Fixture;

        protected T7444_DesktopBase(ITestOutputHelper output, T7444_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange
            Navigate to Home Page and get 3 skus in in an Array
            */
            InitializeVisualTest(config);
            var skus = new[] { Fixture.RandomShortSku1, Fixture.RandomShortSku2, Fixture.RandomShortSku3 };
            foreach (var sku in skus) { Assert.DatabaseObject(sku, "ProductActions.GetAnySkuWithProductDetailPage()"); }

            /*Act
            Navigate to Crystal Chandeliers Page and Navigate back to Home Page
            */
            Sort.Navigate(Sort.CrystalChandeliersUrl);
            Assert.True(Sort.IsCurrentPage, "User is not on the Crystal Chandeliers Page");
            Home.Navigate();
            Assert.True(Home.IsCurrentPage, "User is not on the Home Page");

            /*Assert
            Take screenshot of the Visible Area
            */
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /*Act
            Navigate to 3 Product Detail Pages and Navigate back to Home Page
            */
            ProductDetail.NavigateToEachProductDetailPage(skus);
            Home.Navigate();
            Assert.True(Home.IsCurrentPage, "User is not on the Home Page");

            /*Act
            Take Screenshot of the Visible Area 
            */
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Home.IgnoreCertona());

            /*Act
            Navigate to a PDP and Add the Product to Cart and Navigate back to Home Page
            */
            ProductDetail.AddProductToCart(skus);
            Assert.True(Cart.IsCurrentPage, "User is not on the Cart Page");
            Home.Navigate();
            Assert.True(Home.IsCurrentPage, "User is not on the Home Page");

            /*Act
            Take screenshot of the Visible area 
            */
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Home.IgnoreCertona());
        }
    }
}