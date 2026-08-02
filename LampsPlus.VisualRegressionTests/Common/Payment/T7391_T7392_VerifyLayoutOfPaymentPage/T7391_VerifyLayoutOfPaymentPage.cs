using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Payment.T7391_T7392_VerifyLayoutOfPaymentPage
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7391_Windows_VerifyTheLayoutOfPaymentPage : DesktopBase
    {
        public T7391_Windows_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7391_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7391_Mac_VerifyTheLayoutOfPaymentPage : DesktopBase
    {
        public T7391_Mac_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7391_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7391_iPad_VerifyTheLayoutOfPaymentPage : DesktopBase
    {
        public T7391_iPad_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7391_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7391_TabletEmulator_VerifyTheLayoutOfPaymentPage : DesktopBase
    {
        public T7391_TabletEmulator_VerifyTheLayoutOfPaymentPage(ITestOutputHelper output, T7391_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfPaymentPage(string config) => Validate(Validate, config);
    }


    public class T7391_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7391_Fixture()
        {
            ShortSku = ProductActions.GetShortSkuThatMeetsMinimumOrder;
        }
    }


    /// <summary>
    /// Verify the Layout of the Payment Page and Field Validation.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9834
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7391
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9834"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7391")]
    public abstract class DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7391_Fixture>
    {
        protected readonly T7391_Fixture Fixture;

        protected DesktopBase(ITestOutputHelper output, T7391_Fixture fixture) : base(output, fixture)
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
            ScreenCapturer.CaptureScreen(Browser.PageUrl,ScreenshotType.FullPageCapture);
        }
    }
}
