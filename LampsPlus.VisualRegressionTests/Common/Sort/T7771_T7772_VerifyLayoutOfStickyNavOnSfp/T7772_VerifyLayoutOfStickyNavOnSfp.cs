using Xunit;
using Xunit.Priority;
using Xunit.Abstractions;
using xRetry;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.Sort.T7771_T7772_VerifyLayoutOfStickyNavOnSfp
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7772_iPhone_VerifyLayoutOfStickyNavOnSfp : T7772_MobileBase
    {
        public T7772_iPhone_VerifyLayoutOfStickyNavOnSfp(ITestOutputHelper output, T7772_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfStickyNavOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7772_AndroidPhone_VerifyLayoutOfStickyNavOnSfp : T7772_MobileBase
    {
        public T7772_AndroidPhone_VerifyLayoutOfStickyNavOnSfp(ITestOutputHelper output, T7772_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfStickyNavOnSfp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7772_Emulator_VerifyLayoutOfStickyNavOnSfp : T7772_MobileBase
    {
        public T7772_Emulator_VerifyLayoutOfStickyNavOnSfp(ITestOutputHelper output, T7772_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfStickyNavOnSfp(string config) => Validate(Validate, config);
    }


    public class T7772_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }
        public T7772_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    /// <summary>
    /// Verify the layout of the Sticky Nav on the SFP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9885
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7772
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9885"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7772")]
    public abstract class T7772_MobileBase : VisualTestsBaseMobile, IClassFixture<T7772_SharedSkus_Fixture>
    {
        protected readonly T7772_SharedSkus_Fixture Fixture;

        protected T7772_MobileBase(ITestOutputHelper output, T7772_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange : Identify a SKU and navigate to its SFP page
            InitializeVisualTest(config);

            var Sku = Fixture.ShortSku;
            Browser.Navigate(Urls.ProductFullPageBaseUrl + Sku);

            // Act : Scroll down view the Sticky Nav
            Browser.ScrollToBottomOfPage(Browser.PageUrl);

            // Act: Capture Screenshot of visible screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, Sort.IgnoreFooterContainer());
        }
    }
}
