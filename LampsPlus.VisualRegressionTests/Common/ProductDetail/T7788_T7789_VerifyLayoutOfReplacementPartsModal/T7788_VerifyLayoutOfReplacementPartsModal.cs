using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using LampsPlus.VisualRegressionTests.BaseRefactored;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail.T7788_T7789_VerifyLayoutOfReplacementPartsModal
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7788_Window_VerifyLayoutOfReplacementPartsModal : T7788_DesktopBase
    {
        public T7788_Window_VerifyLayoutOfReplacementPartsModal(ITestOutputHelper output, T7788_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void VerifyLayoutOfReplacementPartsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7788_Mac_VerifyLayoutOfReplacementPartsModal : T7788_DesktopBase
    {
        public T7788_Mac_VerifyLayoutOfReplacementPartsModal(ITestOutputHelper output, T7788_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfReplacementPartsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7788_iPad_VerifyLayoutOfReplacementPartsModal : T7788_DesktopBase
    {
        public T7788_iPad_VerifyLayoutOfReplacementPartsModal(ITestOutputHelper output, T7788_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void VerifyLayoutOfReplacementPartsModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7788_TabletEmulator_VerifyLayoutOfReplacementPartsModal : T7788_DesktopBase
    {
        public T7788_TabletEmulator_VerifyLayoutOfReplacementPartsModal(ITestOutputHelper output, T7788_SharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void VerifyLayoutOfReplacementPartsModal(string config) => Validate(Validate, config);
    }


    public class T7788_SharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7788_SharedSku_Fixture()
        {
            ShortSku = ProductActions.GetReplacementParentSku.ParentSkuString;
        }
    }


    /// <summary>
    /// Verify the layout of the Replacement Part Modal
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-9845
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7788
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-9845"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7788")]
    public abstract class T7788_DesktopBase : VisualTestsBaseDesktop, IClassFixture<T7788_SharedSku_Fixture>
    {
        protected readonly T7788_SharedSku_Fixture Fixture;

        protected T7788_DesktopBase(ITestOutputHelper output, T7788_SharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }
        
        protected void Validate(string config)
        {
            //Arrange: User has identified a qualifying SKU.
            InitializeVisualTest(config);
            var sku = Fixture.ShortSku;
            Assert.DatabaseObject(sku, "ProductActions.ReplacementPartShortSku");

            //Act: Use the SKU in the pre-conditions and enter it at the end of the following URL: https://www.lampsplus.com/products/<SKU>.
            ProductDetail.NavigateToProductDetailByShortSku(sku);
            Assert.True(ProductDetail.IsPaypalLaterWidgetDisplayed(), "Paypal Later Widget is Not Displayed");

            //Act: Capture a screenshot of the entire page.
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.IgnoreStockCheckWrapper(), ProductDetail.IgnoreMoreYouMayLikeSection() }, true, true, maxLeftOffset:10, maxRightOffset:10);

            //Act: Tap on the Bulbs & Replacement Parts for Style#<SKU> link in the product details section.
            ProductDetail.OpenBulbAndReplacementPartsModal();

            //Act: Capture a screenshot of the modal element.
            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, ProductDetail.GetMediaModalContentModal(), true);
        }
    }
}
