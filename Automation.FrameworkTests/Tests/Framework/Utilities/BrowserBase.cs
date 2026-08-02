using System;
using Xunit.Abstractions;

using Automation.Framework;
using Automation.Framework.Core;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;

namespace Automation.FrameworkTests.Tests.Framework.Utilities
{
    /// <summary>
    /// Base class to provide Browser, Log, and Verify objects.
    /// </summary>
    public class BrowserBase : IDisposable
    {
        /// <summary>
        /// Provides access to Selenium.
        /// </summary>
        internal IBrowser Browser { get; }

        /// <summary>
        /// Log class to provide common logging format.
        /// </summary>
        internal Log Log { get; }

        /// <summary>
        /// Fake element that doesn't exist to test what happens if an invalid element is requested.
        /// </summary>
        internal IElement NotValidElement => new Element(Browser.Locate.ElementById("Not Valid").InternalElement, Log, "Not Valid", LocatorStrategy.Id);

        internal IElement SearchButton => new Element(Browser.Locate.ElementById("searchBtn").InternalElement, Log, "searchBtn", LocatorStrategy.Id);

        internal const string LampsPlusHomePageUrl = "https://www.lampsplus.com";

        /// <summary>
        /// Base class to provide Browser, Log, and Verify objects.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        /// <param name="testName">Name of the test for logging.</param>
        /// <param name="enableRealTimeLogging">When true logs will not be written to the console. Logs in the Logs folder will be updated on disk in real time.</param>
        /// <param name="disposeBrowserAfterTest">Dispose of the browser and driver after the test has completed when true.</param>
        /// <param name="browser">Which browser configuration to use for the Browser.</param>
        public BrowserBase(ITestOutputHelper output, string testName,bool enableRealTimeLogging = false, bool disposeBrowserAfterTest = true, WebBrowser browser = WebBrowser.Chrome)
        {
            var testTagName = "IntegrationTestTag";
            var desiredViewPortWidth = 0;
            var defaultViewPortWidth = 0;
            var visualTest = false;

            Log = new Log(output, testName, enableRealTimeLogging);

            var settings = new SessionSettings { IsLocalEnvironment = true };
            Browser = new Browser(browser, Log, testName, testTagName, desiredViewPortWidth, settings, 10, string.Empty, visualTest, disposeBrowserAfterTest);
        }

        /// <summary>
        /// Dispose of test resources.
        /// </summary>
        public void Dispose()
        {
            Browser.Dispose();
        }
    }
}
