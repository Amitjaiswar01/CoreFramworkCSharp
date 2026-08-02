using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.Payment;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.CartOverview.T7343_T7348_VerifyLayoutOfCartOverviewPageWhenUsingAPromoCode
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7348_iPhone_VerifyLayoutCartPromoCode : T7348_MobileBase
    {
        public T7348_iPhone_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7348_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7348_AndroidPhone_VerifyLayoutCartPromoCode : T7348_MobileBase
    {
        public T7348_AndroidPhone_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7348_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7348_Emulator_VerifyLayoutCartPromoCode : T7348_MobileBase
    {
        public T7348_Emulator_VerifyLayoutCartPromoCode(ITestOutputHelper output, T7348_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutCartPromoCode(string config) => Validate(Validate, config);
    }


    public class T7348_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku1 { get; }

        public T7348_SharedSkus_Fixture()
        {
            ShortSku1 = ProductActions.GetShortSkuThatMeetsMinimumOrder;
        }
    }


    /// <summary>
    /// Verify Layout Of CartOverview Page When Using A PromoCode.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9781
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7348
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9781"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7348")]

    public abstract class T7348_MobileBase : VisualTestsBaseMobile, IClassFixture<T7348_SharedSkus_Fixture>
    { 
        protected readonly T7348_SharedSkus_Fixture Fixture;

        protected T7348_MobileBase(ITestOutputHelper output, T7348_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrangement: User has added the SKU item to the cart.
            InitializeVisualTest(config);
            Assert.DatabaseObject(Fixture.ShortSku1, "ProductionActions.GetShortSkuThatMeetsMinimumOrder");
            ProductDetail.AddSingleProductToCart(Fixture.ShortSku1);
            Assert.True(Cart.IsCurrentPage, "Current page is not Cart page");
            Cart.ScrollToPromoCodeSection();

            /* Act:
            Click on the 'Add Promo Code' link on the Cart Overview page 
            Capture a screenshot of the Visible Screen.
             */
            Cart.OpenPromoCodeEntryField();
            Cart.IsPromoCodeTextFieldVisible();
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, true, Cart.IgnoreCartId(), 10);

            /* Act:
            Enter 1234 in the promo code field
            Capture a screenshot of the Visible Screen.
            */
            var invalidPromoCode = Cart.GetInvalidPromoCodeValue();
            Cart.UpdatePromoCode(invalidPromoCode);
            Cart.ScrollToPromoCodeSection();
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, true, Cart.IgnoreCartId(), 10);

            /* Act:
            Enter AutoPromoCodeTest in the promo code field
            Capture a screenshot of the Visible Screen.
            */
            Cart.ClearPromoCode();
            Cart.UpdatePromoCode(PromoCodeList.AutoPromoCodeTest.Name);
            Cart.ScrollToPromoCodeSection();
            Assert.True(Cart.IsPromoCodeMessageVisible(), "Promo code message is not visible.");
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Cart.IgnoreCartIdAndMoreYouMayLike(), true, true, Cart.IgnoreCartId(), 08);
        }
    }
}