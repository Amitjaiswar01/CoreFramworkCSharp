using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Payment.T7391_T7392_VerifyLayoutOfPaymentPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7392_iPhone_VerifyTheLayoutOfPaymentPage : T7392_MobileBase
    {
        public T7392_iPhone_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7392_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7392_AndroidPhone_VerifyTheLayoutOfPaymentPage : T7392_MobileBase
    {
        public T7392_AndroidPhone_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7392_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7392_Emulator_VerifyTheLayoutOfPaymentPage : T7392_MobileBase
    {
        public T7392_Emulator_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7392_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    public class T7392_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7392_Fixture()
        {
            ShortSku = ProductActions.GetShortSkuThatMeetsMinimumOrder;
        }
    }


    /// <summary>
    /// Verify the Layout of the Payment Page and Field Validation.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10855
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7392
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10855"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7392")]
    public abstract class T7392_MobileBase : VisualTestsBaseMobile, IClassFixture<T7392_Fixture>
    {
        protected readonly T7392_Fixture Fixture;

        protected T7392_MobileBase(ITestOutputHelper output, T7392_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            //Arrangement : User has identified a SKU and added it to the cart
            InitializeVisualTest(config);

            var sku = Fixture.ShortSku;

            /* Act: Fill out the Shipping information with a California address.
            User proceeds to the Payment page.
            */
            ShoppingCartWorkflow.ProceedToPaymentWithSingleProduct(sku);

            //Act: Uncheck the Same as shipping box
            Payment.SelectSameAsShippingCheckbox();

            //Act: Capture a screenshot of the modal element
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture,  true);
        }
    }
}