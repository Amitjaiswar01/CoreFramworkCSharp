using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;

namespace LampsPlus.VisualRegressionTests.Common.OrderDetails.T7650_T7651_VerifyLayoutOrderDetailsForOpenBoxOrder
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7650_Windows_VerifyLayoutOrderDetailsForOpenBoxOrder : T7650_DesktopBase
    {
        public T7650_Windows_VerifyLayoutOrderDetailsForOpenBoxOrder(ITestOutputHelper output, T7650_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOrderDetailsOpenBoxOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7650_Mac_VerifyLayoutOrderDetailsForOpenBoxOrder : T7650_DesktopBase
    {
        public T7650_Mac_VerifyLayoutOrderDetailsForOpenBoxOrder(ITestOutputHelper output, T7650_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOrderDetailsOpenBoxOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7650_iPad_VerifyLayoutOrderDetailsForOpenBoxOrder : T7650_DesktopBase
    {
        public T7650_iPad_VerifyLayoutOrderDetailsForOpenBoxOrder(ITestOutputHelper output, T7650_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOrderDetailsOpenBoxOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7650_TabletEmulator_VerifyLayoutOrderDetailsForOpenBoxOrder : T7650_DesktopBase
    {
        public T7650_TabletEmulator_VerifyLayoutOrderDetailsForOpenBoxOrder(ITestOutputHelper output, T7650_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOrderDetailsOpenBoxOrder(string config) => Validate(Validate, config);
    }


    public class T7650_ShareOrderDetails_Fixture : FixtureBase
    {
        public OrderIdModel Order { get; }

        public T7650_ShareOrderDetails_Fixture()
        {
            Order = OrderActions.GetOpenBoxOrder();
        }
    }


    /// <summary>
    /// Verify the layout of the Order Details page for an Open Box order.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9810
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7650
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9810"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7650")]
    public abstract class T7650_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7650_ShareOrderDetails_Fixture>
    {
        protected readonly T7650_ShareOrderDetails_Fixture Fixture;

        protected T7650_DesktopBase(ITestOutputHelper output, T7650_ShareOrderDetails_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified existing order from the Query
            InitializeVisualTest(config);
            var orderDetails = Fixture.Order;

            //Act : Navigate to Order History Page
            Browser.Navigate(Urls.OrderHistoryPageUrl);

            //Act : Enter the OrderID and associated email into the correct fields and click the 'Check Status' button
            OrderHistory.CheckOrderStatus(orderDetails);

            //Act : Capture Screenshot of the entire page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement>{OrderDetails.IgnoreMoreYouMayLikeSection()});
        }
    }
}
