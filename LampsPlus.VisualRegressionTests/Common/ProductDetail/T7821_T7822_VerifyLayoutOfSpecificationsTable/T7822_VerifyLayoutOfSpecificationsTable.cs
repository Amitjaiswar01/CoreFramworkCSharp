using Automation.Framework.Enums;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7821_T7822_VerifyLayoutOfSpecificationsTable
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7822_iPhone_VerifyLayoutOfSpecificationsTables : T7822_MobileBase
    {
        public T7822_iPhone_VerifyLayoutOfSpecificationsTables(ITestOutputHelper output, T7822_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfSpecificationsTables(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7822_AndroidPhone_VerifyLayoutOfSpecificationsTables : T7822_MobileBase
    {
        public T7822_AndroidPhone_VerifyLayoutOfSpecificationsTables(ITestOutputHelper output, T7822_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfSpecificationsTables(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7822_Emulator_VerifyLayoutOfSpecificationsTables : T7822_MobileBase
    {
        public T7822_Emulator_VerifyLayoutOfSpecificationsTables(ITestOutputHelper output, T7822_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void VerifyLayoutOfSpecificationsTables(string config) => Validate(Validate, config);
    }


    public class T7822_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7822_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetSkuThatHasSpecificationsTables;
        }
    }


    /// <summary>
    /// Verify the Layout of the Specifications Tables Displayed on PDP 
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9846
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7822
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9846"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7822")]
    public abstract class T7822_MobileBase : VisualTestsBaseMobile, IClassFixture<T7822_SharedSku_Fixture>
    {
        protected readonly T7822_SharedSku_Fixture Fixture;

        protected T7822_MobileBase(ITestOutputHelper output, T7822_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a SKU that has a Specification table.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetSkuThatHasSpecificationsTables()");

            /*Act:
             Navigate to the PDP https://www.lampsplus.com/products/<SKU>.
             Once the PDP loads, scroll down the page to the Specifications section.
            */
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            ProductDetail.OpenSpecificationTableDrawer();

            //Act: Capture the visual screen.
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }
}
