using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using Automation.Framework.Enums;

namespace LampsPlus.VisualRegressionTests.Common.OrderDetails.T7900_T7901_VerifyLayoutForRequestReturnPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7900_Windows_VerifyLayoutForRequestReturnPage : T7900_DesktopBase
    {
        public T7900_Windows_VerifyLayoutForRequestReturnPage(ITestOutputHelper output, T7900_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutForRequestReturnPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7900_Mac_VerifyLayoutForRequestReturnPage : T7900_DesktopBase
    {
        public T7900_Mac_VerifyLayoutForRequestReturnPage(ITestOutputHelper output, T7900_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutForRequestReturnPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7900_iPad_VerifyLayoutForRequestReturnPage : T7900_DesktopBase
    {
        public T7900_iPad_VerifyLayoutForRequestReturnPage(ITestOutputHelper output, T7900_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutForRequestReturnPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7900_TabletEmulator_VerifyLayoutForRequestReturnPage : T7900_DesktopBase
    {
        public T7900_TabletEmulator_VerifyLayoutForRequestReturnPage(ITestOutputHelper output, T7900_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutForRequestReturnPage(string config) => Validate(Validate, config);
    }


    public class T7900_ShareOrderDetails_Fixture : FixtureBase
    {
        public OrderIdModel Order { get; }

        public T7900_ShareOrderDetails_Fixture()
        {
            Order = OrderActions.GetAnOrderIdPlacedWithin60Days();
        }
    }


    /// <summary>
    /// Verify the Layout for Request a Return page from the Order Details Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10413
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7900
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10413"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7900")]
    public abstract class T7900_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7900_ShareOrderDetails_Fixture>
    {
        protected readonly T7900_ShareOrderDetails_Fixture Fixture;

        protected T7900_DesktopBase(ITestOutputHelper output, T7900_ShareOrderDetails_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            // Arrange: User has identified existing orders
            InitializeVisualTest(config, Urls.HomePageUrl);
            var orderDetails = Fixture.Order;
            Assert.DatabaseObject(Fixture.Order, "OrderActions.GetAnOrderIdPlacedWithin60Days()");

            /*Act
            Navigate to Order History Page
            Enter Order Id and Email Address
            Click on Track My Order button
            */
            Browser.Navigate(Urls.OrderHistoryPageUrl);            
            OrderHistory.CheckOrderStatus(orderDetails);

            /*Act
            Click on Request a Return link
            Capture a screenshot of the visible screen.
            */
            OrderDetails.NavigateToRequestReturnModal();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
