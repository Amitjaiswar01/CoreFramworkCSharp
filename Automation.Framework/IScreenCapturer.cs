using System.Collections.Generic;
using Automation.Framework.Enums;
using OpenQA.Selenium;

namespace Automation.Framework
{
    /// <summary>
    /// Applitools vendor access 
    /// </summary>
    public interface IScreenCapturer 
    {
        bool IsCaptureFailed { get; set; }

        /// <summary>
        /// Applitools dispose method
        /// </summary>
        void ApplitoolsDispose();

        /// <summary>
        /// Take screenshot and upload to Applitools methods
        /// </summary>
        void CaptureScreen(string screenshotName, ScreenshotType screenshotType, bool useStitchMode = false, bool useLazyLoad = false, bool checkWindowFully = false);

        void CaptureElementArea(string screenshotName, IElement visualElement, bool useLazyLoad = false);

        void CaptureElementAreaWithIgnoredLayouts(string screenshotName, IElement visualElement, List<IElement> layoutsToBeIgnored, bool useLazyLoad = false);

        void CaptureScreenRegionWithIgnoredLayouts(string screenshotName, IElement regionElement, List<IElement> layoutsToBeIgnored, bool useStitchMode = false, bool useLazyLoad = false);

        void CaptureWholeOverlayModal(string screenshotName, IElement visualElement, bool useLazyLoad = false, bool useStitchMode = false, List< IElement> layoutsToBeIgnored = null, IElement floating = null, int maxUpOffset = 0,
        int maxDownOffset = 0, int maxLeftOffset = 0, int maxRightOffset = 0);

        void CaptureFullPageWithIgnoredLayouts(string browserPageUrl, List<IElement> elements, bool useStitchMode = false, bool useLazyLoad = false, IElement floating = null, int maxUpOffset = 0, int maxDownOffset = 0, int maxLeftOffset = 0, int maxRightOffset = 0);

        void CaptureVisibleScreenWithIgnoredLayouts(string screenshotName, List<IElement> layoutsToBeIgnored, bool useStitchMode = false, bool useLazyLoad = false, IElement floating = null, int offset = 0);

        void CaptureScrollableOverlay(string screenshotName, By elementLocator, bool useStitchMode = false, bool useLazyLoad = false);
    }
}
