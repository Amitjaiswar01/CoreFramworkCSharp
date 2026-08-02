using System;
using Automation.Framework.Core;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using OpenQA.Selenium;


namespace Automation.Framework
{
    /// <summary>
    /// Main access point to the Automation project.
    /// </summary>
    public interface IBrowser : IDisposable
    {
        /// <summary>
        /// Provide xUnit logging.
        /// </summary>
        Log Log { get; }

        /// <summary>
        /// Provide ability to locate elements on the screen.
        /// </summary>
        Locate Locate { get; }

        /// <summary>
        /// Provide ability to override the framework defined implicit wait for situations that require more time.
        /// </summary>
        FluentWait Wait { get; }

        /// <summary>
        /// Provide access to a IWebDriver for Selenium automation. See WebBrowser for all supported browsers and configurations.
        /// </summary>
        WebBrowser WebBrowser { get; }

        /// <summary>
        /// When true the browser will not be closed on the teardown of a test.
        /// </summary>
        bool DisposeBrowserAfterTest { get; set; }

        bool IsMobileCloud { get; set; }
   
        /// <summary>
        /// Has the Browser been properly constructed?
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Name of the test being executed.
        /// </summary>
        string TestName { get; }

        /// <summary>
        /// Version of the website.
        /// </summary>
        string SiteVersion { get; set; }

        /// <summary>
        /// Is Production instance ?
        /// </summary>
        bool IsProdInstance { get; set; }
        string TestTagName { get; }

        /// <summary>
        /// HTML page title of  the current screen.
        /// </summary>
        string PageTitle { get; }

        /// <summary>
        /// Page source of the current screen.
        /// </summary>
        string PageSource { get; }

        /// <summary>
        /// Url of the current screen.
        /// </summary>
        string PageUrl { get; }

        /// <summary>
        /// Navigate to the given URL.
        /// </summary>
        void Navigate(string url);

        /// <summary>
        /// Navigate to the pdp of the given sku
        /// </summary>
        void NavigateToPdp(string sku);

        /// <summary>
        /// Open new tab to the given URL.
        /// </summary>
        void OpenNewTab(string url);

        /// <summary>
        /// Refreshes the page in browser.
        /// </summary>
        void RefreshPage();

        /// <summary>
        /// Keyboard Tab action.
        /// </summary>
        void TabKeyboard();

        /// <summary>
        /// Switch focus to the given IFrame element.
        /// </summary>
        IElement SwitchFocusToIframe(IElement iframe);

        /// <summary>
        /// Switch context to the default content on the page.
        /// </summary>
        void SwitchToDefaultContent();

        /// <summary>
        /// Close the current tab and switch the the default tab.
        /// </summary>
        void CloseCurrentTab();
        
        /// <summary>
        /// Switch context to the currently visible window.
        /// </summary>
        void SwitchToCurrentWindow();

        /// <summary>
        /// Switch context to the window.
        /// </summary>
        void OpenWindow();

        /// <summary>
        /// Switch to the tab by the given index.
        /// </summary>
        void SwitchToTabByIndex(int tabNumber, bool forceRefreshOnSwitch = false);

        /// <summary>
        /// Click on the given element the given percentage from the end of the element.
        /// </summary>
        /// <param name="webElement">Element to click.</param>
        /// <param name="percentageFromEndOfElementToClick">Percentage from the end of element to click. Valid values are 1 - 99</param>
        void ClickHorizontalPositionOnElement(IElement webElement, int percentageFromEndOfElementToClick);

        /// <summary>
        /// Move to the element.
        /// </summary>
        void MoveToElement(IElement element, int offsetX = 0, int offsetY = 0);

        /// <summary>
        /// Move to the element and click on it
        /// </summary>
        /// <param name="element"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        void MoveToAndClickElement(IElement element, int offsetX = 0, int offsetY = 0);

        /// <summary>
        /// Click element using javascript.
        /// </summary>
        /// <param name="element"></param>
        void ClickByJs(IElement element);

        /// <summary>
        /// Simulate moving the mouse pointer over the given element.
        /// </summary>
        IElement MouseOverOnElement(IElement element, IElement forcedElement = null);

        /// <summary>
        /// Simulate moving the mouse pointer over the given element chain.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="elementOption"></param>
        void MouseOverOnElementChain(IElement element, IElement elementOption);

        /// <summary>
        /// Get the opacity of an element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        string GetElementOpacity(IElement element);

        /// <summary>
        /// Take a screenshot of the currently visible page.
        /// The test name will be included automatically.
        /// The format of the file name is {_screenshotPath}\{DateTime.Now:yyyy_MM_dd_hh_mm_ss}_{WebBrowser}_{TestName}{info}.jpg
        /// </summary>
        void TakeScreenshot(string name = "", bool addIndex = false, bool chromeDriverEntirePageScreenshot = false);

        /// <summary>
        /// Location of the last saved screenshot.
        /// </summary>
        string LastScreenshotPath { get; }

        /// <summary>
        /// Flag test status.
        /// </summary>
        bool IsTestFailed { get; set; }

        /// <summary>
        /// Get RemoteWebDriver session id
        /// </summary>
        string GridNodeSessionId { get; }

        string CloudTestStatusPassedJs { get; }
        string CloudTestStatusFailedJs { get; }

        IWebDriver Driver { get; }

        MobileDevice Device { get; }

        /// <summary>
        /// Returns inner Height of browser window.
        /// </summary>
        int GetWindowInnerHeight();

        /// <summary>
        /// Returns inner Width of browser window.
        /// </summary>
        int GetWindowInnerWidth();

        /// <summary>
        /// Update the size of the browser with the given width and height.
        /// </summary>
        /// <param name="width">Desired width of the browser.</param>
        /// <param name="height">Desired height of the browser.</param>
        void UpdateBrowserSize(int width, int height);

        /// <summary>
        /// Scroll to the top of the current window.
        /// </summary>
        void ScrollToTopOfWindow();

        /// <summary>
        /// Scroll to the bottom of page to make sure all images are loaded completely.
        /// </summary>
        void ScrollToBottomOfPage(string pageUrl);

        /// <summary>
        /// Scroll to the bottom of the current window.
        /// </summary>
        void ScrollToBottomOfWindow();

        /// <summary>
        /// Scroll the page until the requested element is in view.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="alignToBottom"></param>
        void ScrollIntoView(IElement element, bool alignToBottom = false);

        /// <summary>
        /// Adds cookie to browser.
        /// </summary>
        /// <param name="cookieName">Name of cookie to add.</param>
        /// <param name="cookieValue">Value of cookie to add.</param>
        void AddCookie(string cookieName, string cookieValue);

        /// <summary>
        /// Deletes a cookie by name.
        /// </summary>
        /// <param name="cookieName">Name of cookie to delete.</param>
        void DeleteCookie(string cookieName);

        /// <summary>
        /// Deletes all cookies in session
        /// </summary>
        void DeleteAllCookies();

        /// <summary>
        /// Gets a cookie object by name of the cookie.
        /// </summary>
        /// <param name="cookieName">Name of the cookie to get.</param>
        Cookie GetCookie(string cookieName);

        /// <summary>
        /// Delete all browser cookies.
        /// </summary>
        void ClearAllCookies();

        /// <summary>
        /// Clicks OK button in browser alert() and confirm() dialogs
        /// </summary>
        void AcceptAlert();

        /// <summary>
        /// Clicks Cancel button in browser confirm() dialogs
        /// </summary>
        void DismissAlert();

        /// <summary>
        /// Execute the given JavaScript code.
        /// </summary>
        object ExecuteJs<T>(string jsCode);

        /// <summary>
        /// Execute the given JavaScript with arguments.
        /// </summary>
        object ExecuteJs(string jsCode, params object[] args);

		/// <summary>
		/// The Selenium Internet Explorer WebDriver does not trigger the change event.
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
	    object DispatchChangeEvent(IElement element);

        /// <summary>
        /// Go back to the previous page.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Go to the next page.
        /// </summary>
        void GoForward();

        /// <summary>
        /// Scrolls down the current page until the specified element is visible.
        /// </summary>
        void ScrollToElement(IElement element);

        /// <summary>
        /// Scrolls vertically by pixels
        /// </summary>
        void ScrollToByPixelsVertical(string pixelScroll);

        /// <summary>
        /// Skips Test if detects the server error page.
        /// </summary>
        void SkipTestIfServerError();

        /// <summary>
        /// Clear browser session
        /// </summary>
        void ClearBrowserSession(string url);

        /// <summary>
        /// Wait for new tab to be opened
        /// </summary>
        void WaitForNewTab(int waitTime);

        void CloseAllWindowsButOriginal(string originalWindowHandle);

        void ClickWithTapByCoordinates(int middleX, int middle);

        void ClickWithTapByElementCoordinates(IElement element);

        void GetElementCoordinates(IElement element, ref int middleX, ref int middleY, int pageZoom);
        void GetAllIframesOnPage();
        void SwitchToIframeByIndex(int index);
        void SwitchToIframeById(string iFrameId, bool partialId = false);
        void ClearBrowserHistoryAndWebsiteData();
        void CloseApp(string appName);
        void ScrollToBottomOfPageJs();
        void ClickOnButtonMultipleTimes(IElement button, int maxRetries, Func<int, bool> expectedCondition);
        void SelectDropDownByText(IElement element, string text);
        void LazyLoadPage();
        void MouseOverJScript(IElement element);
        void ClickByCoordinatesJs(string xpath, int xOffset, int yOffset);
    }
}
