using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Applitools;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using OpenQA.Selenium;

namespace Automation.Framework.Core.ScreenCapturer
{
    public abstract class ApplitoolsScreenCapturerBase : IScreenCapturer
    {
        protected ApplitoolsScreenCapturerBase(Browser browser, Log log, string baselineFixVersion, string targetFixVersion, SessionSettings settings)
        {
            Browser = browser;
            Log = log;
            BaselineFixVersion = baselineFixVersion;
            TargetFixVersion = targetFixVersion;
            Settings = settings;
            AreCapturesOn = Convert.ToBoolean(ConfigurationManager.AppSettings["AreApplitoolsCapturesOn"]);
        }

        //instances
        protected readonly bool AreCapturesOn;
        protected readonly Browser Browser;
        protected readonly Log Log;
        protected readonly SessionSettings Settings;
        protected readonly string BaselineFixVersion;
        protected readonly string TargetFixVersion;

        public bool IsCaptureFailed { get; set; }

        public IEnumerable<Rectangle> GetPaddedRectangles(IEnumerable<IElement> elementsToBeIgnored, int rightPadding = 0, int bottomPadding = 0, IElement region = null)
        {
            var rectangles = new List<Rectangle>();

            var regionPoint = region?.Location ?? new Point(0, 0);

            foreach (var element in elementsToBeIgnored)
            {
                var ignorePoint = element.Location;

                var ignoreSize = element.Size;

                var offsetX = ignorePoint.X - regionPoint.X;
                var offsetY = ignorePoint.Y - regionPoint.Y;

                var rec = new Rectangle(offsetX, offsetY, ignoreSize.Width + rightPadding, ignoreSize.Height + bottomPadding);

                rectangles.Add(rec);
            }

            return rectangles;
        }

        protected string BuildTestScriptName()
        {
            if (Browser.TestName.Split('_').Length == 4)
            {
                var testName = Browser.TestName.Split('_')[1].Split('.')[0];

                var underScore = '_';
                var underScoreNonRefactoredTestsNameOccurrence = 2;
                var testId = Browser.TestName.Count(x => x == underScore) > underScoreNonRefactoredTestsNameOccurrence
                    ? Browser.TestName.Split('_')[1].Split('.').Last()
                    : Browser.TestName.Split('_')[0].Split('.').Last();

                var testTagName = $"{Browser.TestTagName.Split('_')[2]}_{Browser.TestTagName.Split('_')[3]}";
                var testScriptName = $"{testName}_{testTagName}_{testId}";

                return testScriptName;
            }
            else
            {
                var testName = Browser.TestName.Split('_')[2].Split('.')[0];

                var underScore = '_';
                var underScoreNonRefactoredTestsNameOccurrence = 2;
                var testId = Browser.TestName.Count(x => x == underScore) > underScoreNonRefactoredTestsNameOccurrence
                    ? Browser.TestName.Split('_')[2].Split('.').Last()
                    : Browser.TestName.Split('_')[0].Split('.').Last();

                var testTagName = $"{Browser.TestTagName.Split('_')[2]}_{Browser.TestTagName.Split('_')[3]}";
                var testScriptName = $"{testName}_{testTagName}_{testId}";

                return testScriptName;
            }
        }

        protected string GetUniqueScreenshotName()
        {
            return Settings.IsBaseLine ? BaselineFixVersion : TargetFixVersion;
        }

        protected void LazyLoadPage()
        {
            Browser.LazyLoadPage();
        }

        public abstract void ApplitoolsDispose();

        public abstract void CaptureScreen(string screenshotName, ScreenshotType screenshotType,
            bool useStitchMode = false,
            bool useLazyLoad = false, bool checkWindowFully = false);
        

        public abstract void CaptureElementArea(string screenshotName, IElement visualElement, bool useLazyLoad = false);

        public abstract void CaptureElementAreaWithIgnoredLayouts(string screenshotName, IElement visualElement,
            List<IElement> layoutsToBeIgnored, bool useLazyLoad = false);

        public abstract void CaptureScreenRegionWithIgnoredLayouts(string screenshotName, IElement regionElement,
            List<IElement> layoutsToBeIgnored,
            bool useStitchMode = false, bool useLazyLoad = false);

        public abstract void CaptureWholeOverlayModal(string screenshotName, IElement visualElement,
            bool useLazyLoad = false,
            bool useStitchMode = false, List<IElement> layoutsToBeIgnored = null, IElement floating = null,
            int maxUpOffset = 0,
            int maxDownOffset = 0, int maxLeftOffset = 0, int maxRightOffset = 0);
       

        public abstract void CaptureFullPageWithIgnoredLayouts(string browserPageUrl, List<IElement> elements,
            bool useStitchMode = false,
            bool useLazyLoad = false, IElement floating = null, int maxUpOffset = 0, int maxDownOffset = 0,
            int maxLeftOffset = 0, int maxRightOffset = 0);
       

        public abstract void CaptureVisibleScreenWithIgnoredLayouts(string screenshotName,
            List<IElement> layoutsToBeIgnored, bool useStitchMode = false,
            bool useLazyLoad = false, IElement floating = null, int offset = 0);
       

        public abstract void CaptureScrollableOverlay(string screenshotName, By elementLocator,
            bool useStitchMode = false,
            bool useLazyLoad = false);
    }
}