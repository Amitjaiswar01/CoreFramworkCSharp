using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7950_T7951_VerifyLayoutOfNeedHelpWithThisProductsModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7950_Window_VerifyLayoutOfNeedHelpWithThisProductsModal : T7950_DesktopBase
    {
        public T7950_Window_VerifyLayoutOfNeedHelpWithThisProductsModal(ITestOutputHelper output, T7950_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyTheLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7950_Window_VerifyLayoutOfNeedHelpWithThisProductsModalPros : T7950_DesktopBase
    {
        public T7950_Window_VerifyLayoutOfNeedHelpWithThisProductsModalPros(ITestOutputHelper output, T7950_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_PCSI)]
        public void VerifyTheLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7950_Mac_VerifyLayoutOfNeedHelpWithThisProductsModal : T7950_DesktopBase
    {
        public T7950_Mac_VerifyLayoutOfNeedHelpWithThisProductsModal(ITestOutputHelper output, T7950_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyTheLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7950_Mac_VerifyLayoutOfNeedHelpWithThisProductsModalPros : T7950_DesktopBase
    {
        public T7950_Mac_VerifyLayoutOfNeedHelpWithThisProductsModalPros(ITestOutputHelper output, T7950_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_PCSI)]
        public void VerifyTheLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7950_Tablet_VerifyLayoutOfNeedHelpWithThisProductsModal : T7950_DesktopBase
    {
        public T7950_Tablet_VerifyLayoutOfNeedHelpWithThisProductsModal(ITestOutputHelper output, T7950_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyTheLayoutOfNeedHelpModal(string config) => Validate(Validate, config);
    }


    public class T7950_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7950_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetSkuWithShipInOption;
        }
    }

    /// <summary>
    /// Verify the layout of the need help with this product outside chat hours
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-10668
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7950
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-10668"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7950")]

    public abstract class T7950_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7950_SharedSkus_Fixture>
    {
        protected readonly T7950_SharedSkus_Fixture Fixture;

        protected T7950_DesktopBase(ITestOutputHelper output, T7950_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            // Arrange : Navigate to any product detail page
            InitializeVisualTest(config);
            var shortSku = Fixture.ShortSku;
            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetSkuWithShipInOption");

            // Act : Navigate to the PDP by shortSku
            ProductDetail.NavigateToProductDetailByShortSku(shortSku);
            Assert.True(ProductDetail.IsCurrentPage, "User is not on PDP page");

            // Act : Take screenshot of the visible screen
            ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper() });

            // Act : Open the Need help with this product model and take screenshot
            var isChatOpenTime = ProductDetail.IsChatIconEnabled();

            if (isChatOpenTime)
            {
                Log.Message("Chat is in business hours");
            }
            else
            {
                ProductDetail.OpenProductHelpAndStoreAvailabilityModal();
                ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper() });
                ProductDetail.CloseNeedHelpModal();
            }
        }
    }
}