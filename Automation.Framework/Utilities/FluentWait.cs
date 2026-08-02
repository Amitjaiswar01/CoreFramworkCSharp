using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Automation.Framework.Core;
using Automation.Framework.Enums;
using Automation.Framework.Exceptions;
using Automation.Framework.Verifies;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Automation.Framework.Utilities
{
#pragma warning disable 3001, 3002
    /// <summary>
    /// Provide ability to add additional time to the default framework implicit wait (10 sec).
    /// </summary>
    public class FluentWait
    {
        private readonly IWebDriver _driver;
        private readonly Browser _browser;
        private Log _log => _browser.Log;

	    private const int CheckOrderIdExistsInterval = 15;
	    private const int CheckOrderIdExistsTimeOut = 900;

		/// <summary>
		/// Placeholder for error messages.
		/// </summary>
		private string _message;

        private void ExceptionFailTestStatusAndLog(string exceptionMessage, DateTime startWait, int secondsToWait)
        {
            _browser.IsTestFailed = true;
            LogError(exceptionMessage, startWait, secondsToWait);
        }

        /// <summary>
        /// Provide ability to override the default framework defined implicit wait.
        /// </summary>
        public FluentWait(IWebDriver driver, Browser browser, int wait)
        {
            _driver = driver;
            _browser = browser;
            ImplicitSecondsToWait = wait;
        }

        /// <summary>
        /// Default maximum time in seconds to wait to locate elements used by the framework implicit wait.
        /// </summary>
        public int ImplicitSecondsToWait;

        /// <summary>
        /// Enable the standard framework time to wait for an element. 
        /// </summary>
        public void EnableImplicitWait(int secondsToWait)
        {
            if (secondsToWait == 0) { secondsToWait = ImplicitSecondsToWait; }

            var time = new TimeSpan(0, 0, secondsToWait);

            _driver.Manage().Timeouts().ImplicitWait = time;
            _log.Message($"Implicit wait enabled and set to {time}");
        }

        /// <summary>
        /// Get a specified time to wait ignoring the default implicit wait.
        /// </summary>
        /// <param name="timeToWait"></param>
        /// <returns></returns>
        public int FiniteTime(int timeToWait = 1) => Math.Abs(-ImplicitSecondsToWait + timeToWait);

        /// <summary>
        /// Wait for the IFrame to be loaded and switch to it with Expected conditions.
        /// </summary>
        /// <param name="iframe">Frame element to wait to be loaded.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the frame element to be loaded.</param>
        public void WaitForIframeAndSwitchToIt(string iframe, int timeToWait = 0)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(iframe));
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                _browser.IsTestFailed = true;
                LogError("The requested IFrame in the DOM did not load in the requested time.", startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw;
            }
        }


        /// <summary>
        /// Wait the given time to wait for the element to be displayed on the screen.
        /// Note: The timeToWait is added to the framework implicit wait time.
        /// </summary>
        /// <param name="element">Expected element to be displayed.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the given element to be displayed.</param>
        /// <param name="ignoreError">Do not throw an error when false when the ignoreError flag is true.</param>
        /// <returns>Requested element.</returns>
        public IElement ForDisplayedElement(IElement element, int timeToWait = 0, bool ignoreError = false)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.

			try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(delegate { return element.Displayed; });
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs);}
            }

            catch (Exception ex)
            {
                var errorMessage = $"Unable to locate the requested element {element.LocatorString}";
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                if (!ignoreError)
                {
                    ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                    throw new FrameworkWaitException(errorMessage, ex);
                }
            }

            return element;
        }

        /// <summary>
        /// Wait for the given element to be enabled.
        /// </summary>
        /// <param name="element">Enabled element to wait for.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be enabled.</param>
        /// <returns>Requested element.</returns>
        public IElement ForEnabledElement(IElement element, int timeToWait = 0)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(delegate { return element.Enabled; });
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Element {element.LocatorString} is not Enabled";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
            return element;
        }

        /// <summary>
        /// Wait for the given element to be clickable. A maximum of three attempts will be made to click on an element.
        /// </summary>
        /// <param name="element">Clickable element to wait for.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be clickable.</param>
        /// <returns>Requested element.</returns>
        public IElement ForClickableElement(IElement element, int timeToWait = 0)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.
            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(ExpectedConditions.ElementToBeClickable(element.InternalElement));
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Unable to click the requested element {element.LocatorString}";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
            return element;
        }


        /// <summary>
        /// Wait for the given element to be visible. 
        /// </summary>
        /// <param name="By">Selenium By locator.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be visible.</param>
        /// <returns>Requested bool flag</returns>
        public bool IsVisibleElement(By By, int timeToWait = 0)
        {

            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.
            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                wait.Until(ExpectedConditions.ElementIsVisible(By));
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = "Element is not visible";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
            return true;
        }

        public bool AreAllElementsVisible(By by, int timeToWait = 0)
        {

            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.
            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = "Element is not visible";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
            return true;
        }


        /// <summary>
        /// Wait for the given element to become invisible. 
        /// </summary>
        /// <param name="locator">string locator to become invisible.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be visible.</param>
        /// <returns>Requested bool flag</returns>
        public bool IsInvisibleElement(By locator, int timeToWait = 0)
        {

            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.
            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Element is not invisible: {locator}";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
            return true;
        }
        

        /// <summary>
        /// Wait for the given page to load by the URL.
        /// </summary>
        /// <param name="url">URL for the given page</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be clickable.</param>
        public void ForPage(string url, int timeToWait = 1, bool contains = false)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait);
                if (contains)
                    wait.Until(x => x.Url.Contains(url));
                else
                    wait.Until(x => x.Url == url);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                _browser.IsTestFailed = true;
                LogError($"Never reached {url} in given time.", startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw;
            }
        }

        /// <summary>
        /// Wait for the given page to load by the URL with ExpectedConditions.
        /// </summary>
        /// <param name="url">URL for the given page</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be clickable.</param>
        public bool ForPageWait(string url, int timeToWait = 1)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.
            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(ExpectedConditions.UrlContains(url));
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
                return true;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Never reached {url} in given time.";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Wait for the DOM be in the ready state.
        /// </summary>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the DOM to load.</param>
        public void ForDomReady(int timeToWait = 0)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(x => _browser.ExecuteJs("return document.readyState").ToString() == "complete");
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = "The DOM did not load in the requested time.";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Wait for the condition to return true
        /// </summary>
        /// <param name="method">Method that checks a condition and returns a bool</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the frame element to be loaded.</param>
        /// <param name="ignoreError">Do not throw an error when false when the ignoreError flag is true.</param>
        public bool ForCondition(Func<bool> method, int timeToWait = 0, bool ignoreError = false)
        {
            var result = false;
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.
            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(x => method() == true);
                result = true;
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = "The condition did not return true in the requested time.";
                if (!ignoreError)
                {
                    ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                    if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                    throw new FrameworkWaitException(errorMessage, ex);
                }
            }

            return result;
        }

        /// <summary>
        /// Wait for the bool condition to return true
        /// </summary>
        /// <param name="condition">bool condition that checks a condition and returns a bool</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the frame element to be loaded.</param>
        /// <param name="ignoreError">Do not throw an error when false when the ignoreError flag is true.</param>
        public bool ForBoolCondition(bool condition, int timeToWait = 0, bool ignoreError = false)
        {
            var result = false;
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(x => condition == true);
                result = true;
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = "The condition did not return true in the requested time.";
                if (!ignoreError)
                {
                    ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                    if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                    throw new FrameworkWaitException(errorMessage, ex);
                }
            }

            return result;
        }

        /// <summary>
        /// Wait for the IFrame to be in the DOM.
        /// </summary>
        /// <param name="iframe">Frame element to wait to be loaded.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the frame element to be loaded.</param>
        public void ForIframeDomReady(IElement iframe, int timeToWait = 0)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(x => _browser.ExecuteJs("return arguments[0].contentWindow.document.getElementsByTagName('html')[0].classList.contains('js-initialized-global') === true", 
                    iframe.InternalElement));
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = "The requested IFrame in the DOM did not load in the requested time.";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Wait for the given element to be unloaded from the DOM.
        /// </summary>
        /// <param name="element">Element to wait for unloading from the DOM.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be unloaded from the DOM.</param>
        public void UntilElementUnloads(IElement element, int timeToWait = 0)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait + ImplicitSecondsToWait);
                wait.Until(ExpectedConditions.StalenessOf(element.InternalElement));
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Element {element.LocatorString} is still attached to the DOM";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Wait for the given element by selector to not exist in DOM.
        /// </summary>
        /// <param name="selector">Selector targeting the element to wait for unloading from the DOM.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be unloaded from the DOM.</param>
        /// <param name="ignoreError">Do not throw an error when false when the ignoreError flag is true.</param>
        public bool UntilElementDoesntExist(string selector, int timeToWait = 0, bool ignoreError = false)
        {
            var result = false;
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait + ImplicitSecondsToWait);
                wait.Until(delegate { return !(bool)_browser.ExecuteJs($"return !!document.querySelector('{selector}')"); });
                result = true;
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Element {selector} is still attached to the DOM";
                if (!ignoreError)
                {
                    ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                    if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                    throw new FrameworkWaitException(errorMessage, ex);
                }
            }

            return result;
        }

        /// <summary>
        /// Wait for the given element to have an attribute with a specified value.
        /// </summary>
        /// <param name="element">The element to wait for unloading from the DOM.</param>
        /// <param name="attribute">The html attribute that will have the desired value.</param>
        /// <param name="value">The value to wait for in the elements attribute.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be unloaded from the DOM.</param>
        public void ForElementWithAttribute(IElement element, string attribute, string value, int timeToWait = 0)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.
            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(delegate { return element.GetAttribute(attribute) == value; });
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Unable to find hidden element {element} with attribute {attribute}=\"{value}\". Actual value is {element.GetAttribute(attribute)}";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Wait for the given element to get a specific css class.
        /// </summary>
        /// <param name="element">The element to wait for.</param>
        /// <param name="className">The desired class name.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be unloaded from the DOM.</param>
        public void ForElementWithCssClass(IElement element, string className, int timeToWait = 0)
        {
            if (className.StartsWith(".")) { className = className.Remove(0, 1); }

            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.
            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(delegate { return ElementActions.HasClass(element, className); });
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Unable to find element {element.LocatorString} with class {className}";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
        }

        public bool ForElementWithCssClassReturned(IElement element, string className, int timeToWait = 0)
        {
            if (className.StartsWith(".")) { className = className.Remove(0, 1); }

            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.
            var wait = GetDefaultWait(timeToWait);
            return wait.Until(delegate { return ElementActions.HasClass(element, className); });
        }

        /// <summary>
        /// Wait for the given element to have a specific css class removed. Helpful for when some class (e.g. "hidden") gets removed after animation is complete.
        /// </summary>
        /// <param name="element">The element to wait for.</param>
        /// <param name="className">The desired class name.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be unloaded from the DOM.</param>
        public void ForElementWithoutCssClass(IElement element, string className, int timeToWait = 0)
        {
            if (className.StartsWith(".")) { className = className.Remove(0, 1); }

            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.
            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(delegate { return !ElementActions.HasClass(element, className); });
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Unable to find element {element.LocatorString} without class {className}";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Return the requested element when found, or log error.
        /// </summary>
        /// <param name="element">Element to locate.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be found.</param>
        /// <returns></returns>
        public IElement ForElement(IElement element, int timeToWait = 0)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait + ImplicitSecondsToWait);
                wait.Until(delegate { return element.IsInitialized; });
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
                return element;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Unable to find element {element.LocatorString}";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Return the requested elements when found, or log error.
        /// </summary>
        /// <param name="elements">Elements to locate.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the elements to be found.</param>
        /// <returns></returns>
        public IReadOnlyCollection<IElement> ForElements(IReadOnlyCollection<IElement> elements, int timeToWait = 0)
        {
            var startTime = DateTime.Now;
            timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.

            try
            {
                var wait = GetDefaultWait(timeToWait);
                wait.Until(delegate { return elements.Count > 0; });
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
                return elements;
            }
            catch (Exception ex)
            {
                List<string>elementsNames = new List<string>();
                foreach (var element in elements)
                {
                    elementsNames.Add(element.LocatorString);
                }
                var errorMessage = "Unable to find elements:"  + elementsNames;
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }
        }

        /// <summary>
        /// Wait for the given element to stop animating/moving it's position on the page.
        /// </summary>
        /// <param name="element">Element to wait for animation end.</param>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to stop animating.</param>
        public IElement ForElementToStopAnimating(IElement element, int timeToWait = 0)
	    {
	        var startTime = DateTime.Now;
		    timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency wiht other methods in this class.
		    string GetElemCoordinateSum() =>
			    _browser.ExecuteJs("var r = arguments[0].getBoundingClientRect(); return r.top + r.right + r.bottom + r.left;", element.InternalElement).ToString();
		    var cachedElemRect = "0";

		    try
		    {
			    var wait = GetDefaultWait(timeToWait + ImplicitSecondsToWait);

                // A single wait just in case it's called too quickly before element animation begins.
                Thread.Sleep(500);

			    wait.Until(delegate
			    {
			        try
			        {
			            if (GetElemCoordinateSum() == cachedElemRect)
			            {
			                return true;
			            }
			        }
                    // Stale element exception catch. Means we're on another page after element click so instead of breaking the test, we return true.
			        catch (StaleElementReferenceException)
                    {
			            return true;
			        }

				    cachedElemRect = GetElemCoordinateSum();
                    return false;
			    });

                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch (Exception ex)
		    {
                var errorMessage = $"Element {element.LocatorString} never stopped animating.";
                ExceptionFailTestStatusAndLog(errorMessage, startTime, timeToWait);
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
                throw new FrameworkWaitException(errorMessage, ex);
            }

            return element;
        }

		/// <summary>
		/// Wait and check if order id exists in database
		/// </summary>
		/// <param name="orderId"></param>
		/// <param name="orderChecker">Name of the Lambda method</param>
		/// <returns></returns>
		public Object ForOrder(string orderId, Func<string, Object> orderChecker)
		{
			var orderIdModel = new object();
			var orderIdInDatabase = string.Empty;
			var startTime = DateTime.Now;
		    var maxAttempt = CheckOrderIdExistsTimeOut / CheckOrderIdExistsInterval;

		    for (var i = 0; i < maxAttempt; i++)
		    {
			    Thread.Sleep(CheckOrderIdExistsInterval * 1000);
			    orderIdModel = orderChecker(orderId);
			    if (orderIdModel == null) continue;

				var orderIdProperty = orderIdModel.GetType().GetProperty("OrderId");
			    if (orderIdProperty == null) continue;

			    orderIdInDatabase = orderIdProperty.GetValue(orderIdModel, null).ToString();
			    if (!string.IsNullOrEmpty(orderIdInDatabase))
				    break;
		    }

		    _message = $"Unable to find order {orderId} in database for {orderChecker.Method.Name}. First check time: {startTime}. Last check time: {DateTime.Now}.";
			var verify = new Assert(_browser);
			verify.Equals(orderId, orderIdInDatabase, _message);

			return orderIdModel;
		}

        /// <summary>
        /// Wait for mobile modal (lpMobileDrawer or lpMobileOverlay) to completely close (after animation).
        /// </summary>
        /// <param name="mobileModalContainer">The lpMobileDrawer or lpMobileOverlay element</param>
        public void ForMobileModalToFullyClose(IElement mobileModalContainer)
        {
            try
            {
                ForElementWithCssClass(mobileModalContainer, "hidden");
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
            }
        }

        /// <summary>
        /// Wait for mobile modal (lpMobileDrawer or lpMobileOverlay) to completely open (after animation).
        /// </summary>
        /// <param name="mobileModalContainer">The lpMobileDrawer or lpMobileOverlay element</param>
        public void ForMobileModalToFullyOpen(IElement mobileModalContainer)
        {
            try
            {
                ForElementWithoutCssClass(mobileModalContainer, "hidden");
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
            }
        }

        /// <summary>
        /// Wait for Ajax to complete
        /// </summary>
        public void WaitForAjaxComplete(int timeToWait = 0)
        {
            try
            {
                timeToWait += ImplicitSecondsToWait; // Add implicit wait time for consistency with other methods in this class.
                var wait = GetDefaultWait(timeToWait);
                wait.Until(driver => (bool)_browser.ExecuteJs("return jQuery.active == 0"));
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusPassedJs); }
            }
            catch
            {
                if (_browser.IsMobileCloud) { _browser.ExecuteJs(_browser.CloudTestStatusFailedJs); }
            }
        }

        /// <summary>
        /// Return a DefaultWait object configured with a 250ms polling and set to the given max timeToWait.
        /// NoSuchElement and NullReference exceptions are ignored here.
        /// </summary>
        /// <param name="timeToWait">Maximum time (added to the implicit wait) in seconds to wait for the given element to be found.</param>
        /// <returns></returns>
        public DefaultWait<IWebDriver> GetDefaultWait(int timeToWait)
        {
            var wait = new DefaultWait<IWebDriver>(_driver)
            {
                Timeout = TimeSpan.FromSeconds(timeToWait),
                PollingInterval = TimeSpan.FromMilliseconds(250)
            };
            wait.IgnoreExceptionTypes(typeof(Exception), typeof(NoSuchElementException), typeof(NullReferenceException), typeof(StaleElementReferenceException));

            return wait;
        }

        private void LogError(string message, DateTime startTime, int secondsToWait)
        {
            _browser.SkipTestIfServerError();

            _message = $"{message} in {DateTime.Now - startTime} seconds of the requested {secondsToWait} seconds.";
            _log.Message(_message);
        }
    }
#pragma warning restore 3001, 3002
}
