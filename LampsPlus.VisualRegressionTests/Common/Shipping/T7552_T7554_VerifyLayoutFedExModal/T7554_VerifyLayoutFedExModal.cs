using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Shipping.T7552_T7554_VerifyLayoutFedExModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7554_iPhone_VerifyLayoutFedExModal : T7554_MobileBase
    {
        public T7554_iPhone_VerifyLayoutFedExModal(ITestOutputHelper output, T7554_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutFedExModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7554_AndroidPhone_VerifyLayoutFedExModal : T7554_MobileBase
    {
        public T7554_AndroidPhone_VerifyLayoutFedExModal(ITestOutputHelper output, T7554_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutFedExModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7554_Emulator_VerifyLayoutFedExModal : T7554_MobileBase
    {
        public T7554_Emulator_VerifyLayoutFedExModal(ITestOutputHelper output, T7554_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutFedExModal(string config) => Validate(Validate, config);
    }


    public class T7554_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7554_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the FedEx Verification modal.
    /// Jira Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8631
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7554
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8631"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7554")]
    public abstract class T7554_MobileBase : VisualTestsBaseMobile, IClassFixture<T7554_SharedSku_Fixture>
    {
        protected readonly T7554_SharedSku_Fixture Fixture;

        protected T7554_MobileBase(ITestOutputHelper output, T7554_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            /* Arrange:
            1. Add any item to cart.
            2. Proceed to the Shipping page. 
            */
            InitializeVisualTest(config);
            ShoppingCartWorkflow.AddMultipleItemsToCart(Urls.ContemporaryFloorLampsSortPageUrl, 1);
            Cart.CheckOut();
            Assert.True(Shipping.IsCurrentPage, "Current page is not a Shipping page");

            //Act: Open FexEx modal
            ShoppingCartWorkflow.ShowFedExValidationModal(enterApartment: false);

            //Act: A screenshot is captured for the entire page.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}