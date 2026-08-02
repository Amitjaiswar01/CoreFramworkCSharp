using System.Collections.Generic;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using xRetry;
using OpenQA.Selenium;
using Automation.Framework;
using Automation.Framework.Utilities;
using LampsPlus.AutomationFramework.Constants;
using LampsPlus.AutomationFramework.Utilities.TestConfiguration;

namespace LampsPlus.VisualRegressionTests.Common.ProductDetail

{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7279_Windows_VerifyLayoutRelatedVideoChatModal : T7279_DesktopBase
    {
        public T7279_Windows_VerifyLayoutRelatedVideoChatModal(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.WindowsTen)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_Chrome_SNIS_UNSI)]
        public void LayoutRelatedVideoChatModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7279_Mac_VerifyLayoutRelatedVideoChatModal : T7279_DesktopBase
    {
        public T7279_Mac_VerifyLayoutRelatedVideoChatModal(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.MacMojave)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Mac_Safari_SNIS_UNSI)]
        public void LayoutRelatedVideoChatModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7279_iPad_VerifyLayoutRelatedVideoChatModal : T7279_DesktopBase
    {
        public T7279_iPad_VerifyLayoutRelatedVideoChatModal(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsTabletLatestSupportedVersion)]
        [SkippableTheory]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPad_Safari_SNIS_UNSI)]
        public void LayoutRelatedVideoChatModal(string config) => Validate(Validate, config);
    }

    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7279_TabletEmulator_VerifyLayoutRelatedVideoChatModal : T7279_DesktopBase
    {
        public T7279_TabletEmulator_VerifyLayoutRelatedVideoChatModal(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeTabletEmulation)]
        [Theory(Skip = "Rework required for tablet emulator test.")]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeTabletView_SNIS_UNSI)]
        public void LayoutRelatedVideoChatModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7302_iPhone_VerifyLayoutRelatedVideoChatModal : T7302_MobileBase
    {
        public T7302_iPhone_VerifyLayoutRelatedVideoChatModal(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.iOsPhoneLatestSupportedVersion)]
        [RetryTheory(2)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth_Baseline)]
        //[InlineData(TestConfiguration.iPhone_Safari_SNIS_UNSI_SecondaryViewPortWidth)]
        public void LayoutRelatedVideoChatModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7302_AndroidPhone_VerifyLayoutRelatedVideoChatModal : T7302_MobileBase
    {
        public T7302_AndroidPhone_VerifyLayoutRelatedVideoChatModal(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.AndroidEightPhone)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Android_Chrome_SNIS_UNSI)]
        public void LayoutRelatedVideoChatModal(string config) => Validate(Validate, config);
    }


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class T7302_Emulator_VerifyLayoutRelatedVideoChatModal : T7302_MobileBase
    {
        public T7302_Emulator_VerifyLayoutRelatedVideoChatModal(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture) { }

        [Trait(LpTraits.Keys.Category, LpTraits.OperatingSystem.ChromeMobileEmulation)]
        [SkippableTheory]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI_Baseline)]
        [InlineData(TestConfiguration.Windows_ChromeMobileView_SNIS_UNSI)]
        public void LayoutRelatedVideoChatModal(string config) => Validate(Validate, config);
    }


    /// <summary>
    /// Verify the layout of the Related Video modal and the Chat Modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7373
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7279
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Desktop)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7373"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7279")]
    public abstract class T7279_DesktopBase : T7279_T7302_Base
    {
        protected T7279_DesktopBase(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshotRelatedVideo()
        {
            if (OperatingSystem == OperatingSystem.iPad)
            {
                Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdRelVideosId.ToCssIdSelector()));
                ProductDetail.RelatedVideo.Click();
                Browser.Wait.ForDomReady();
                Browser.Wait.ForDisplayedElement(Browser.Locate.ElementById(GlobalLocators.LpModalId));

                Browser.Wait.ForDomReady();
                ScreenCapturer.CaptureScreenRegionWithIgnoredLayouts(Browser.PageUrl, GlobalLocators.Iframe, new List<IElement> { ProductDetail.VideoWindow });

                GlobalLocators.LpModalCloseElement.Click();
                Browser.Wait.UntilElementDoesntExist(GlobalLocators.LpModalId.ToCssIdSelector());
            }
            else
            {
                Browser.ScrollIntoView(ProductDetail.RelatedVideo);
                ProductDetail.RelatedVideo.Click();
                Browser.Wait.ForDomReady();
                Browser.Wait.IsVisibleElement(By.Id(GlobalLocators.LpModalId));
                Browser.ScrollToTopOfWindow();

                //Pause the Video
                Browser.Wait.WaitForIframeAndSwitchToIt("player");
                Browser.Wait.ForDomReady();
                PauseVideo("1");
                Browser.SwitchToDefaultContent();

                ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { ProductDetail.VideoWindow });
            }
        }
    }


    /// <summary>
    /// Verify the layout of the Related Video modal and the Chat Modal.
    /// JIRA Task Link: https://lampstrack.lampsplus.com:8443/browse/ACD-7373
    /// Test Case Link: https://lampstrack.lampsplus.com:8443/secure/Tests.jspa#/testCase/LP-T7302
    /// </summary>
    [Trait(LpTraits.Keys.Category, LpTraits.Suite.Mobile)]
    [Trait(LpTraits.RequiredTestCaseTags.TaskId, "ACD-7373"), Trait(LpTraits.RequiredTestCaseTags.TId, "LP-T7302")]
    public abstract class T7302_MobileBase : T7279_T7302_Base
    {
        protected T7302_MobileBase(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture) { }

        protected override void TakeScreenshotRelatedVideo()
        {
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PnlProductDescriptionId.ToCssIdSelector()));
            ProductDetail.ProductDescDropDown.Click();
            Browser.Wait.ForElementToStopAnimating(ProductDetail.ProductDescriptionAccordion);
            Browser.Wait.IsVisibleElement(By.CssSelector(ProductDetail.PdRelVideosId.ToCssIdSelector()));
            ProductDetail.RelatedVideo.Click();
            Browser.Wait.ForMobileModalToFullyOpen(GlobalLocators.LpMobileOverlayVideoElement);

            if (OperatingSystem == OperatingSystem.iPhone)
            {
                //step for iPhone only to workaround cross-origin frame issue
                var frameUrl = Browser.Locate.ElementByXpath("//*[@id=\"videoPlayer\"]").GetAttribute("src");
                Browser.Navigate(frameUrl);
                Browser.Wait.ForDomReady();

                //Click play the video
                var playBtn = Browser.Locate.ElementByXpath("//*[@aria-label='Play']");
                var xElementCoordinate = 0;
                var yElementCoordinate = 0;
                Browser.GetElementCoordinates(playBtn, ref xElementCoordinate,
                    ref yElementCoordinate, 100);
                Browser.ClickWithTapByCoordinates(xElementCoordinate, yElementCoordinate);
                Browser.Wait.ForDomReady();

                //Pause the Video
                PauseVideo("1");
                Browser.Wait.ForDomReady();

                //var progressBarSelector = ".ytp-progress-bar-padding";
                var progressBarSelector = ".ytp-chapters-container";
                var videoStreamingClass = "video-stream";
                //Capture overlay
                ScreenCapturer.CaptureVisibleScreenWithIgnoredLayouts(Browser.PageUrl, new List<IElement> { Browser.Locate.ElementBySelector(progressBarSelector), Browser.Locate.ElementByClassName(videoStreamingClass) }, false, false, Browser.Locate.ElementBySelector(progressBarSelector), 15);
            }
            else
            {
                //Pause the Video
                Browser.Wait.WaitForIframeAndSwitchToIt("videoPlayer");
                PauseVideo("1");
                Browser.SwitchToDefaultContent();

                //Capture overlay
                Browser.Wait.ForDomReady();
                ScreenCapturer.CaptureWholeOverlayModal(Browser.PageUrl, GlobalLocators.LpMobileOverlayVideoElement);
                GlobalLocators.LpModalCloseVideoElement.Click();
                Browser.Wait.UntilElementUnloads(GlobalLocators.LpMobileOverlayVideoElement);
            }
        }
    }

    public class T7279_T7302_ShareSkus_Fixture : FixtureBase
    {
        public string RelatedVideosShortSku { get; }

        public T7279_T7302_ShareSkus_Fixture()
        {
            RelatedVideosShortSku = ProductActions.GetAnySkuWithRelatedVideos;
        }
    }


    public abstract class T7279_T7302_Base : VisualTestsBase, IClassFixture<T7279_T7302_ShareSkus_Fixture>
    {
        protected readonly T7279_T7302_ShareSkus_Fixture Fixture;

        protected T7279_T7302_Base(ITestOutputHelper output, T7279_T7302_ShareSkus_Fixture fixture) : base(output, fixture)
        {
            Fixture = fixture;
        }

        /// <summary> 
        /// Verify the layout of the Related Video modal and the Chat Modal.
        /// </summary>
        protected void Validate(string config)
        {
            InitializeVisualTest(config);

            Assert.DatabaseObject(Fixture.RelatedVideosShortSku, "ProductActions.GetAnySkuWithRelatedVideos()");

            ProductDetail.NavigateToProductDetailByShortSku(Fixture.RelatedVideosShortSku);

            Browser.Wait.ForDomReady();

            TakeScreenshotRelatedVideo();
        }

        /// <summary> 
        /// Click on Related Video.
        /// </summary>
        protected abstract void TakeScreenshotRelatedVideo();

        protected void PauseVideo(string frameNumber)
        {
            Browser.ExecuteJs("document.querySelector('video').pause();");
            Browser.ExecuteJs($"var vid = document.querySelector('video'); vid.currentTime = {frameNumber};");
            Browser.ExecuteJs("document.querySelector('video').pause();");
        }
    }
}
