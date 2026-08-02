using System.Collections.Generic;
using xRetry;
using Xunit;
using Xunit.Priority;
using Xunit.Abstractions;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T8003_T8004_VerifyTheLayoutOfTheFinishOrColorRelationshipWidget
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T8004_iPhone_VerifyLayoutOfFinishOrColorRelationshipWidget : T8004_MobileBase
    {
        public T8004_iPhone_VerifyLayoutOfFinishOrColorRelationshipWidget(ITestOutputHelper output, T8004_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfFinishOrColorRelationshipWidget(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T8004_Android_VerifyLayoutOfFinishOrColorRelationshipWidget : T8004_MobileBase
    {
        public T8004_Android_VerifyLayoutOfFinishOrColorRelationshipWidget(ITestOutputHelper output, T8004_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfFinishOrColorRelationshipWidget(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T8004_Emulator_VerifyLayoutOfFinishOrColorRelationshipWidget : T8004_MobileBase
    {
        public T8004_Emulator_VerifyLayoutOfFinishOrColorRelationshipWidget(ITestOutputHelper output, T8004_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfFinishOrColorRelationshipWidget(string config) => Validate(Validate, config);
    }


    public class T8004_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T8004_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuForFinishAndColorRelationshipWidget;
        }
    }


    /// <summary>
    /// Verify the layout of the Finish or Color Relationship Widget
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10920
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T8004
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10920"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T8004")]
    public abstract class T8004_MobileBase : VisualTestsBaseMobile, IClassFixture<T8004_SharedSkus_Fixture>
    {
        protected readonly T8004_SharedSkus_Fixture Fixture;

        protected T8004_MobileBase(ITestOutputHelper output, T8004_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            //Arrange: Identify a ShortSKU that has Relationship widget
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuForFinishAndColorRelationshipWidget");

            //Act: Take one of the SKUs in the pre-conditions and enter it at the end of the URL https://www.lampsplus.com/products/.
            Browser.Navigate(Urls.LampsPlusProductsUrl + shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on the PDP.");

            //Act: Scroll to relationship widget and capture screenshot for visible area
            ProductDetail.GetRelationshipWidgetSection();
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreMoreYouMayLikeContainer() });
        }
    }
}
