using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.OrderDetails
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T7427_Window_VerifyLayoutOfEmployeeToolsAndOrderHistoryPageForEmployees : T7427_DesktopBase
    {
        public T7427_Window_VerifyLayoutOfEmployeeToolsAndOrderHistoryPageForEmployees(ITestOutputHelper output, T7427_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_ESI)]
        public void LayoutOfEmployeeToolsAndOrderHistoryPageForEmployees(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T7427_Mac_VerifyLayoutOfEmployeeToolsAndOrderHistoryPageForEmployees : T7427_DesktopBase
    {
        public T7427_Mac_VerifyLayoutOfEmployeeToolsAndOrderHistoryPageForEmployees(ITestOutputHelper output, T7427_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_ESI)]
        public void LayoutOfEmployeeToolsAndOrderHistoryPageForEmployees(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T7427_iPad_VerifyLayoutOfEmployeeToolsAndOrderHistoryPageForEmployees : T7427_DesktopBase
    {
        public T7427_iPad_VerifyLayoutOfEmployeeToolsAndOrderHistoryPageForEmployees(ITestOutputHelper output, T7427_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_ESI)]
        public void LayoutOfEmployeeToolsAndOrderHistoryPageForEmployees(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [Trait(LpTraits.Keys.Category, LpTraits.RunEnvironment.ProductionOnly)]
    public class T7427_TabletEmulator_VerifyLayoutOfEmployeeToolsAndOrderHistoryPageForEmployees : T7427_DesktopBase
    {
        public T7427_TabletEmulator_VerifyLayoutOfEmployeeToolsAndOrderHistoryPageForEmployees(ITestOutputHelper output, T7427_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_ESI)]
        public void LayoutOfEmployeeToolsAndOrderHistoryPageForEmployees(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Employee Tools and Order History page for employees.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7596
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7427
    /// </summary>
    //[Collection(LpTraits.UserRole.Employee)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7596"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7427")]

    public abstract class T7427_DesktopBase : T7427_Base
    {
        protected T7427_DesktopBase(ITestOutputHelper output, T7427_SharedSku_Fixture fixture) : base(output, fixture) { }
    }


    public class T7427_SharedSku_Fixture : FixtureBase
    {
        public OrderIdModel PaypalOrder { get; }

        public T7427_SharedSku_Fixture()
        {
            PaypalOrder = OrderActions.GetAnOrderIdPlacedWithPayPal();
        }
    }


    public abstract class T7427_Base : VisualTestsBase, IClassFixture<T7427_SharedSku_Fixture>
    {
        protected readonly T7427_SharedSku_Fixture Fixture;

        protected T7427_Base(ITestOutputHelper output, T7427_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config, Urls.EmployeeToolsPageUrl);
            
            Browser.Wait.ForDisplayedElement(EmployeeTools.QuickAddToCartElement);

            //Capture the Employee Tools page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}
