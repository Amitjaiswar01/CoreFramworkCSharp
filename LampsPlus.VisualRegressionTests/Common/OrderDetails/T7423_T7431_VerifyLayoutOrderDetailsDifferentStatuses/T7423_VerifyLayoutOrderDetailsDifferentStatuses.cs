using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.VisualRegressionTests.Common.OrderDetails.T7423_T7431_VerifyLayoutOrderDetailsDifferentStatuses
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7423_Windows_VerifyLayoutOrderDetailsDifferentStatuses : T7423_DesktopBase
    {
        public T7423_Windows_VerifyLayoutOrderDetailsDifferentStatuses(ITestOutputHelper output, T7423_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOrderDetailsDifferentStatuses(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7423_Mac_VerifyLayoutOrderDetailsDifferentStatuses : T7423_DesktopBase
    {
        public T7423_Mac_VerifyLayoutOrderDetailsDifferentStatuses(ITestOutputHelper output, T7423_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOrderDetailsDifferentStatuses(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7423_iPad_VerifyLayoutOrderDetailsDifferentStatuses : T7423_DesktopBase
    {
        public T7423_iPad_VerifyLayoutOrderDetailsDifferentStatuses(ITestOutputHelper output, T7423_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOrderDetailsDifferentStatuses(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7423_TabletEmulator_VerifyLayoutOrderDetailsDifferentStatuses : T7423_DesktopBase
    {
        public T7423_TabletEmulator_VerifyLayoutOrderDetailsDifferentStatuses(ITestOutputHelper output, T7423_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOrderDetailsDifferentStatuses(string config) => Validate(Validate, config);
    }


    public class T7423_ShareOrderDetails_Fixture : FixtureBase
    {
        public List<OrderIdModel> Orders { get; }

        public T7423_ShareOrderDetails_Fixture()
        {
            Orders = OrderActions.GetOrderForTheEachStatus();
        }
    }


    /// <summary>
    /// Verify the layout of the Order Details page for orders with different statuses
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9811
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7423
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9811"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7423")]
    public abstract class T7423_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7423_ShareOrderDetails_Fixture>
    {
        protected readonly T7423_ShareOrderDetails_Fixture Fixture;

        protected T7423_DesktopBase(ITestOutputHelper output, T7423_ShareOrderDetails_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified orders that cover 4 different statuses: Shipped, Cancelled, Picked Up, Unshipped.
            InitializeVisualTest(config);
            var orders = Fixture.Orders;
            Assert.True(orders.Count == 4, "OrderActions.GetOrderForEachStatus() didn't return a result for each of the four status");

            //Act : Perform below Action for Orders with each Status i.e. Shipped, Cancelled, Picked-Up, Unshipped.
            foreach (var order in orders)
            {
                //Act : Navigate to Order History Page
                OrderHistory.Navigate();

                //Act : Search for the Order by Entering Order Id and Email
                OrderHistory.CheckOrderStatus(order);

                //Act : Capture Screenshot of the Entire Page 
                ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
            }
        }
    }
}
