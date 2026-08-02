using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using OpenQA.Selenium;
using Applitools;
using Applitools.Selenium;
using Automation.Framework.Enums;
using Automation.Framework.Exceptions;
using Automation.Framework.Utilities;
using Configuration = Applitools.Selenium.Configuration;

namespace Automation.Framework.Core.ScreenCapturer
{
    public class ApplitoolsScreenCapturer : ApplitoolsScreenCapturerBase
    {
        public ApplitoolsScreenCapturer(Browser browser, Log log, string baselineFixVersion, string targetFixVersion, SessionSettings settings) : base(browser, log, baselineFixVersion, targetFixVersion, settings)
        {
        }

        //Eyes instance
        private Applitools.Selenium.Eyes _eyes;
        protected Applitools.Selenium.Eyes Eyes => _eyes ?? (_eyes = CreateAndInitializeEyes());

        private Applitools.Selenium.Eyes CreateAndInitializeEyes()
        {
            Applitools.Selenium.Eyes eyes = new Applitools.Selenium.Eyes();

            Configuration config = new Configuration();
            Applitools.IConfiguration configVal = config.SetIgnoreDisplacements(true)
                .SetApiKey(ConfigurationManager.AppSettings["ApplitoolsApiKey"])
                .SetServerUrl(ConfigurationManager.AppSettings["ApplitoolsServerUrl"]).SetMatchLevel(MatchLevel.Strict)
                .SetForceFullPageScreenshot(true).SetSaveDiffs(false).SetHideCaret(true);

            eyes.SetConfiguration(configVal);

            if (Browser.Settings.IsBaseLine)
            {
                eyes.SaveDiffs = true;//Overrides baseline with the same name
                eyes.BranchName = BaselineFixVersion;
                eyes.BaselineBranchName = BaselineFixVersion;
            }
            else
            {
                eyes.BranchName = TargetFixVersion;
                eyes.BaselineBranchName = BaselineFixVersion;
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var bambooJobNumber = ConfigurationManager.AppSettings["BambooJobNumber"];
            BatchInfo batchInfo = new BatchInfo($"{BaselineFixVersion}vs{TargetFixVersion}:{bambooJobNumber}");
            batchInfo.Id = $"{BaselineFixVersion}vs{TargetFixVersion}:{bambooJobNumber}";

            eyes.Batch = batchInfo;

            if (Settings.SettingsTestName.Contains("Mac"))
            {
                eyes.Open(Browser.Driver, "LampsPlusVisualRegressionTests", BuildTestScriptName(), new Size(1920, 1080));
            }
            else
            {
                eyes.Open(Browser.Driver, "LampsPlusVisualRegressionTests", BuildTestScriptName());
            }

            Log.Message(Browser.Driver.Manage().Window.Size.ToString());

            return eyes;
        }

        private TestResults CapturerResults(int timeToWait = 0)
        {
            var wait = Browser.Wait.GetDefaultWait(timeToWait);
            var results = wait.Until(x => Eyes?.Close(false));//NOTE:If Close() argument bool is "true", comparison test will fail if it fails on Applitools.
            return results;
        }

        public override void ApplitoolsDispose()
        {
            if (_eyes == null) return;

            Log.Message("Eyes disposal started");
            var startTime = DateTime.Now;
            var secondsToWait = 60;

            try
            {
                var result = CapturerResults(secondsToWait);

                if (result != null)
                {
                    Log.Message($"Applitools test status: {result.Status}"); //Provides test status result on Applitools dashboard (Applitools SDK)
                    Log.Message($"Applitools test started: {result.StartedAt}"); //Provides test start date & time (Applitools SDK)

                    if (Browser.Settings.IsBaseLine) //Delete baseline record to have combined test result on Applitools dashboard for the test (Baseline is still saved to Branch baselines).
                    {
                        result.Delete();
                    }

                    Eyes.AbortIfNotClosed();
                }
            }

            catch (Exception ex)
            {
                var message = $"{ex} in {DateTime.Now - startTime} seconds of the requested {secondsToWait} seconds.";
                Log.Message(message);
                throw new FrameworkVisualTestsException($"Visual test disposal failed with the message: {ex}");
            }

            foreach (var process in Process.GetProcessesByName("eyes.universal.win"))
            {
                process.Kill();
            }

            Log.Message("Eyes disposed");
        }

        public override void CaptureScreen(string screenshotName, ScreenshotType screenshotType, bool useStitchMode = false, bool useLazyLoad = false, bool checkWindowFully = false)
        {
            if (!AreCapturesOn) return;

            if (useStitchMode)
            {
                Eyes.StitchMode = StitchModes.CSS;
            }

            if (screenshotType == ScreenshotType.None)
            {
                throw new InvalidEnumArgumentException($"{nameof(screenshotType)} cannot be 'None'");
            }

            var uniqueScreenshotName = GetUniqueScreenshotName();

            if (screenshotType == ScreenshotType.FullPageCapture)
            {
                Eyes.ForceFullPageScreenshot = true;

                if (useLazyLoad)
                {
                    LazyLoadPage();
                }

                if (checkWindowFully)
                {
                    Eyes.CheckWindow($"{uniqueScreenshotName} ");
                }
                else
                {
                    Eyes.CheckWindow($"{uniqueScreenshotName} ");
                }

                Log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");

            }
            else if (screenshotType == ScreenshotType.VisualAreaCapture)
            {
                Eyes.ForceFullPageScreenshot = false;

                Eyes.CheckWindow($"{uniqueScreenshotName}");

                Log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
            }
        }

        public override void CaptureElementArea(string screenshotName, IElement visualElement, bool useLazyLoad = false)
        {
            if (!AreCapturesOn) return;

            var uniqueScreenshotName = GetUniqueScreenshotName();

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
                LazyLoadPage();
            }

            Eyes.ForceFullPageScreenshot = false;

            try
            {
                Eyes.Check($"{uniqueScreenshotName} ", Target.Region(visualElement.InternalElement).IgnoreDisplacements());
            }
            catch (Exception ex)
            {
                IsCaptureFailed = true;
                throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
            }

            Log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }

        public override void CaptureElementAreaWithIgnoredLayouts(string screenshotName, IElement visualElement, List<IElement> layoutsToBeIgnored, bool useLazyLoad = false)
        {
            if (!AreCapturesOn) return;

            var uniqueScreenshotName = GetUniqueScreenshotName();

            if (visualElement == null)
            {
                throw new ArgumentNullException(nameof(visualElement));
            }

            if (layoutsToBeIgnored == null)
            {
                throw new ArgumentNullException(nameof(layoutsToBeIgnored));
            }

            if (visualElement.InternalElement == null)
            {
                throw new ArgumentNullException($"{nameof(visualElement)} InternalElement is null");
            }


            if (useLazyLoad)
            {
                LazyLoadPage();
            }

            Eyes.ForceFullPageScreenshot = false;

            try
            {
                Eyes.Check($"{uniqueScreenshotName} ", Target.Region(visualElement.InternalElement)
                    .Layout(layoutsToBeIgnored.Select(i => i.InternalElement)));
            }
            catch (Exception ex)
            {
                IsCaptureFailed = true;
                throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
            }

            Log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }

        public override void CaptureWholeOverlayModal(string screenshotName, IElement allContentOfOverlayModalElement, bool useLazyLoad = false, bool useStitchMode = false, List<IElement> layoutsToBeIgnored = null, IElement floating = null, int maxUpOffset = 0,
           int maxDownOffset = 0, int maxLeftOffset = 0, int maxRightOffset = 0)
        {
            if (!AreCapturesOn) return;

            var uniqueScreenshotName = GetUniqueScreenshotName();

            if (useStitchMode)
            {
                Eyes.StitchMode = StitchModes.CSS;
            }

            if (useLazyLoad)
            {
                LazyLoadPage();
            }

            if (layoutsToBeIgnored == null)
            {
                try
                {
                    Eyes.Check($"{uniqueScreenshotName} ",
                        Target.Region(allContentOfOverlayModalElement.InternalElement).Fully());
                }
                catch (Exception ex)
                {
                    IsCaptureFailed = true;
                    throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
                }
            }
            else
            {
                if (floating != null)
                {
                    try
                    {
                        Eyes.Check($"{uniqueScreenshotName} ", Target.Region(allContentOfOverlayModalElement.InternalElement).Fully().Floating(floating.InternalElement,
                            maxUpOffset, maxDownOffset, maxLeftOffset,
                            maxRightOffset).Layout(layoutsToBeIgnored.Select(i => i.InternalElement)));
                    }
                    catch (Exception ex)
                    {
                        IsCaptureFailed = true;
                        throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
                    }
                }
                else
                {
                    try
                    {
                        Eyes.Check($"{uniqueScreenshotName} ", Target.Window().Fully().Layout(layoutsToBeIgnored.Select(i => i.InternalElement)));
                    }
                    catch (Exception ex)
                    {
                        IsCaptureFailed = true;
                        throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
                    }
                }
            }

            Log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }

        public override void CaptureScreenRegionWithIgnoredLayouts(string screenshotName, IElement regionElement, List<IElement> layoutsToBeIgnored, bool useStitchMode = false, bool useLazyLoad = false)
        {
            if (!AreCapturesOn) return;

            var uniqueScreenshotName = GetUniqueScreenshotName();

            if (useStitchMode)
            {
                Eyes.StitchMode = StitchModes.CSS;
            }

            if (regionElement == null)
            {
                throw new ArgumentNullException(nameof(regionElement));
            }

            if (layoutsToBeIgnored == null)
            {
                throw new ArgumentNullException(nameof(layoutsToBeIgnored));
            }

            if (useLazyLoad)
            {
                LazyLoadPage();
            }

            Eyes.ForceFullPageScreenshot = true;

            try
            {
                Eyes.Check($"{uniqueScreenshotName} ", Target.Region(regionElement.InternalElement).Fully()
                        .Ignore(GetPaddedRectangles(layoutsToBeIgnored, 15, 5, regionElement)).IgnoreDisplacements());
            }
            catch (Exception ex)
            {
                IsCaptureFailed = true;
                throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
            }

            Log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
        }

        public override void CaptureFullPageWithIgnoredLayouts(string screenshotName, List<IElement> layoutsToBeIgnored,
            bool useStitchMode = false, bool useLazyLoad = false, IElement floating = null, int maxUpOffset = 0,
            int maxDownOffset = 0, int maxLeftOffset = 0, int maxRightOffset = 0)
        {
            if (!AreCapturesOn) return;

            var uniqueScreenshotName = GetUniqueScreenshotName();

            if (useStitchMode)
            {
                Eyes.StitchMode = StitchModes.CSS;
            }

            if (layoutsToBeIgnored == null)
            {
                throw new ArgumentNullException(nameof(layoutsToBeIgnored));
            }

            if (useLazyLoad)
            {
                LazyLoadPage();
            }

            if (floating != null)
            {
                try
                {
                    Eyes.Check($"{uniqueScreenshotName} ", Target.Window().Fully().Floating(floating.InternalElement,
                        maxUpOffset, maxDownOffset, maxLeftOffset,
                        maxRightOffset).Layout(layoutsToBeIgnored.Select(i => i.InternalElement)).IgnoreDisplacements());
                }
                catch (Exception ex)
                {
                    IsCaptureFailed = true;
                    throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
                }
            }
            else
            {
                try
                {
                    Eyes.Check($"{uniqueScreenshotName} ", Target.Window().Fully().Layout(layoutsToBeIgnored.Select(i => i.InternalElement)));
                }
                catch (Exception ex)
                {
                    IsCaptureFailed = true;
                    throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
                }

                Log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
            }
        }

        public override void CaptureVisibleScreenWithIgnoredLayouts(string screenshotName, List<IElement> layoutsToBeIgnored, bool useStitchMode = false, bool useLazyLoad = false, IElement floating = null, int offset = 0)
            {
                if (!AreCapturesOn) return;

                var uniqueScreenshotName = GetUniqueScreenshotName();

                if (useStitchMode)
                {
                    Eyes.StitchMode = StitchModes.CSS;
                }

                if (layoutsToBeIgnored == null)
                {
                    throw new ArgumentNullException(nameof(layoutsToBeIgnored));
                }

                if (useLazyLoad)
                {
                    LazyLoadPage();
                }

                Eyes.ForceFullPageScreenshot = false;

                if (floating != null)
                {
                    try
                    {
                        Eyes.Check($"{uniqueScreenshotName} ", Target.Window().Floating(floating.InternalElement, offset)
                                .Layout(layoutsToBeIgnored.Select(i => i.InternalElement)).IgnoreDisplacements());
                    }
                    catch (Exception ex)
                    {
                        IsCaptureFailed = true;
                        throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
                    }
                }
                else
                {
                    try
                    {
                        Eyes.Check($"{uniqueScreenshotName} ", Target.Window().Layout(layoutsToBeIgnored.Select(i => i.InternalElement)));
                    }
                    catch (Exception ex)
                    {
                        IsCaptureFailed = true;
                        throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
                    }
                }

                Log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
            }

        public override void CaptureScrollableOverlay(string screenshotName, By elementLocator, bool useStitchMode = false, bool useLazyLoad = false)
            {
                if (!AreCapturesOn) return;

                var uniqueScreenshotName = BuildTestScriptName();

                if (useLazyLoad)
                {
                    LazyLoadPage();
                }

                Eyes.ForceFullPageScreenshot = true;

                try
                {
                    Eyes.Check($"{uniqueScreenshotName} ", Target.Window().Fully().ScrollRootElement(elementLocator).IgnoreDisplacements());
                }
                catch (Exception ex)
                {
                    IsCaptureFailed = true;
                    throw new FrameworkVisualTestsException($"Visual test check failed with the message: {ex}", ex);
                }

                Log.Message($"Take screenshot of {uniqueScreenshotName} and upload to Applitools");
            }

    }
}
