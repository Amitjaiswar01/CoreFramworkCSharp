using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI;
using Newtonsoft.Json.Linq;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Automation.Framework.Utilities;
using Cookie = OpenQA.Selenium.Cookie;
using Keys = OpenQA.Selenium.Keys;
using WebBrowser = Automation.Framework.Enums.WebBrowser;

namespace Automation.Framework.Core
{
    /// <summary>
    /// Provides access to automate websites navigation using Selenium WebDriver.
    /// Initializes framework helper classes such as Page, Locate, and Navigate.
    /// </summary>
    public class Browser : IBrowser
    {
        private const string _jsCodeDispatchChangeEvent = "var ev = document.createEvent('Event'); ev.initEvent('change', true, true); arguments[0].dispatchEvent(ev)";

        public readonly int BrowserWindowWidth = 1920;
        public readonly int BrowserWindowHeight = 1080;

        public bool IsProdInstance { get; set; }
        public bool IsMobileCloud { get; set; }

        private string _requiredStringInSource { get; }
        private Screenshot _screenshot { get; set; }
        private string _screenshotPath { get; set; }
        private string _testTagName { get; }
        private int _desiredViewPortWidth { get; }
        public string SiteVersion { get; set; }
        private bool IsVisualTest { get; }
        private string _gridNodeSessionId { get; }

        public SessionSettings Settings { get; }

        /// <inheritdoc />
        public Log Log { get; }

        /// <inheritdoc />
        public Locate Locate { get; }

        /// <inheritdoc />
        public FluentWait Wait { get; }

        /// <inheritdoc />
        public WebBrowser WebBrowser { get; set; }

        /// <inheritdoc />
        public string TestName { get; }

        public string TestTagName => _testTagName;

        /// <inheritdoc />
        public string PageTitle => Driver.Title;

        /// <inheritdoc />
        public string PageUrl => Driver.Url;

        /// <inheritdoc />
        public string PageSource => Driver.PageSource;

        /// <summary>
        /// Indicator of the index of the screenshot for the given test.
        /// </summary>
        public int ScreenshotIndex { get; }

        /// <inheritdoc />
        public string LastScreenshotPath { get; private set; }

        public string CloudTestStatusPassedJs => "sauce:job-result=passed";
        public string CloudTestStatusFailedJs => "sauce:job-result=failed";

        public bool IsTestFailed { get; set; }

        /// <inheritdoc />
        public bool DisposeBrowserAfterTest { get; set; }

        /// <inheritdoc />
        public bool IsInitialized { get; }

        /// <summary>
        /// Provide access to a IWebDriver for Selenium automation.
        /// </summary>
        public IWebDriver Driver { get; }

        public MobileDevice Device { get; }

        /// <summary>
        /// Provides access to automate websites navigation using Selenium WebDriver.
        /// Initializes framework helper classes such as Page, Locate, and Navigate.
        /// </summary>
        /// <param name="browser">Browser to configure for the test</param>
        /// <param name="log">Log utility to log results in xUnit.</param>
        /// <param name="testName">Name of the test being executed.</param>
        /// <param name="settings">Test configuration settings.</param>
        /// <param name="implicitSecondsToWaitForElement">Number of seconds to wait for Selenium interactions.</param>
        /// <param name="requiredStringInSource">Common word to designate you are on the expected website.</param>
        /// <param name="disposeBrowserAfterTest">By default the browser and driver will be closed at the end of execution.</param>
        public Browser(WebBrowser browser, Log log, string testName, string testTagName, int desiredViewPortWidth, SessionSettings settings, int implicitSecondsToWaitForElement, string requiredStringInSource, bool visualTest, bool disposeBrowserAfterTest = true)
        {
            _requiredStringInSource = requiredStringInSource;
            Settings = settings;
            _testTagName = testTagName;
            _desiredViewPortWidth = desiredViewPortWidth;
            IsVisualTest = visualTest;

            Log = log;
            Locate = new Locate(this);
            WebBrowser = browser;
            TestName = testName;
            ScreenshotIndex = 0;
            DisposeBrowserAfterTest = disposeBrowserAfterTest;

            InitializeScreenshotDirectory();
            Log.TestStarted(TestName);

            Device = null;

            Driver = new Driver().CreateDriver(browser, settings);

            //LocalFileDetector is required to upload images for tests running in docker containers
            ((RemoteWebDriver)Driver).FileDetector = new LocalFileDetector();

            if (Settings.IsLocalEnvironment)
            {
                Driver.Manage().Window.Maximize();
            }
            else
            {
                UpdateBrowserSize(BrowserWindowWidth, BrowserWindowHeight);
                //Get RemoteWebDriver session id
                var remoteWebDriver = Driver as RemoteWebDriver;
                if (remoteWebDriver == null) throw new Exception("Expecting a RemoteWebDriver in the Selenium Grid Environment");
                _gridNodeSessionId = remoteWebDriver.SessionId.ToString();
            }

            Log.Message(Driver.Manage().Window.Size.ToString());

            Wait = new FluentWait(Driver, this , implicitSecondsToWaitForElement);
            IsInitialized = true;
        }

        /// <summary>
        /// Simple Browser constructor used to build a Browser for pre initialization setup.
        /// </summary>
        /// <param name="log">Log utility to log results in xUnit.</param>
        /// <param name="testName">Name of the test being executed.</param>
        public Browser(Log log, string testName)
        {
            Log = log;
            TestName = testName;
        }

        /// <summary>
        /// Provides access to automate websites navigation using Selenium WebDriver for mobile devices.
        /// Initializes framework helper classes such as Page, Locate, and Navigate.
        /// </summary>
        public Browser(MobileDevice device, Log log, string testName, string testTagName, int desiredViewPortWidth, SessionSettings settings, int implicitSecondsToWaitForElement, string requiredStringInSource, bool visualTest, bool disposeBrowserAfterTest = true)
        {
            _requiredStringInSource = requiredStringInSource;
            Settings = settings;
            _testTagName = testTagName;
            _desiredViewPortWidth = desiredViewPortWidth;
            IsVisualTest = visualTest;
            IsMobileCloud = true;

            Log = log;

            if (Settings.MobileDevice.IsIphone)
            {
                Locate = new LocateIos(this);
            }
            else if(Settings.MobileDevice.IsAndroid)
            {
                Locate = new LocateAndroid(this);
            }
            else
            {
                Locate = new Locate(this);
            }

            TestName = testName;
            ScreenshotIndex = 0;
            DisposeBrowserAfterTest = disposeBrowserAfterTest;

            InitializeScreenshotDirectory();
            Log.TestStarted(TestName);

            var cloudRunConfig = ConfigurationManager.AppSettings["MobileGridCloud"];

            Driver = cloudRunConfig.Equals("true") ? new DriverCloud().CreateMobileCloudDriver(device, settings, "SauceLabs", TestName)
                : new Driver().CreateMobileWebViewDriver(device, settings, log, IsVisualTest);

            if (!Settings.IsLocalEnvironment)
            {
                //Get RemoteWebDriver session id
                var remoteWebDriver = Driver as RemoteWebDriver;
                if (remoteWebDriver == null) throw new Exception("Expecting a RemoteWebDriver in the Selenium Grid Environment");
                _gridNodeSessionId = remoteWebDriver.SessionId.ToString();
            }

            Log.Message(Driver.Manage().Window.Size.ToString());

            Wait = new FluentWait(Driver, this, implicitSecondsToWaitForElement);
            Wait.EnableImplicitWait(implicitSecondsToWaitForElement);

            IsInitialized = true;

            Device = device;
        }

        /// <inheritdoc />
        /// NOTE: This will silently ignore url requests that are empty.
        public void Navigate(string url)
        {
            var retryCount = 3;

            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    Driver.Navigate().GoToUrl(url);
                    Wait.ForDomReady();
                    Log.Message($"Navigate to {url}");
                }
                else
                {
                    throw new ArgumentException("URL is null or empty.");
                }
            }
            catch (Exception ex)
            {
                Log.Message($"Initial navigation attempt failed: {ex.Message}");

                for (var currentRetry = 1; currentRetry <= retryCount; currentRetry++)
                {
                    try
                    {
                        Wait.EnableImplicitWait(20);
                        Driver.Navigate().GoToUrl(url);
                        Log.Message($"Retry #{currentRetry}: Navigate to {url}");
                        Wait.ForDomReady();
                        break;  // If the navigation succeeded, break out of the retry loop.
                    }
                    catch (Exception retryEx)
                    {
                        Log.Message($"Retry #{currentRetry} failed: {retryEx.Message}");

                        if (currentRetry == retryCount)
                        {
                            // If maximum retry count exceeded, rethrow the exception.
                            throw;
                        }
                    }
                }
            }
            finally
            {
                if (Wait.ImplicitSecondsToWait == 20)
                {
                    Wait.EnableImplicitWait(70);
                }
                
                Log.Message("Navigation was successful.");
            }
        }

        public string GridNodeSessionId => _gridNodeSessionId;

        /// <inheritdoc />
        public void TabKeyboard()
        {
            var actions = new Actions(Driver);
            actions.SendKeys(Keys.Tab).Build().Perform();
        }

        public void NavigateToPdp(string sku) { Navigate($"https://lampsplus.com/products/~__{sku}.html"); }

        public void OpenNewTab(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                ExecuteJs("window.open('" + url + "')");
                Log.Message($"Open New Tab to {url}");
            }
        }

        /// <inheritdoc />
        public void RefreshPage()
        {
            Navigate(PageUrl);
        }

        public void SelectDropDownByText(IElement element, string text)
        {
            var dropDown = new SelectElement(element.InternalElement);
            dropDown.SelectByText(text);
        }

        /// <inheritdoc />
        public IElement SwitchFocusToIframe(IElement iFrame)
        {
            if (string.Equals(iFrame.TagName, HtmlTextWriterTag.Iframe.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Wait.ForDisplayedElement(iFrame, 5);
                Driver.SwitchTo().Frame(iFrame.InternalElement);
                Log.Message("Context was switched to iframe");
            }
            else { Log.Message("The requested element was not a frame. Unable to switch context."); }

            return iFrame;
        }

        public void SwitchToIframeByIndex(int iFrameIndex)
        {

            Driver.SwitchTo().Frame(iFrameIndex);
                Log.Message($"Context was switched to iframe {iFrameIndex}");
        }

        public void SwitchToIframeById(string iFrameId, bool partialId = false)
        {

            if (partialId)
            {
                ReadOnlyCollection<IElement> framesList = Locate.ElementsByTagName(HtmlTextWriterTag.Iframe);
                for (int i = 0; i <= framesList.Count; i++)
                {
                    if (framesList[i].GetAttribute("id").Contains(iFrameId))
                    {
                        Driver.SwitchTo().Frame(framesList[i].GetAttribute("id"));
                        break;
                    }
                }
            }
            else
            {
                Driver.SwitchTo().Frame(iFrameId);
                Log.Message($"Context was switched to iframe {iFrameId}");
            }
        }

        public virtual void ClearBrowserHistoryAndWebsiteData()
        {
        }

        public virtual void CloseApp(string appName)
        {
        }

        public void GetAllIframesOnPage()
        {
            ReadOnlyCollection<IElement> framesList = Locate.ElementsByTagName(HtmlTextWriterTag.Iframe);
            foreach (var frame in framesList)
            {
                Log.Message($"iframe id is:{frame.GetAttribute("id")}");
            }
        }

        /// <inheritdoc />
        public void SwitchToDefaultContent()
        {
            Driver.SwitchTo().DefaultContent();
            Log.Message("Context switched to a main document");
        }

        /// <inheritdoc />
        public virtual void CloseCurrentTab()
        {
            var handles = Driver.WindowHandles;
            Log.Message($"Number of windows: {handles.Count}");

            if (handles.Count > 1)
            {
                Driver.SwitchTo().Window(handles[1]).Close();
                Driver.SwitchTo().Window(handles[0]);
            }
        }

        public void CloseAllWindowsButOriginal(string originalWindowHandle)
        {
            //close current browser tab
            var handles = Driver.WindowHandles;
            foreach (var handle in handles)
            {
                if (handle != originalWindowHandle)
                {
                    Driver.SwitchTo().Window(handle).Close();
                }
            }
        }

        /// <inheritdoc />
        public void SwitchToCurrentWindow()
        {
            var homeWindow = Driver.CurrentWindowHandle;
            var allWindows = Driver.WindowHandles;

            foreach (var handle in allWindows) { if (handle != homeWindow) { Driver.SwitchTo().Window(handle); } }
        }

        /// <inheritdoc />
        public void OpenWindow()
        {
            Driver.SwitchTo().Window(string.Empty);
            Log.Message("Window opened");
        }

        public void WaitForNewTab(int waitTime)
        {
            Wait.ForDomReady();
            var windowHandles = Driver.WindowHandles;
            Log.Message($"Number of handles is: {windowHandles.Count}");
            Wait.ForBoolCondition(windowHandles.Count > 1, waitTime);
        }

        public void ClickWithTapByCoordinates(int middleX, int middle)
        {
            var tap = new Dictionary<string, string>
            {
                {"x", middleX.ToString()}, {"y", middle.ToString()}
            };

            var wait = new Dictionary<string, object>

            {
                { "duration", 2 } // Duration in seconds
            };

            ((IJavaScriptExecutor)Driver).ExecuteScript("mobile: tap", tap, wait);
        }

        public void ClickWithTapByElementCoordinates(IElement element)
        {
            var xElementCoordinate = 0;
            var yElementCoordinate = 0;
            GetElementCoordinates(element, ref xElementCoordinate, ref yElementCoordinate, 100);

            var tap = new Dictionary<string, string>
            {
                {"x", xElementCoordinate.ToString()}, {"y", yElementCoordinate.ToString()}
            };
            ((IJavaScriptExecutor)Driver).ExecuteScript("mobile: tap", tap);
        }

        public void GetElementCoordinates(IElement element, ref int middleX, ref int middleY, int pageZoom)
        {
            var zoomFactor = pageZoom;//LP website zoom factor
            var leftX = element.Location.X * zoomFactor / 100;
            var rightX = leftX + element.Size.Width * zoomFactor / 100;
            var middleXraw = (rightX + leftX) / 2;
            middleX = middleXraw * zoomFactor / 100;
            var upperY = element.Location.Y * zoomFactor / 100; 
            var lowerY = upperY + element.Size.Height * zoomFactor / 100;
            middleY = lowerY * 110 / 100;
        }

        /// <inheritdoc />
        public void SwitchToTabByIndex(int tabNumber, bool forceRefreshOnSwitch = false)
        {
            Driver.SwitchTo().Window(Driver.WindowHandles[tabNumber]);
            Log.Message($"Switched to browser tab at index {tabNumber}");

            if (!forceRefreshOnSwitch) return;

            Wait.ForDomReady();
        }

        /// <inheritdoc />
        public virtual void MoveToElement(IElement element, int offsetX = 0, int offsetY = 0)
        {
            var actions = new Actions(Driver);

            if (offsetX == 0 && offsetY == 0) { actions.MoveToElement(element.InternalElement).Build().Perform(); }
            else { actions.MoveToElement(element.InternalElement, offsetX, offsetY).Build().Perform(); }
        }

        /// <inheritdoc />
        public void MoveToAndClickElement(IElement element, int offsetX = 0, int offsetY = 0)
        {
            var actions = new Actions(Driver);

            if (offsetX == 0 && offsetY == 0) { actions.MoveToElement(element.InternalElement).Click().Build().Perform(); }
            else { actions.MoveToElement(element.InternalElement, offsetX, offsetY).Click().Build().Perform(); }
        }

        /// <inheritdoc />
        public void ClickByJs(IElement element)
        {
            Driver.ExecuteJavaScript<object>("arguments[0].click()", element.InternalElement);
        }

        /// <inheritdoc />
        public virtual IElement MouseOverOnElement(IElement element, IElement forcedElement = null)
        {
            if (Settings.IsTabletEmulationView)
            {
                element.Click();
                return element;
            }

            var action = new Actions(Driver);

            try
            {
                MoveToElement(element);
            }
            catch
            {
                MoveToElement(element);
                // Firefox throws error when trying to hover over element not in viewport
                ExecuteJs("arguments[0].scrollIntoView()", element.InternalElement);
                action.MoveToElement(element.InternalElement).Perform();
            }

            if (forcedElement == null || forcedElement.Displayed) return element;

            if (forcedElement.GetCssValue("visibility") == "hidden")
            {
                ExecuteJs("arguments[0].style.visibility = 'visible'", forcedElement.InternalElement);
            }

            if (forcedElement.GetCssValue("display") == "none")
            {
                ExecuteJs("arguments[0].style.display = 'block'", forcedElement.InternalElement);
            }

            if (forcedElement.GetCssValue("opacity") == "0")
            {
                ExecuteJs("arguments[0].style.opacity = '1'", forcedElement.InternalElement);
            }

            return element;
        }

        public void MouseOverJScript(IElement element)
        {
            var mouseOverScript = "if(document.createEvent){var evObj = document.createEvent('MouseEvents');evObj.initEvent('mouseover',true, false); arguments[0].dispatchEvent(evObj);} else if (document.createEventObject) { arguments[0].fireEvent('onmouseover'); }";

            ExecuteJs(mouseOverScript, element.InternalElement);
        }

        /// <inheritdoc />
        public void MouseOverOnElementChain(IElement element, IElement elementOption)
        {
            var action = new Actions(Driver);
            action.MoveToElement(element.InternalElement).ClickAndHold().MoveToElement(elementOption.InternalElement).Build().Perform();
        }

        /// <inheritdoc />
        public string GetElementOpacity(IElement element) => element.GetCssValue("opacity");

        /// <inheritdoc />
        public void TakeScreenshot(string info = "", bool addIndex = false, bool chromeDriverEntirePageScreenshot = false)
        {
            if (addIndex) { info = $"{info} {ScreenshotIndex}"; }
            if (!string.IsNullOrWhiteSpace(info)) { info = $"_{info}"; }

            var testPath = TestName.Split('.');
            var testName = string.Empty;

            if (testPath.Length > 3) { testName = $"{testPath[3]}.{testPath[4]}.{testPath[testPath.Length - 1]}"; } // Get NameOfTestClassAndBrowser.TestName

            LastScreenshotPath = string.IsNullOrEmpty(info) ? $@"{_screenshotPath}\{DateTime.Now:yyyy_MM_dd_hh_mm_ss}_{testName}.jpg" : $@"{_screenshotPath}\{DateTime.Now:yyyy_MM_dd_hh_mm_ss}_{testName} {info}.jpg";

            try
            {
                if (chromeDriverEntirePageScreenshot)
                {
                    GetEntirePageScreenshot();
                }
                else
                {
                    _screenshot = Driver.TakeScreenshot();
                    _screenshot.SaveAsFile(LastScreenshotPath, ScreenshotImageFormat.Jpeg);
                }

                Log.Message($"Screen shot saved {Log.GetHtmlLinkString(LastScreenshotPath)}");
            }
            catch (Exception ex)
            {
                Log.Message($"Unable to take screenshot of {Log.GetHtmlLinkString(LastScreenshotPath)}");
                Log.Message($"Stack trace: {ex}");
            }
        }

        /// <inheritdoc />
        public int GetWindowInnerHeight()
        {
            if (!int.TryParse(ExecuteJs("return window.innerHeight").ToString(), out var innerHeight))
            {
                throw new InvalidCastException("Cannot cast innerHeight");
            }

            return innerHeight;
        }

        /// <inheritdoc />
        public int GetWindowInnerWidth()
        {
            if (!int.TryParse(ExecuteJs("return window.innerWidth").ToString(), out var innerWidth))
            {
                throw new InvalidCastException("Cannot cast innerWidth");
            }

            return innerWidth;
        }

        /// <inheritdoc />
        public void UpdateBrowserSize(int width, int height)
        {
            Driver.Manage().Window.Size = new Size(width, height);
            Log.Message($"Window width = {width} and height = {height}");
        }

        /// <inheritdoc />
        public void ScrollToTopOfWindow()
        {
            ExecuteJs("window.scrollTo(document.body.scrollHeight, 0)");
            Log.Message("Scroll to top of current window");
        }

        public void ScrollToByPixelsVertical(string pixelScroll)
        {
            ExecuteJs($"window.scrollBy(0, {pixelScroll})");
        }

        /// <inheritdoc />
        public virtual void ScrollToBottomOfPage(string pageUrl)
        {
            var scrollNumberCounter = 0;
            if (pageUrl.Contains("products") && !PageUrl.Contains(HtmlTextWriterTag.Html.ToString().ToLower()))
            {
                scrollNumberCounter = 200;
            }
            else if (!pageUrl.Contains("products"))
            {
                scrollNumberCounter = 80;
            }

            for (var i = 0; i < scrollNumberCounter; i++)
                ExecuteJs("window.scrollBy(0, 100)");
        }

        public void ScrollToBottomOfPageJs()
        {
            ExecuteJs("window.scrollTo(0,document.body.scrollHeight);");
            Wait.ForDomReady();
            Log.Message("Scroll to bottom of page");
        }

        /// <inheritdoc />
        public void ScrollToBottomOfWindow()
        {
            ExecuteJs("window.scrollTo(0, document.body.scrollHeight)");
            Log.Message("Scroll to bottom of the current window");
        }

        /// <inheritdoc />
        public void ScrollIntoView(IElement element, bool alignToBottom = false)
        {
            ExecuteJs($"arguments[0].scrollIntoView({(alignToBottom ? "false" : "")})", element.InternalElement);
            Log.Message("Scroll to a particular element until it is visible");
        }

        /// <inheritdoc />
        public void ClickHorizontalPositionOnElement(IElement element, int percentageFromEndOfElementToClick)
        {
            try
            {
                Assert.InRange(percentageFromEndOfElementToClick, 1, 99);

            }
            catch
            {
                Log.Message($"{percentageFromEndOfElementToClick} is not in the valid range 1 - 99");
                throw;
            }
            MoveToAndClickElement(element, element.Size.Width * percentageFromEndOfElementToClick / 100, element.Size.Height / 2);
        }

        /// <inheritdoc />
        public void AddCookie(string cookieName, string cookieValue) { Driver.Manage().Cookies.AddCookie(new Cookie(cookieName, cookieValue)); }

        /// <inheritdoc />
        public void DeleteCookie(string cookieName) { Driver.Manage().Cookies.DeleteCookieNamed(cookieName); }

        /// <inheritdoc />
        public void DeleteAllCookies() { Driver.Manage().Cookies.DeleteAllCookies(); }

        public void ClearBrowserSession(string url)
        {
            if (IsVisualTest)
            {
                Navigate(url);
                Wait.ForPage(url);
                RefreshPage();
                Wait.ForDomReady();
                var jsonString = Locate.ElementByTagName(HtmlTextWriterTag.Pre).Text;
                var jsonObject = JObject.Parse(jsonString);
                var InstanceName = jsonObject.SelectToken("InstanceName").ToString();
                Log.Message($"Denv.aspx instance is: {InstanceName}");
            }

            Driver.Manage().Cookies.DeleteAllCookies();
            Log.Message("All cookies have been deleted successfully");
        }

        /// <inheritdoc />
        public Cookie GetCookie(string cookieName) { return Driver.Manage().Cookies.GetCookieNamed(cookieName); }

        /// <inheritdoc />
        public void ClearAllCookies() { Driver.Manage().Cookies.DeleteAllCookies(); }

        /// <inheritdoc />
        public void AcceptAlert() { Driver.SwitchTo().Alert().Accept(); }

        /// <inheritdoc />
        public void DismissAlert() { Driver.SwitchTo().Alert().Dismiss(); }

        /// <inheritdoc />
        public object ExecuteJs<T>(string jsCode) => Driver.ExecuteJavaScript<object>(jsCode);

        /// <inheritdoc />
        public object ExecuteJs(string jsCode, params object[] args) => Driver.ExecuteJavaScript<object>(jsCode, args);

        /// <inheritdoc />
        public object DispatchChangeEvent(IElement webElement) => ExecuteJs(_jsCodeDispatchChangeEvent, webElement.InternalElement);

        /// <inheritdoc />
        public void GoBack() { Driver.Navigate().Back(); }

        /// <inheritdoc />
        public void GoForward() { Driver.Navigate().Forward(); }

        /// <inheritdoc />
        public void ScrollToElement(IElement element)
        {
            var el = (RemoteWebElement)element.InternalElement;
            // ReSharper disable once UnusedVariable
            var location = el.LocationOnScreenOnceScrolledIntoView;
        }

        /// <inheritdoc />
        public void SkipTestIfServerError()
        {
            if (IsInitialized)
            {
                // Js makes sure body element exists, and if it does it returns the inner text.
                // Not using IWebElement for performance, and not using Browser.Locate.ElementImmediately to avoid recursion.
                var bodyText = ExecuteJs("return document.querySelector('body') && document.querySelector('body').innerText").ToString().ToLower();

                // If it can't find text of body, then we don't have the prerequisite to deem this a server error.
                if (string.IsNullOrWhiteSpace(bodyText))
                {
                    return;
                }

                // Checks if on site by checking if specific string is contained in the source that wouldn't be present in a server error page.
                var isOnSite = PageSource.ToLower().Contains(_requiredStringInSource.ToLower());
                var matchErrorCode = new Regex($@"({HttpStatusCode.NotFound}|{HttpStatusCode.InternalServerError}|{HttpStatusCode.ServiceUnavailable})").Match(bodyText);
                var isServerErrorPage = bodyText.Contains("error") && matchErrorCode.Success && !isOnSite;

                if (isServerErrorPage)
                {
                    if (!string.IsNullOrEmpty(matchErrorCode.Value))
                    {
                        Skip.If(true, $"Server Error {matchErrorCode.Value}: {System.Web.HttpWorkerRequest.GetStatusDescription(Convert.ToInt32(matchErrorCode.Value))}");
                    }
                }
            }
            else
            {
                Log.Message("WARNING: The Browser was not initialized");
            }
        }

        /// <summary>
        /// Take a screenshot of the currently displayed page. Call quit on the Driver and log test cleanup.
        /// </summary>
        // ReSharper disable once InheritdocConsiderUsage
        public void Dispose()
        {
            Log.Message("Browser disposal request");

            if (DisposeBrowserAfterTest)
            {
                //Driver?.Close();
                Driver?.Quit();
            }
            Log.Message("Browser disposed");
            Log?.LogToFile();
        }

        private void InitializeScreenshotDirectory()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _screenshotPath = $@"{baseDirectory}\ScreenCaptures";

            if (!Directory.Exists(_screenshotPath)) { Directory.CreateDirectory(_screenshotPath); }
        }

        /// <summary>
        /// Method to lazy load entire page content
        /// </summary>
        public void LazyLoadPage()
        {
            // Get web page total Size
            var totalWidthTemp = ((IJavaScriptExecutor)Driver).ExecuteScript("return document.body.offsetWidth").ToString();
            var totalWidth = Convert.ToInt32(totalWidthTemp);
            var totalHeightTemp = ((IJavaScriptExecutor)Driver).ExecuteScript("return  document.body.parentNode.scrollHeight").ToString();
            var totalHeight = Convert.ToInt32(totalHeightTemp);

            // Get the Size of the Viewport
            var viewportWidthTemp = ((IJavaScriptExecutor)Driver).ExecuteScript("return document.body.clientWidth").ToString();
            var viewportWidth = Convert.ToInt32(viewportWidthTemp);
            var viewportHeightTemp = ((IJavaScriptExecutor)Driver).ExecuteScript("return window.innerHeight").ToString();
            var viewportHeight = Convert.ToInt32(viewportHeightTemp);

            // Split the Screen in multiple Rectangles
            List<Rectangle> rectangles = new List<Rectangle>();
            // Loop until the Total Height is reached
            for (var tempHeight = 0; tempHeight < totalHeight; tempHeight += viewportHeight)
            {
                var newHeight = viewportHeight;
                // Fix if the Height of the Element is too big
                if (tempHeight + viewportHeight > totalHeight)
                {
                    newHeight = totalHeight - tempHeight;
                }

                // Loop until the Total Width is reached
                for (var tempWidth = 0; tempWidth < totalWidth; tempWidth += viewportWidth)
                {
                    var newWidth = viewportWidth;
                    // Fix if the Width of the Element is too big
                    if (tempWidth + viewportWidth > totalWidth)
                    {
                        newWidth = totalWidth - tempWidth;
                    }

                    // Create and add the Rectangle
                    Rectangle currRect = new Rectangle(tempWidth, tempHeight, newWidth, newHeight);
                    rectangles.Add(currRect);
                }
            }

            //Scroll page
            Rectangle previous = Rectangle.Empty;

            foreach (var rectangle in rectangles)
            {
                // Calculate the Scrolling (if needed)
                if (previous != Rectangle.Empty)
                {
                    int xDiff = rectangle.Right - previous.Right;
                    int yDiff = rectangle.Bottom - previous.Bottom;

                    // Scroll
                    ((IJavaScriptExecutor)Driver).ExecuteScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                    Thread.Sleep(200);
                }

                // Set the Previous Rectangle
                previous = rectangle;
            }

            //Scroll to top of page
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight);");
        }

        /// <summary>
        /// Method to take a screenshot of the entire browser page at test failure. 
        /// </summary>
        public void GetEntirePageScreenshot()
        {
            Bitmap stitchedImage = null;//setup Bitmap

            Wait.WaitForAjaxComplete();//Let page load

            try
            {
                // Get web page total Size
                var totalWidthTemp = ((IJavaScriptExecutor)Driver).ExecuteScript("return document.body.offsetWidth").ToString();
                var totalWidth = Convert.ToInt32(totalWidthTemp);
                var totalHeightTemp = ((IJavaScriptExecutor)Driver).ExecuteScript("return  document.body.parentNode.scrollHeight").ToString();
                var totalHeight = Convert.ToInt32(totalHeightTemp);

                // Get the Size of the Viewport
                var viewportWidthTemp = ((IJavaScriptExecutor)Driver).ExecuteScript("return document.body.clientWidth").ToString();
                var viewportWidth = Convert.ToInt32(viewportWidthTemp);
                var viewportHeightTemp = ((IJavaScriptExecutor)Driver).ExecuteScript("return window.innerHeight").ToString();
                var viewportHeight = Convert.ToInt32(viewportHeightTemp);

                // Split the Screen in multiple Rectangles
                List<Rectangle> rectangles = new List<Rectangle>();
                // Loop until the Total Height is reached
                for (var tempHeight = 0; tempHeight < totalHeight; tempHeight += viewportHeight)
                {
                    var newHeight = viewportHeight;
                    // Fix if the Height of the Element is too big
                    if (tempHeight + viewportHeight > totalHeight)
                    {
                        newHeight = totalHeight - tempHeight;
                    }

                    // Loop until the Total Width is reached
                    for (var tempWidth = 0; tempWidth < totalWidth; tempWidth += viewportWidth)
                    {
                        var newWidth = viewportWidth;
                        // Fix if the Width of the Element is too big
                        if (tempWidth + viewportWidth > totalWidth)
                        {
                            newWidth = totalWidth - tempWidth;
                        }

                        // Create and add the Rectangle
                        Rectangle currRect = new Rectangle(tempWidth, tempHeight, newWidth, newHeight);
                        rectangles.Add(currRect);
                    }
                }

                // Build the Image
                stitchedImage = new Bitmap(totalWidth, totalHeight);

                // Get all Screenshots and stitch them together
                Rectangle previous = Rectangle.Empty;

                foreach (var rectangle in rectangles)
                {
                    // Calculate the Scrolling (if needed)
                    if (previous != Rectangle.Empty)
                    {
                        int xDiff = rectangle.Right - previous.Right;
                        int yDiff = rectangle.Bottom - previous.Bottom;

                        // Scroll
                        ((IJavaScriptExecutor)Driver).ExecuteScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                        Thread.Sleep(200);
                    }

                    // Take Screenshot
                    var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

                    // Build an Image out of the Screenshot
                    Image screenshotImage;

                    using (MemoryStream memStream = new MemoryStream(screenshot.AsByteArray))
                    {
                        screenshotImage = Image.FromStream(memStream);
                    }

                    // Calculate the Source Rectangle
                    Rectangle sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);

                    // Copy the Image
                    using (Graphics g = Graphics.FromImage(stitchedImage))
                    {
                        g.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
                    }

                    // Set the Previous Rectangle
                    previous = rectangle;
                }
                stitchedImage.Save(LastScreenshotPath, ImageFormat.Jpeg);//Save stitched Image to Screen Captures assembly folder
            }
            catch (Exception ex)
            {
                Log.Message("Unable to take entire page screenshot");
                Log.Message($"Stack trace: {ex}");
            }
        }

        public void ClickOnButtonMultipleTimes(IElement button, int maxRetries, Func<int, bool> expectedCondition)
        {
            var ifButtonClicked = false;

            for (var i = 0; i < maxRetries; i++)
            {
                try
                {
                    Wait.EnableImplicitWait(5);
                    ClickByJs(button);
                    expectedCondition(-65);
                    ifButtonClicked = true;
                    return; // Exit the method if the expected condition is met
                }
                catch (Exception)
                {
                    Console.WriteLine($"Expected condition not met after wait. Retrying...");
                }
                finally
                {
                    Wait.EnableImplicitWait(70);
                }
            }

            if (ifButtonClicked == false)
            {
                throw new Exception($"Failed to click the button after {maxRetries} retries.");
            }
        }

        //Use the xpath as an anchor point and use the x/y offset to click on the desired element.
        public void ClickByCoordinatesJs(string xpath, int xOffset, int yOffset)
        {
            var jsCode = $@"
                            function clickAboveElement(xpath, xOffset, yOffset) {{
                                var xpathResult = document.evaluate(xpath, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null);
                                var element = xpathResult.singleNodeValue;
                                if (element) {{
                                    var rect = element.getBoundingClientRect();
                                    var newX = rect.left + xOffset + window.scrollX;
                                    var newY = rect.top + yOffset + window.scrollY;
                                    var targetElement = document.elementFromPoint(newX, newY);
                                    if (targetElement) {{
                                        var clickEvent = new MouseEvent('click', {{
                                            'view': window,
                                            'bubbles': true,
                                            'cancelable': true
                                        }});
                                        targetElement.dispatchEvent(clickEvent);
                                        console.log('Clicked at: ' + newX + ', ' + newY);
                                    }} else {{
                                        console.log('No element found at the specified coordinates.');
                                    }}
                                }} else {{
                                    console.log('Reference element not found.');
                                }}
                            }}
                            clickAboveElement(""{xpath}"", {xOffset}, {yOffset});
                            ";
            ExecuteJs(jsCode);
        }
    }
}
