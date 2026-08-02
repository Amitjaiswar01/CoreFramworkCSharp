using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Databases.Entities;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.OrderDetails.T7900_T7901_VerifyLayoutForRequestReturnPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7901_iPhone_VerifyLayoutForRequestReturnPage : T7901_MobileBase
    {
        public T7901_iPhone_VerifyLayoutForRequestReturnPage(ITestOutputHelper output, T7901_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutForRequestReturnPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7901_Android_VerifyLayoutForRequestReturnPage : T7901_MobileBase
    {
        public T7901_Android_VerifyLayoutForRequestReturnPage(ITestOutputHelper output, T7901_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutForRequestReturnPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7901_Emulator_VerifyLayoutForRequestReturnPage : T7901_MobileBase
    {
        public T7901_Emulator_VerifyLayoutForRequestReturnPage(ITestOutputHelper output, T7901_ShareOrderDetails_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutForRequestReturnPage(string config) => Validate(Validate, config);
    }


    public class T7901_ShareOrderDetails_Fixture : FixtureBase
    {
        public OrderIdModel Order { get; }

        public T7901_ShareOrderDetails_Fixture()
        {
            Order = OrderActions.GetAnOrderIdPlacedWithin60Days();
        }
    }


    /// <summary>
    /// Verify the Layout for Request a Return page from the Order Details Page.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10413
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7901
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10413"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7901")]
    public abstract class T7901_MobileBase : VisualTestsBaseMobile, IClassFixture<T7901_ShareOrderDetails_Fixture>
    {
        protected readonly T7901_ShareOrderDetails_Fixture Fixture;

        protected T7901_MobileBase(ITestOutputHelper output, T7901_ShareOrderDetails_Fixture fixture) : base(output, fixture)
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
