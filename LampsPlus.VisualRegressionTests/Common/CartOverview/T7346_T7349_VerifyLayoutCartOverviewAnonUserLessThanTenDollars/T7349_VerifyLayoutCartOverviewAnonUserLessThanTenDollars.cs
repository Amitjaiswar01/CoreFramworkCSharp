using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;


namespace LampsPlus.VisualRegressionTests.Common.CartOverview.T7346_T7349_VerifyLayoutCartOverviewAnonUserLessThanTenDollars
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7349_iPhone_VerifyLayoutCartOverViewLessThanTenDollars : T7349_MobileBase
    {
        public T7349_iPhone_VerifyLayoutCartOverViewLessThanTenDollars(ITestOutputHelper output, T7349_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutCartOverViewLessThanTenDollars(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7349_AndroidPhone_VerifyLayoutCartOverViewLessThanTenDollars : T7349_MobileBase
    {
        public T7349_AndroidPhone_VerifyLayoutCartOverViewLessThanTenDollars(ITestOutputHelper output, T7349_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutCartOverViewLessThanTenDollars(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7349_Emulator_VerifyLayoutCartOverViewLessThanTenDollars : T7349_MobileBase
    {
        public T7349_Emulator_VerifyLayoutCartOverViewLessThanTenDollars(ITestOutputHelper output, T7349_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutCartOverViewLessThanTenDollars(string config) => Validate(Validate, config);
    }


    public class T7349_ShareSku_Fixture : FixtureBase
    {
        public string LessThanTenDollarsShortSku { get; }

        public T7349_ShareSku_Fixture()
        {
            LessThanTenDollarsShortSku = ProductActions.GetLessThanTenDollarItem;
        }
    }

    /// <summary>
    /// Verify the layout of the Cart Overview page when an Anonymous user has less than $10 in the cart.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9782
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7349
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9782"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7349")]
    public abstract class T7349_MobileBase : VisualTestsBaseMobile, IClassFixture<T7349_ShareSku_Fixture>
    {
        protected readonly T7349_ShareSku_Fixture Fixture;

        protected T7349_MobileBase(ITestOutputHelper output, T7349_ShareSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            // Arrange: Add less than $10 item to Cart
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.LessThanTenDollarsShortSku, "ProductActions.GetLessThanTenDollarItem()");
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel { Sku = Fixture.LessThanTenDollarsShortSku});

            // Act: Capture a screenshot of the entire page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, true, Cart.IgnoreCartId(), 5, 5, 5, 5);
        }
    }
}