using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;


namespace LampsPlus.VisualRegressionTests.Common.CartOverview.T7331_T7347_VerifyLayoutOfCartOverviewEmailAndThankYouModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7347_iPhone_VerifyLayoutOfCartOverviewEmailAndThankYouModal : T7347_MobileBase
    {
        public T7347_iPhone_VerifyLayoutOfCartOverviewEmailAndThankYouModal(ITestOutputHelper output, T7347_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfCartOverviewEmailAndThankYouModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7347_AndroidPhone_VerifyLayoutOfCartOverviewEmailAndThankYouModal : T7347_MobileBase
    {
        public T7347_AndroidPhone_VerifyLayoutOfCartOverviewEmailAndThankYouModal(ITestOutputHelper output, T7347_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfCartOverviewEmailAndThankYouModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7347_Emulator_VerifyLayoutOfCartOverviewEmailAndThankYouModal : T7347_MobileBase
    {
        public T7347_Emulator_VerifyLayoutOfCartOverviewEmailAndThankYouModal(ITestOutputHelper output, T7347_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfCartOverviewEmailAndThankYouModal(string config) => Validate(Validate, config);
    }


    public class T7347_ShareSkus_Fixture : FixtureBase
    {
        public string RegularPricedShortSku { get; }
        public string SaleShortSku { get; }

        public T7347_ShareSkus_Fixture()
        {
            RegularPricedShortSku = ProductActions.GetItemNotOnSale;
            SaleShortSku = ProductActions.GetShortSkuOnClearance;
        }
    }

    /// <summary>
    /// Verify the Layout of Cart Overview Email And Thank You Modal
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9780
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7347
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9780"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7347")]
    public abstract class T7347_MobileBase : VisualTestsBaseMobile, IClassFixture<T7347_ShareSkus_Fixture>
    {
        protected readonly T7347_ShareSkus_Fixture Fixture;

        protected T7347_MobileBase(ITestOutputHelper output, T7347_ShareSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            /*Arrangement:
            Navigate to any Regular Products and Add them to the cart
            Navigate to any Sale Products and Add them to the cart
            */
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.RegularPricedShortSku, "ProductActions.GetItemNotOnSale()");
            Assert.DatabaseObject(Fixture.SaleShortSku, "ProductActions.GetShortSkuOnClearance()");

            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.RegularPricedShortSku));
            ShoppingCartWorkflow.AddItemsToCartBySku(new ProductModel(Fixture.SaleShortSku));

            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");

            //Act:Capture the cart overview page
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, floating: Cart.IgnoreCartId(), maxDownOffset: 10, maxRightOffset: 10);

            Cart.OpenAndFocusEmailModal();

            //Act:Capture the cart overview page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);

            Cart.CloseEmailModal();

            Cart.EmailShoppingCart("testingLP1@mailinator.com", "testingLP2@mailinator.com", "testingLP3@mailinator.com");
            Browser.SwitchToDefaultContent();

            //Act:Capture the cart overview page
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}
