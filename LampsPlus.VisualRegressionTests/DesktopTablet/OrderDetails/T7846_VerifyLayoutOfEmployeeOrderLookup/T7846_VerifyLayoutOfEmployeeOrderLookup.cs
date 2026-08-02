using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.OrderDetails.T7846_VerifyLayoutOfEmployeeOrderLookup
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7846_Windows_VerifyLayoutOfEmployeeOrderLookup : T7846_DesktopBase
    {
        public T7846_Windows_VerifyLayoutOfEmployeeOrderLookup(ITestOutputHelper output, T7846_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfEmployeeOrderLookup(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7846_Mac_VerifyLayoutOfEmployeeOrderLookup : T7846_DesktopBase
    {
        public T7846_Mac_VerifyLayoutOfEmployeeOrderLookup(ITestOutputHelper output, T7846_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [Theory(Skip = "This test should be retested manually during regression. Please use the test case in Adaptavist: T7846. Rework - ACD-10776")]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfEmployeeOrderLookup(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7846_iPad_VerifyLayoutOfEmployeeOrderLookup : T7846_DesktopBase
    {
        public T7846_iPad_VerifyLayoutOfEmployeeOrderLookup(ITestOutputHelper output, T7846_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfEmployeeOrderLookup(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7846_TabletEmulator_VerifyLayoutOfEmployeeOrderLookup : T7846_DesktopBase
    {
        public T7846_TabletEmulator_VerifyLayoutOfEmployeeOrderLookup(ITestOutputHelper output, T7846_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfEmployeeOrderLookup(string config) => Validate(Validate, config);
    }


    public class T7846_SharedSku_Fixture : FixtureBase
    {
        public OrderIdModel PaypalOrderDetail { get; }

        public T7846_SharedSku_Fixture()
        {
            PaypalOrderDetail = OrderActions.GetAnOrderIdPlacedWithPayPal();
        }
    }


    /// <summary>
    /// Verify the Layout of the Employee Order Lookup.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9645
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7846
    /// </summary>
    public abstract class T7846_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7846_SharedSku_Fixture>
    {
        protected readonly T7846_SharedSku_Fixture Fixture;

        protected T7846_DesktopBase(ITestOutputHelper output, T7846_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            InitializeVisualTest(config);

            /* Arrangement
            Sign in as Employee
            Get and Paypal order detail
            Navigate to Employee Tools */
            Browser.Navigate(Urls.EmployeeToolsPageUrl);
            Assert.DatabaseObject(Fixture.PaypalOrderDetail, "OrderActions.GetAnOrderIdPlacedWithPayPal();");

            /* Act: Click on My order Button */
            OrderLookup.NavigateToMyOrderPage();

            /* Act: Click on Email Dropdown Button */
            OrderLookup.OpenEmployeeEmailDropDown();

            /* Act: Take Screenshot of the visible page */
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /* Act: Click on Store radio Button */
            OrderLookup.OpenEmployeeStoreDropdown();

            /* Act: Take Screenshot of the visible page */
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            /* Act: Click on Find past order input field */
            OrderLookup.LocatePastOrders(Fixture.PaypalOrderDetail);

            /* Act: Take Screenshot of the full page */
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}
