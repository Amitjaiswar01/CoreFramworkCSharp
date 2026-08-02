using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7278_T7301_VerifyLayoutOfQuestionsAnswersSection
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7301_iPhone_VerifyLayoutOfQuestionsAnswersSection : T7301_MobileBase
    {
        public T7301_iPhone_VerifyLayoutOfQuestionsAnswersSection(ITestOutputHelper output, T7301_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7301_iPhone_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn : T7301_MobileBase
    {
        public T7301_iPhone_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn(ITestOutputHelper output, T7301_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_NPCSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7301_Android_VerifyLayoutOfQuestionsAnswersSection : T7301_MobileBase
    {
        public T7301_Android_VerifyLayoutOfQuestionsAnswersSection(ITestOutputHelper output, T7301_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7301_Android_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn : T7301_MobileBase
    {
        public T7301_Android_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn(ITestOutputHelper output, T7301_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_NPCSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7301_Emulator_VerifyLayoutOfQuestionsAnswersSection : T7301_MobileBase
    {
        public T7301_Emulator_VerifyLayoutOfQuestionsAnswersSection(ITestOutputHelper output, T7301_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7301_Emulator_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn : T7301_MobileBase
    {
        public T7301_Emulator_VerifyLayoutOfQuestionsAnswersSectionForCustomerSignedIn(ITestOutputHelper output, T7301_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_NPCSI)]
        public void VerifyLayoutOfQuestionsAnswersSection(string config) => Validate(Validate, config);
    }


    public class T7301_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7301_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatQualifiesForReviews;
        }
    }


    /// <summary>
    /// Verify the Layout of the 'Q & A' Section
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10766
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7301
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10766"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7301")]
    public abstract class T7301_MobileBase : VisualTestsBaseMobile, IClassFixture<T7301_SharedSkus_Fixture>
    {
        protected readonly T7301_SharedSkus_Fixture Fixture;

        protected T7301_MobileBase(ITestOutputHelper output, T7301_SharedSkus_Fixture fixture) : base(output, fixture)
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

            //Act: Open 'Q & A' section and capture a screenshot of the 'Q & A' section element.
            ProductDetail.ToggleTurnToQuestionsAndAnswersSection();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetTurnToQuestionsAndAnswersSection());

            //Act: Enter Question in 'Ask A Question' section and capture screenshot
            ProductDetail.QnASearchByText("What color is it?");
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetTurnToQuestionsAndAnswersSection());
            
            //Act: Enter Question in 'Ask A Question' section and capture screenshot
            ProductDetail.GetFirstResultFromAskQuestionSection();
            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetail.GetTurnToQuestionsAndAnswersSection());
        }
    }
}