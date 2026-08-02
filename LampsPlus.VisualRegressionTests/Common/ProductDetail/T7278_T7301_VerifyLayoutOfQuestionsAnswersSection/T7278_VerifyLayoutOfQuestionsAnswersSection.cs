using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7278_T7301_VerifyLayoutOfQuestionsAnswersSection
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7278_Window_VerifyLayoutOfQuestionsAnswersSection : T7278_DesktopBase
    {
        public T7278_Window_VerifyLayoutOfQuestionsAnswersSection(ITestOutputHelper output, T7278_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7278_Window_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn : T7278_DesktopBase
    {
        public T7278_Window_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn(ITestOutputHelper output, T7278_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7278_Mac_VerifyLayoutOfQuestionsAnswersSection : T7278_DesktopBase
    {
        public T7278_Mac_VerifyLayoutOfQuestionsAnswersSection(ITestOutputHelper output, T7278_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7278_Mac_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn : T7278_DesktopBase
    {
        public T7278_Mac_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn(ITestOutputHelper output, T7278_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7278_iPad_VerifyLayoutOfQuestionsAnswersSection : T7278_DesktopBase
    {
        public T7278_iPad_VerifyLayoutOfQuestionsAnswersSection(ITestOutputHelper output, T7278_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7278_iPad_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn : T7278_DesktopBase
    {
        public T7278_iPad_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn(ITestOutputHelper output, T7278_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7278_TabletEmulator_VerifyLayoutOfQuestionsAnswersSection : T7278_DesktopBase
    {
        public T7278_TabletEmulator_VerifyLayoutOfQuestionsAnswersSection(ITestOutputHelper output, T7278_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7278_TabletEmulator_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn : T7278_DesktopBase
    {
        public T7278_TabletEmulator_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn(ITestOutputHelper output, T7278_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_NPCSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    public class T7278_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7278_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatQualifiesForReviews;
        }
    }


    /// <summary>
    /// Verify the Layout of the 'Q & A' Section
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10766
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7278
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10766"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7278")]
    public abstract class T7278_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7278_SharedSkus_Fixture>
    {
        protected readonly T7278_SharedSkus_Fixture Fixture;

        protected T7278_DesktopBase(ITestOutputHelper output, T7278_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: Identify a ShortSKU that has 'Q & A' section
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuThatQualifiesForReviews");

            //Act: Navigate to the PDP by shortSku.
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is Not on PDP Page");

            //Act: Scroll down to 'Q & A' section, capture a screenshot of the 'Q & A' section element.
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetTurnToQuestionsAndAnswersSection());

            //Act: Enter Question in 'Ask A Question' section and capture screenshot
            ProductDetail.QnASearchByText("What color is it?");
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetTurnToQuestionsAndAnswersSection());

            //Act: Select one of Result from 'Ask A Question' section and capture screenshot
            ProductDetail.GetFirstResultFromAskQuestionSection();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetTurnToQuestionsAndAnswersSection());
        }
    }
}