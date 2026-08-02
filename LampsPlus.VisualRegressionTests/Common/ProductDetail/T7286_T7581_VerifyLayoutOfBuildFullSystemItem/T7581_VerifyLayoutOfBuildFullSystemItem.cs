using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7286_T7581_VerifyLayoutOfBuildFullSystemItem
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7581_iPhone_VerifyLayoutOfBuildFullSystemItem : T7581_MobileBase
    {
        public T7581_iPhone_VerifyLayoutOfBuildFullSystemItem(ITestOutputHelper output, T7581_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfBuildFullSystemItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7581_Android_VerifyLayoutOfBuildFullSystemItem : T7581_MobileBase
    {
        public T7581_Android_VerifyLayoutOfBuildFullSystemItem(ITestOutputHelper output, T7581_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfBuildFullSystemItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7581_Emulator_VerifyLayoutOfBuildFullSystemItem : T7581_MobileBase
    {
        public T7581_Emulator_VerifyLayoutOfBuildFullSystemItem(ITestOutputHelper output, T7581_ShareSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfBuildFullSystemItem(string config) => Validate(Validate, config);
    }


    public class T7581_ShareSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7581_ShareSku_Fixture()
        {
            ShortSku = ProductActions.GetProductWithBuildFullSystemSkus().PrimarySku;
        }
    }


    /// <summary>
    /// Verify the layout for an item with Build Full System.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9836
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7581
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9836"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7581")]
    public abstract class T7581_MobileBase : VisualTestsBaseMobile, IClassFixture<T7581_ShareSku_Fixture>
    {
        protected readonly T7581_ShareSku_Fixture Fixture;

        protected T7581_MobileBase(ITestOutputHelper output, T7581_ShareSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: User has identified a BFS SKU.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.GetByoDimmerWithItemOptions().PrimarySku");

            //Act: Navigate to the PDP of the SKU from the pre-conditions.
            ProductDetail.NavigateToProductDetailByShortSku(sku);

            //Act: Once the PDP opens, scroll down and open the Build Full System drawer.
            ProductDetail.OpenBuildFullSystemDrawer();

            //Act: Capture a screenshot of the entire page but ignore the Ships Today verbiage.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), ProductDetail.IgnoreMoreYouMayLikeContainer() }, true, true, ProductDetail.IgnoreStockCheckWrapper(), 10, 0, 10);
        }
    }
}
