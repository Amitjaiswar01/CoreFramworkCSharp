using System.Collections.Generic;
using System.Drawing;
using Automation.Framework.Enums;

namespace Automation.Framework
{
    /// <summary>
    /// Applitools vendor access 
    /// </summary>
    public interface IApplitools 
    {
        /// <summary>
        /// Applitools dispose method
        /// </summary>
        void ApplitoolsDispose();

        /// <summary>
        /// Take screenshot and upload to Applitools methods
        /// </summary>
        void CaptureScreen(string screenshotName, ScreenshotType screenshotType, bool useStitchMode = false, bool useLazyLoad = false);

        void CaptureElementArea(string screenshotName, IElement visualElement, bool useLazyLoad = false);

        void CaptureScreenWithIgnoreElement(string screenshotName, IElement regionElement, IElement elementToBeIgnored, bool useStitchMode = false);

        void CaptureScreenWithIgnoreElements(string screenshotName, IElement regionElement, List<IElement> elementToBeIgnored, bool useStitchMode = false);

        void CaptureRegionIgnoreElementRectangle(string screenshotName, IElement regionElement, Rectangle elementToBeIgnored, bool useStitchMode = false, bool useLazyLoad = false);

        void CaptureWindowIgnoreElementRectangle(string screenshotName, Rectangle elementToBeIgnored, bool useStitchMode = false);

        void CaptureWholeOverlayModal(string screenshotName, IElement visualElement);

        /// <summary>
        /// Get ignored element rectangle with custom padding
        /// </summary>
        Rectangle GetPaddedRectangle(IElement elementToBeIgnored, int rightPadding, int bottomPadding, IElement region = null);

        /// <summary>
        /// Get iframe's ignored element rectangle with custom padding
        /// </summary>
        Rectangle GetPaddedRectangleOffset(IElement elementToBeIgnored, int pointOffsetX, int pointOffsetY, int rightPadding = 0, int bottomPadding = 0);
    }
}
