using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.OrderDetails
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7847_Windows_VerifyLayoutOfOrderHistoryPage : T7847_DesktopBase
    {
        public T7847_Windows_VerifyLayoutOfOrderHistoryPage(ITestOutputHelper output, T7847_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void VerifyLayoutOfOrderHistoryPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7847_Mac_VerifyLayoutOfOrderHistoryPage : T7847_DesktopBase
    {
        public T7847_Mac_VerifyLayoutOfOrderHistoryPage(ITestOutputHelper output, T7847_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void VerifyLayoutOfOrderHistoryPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7847_iPad_VerifyLayoutOfOrderHistoryPage : T7847_DesktopBase
    {
        public T7847_iPad_VerifyLayoutOfOrderHistoryPage(ITestOutputHelper output, T7847_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void VerifyLayoutOfOrderHistoryPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7847_TabletEmulator_VerifyLayoutOfOrderHistoryPage : T7847_DesktopBase
    {
        public T7847_TabletEmulator_VerifyLayoutOfOrderHistoryPage(ITestOutputHelper output, T7847_SharedProductSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void VerifyLayoutOfOrderHistoryPage(string config) => Validate(Validate, config);
    }


    public class T7847_SharedProductSku_Fixture : FixtureBase
    {
        public OrderIdModel PaypalOrderDetail { get; }

        public T7847_SharedProductSku_Fixture()
        {
            PaypalOrderDetail = OrderActions.GetAnOrderIdPlacedWithPayPal();
        }
    }


    /// <summary>
    /// Verify the layout of the Order History Page
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9646
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7847
    /// </summary>
    [Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9646"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7847")]

    public abstract class T7847_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7847_SharedProductSku_Fixture>
    {
        protected readonly T7847_SharedProductSku_Fixture Fixture;

        protected T7847_DesktopBase(ITestOutputHelper output, T7847_SharedProductSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            /* Arrangement
            Sign in as Employee
            Get and Paypal order detail*/
            Assert.DatabaseObject(Fixture.PaypalOrderDetail, "OrderActions.GetAnOrderIdPlacedWithPayPal();");

            // Act: Navigate to Order Details 
            Browser.Navigate(Urls.OrderHistoryPageUrl);

            // Act: Take screenshot of the full page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            // Act: Insert order table and check order status
            OrderHistory.CheckOrderStatus(Fixture.PaypalOrderDetail);

            // Act: Click on Order Track order Button
            OrderHistory.ClickOnTrackOrder();

            // Act: Switch the tab to focus on correct tab after clicking
            Browser.SwitchToCurrentWindow();
            OrderHistory.HandleShippingUpdatesModal();

            // Act: Capture the screenshot of the page 
            OrderHistory.WaitForMoreYouMayLikeWidget();
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Orderhistory.IgnoreMoreYouMayLike(), Orderhistory.IgnoreSimilarItem()});
        }
    }
}