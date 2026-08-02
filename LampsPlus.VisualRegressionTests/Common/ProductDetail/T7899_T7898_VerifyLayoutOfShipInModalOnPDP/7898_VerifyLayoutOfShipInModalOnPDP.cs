using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7899_T7898_VerifyLayoutOfShipInModalOnPDP
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7898_iPhone_VerifyLayoutOfShipInModal : T7898_MobileBase
    {
        public T7898_iPhone_VerifyLayoutOfShipInModal(ITestOutputHelper output, T7898_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyShipInModal(string config) => Validate(Validate, config);
    }


    public class T7898_Android_VerifyLayoutOfShipInModal : T7898_MobileBase
    {
        public T7898_Android_VerifyLayoutOfShipInModal(ITestOutputHelper output, T7898_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyShipInModal(string config) => Validate(Validate, config);
    }


    public class T7898_Emulator_VerifyLayoutOfShipInModal : T7898_MobileBase
    {
        public T7898_Emulator_VerifyLayoutOfShipInModal(ITestOutputHelper output, T7898_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyShipInModal(string config) => Validate(Validate, config);
    }

    public class T7898_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7898_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuWithShipInOption;
        }
    }


    /// <summary>
    /// Verify the layout of the Ships In Modal on the PDP
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10370
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7898
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10370"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7898")]
    public abstract class T7898_MobileBase : VisualTestsBaseMobile, IClassFixture<T7898_SharedSkus_Fixture>
    {
        protected readonly T7898_SharedSkus_Fixture Fixture;

        protected T7898_MobileBase(ITestOutputHelper output, T7898_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            ///Arrange : Find the sku that has Ship in option
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuThatQualifiesForReviews");

            // Act : Navigate to the PDP by shortSku. and Open the ship in modal
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is Not on PDP Page");
            ProductDetail.OpenShipInModal();

            // Act : Capture the screenshot of the visible screen
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.FullPageCapture);
        }
    }
}

