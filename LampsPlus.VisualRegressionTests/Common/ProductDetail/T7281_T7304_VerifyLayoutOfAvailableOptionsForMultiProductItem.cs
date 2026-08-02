using System.Collections.Generic;
using Automation.Framework;
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
    public class T7281_Window_VerifyLayoutOfAvailableOptionsForMultiProductItem : T7281_DesktopBase
    {
        public T7281_Window_VerifyLayoutOfAvailableOptionsForMultiProductItem(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture) {}

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutOfAvailableOptionsForMultiProductItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7281_Mac_VerifyLayoutOfAvailableOptionsForMultiProductItem : T7281_DesktopBase
    {
        public T7281_Mac_VerifyLayoutOfAvailableOptionsForMultiProductItem(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutOfAvailableOptionsForMultiProductItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7281_iPad_VerifyLayoutOfAvailableOptionsForMultiProductItem : T7281_DesktopBase
    {
        public T7281_iPad_VerifyLayoutOfAvailableOptionsForMultiProductItem(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutOfAvailableOptionsForMultiProductItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7281_TabletEmulator_VerifyLayoutOfAvailableOptionsForMultiProductItem : T7281_DesktopBase
    {
        public T7281_TabletEmulator_VerifyLayoutOfAvailableOptionsForMultiProductItem(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutOfAvailableOptionsForMultiProductItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7304_iPhone_VerifyLayoutOfAvailableOptionsForMultiProductItem : T7304_MobileBase
    {
        public T7304_iPhone_VerifyLayoutOfAvailableOptionsForMultiProductItem(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture) { }
        
        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutOfAvailableOptionsForMultiProductItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7304_AndroidPhone_VerifyLayoutOfAvailableOptionsForMultiProductItem : T7304_MobileBase
    {
        public T7304_AndroidPhone_VerifyLayoutOfAvailableOptionsForMultiProductItem(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutOfAvailableOptionsForMultiProductItem(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7304_Emulator_VerifyLayoutOfAvailableOptionsForMultiProductItem : T7304_MobileBase
    {
        public T7304_Emulator_VerifyLayoutOfAvailableOptionsForMultiProductItem(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutOfAvailableOptionsForMultiProductItem(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Available Options for a Multi-Product item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7375
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7281
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7375"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7281")]
    public abstract class T7281_DesktopBase : T7281_T7304_Base
    {
        protected T7281_DesktopBase(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void CapturePageLoadedScreenshot()
        {
            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }

        protected override void CaptureAvailableOptionsExpandedScreenshot()
        {
            ProductDetailMultiProduct.SelectedMultiProductDropdownOption.Click();

            ScreenCapturer.CaptureElementArea(Browser.PageUrl, ProductDetailMultiProduct.MultiProdSizeOptionsElement);
        }

        protected override void CaptureSelectedAvailableOptionScreenshot()
        {
            ProductDetailMultiProduct.UnselectedMultiProductDropdownOption.Click();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    /// <summary>
    /// Verify the layout of the Available Options for a Multi-Product item.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7375
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7304
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7375"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7304")]
    public abstract class T7304_MobileBase : T7281_T7304_Base
    {
        protected T7304_MobileBase(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture) { }

        protected override void CapturePageLoadedScreenshot()
        {
            ScreenCapturer.CaptureFullPageWithIgnoredLayouts(Browser.PageUrl, new List<IElement>{ProductDetail.MoreYouMayLikeContainer});
        }

        protected override void CaptureAvailableOptionsExpandedScreenshot()
        {
            ProductDetailMultiProduct.SelectedMultiProductDropdownOption.Click();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }

        protected override void CaptureSelectedAvailableOptionScreenshot()
        {
            ProductDetailMultiProduct.UnselectedMultiProductDropdownOption.Click();

            ScreenCapturer.CaptureScreen(Browser.PageUrl, ScreenshotType.VisualAreaCapture);
        }
    }


    public class MultiProductSharedSku_Fixture : FixtureBase
    {
        public string ShortSku { get; }

        public MultiProductSharedSku_Fixture()
        {
            ShortSku = ProductActions.GetMultiProductShortSku;
        }
    }


    public abstract class T7281_T7304_Base : VisualTestsBase, IClassFixture<MultiProductSharedSku_Fixture>
    {
        protected readonly MultiProductSharedSku_Fixture Fixture;

        protected T7281_T7304_Base(ITestOutputHelper output, MultiProductSharedSku_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            Assert.DatabaseObject(Fixture.ShortSku, "ProductActions.GetMultiProductShortSku()");

            ProductDetail.NavigateToProductDetailByShortSku(Fixture.ShortSku);

            Browser.Wait.ForDomReady();

            CapturePageLoadedScreenshot();

            CaptureAvailableOptionsExpandedScreenshot();

            CaptureSelectedAvailableOptionScreenshot();
        }

        protected abstract void CapturePageLoadedScreenshot();

        protected abstract void CaptureAvailableOptionsExpandedScreenshot();

        protected abstract void CaptureSelectedAvailableOptionScreenshot();
    }
}
