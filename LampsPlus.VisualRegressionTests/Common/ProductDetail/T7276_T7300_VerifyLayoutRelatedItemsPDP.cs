using OpenQA.Selenium;
using xRetry;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using Automation.Framework.Enums;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail

{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7276_Window_VerifyLayoutRelatedItemsPdp : T7276_DesktopBase
    {
        public T7276_Window_VerifyLayoutRelatedItemsPdp(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutRelatedItemsPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7276_Mac_VerifyLayoutRelatedItemsPdp : T7276_DesktopBase
    {
        public T7276_Mac_VerifyLayoutRelatedItemsPdp(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutRelatedItemsPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7276_iPad_VerifyLayoutRelatedItemsPdp : T7276_DesktopBase
    {
        public T7276_iPad_VerifyLayoutRelatedItemsPdp(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutRelatedItemsPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7276_TabletEmulator_VerifyLayoutRelatedItemsPdp : T7276_DesktopBase
    {
        public T7276_TabletEmulator_VerifyLayoutRelatedItemsPdp(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutRelatedItemsPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7300_iPhone_VerifyLayoutRelatedItemsPdp : T7300_MobileBase
    {
        public T7300_iPhone_VerifyLayoutRelatedItemsPdp(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutRelatedItemsPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7300_AndroidPhone_VerifyLayoutRelatedItemsPdp : T7300_MobileBase
    {
        public T7300_AndroidPhone_VerifyLayoutRelatedItemsPdp(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutRelatedItemsPdp(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7300_Emulator_VerifyLayoutRelatedItemsPdp : T7300_MobileBase
    {
        public T7300_Emulator_VerifyLayoutRelatedItemsPdp(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory] 
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutRelatedItemsPdp(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of a Related Item's PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7371
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7276
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7371"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7276")]
    public abstract class T7276_DesktopBase : T7276_T7300_Base
    {
        protected T7276_DesktopBase(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture) { }

        protected override void ScrollToRelatedItemsSection()
        {
            Browser.Wait.ForDomReady();
            Browser.ScrollIntoView(ProductDetail.RelatedItemSection);
            Browser.ExecuteJs("window.scrollBy(0,-150)");
        }

        protected override void TakeScreenshot()
        {
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    /// <summary>
    /// Verify the layout of a Related Item's PDP.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7371
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7300
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7371"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7300")]
    public abstract class T7300_MobileBase : T7276_T7300_Base
    {
        protected T7300_MobileBase(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture) { }

        protected override void ScrollToRelatedItemsSection()
        {
            Browser.Wait.ForDomReady();
            Browser.ScrollIntoView(ProductDetail.RelatedItemSection);
            Browser.ExecuteJs("window.scrollBy(0,-150)");
            Browser.Wait.ForDomReady();
        }

        protected override void TakeScreenshot()
        {
            Browser.Wait.IsVisibleElement(By.ClassName(ProductDetail.AssetActionsClass));
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    public class T7276_T7300_ShareSkus_Fixture : FixtureBase
    {
        public string RelatedProductsShortSku { get; }

        public T7276_T7300_ShareSkus_Fixture()
        {
            RelatedProductsShortSku = ProductActions.GetShortSkuThatHasRelatedProducts;
        }
    }


    public abstract class T7276_T7300_Base : VisualTestsBase, IClassFixture<T7276_T7300_ShareSkus_Fixture>
    {
        protected readonly T7276_T7300_ShareSkus_Fixture Fixture;

        protected T7276_T7300_Base(ITestOutputHelper output, T7276_T7300_ShareSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }
        
        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            Assert.DatabaseObject(Fixture.RelatedProductsShortSku, "ProductActions.GetShortSkuThatHasRelatedProducts()");

            ProductDetail.NavigateToProductDetailByShortSku(Fixture.RelatedProductsShortSku);

            ScrollToRelatedItemsSection();

            TakeScreenshot();
        }

        /// <summary> 
        /// Click on Related Item.
        /// </summary>
        protected abstract void ScrollToRelatedItemsSection();

        protected abstract void TakeScreenshot();
    }
}