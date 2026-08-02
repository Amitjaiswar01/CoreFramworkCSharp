using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.OrderDetails.T7650_T7651_VerifyLayoutOrderDetailsForOpenBoxOrder
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7651_iPhone_VerifyLayoutOrderDetailsForOpenBoxOrder : T7651_MobileBase
    {
        public T7651_iPhone_VerifyLayoutOrderDetailsForOpenBoxOrder(ITestOutputHelper output, T7651_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOrderDetailsOpenBoxOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7651_Android_VerifyLayoutOrderDetailsForOpenBoxOrder : T7651_MobileBase
    {
        public T7651_Android_VerifyLayoutOrderDetailsForOpenBoxOrder(ITestOutputHelper output, T7651_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOrderDetailsOpenBoxOrder(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7651_Emulator_VerifyLayoutOrderDetailsForOpenBoxOrder : T7651_MobileBase
    {
        public T7651_Emulator_VerifyLayoutOrderDetailsForOpenBoxOrder(ITestOutputHelper output, T7651_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOrderDetailsOpenBoxOrder(string config) => Validate(Validate, config);
    }


    public class T7651_ShareOrderDetails_Fixture : FixtureBase
    {
        public OrderIdModel Order { get; }

        public T7651_ShareOrderDetails_Fixture()
        {
            Order = OrderActions.GetOpenBoxOrder();
        }
    }


    /// <summary>
    /// Verify the layout of the Order Details page for an Open Box order.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9810
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7651
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9810"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7651")]
    public abstract class T7651_MobileBase : VisualTestsBaseMobile, IClassFixture<T7651_ShareOrderDetails_Fixture>
    {
        protected readonly T7651_ShareOrderDetails_Fixture Fixture;

        protected T7651_MobileBase(ITestOutputHelper output, T7651_ShareOrderDetails_Fixture fixture) : base(output, fixture)
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
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { OrderDetails.IgnoreMoreYouMayLikeSection() }, true, true);
        }
    }
}
