using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;
using LampsPlus.AutomationFramework.Databases.Entities;

namespace LampsPlus.VisualRegressionTests.DesktopTablet.Sort
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7829_Windows_VerifyLayoutOfLampsPlusChoiceCallout : T7829_DesktopBase
    {
        public T7829_Windows_VerifyLayoutOfLampsPlusChoiceCallout(ITestOutputHelper output, T7829_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_EasyAsk)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline_ElasticSearch)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_ElasticSearch)]
        public void LayoutOfLampsPlusChoiceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7829_Mac_VerifyLayoutOfLampsPlusChoiceCallout : T7829_DesktopBase
    {
        public T7829_Mac_VerifyLayoutOfLampsPlusChoiceCallout(ITestOutputHelper output, T7829_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfLampsPlusChoiceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7829_iPad_VerifyLayoutOfLampsPlusChoiceCallout : T7829_DesktopBase
    {
        public T7829_iPad_VerifyLayoutOfLampsPlusChoiceCallout(ITestOutputHelper output, T7829_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfLampsPlusChoiceCallout(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7829_TabletEmulator_VerifyLayoutOfLampsPlusChoiceCallout : T7829_DesktopBase
    {
        public T7829_TabletEmulator_VerifyLayoutOfLampsPlusChoiceCallout(ITestOutputHelper output, T7829_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfLampsPlusChoiceCallout(string config) => Validate(Validate, config);
    }


    public class T7829_SharedSku_Fixture : FixtureBase 
    {
        public ProductModel LampsPlusChoiceCallout { get; }

        public T7829_SharedSku_Fixture()
        {
            LampsPlusChoiceCallout = ProductActions.GetLampsPlusChoiceSku();
        }
    }


    /// <summary>
    /// Verify the Layout of the 'Lamps Plus Choice' Callout.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9582
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7829
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7501"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7829")]
    public abstract class T7829_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7829_SharedSku_Fixture>
    {
        protected readonly T7829_SharedSku_Fixture Fixture;

        protected T7829_DesktopBase(ITestOutputHelper output, T7829_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected virtual void Validate(string config)
        {
            /*Arrange: 
            Find a SKU that has the Lamps Plus Choice callout.
            Use the Category and Product Name returned from query.
            */
            InitializeVisualTest(config);
            var lampsPlusChoiceProductData = Fixture.LampsPlusChoiceCallout;
            var productCategory = lampsPlusChoiceProductData.Category;
            var productFinish = lampsPlusChoiceProductData.Finish;
            var productStyle = lampsPlusChoiceProductData.Style;
            var productUsage = lampsPlusChoiceProductData.Usage;
            var productType = lampsPlusChoiceProductData.Type;

            //Act:Navigate to the Sort page which has product category and attributes.
            Sort.Navigate(productCategory + "/" + productFinish + "/" + productStyle + "/" + productUsage + "/" + productType);

            //Act: Scroll down until first product with the Lamps Plus Choice callout.
            Sort.ScrollDownToCallout("Lamps Plus Choice");

            //Act: Capture a screenshot of the visible screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}