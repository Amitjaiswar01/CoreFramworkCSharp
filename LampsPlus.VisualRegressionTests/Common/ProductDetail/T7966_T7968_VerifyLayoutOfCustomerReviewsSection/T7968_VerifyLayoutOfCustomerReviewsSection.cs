using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using Automation.Framework.Enums;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7966_T7968_VerifyLayoutOfCustomerReviewsSection
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7968_iPhone_VerifyLayoutOfCustomerReviewsSection : T7968_MobileBase
    {
        public T7968_iPhone_VerifyLayoutOfCustomerReviewsSection(ITestOutputHelper output, T7968_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7968_iPhone_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn : T7968_MobileBase
    {
        public T7968_iPhone_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7968_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7968_Android_VerifyLayoutOfCustomerReviewsSection : T7968_MobileBase
    {
        public T7968_Android_VerifyLayoutOfCustomerReviewsSection(ITestOutputHelper output, T7968_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7968_Android_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn : T7968_MobileBase
    {
        public T7968_Android_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7968_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7968_Emulator_VerifyLayoutOfCustomerReviewsSection : T7968_MobileBase
    {
        public T7968_Emulator_VerifyLayoutOfCustomerReviewsSection(ITestOutputHelper output, T7968_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7968_Emulator_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn : T7968_MobileBase
    {
        public T7968_Emulator_VerifyLayoutOfCustomerReviewsSectionForCustomerSignedIn(ITestOutputHelper output, T7968_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void VerifyLayoutOfCustomerReviewsSection(string config) => Validate(Validate, config);
    }


    public class T7968_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7968_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatQualifiesForReviews;
        }
    }


    /// <summary>
    /// Verify the Layout of the 'Customer Reviews' Section
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10767
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7968
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10767"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7968")]
    public abstract class T7968_MobileBase : VisualTestsBaseMobile, IClassFixture<T7968_SharedSkus_Fixture>
    {
        protected readonly T7968_SharedSkus_Fixture Fixture;

        protected T7968_MobileBase(ITestOutputHelper output, T7968_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: Identify a ShortSKU that has Customer Reviews section
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuThatQualifiesForReviews");

            //Act: Navigate to the PDP by ShortSku
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is Not on PDP Page");

            //Act: Open drawer labeled 'Reviews' and capture screenshot
            ProductDetail.ToggleProductReviewsSection();
            ProductDetail.GetTurnToReviewSection();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
            
            //Act: Scroll to first review and capture screenshot
            ProductDetail.FocusCustomerReviewsSection();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);

            //Act: Tap on 'Write a Review' button and capture screenshot
            ProductDetail.OpenReviewsModal();
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}