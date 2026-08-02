using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;
using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7574_Windows_VerifyLayoutOfProductImageModal : T7574_DesktopBase
    {
        public T7574_Windows_VerifyLayoutOfProductImageModal(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfProductImageModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7574_Mac_VerifyLayoutOfProductImageModal : T7574_DesktopBase
    {
        public T7574_Mac_VerifyLayoutOfProductImageModal(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfProductImageModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7574_iPad_VerifyLayoutOfProductImageModal : T7574_DesktopBase
    {
        public T7574_iPad_VerifyLayoutOfProductImageModal(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfProductImageModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7574_TabletEmulator_VerifyLayoutOfProductImageModal : T7574_DesktopBase
    {
        public T7574_TabletEmulator_VerifyLayoutOfProductImageModal(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfProductImageModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7575_iPhone_VerifyLayoutOfProductImageModal : T7575_MobileBase
    {
        public T7575_iPhone_VerifyLayoutOfProductImageModal(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        public void LayoutOfProductImageModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7575_AndroidPhone_VerifyLayoutOfProductImageModal : T7575_MobileBase
    {
        public T7575_AndroidPhone_VerifyLayoutOfProductImageModal(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfProductImageModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7575_Emulator_VerifyLayoutOfProductImageModal : T7575_MobileBase
    {
        public T7575_Emulator_VerifyLayoutOfProductImageModal(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfProductImageModal(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the product image modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8764
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7574
    /// </summary>
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8764"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7574")]
    public abstract class T7574_DesktopBase : T7574_T7575_Base
    {
        protected T7574_DesktopBase(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, GlobalLocators.Iframe);
        }

        protected override void ClickOnProductImage()
        {
            ProductDetail.ProductImage.Click();
        }
    }


    /// <summary>
    /// Verify the layout of the product image modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-8764
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7575
    /// </summary>
    //[Collection(LpTraits.UserRole.Anonymous)]
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-8764"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7575")]
    public abstract class T7575_MobileBase : T7574_T7575_Base
    {
        protected T7575_MobileBase(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshot()
        {
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }

        protected override void ClickOnProductImage()
        {
            ProductDetail.ZoomIcon.Click();
        }
    }


    public class T7574_T7575_SharedSkus_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public T7574_T7575_SharedSkus_Fixture()
        {
            ShortSku = ProductActions.GetAnySkuWithProductDetailPage;
        }
    }


    public abstract class T7574_T7575_Base : VisualTestsBase, IClassFixture<T7574_T7575_SharedSkus_Fixture>
    {
        protected readonly T7574_T7575_SharedSkus_Fixture Fixture;

        protected T7574_T7575_Base(ITestOutputHelper output, T7574_T7575_SharedSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetAnySkuWithProductDetailPage");

            var shortSku = Fixture.ShortSku;

            ProductDetail.NavigateToProductDetailByShortSku(shortSku);

            Browser.Wait.IsVisibleElement(By.CssSelector(GlobalLocators.PdAddToCartId.ToCssIdSelector()));

            ClickOnProductImage();

            Browser.Wait.ForElementToStopAnimating(GlobalLocators.Iframe);

            TakeScreenshot();
        }

        protected abstract void TakeScreenshot();

        protected abstract void ClickOnProductImage();
    }
}
