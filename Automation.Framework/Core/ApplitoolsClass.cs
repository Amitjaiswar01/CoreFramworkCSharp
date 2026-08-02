using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using Applitools;
using Applitools.Selenium;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using Applitools.Utils.Geometry;

namespace Automation.Framework.Core
{
        public class ApplitoolsClass : IApplitools
        {

        public ApplitoolsClass(Browser browser, Log log)
        {
            _browser = browser;
            _log = log;
        }

        //instances
        private Browser _browser;
        private Log _log;
        private Locate _locate;

        private Eyes _eyes;
        protected Eyes Eyes => _eyes ?? (_eyes = CreateAndInitializeEyes());

        private const string _lazyLoadImageSelector = "img.unveil[src=\"/img/global/trans.gif\"]";

        private Eyes CreateAndInitializeEyes()
        {
            var eyes = new Eyes()
            {
                MatchLevel = MatchLevel.Exact,
                ForceFullPageScreenshot = true,
                SaveDiffs = false,
                ApiKey = ConfigurationManager.AppSettings["ApplitoolsApiKey"],
                ServerUrl = ConfigurationManager.AppSettings["ApplitoolsServerUrl"]
            };

            if (_browser.Settings.IsBaseLine)
            {
                eyes.SaveDiffs = true;//Overrides baseline with the same name
            }

            eyes.MatchTimeout = new TimeSpan(0);

            eyes.SendDom = false;//Mobile tests failed without this command, solution was provided by Applitools support.

            int.TryParse(MajorSiteVersion, out var siteVersion);
            var majorProdSiteVersion = _browser.IsProdInstance ? siteVersion : siteVersion - 1;
            var majorPpeSiteVersion = _browser.IsProdInstance ? siteVersion + 1 : siteVersion;

            var bambooJobNumber = ConfigurationManager.AppSettings["BambooJobNumber"];

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            BatchInfo batchInfo = new BatchInfo($"Prod_{majorProdSiteVersion}_vs_Ppe_{majorPpeSiteVersion}_BambooJobNumber:{bambooJobNumber}");
            batchInfo.Id = $"Prod_{majorProdSiteVersion}_vs_Ppe_{majorPpeSiteVersion}_BambooJobNumber:{bambooJobNumber}";

            eyes.Batch = batchInfo;

            if (!_browser.Settings.IsMobileView && !_browser.Settings.IsLocalEnvironment)
            {
                eyes.Open(_browser.Driver, "LampsPlusVisualRegressionTests", BuildTestScriptName(),
                        new Size(_browser.BrowserWindowWidth, _browser.BrowserWindowHeight));
                _log.Message(_browser.Driver.Manage().Window.Size.ToString());
            }
            else
            {
                eyes.Open(_browser.Driver, "LampsPlusVisualRegressionTests", BuildTestScriptName());
            }

            return eyes;
        }

        public void ApplitoolsDispose()
        {
            _log.Message("Eyes disposal request");
            if (_eyes != null)
            {

                TestResults result = Eyes?.Close(!_browser.Settings.IsBaseLine); //If Close() argument bool is "true", comparison test will fail if it fails on Applitools; throws exception.

                _log.Message("Applitools test status: " + result.Status);//Provides test status result on Applitools dashboard (Applitools SDK)
                _log.Message("Applitools test started: " + result.StartedAt);//Provides test start date & time (Applitools SDK)

                if (_browser.Settings.IsBaseLine)//Delete baseline to have combined test result on Applitools dashboard.
                {
                    result.Delete();
                }

                Eyes.AbortIfNotClosed(); //Eyes method: If you call it after the test has been succesfully closed, then the call is ignored.

                _log.Message("Eyes disposed");
            }
        }

        public void CaptureScreen(string screenshotName, ScreenshotType screenshotType, bool useStitchMode = false, bool useLazyLoad = false)
        {
            if (useStitchMode)
            {
                Eyes.StitchMode = StitchModes.CSS;
            }

            if (screenshotType == ScreenshotType.None)
            {
                throw new InvalidEnumArgumentException($"{nameof(screenshotType)} cannot be 'None'");
            }

            var uniqueScreenshotName = BuildTestScriptName();

            if (screenshotType == ScreenshotType.FullPageCapture)
            {
                Eyes.ForceFullPageScreenshot = true;

                var height = GetEntireHeight();

                if (useLazyLoad)
                {
                    LazyLoadPage(height);
                }


                Eyes.CheckWindow($"{uniqueScreenshotName} ");
                _log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");

            }
            else if (screenshotType == ScreenshotType.VisualAreaCapture)
            {
                Eyes.ForceFullPageScreenshot = false;

                Eyes.CheckWindow($"{uniqueScreenshotName}");
                _log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
            }
        }

        public void CaptureElementArea(string screenshotName, IElement visualElement, bool useLazyLoad = false)
        {
            var uniqueScreenshotName = BuildTestScriptName();

            if (visualElement == null)
            {
                throw new ArgumentNullException(nameof(visualElement));
            }

            if (visualElement.InternalElement == null)
            {
                throw new ArgumentNullException($"{nameof(visualElement)} InternalElement is null");
            }

            if (useLazyLoad)
            {
                var height = GetEntireHeight();
                LazyLoadPage(height);
            }

            Eyes.ForceFullPageScreenshot = false;

            Eyes.Check($"{uniqueScreenshotName} ", Target.Region(visualElement.InternalElement));
            _log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }

        public void CaptureWholeOverlayModal(string screenshotName, IElement allContentOfOverlayModalElement)
        {
            var uniqueScreenshotName = BuildTestScriptName();

            if (allContentOfOverlayModalElement == null)
            {
                throw new ArgumentNullException(nameof(allContentOfOverlayModalElement));
            }

            if (allContentOfOverlayModalElement.InternalElement == null)
            {
                throw new ArgumentNullException($"{nameof(allContentOfOverlayModalElement)} InternalElement is null");
            }

            Eyes.ForceFullPageScreenshot = true;

            Eyes.Check($"{uniqueScreenshotName} ", Target.Region(allContentOfOverlayModalElement.InternalElement).Fully());
            _log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }

        public void CaptureScreenWithIgnoreElement(string screenshotName, IElement regionElement, IElement elementToBeIgnored, bool useStitchMode = false)
        {
            var uniqueScreenshotName = BuildTestScriptName();

            if (useStitchMode)
            {
                Eyes.StitchMode = StitchModes.CSS;
            }

            if (regionElement == null)
            {
                throw new ArgumentNullException(nameof(regionElement));
            }

            if (elementToBeIgnored == null)
            {
                throw new ArgumentNullException(nameof(elementToBeIgnored));
            }

            Eyes.ForceFullPageScreenshot = true;
            Eyes.Check($"{uniqueScreenshotName} ", Target.Region(regionElement.InternalElement).Fully().Ignore(GetPaddedRectangle(elementToBeIgnored, 15, 5, regionElement)));//
            _log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }

        public void CaptureRegionIgnoreElementRectangle(string screenshotName, IElement regionElement, Rectangle elementToBeIgnored, bool useStitchMode = false, bool useLazyLoad = false)
        {
            var uniqueScreenshotName = BuildTestScriptName();

            if (useStitchMode)
            {
                Eyes.StitchMode = StitchModes.CSS;
            }

            if (elementToBeIgnored == null)
            {
                throw new ArgumentNullException(nameof(elementToBeIgnored));
            }

            if (useLazyLoad)
            {
                var height = GetEntireHeight();
                LazyLoadPage(height);
            }

            Eyes.ForceFullPageScreenshot = false;
            Eyes.Check($"{uniqueScreenshotName} ", Target.Region(regionElement.InternalElement).Fully().Ignore(elementToBeIgnored));
            _log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }

        public void CaptureWindowIgnoreElementRectangle(string screenshotName, Rectangle elementToBeIgnored, bool useStitchMode = false)
        {
            var uniqueScreenshotName = BuildTestScriptName();

            if (useStitchMode)
            {
                Eyes.StitchMode = StitchModes.CSS;
            }

            if (elementToBeIgnored == null)
            {
                throw new ArgumentNullException(nameof(elementToBeIgnored));
            }

            Eyes.ForceFullPageScreenshot = false;
            Eyes.Check($"{uniqueScreenshotName} ", Target.Window().Ignore(elementToBeIgnored));
            _log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }


        public void CaptureScreenWithIgnoreElements(string screenshotName, IElement regionElement, List<IElement> elementsToBeIgnored, bool useStitchMode = false)
        {
            var uniqueScreenshotName = BuildTestScriptName();

            if (useStitchMode)
            {
                Eyes.StitchMode = StitchModes.CSS;
            }

            if (regionElement == null)
            {
                throw new ArgumentNullException(nameof(regionElement));
            }

            if (elementsToBeIgnored == null)
            {
                throw new ArgumentNullException(nameof(elementsToBeIgnored));
            }

            Eyes.ForceFullPageScreenshot = true;

            Eyes.Check($"{uniqueScreenshotName} ", Target.Region(regionElement.InternalElement).Fully().Ignore(GetPaddedRectangles(elementsToBeIgnored, 15, 5, regionElement)));
            _log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }

        private string MajorSiteVersion => _browser._siteVersion.Split('.')[0];

        private string BuildTestScriptName()
        {
            var testName = _browser.TestName.Split('_')[2].Split('.')[0];
            var testId = _browser.TestName.Split('_')[0].Split('.').Last();
            var testTagName = $"{_browser.TestTagName.Split('_')[2]}_{_browser.TestTagName.Split('_')[3]}";
            var testScriptName = $"{testName}_{testTagName}_{testId}";

            return testScriptName;
        }

        private void LazyLoadPage(int heightSize)
        {
            var elements = _locate.ElementsBySelector(_lazyLoadImageSelector);
            var viewport = new RectangleSize(_browser.GetWindowInnerWidth(), _browser.GetWindowInnerHeight());
            for (int j = 0; j < heightSize; j += viewport.Height - viewport.Height / 10)
            {
                _browser.ExecuteJs("window.scrollTo(0," + j + ")");
                for (int i = 0; i < elements.Count; i++)
                {
                    var isElementInViewPort = _locate.IsVisibleInViewport(elements[i]);
                    if (isElementInViewPort)
                    {
                        SpinWait.SpinUntil(() => _locate.IsImageVisible(elements[i]), TimeSpan.FromSeconds(5));
                    }
                }
            }
            _browser.ExecuteJs("window.scrollTo(0, 0);");
        }

        private int GetEntireHeight()
        {
            var parseSuccessful =
                int.TryParse(_browser.ExecuteJs("return document.documentElement.clientHeight").ToString(), out var clientHeight) & // need to use bitwise and or compiler will flag some of these as "unused"
                int.TryParse(_browser.ExecuteJs("return document.body.clientHeight").ToString(), out var bodyClientHeight) &
                int.TryParse(_browser.ExecuteJs("return document.documentElement.scrollHeight").ToString(), out var scrollHeight) &
                int.TryParse(_browser.ExecuteJs("return document.body.scrollHeight").ToString(), out var bodyScrollHeight);
            if (!parseSuccessful)
                throw new Exception("Unable to parse javascript document heights");
            int maxDocElementHeight = Math.Max(clientHeight, scrollHeight);
            int maxBodyHeight = Math.Max(bodyClientHeight, bodyScrollHeight);
            return Math.Max(maxDocElementHeight, maxBodyHeight);
        }

        public Rectangle GetPaddedRectangle(IElement elementToBeIgnored, int rightPadding = 0, int bottomPadding = 0, IElement region = null)
        {
            var regionPoint = region?.Location ?? new Point(0, 0);

            var ignorePoint = elementToBeIgnored.Location;

            var ignoreSize = elementToBeIgnored.Size;

            var offsetX = ignorePoint.X - regionPoint.X;
            var offsetY = ignorePoint.Y - regionPoint.Y;

            return new Rectangle(offsetX, offsetY, ignoreSize.Width + rightPadding, ignoreSize.Height + bottomPadding);
        }

        public Rectangle GetPaddedRectangleOffset(IElement elementToBeIgnored, int pointOffsetX, int pointOffsetY, int rightPadding = 0, int bottomPadding = 0)
        {
            var ignorePoint = elementToBeIgnored.Location;

            var ignoreSize = elementToBeIgnored.Size;

            var offsetX = ignorePoint.X + pointOffsetX;
            var offsetY = ignorePoint.Y + pointOffsetY;

            return new Rectangle(offsetX, offsetY, ignoreSize.Width + rightPadding, ignoreSize.Height + bottomPadding);
        }

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
        }
}
