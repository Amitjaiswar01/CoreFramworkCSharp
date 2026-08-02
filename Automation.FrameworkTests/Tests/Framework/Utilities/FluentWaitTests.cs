using System;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

using Automation.Framework.Utilities;
using Automation.FrameworkTests.Utilities;

namespace Automation.FrameworkTests.Tests.Framework.Utilities
{
    /// <summary>
    /// Tests to verify the functionality of the FluentWait class located Automation.Framework.Utilities.
    /// </summary>
    [Trait(Traits.Category, Traits.Unit), Trait(Traits.Feature, "FluentWait")]
    // ReSharper disable once InheritdocConsiderUsage
    public class FluentWaitTests : BrowserBase
    {
        private DateTime _startTime { get; }

        /// <summary>
        /// Tests to verify the functionality of the FluentWait class located Automation.Framework.Utilities.
        /// </summary>
        public FluentWaitTests(ITestOutputHelper output) : base(output, "FluentWaitTests")
        {
            Browser.Navigate(LampsPlusHomePageUrl);

            _startTime = DateTime.Now;
        }

        /// <summary>
        /// Assert FluentWait.ForDisplayedElement returns in the expected time when an element is displayed on the screen.
        /// </summary>
        [SkippableFact]
        public void AssertFluentWaitForDisplayedElementFoundTest()
        {
            Browser.Wait.ForDisplayedElement(SearchButton);

            AssertFluentWait();
        }

        /// <summary>
        /// Assert when using the fluent wait method, Browser.Wait.ForDisplayedElement(IWebElement, int),
        /// An error will be triggered at most 1 seconds after the expected condition has elapsed.
        /// </summary>
        /// <param name="secondsToWait">Maximum time (added to the implicit wait) in seconds to wait for the given element to be displayed.</param>
        [SkippableTheory]
        [InlineData(2)]
        [InlineData(10)]
        public void AssertWaitForDisplayedElementAddsWaitTimeToImplicitWaitTest(int secondsToWait)
        {
            Assert.Throws<NoSuchElementException>(() => Browser.Wait.ForDisplayedElement(NotValidElement, secondsToWait));

            AssertFluentWait(secondsToWait);
        }

        /// <summary>
        /// Assert when using the fluent wait method, Browser.Wait.ForDisplayedElement(IWebElement, int),
        /// An error will be triggered at most 1 seconds after the expected condition has elapsed.
        /// </summary>
        [SkippableFact]
        public void AssertWaitForDisplayedElementAddsDefaultWaitTimeToImplicitWaitTest()
        {
            Assert.Throws<NoSuchElementException>(() => Browser.Wait.ForDisplayedElement(NotValidElement));

            AssertFluentWait();
        }

        /// <summary>
        /// Assert FluentWait.ForEnabledElement returns in the expected time when an element is enabled on the screen.
        /// </summary>
        [SkippableFact]
        public void AssertFluentWaitForEnabledElementFoundTest()
        {
            Browser.Wait.ForEnabledElement(SearchButton);

            AssertFluentWait();
        }

        /// <summary>
        /// Assert when using the fluent wait method, Browser.Wait.ForEnabledElement(IWebElement, int),
        /// An error will be triggered at most 1 seconds after the expected condition has elapsed.
        /// </summary>
        /// <param name="secondsToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be enabled.</param>
        [SkippableTheory]
        [InlineData(2)]
        [InlineData(10)]
        public void AssertWaitForEnabledElementAddsWaitTimeToImplicitWaitTest(int secondsToWait)
        {
            Assert.Throws<Exception>(() => Browser.Wait.ForEnabledElement(NotValidElement, secondsToWait));

            AssertFluentWait(secondsToWait);
        }

        /// <summary>
        /// Assert when using the fluent wait method, Browser.Wait.ForEnabledElement(IWebElement),
        /// An error will be triggered at most 1 seconds after the expected condition has elapsed.
        /// </summary>
        [SkippableFact]
        public void AssertWaitForEnabledElementAddsDefaultWaitTimeToImplicitWaitTest()
        {
            Assert.Throws<Exception>(() => Browser.Wait.ForEnabledElement(NotValidElement));

            AssertFluentWait();
        }

        /// <summary>
        /// Assert FluentWait.ForClickableElement returns in the expected time when an element is displayed and clickable on the screen.
        /// </summary>
        [SkippableFact]
        public void AssertFluentWaitForClickableElementFoundTest()
        {
            Browser.Wait.ForClickableElement(SearchButton);

            AssertFluentWait();
        }

        /// <summary>
        /// Assert when using the fluent wait method, Browser.Wait.ForClickableElement(IWebElement, int),
        /// An error will be triggered at most 1 seconds after the expected condition has elapsed.
        /// </summary>
        /// <param name="secondsToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be enabled.</param>
        [SkippableTheory]
        [InlineData(2)]
        [InlineData(10)]
        public void AssertWaitForClickableElementAddsWaitTimeToImplicitWaitTest(int secondsToWait)
        {
            Assert.Throws<Exception>(() => Browser.Wait.ForClickableElement(NotValidElement, secondsToWait));

            AssertFluentWait(secondsToWait);
        }

        /// <summary>
        /// Assert when using the fluent wait method, Browser.Wait.ForClickableElement(IWebElement),
        /// An error will be triggered at most 1 seconds after the expected condition has elapsed.
        /// </summary>
        [SkippableFact]
        public void AssertWaitForClickableElementAddsDefaultWaitTimeToImplicitWaitTest()
        {
            Assert.Throws<Exception>(() => Browser.Wait.ForClickableElement(NotValidElement));

            AssertFluentWait();
        }

        /// <summary>
        /// Assert FluentWait.ForDisplayedElement returns in the expected time when an element is displayed on the screen.
        /// </summary>
        [SkippableFact]
        public void AssertFluentWaitForDomReadyFoundTest()
        {
            Browser.Wait.ForDomReady();

            AssertFluentWait();
        }

        /// <summary>
        /// Assert FluentWait.ForIframeDomReady returns in the expected time when an IFrame element is displayed on the screen.
        /// </summary>
        [SkippableFact]
        public void AssertFluentWaitForIframeDomReadyFoundTest()
        {
            Assert.Throws<Exception>(() => Browser.Wait.ForIframeDomReady(SearchButton, 2));

            AssertFluentWait();
        }

        /// <summary>
        /// Assert when using the fluent wait method, Browser.Wait.UntilElementUnloads(IWebElement, int),
        /// An error will be triggered at most 1 seconds after the expected condition has elapsed.
        /// </summary>
        /// <param name="secondsToWait">Maximum time (added to the implicit wait) in seconds to wait for the element to be unloaded from the DOM.</param>
        [SkippableTheory]
        [InlineData(2)]
        [InlineData(10)]
        public void AssertWaitUntilElementUnloadsAddsWaitTimeToImplicitWaitTest(int secondsToWait)
        {
            Assert.Throws<Exception>(() => Browser.Wait.UntilElementUnloads(SearchButton, secondsToWait));

            AssertFluentWait(secondsToWait);
        }

        /// <summary>
        /// Assert when using the fluent wait method, Browser.Wait.UntilElementUnloads(IWebElement),
        /// An error will be triggered at most 1 seconds after the expected condition has elapsed.
        /// </summary>
        [SkippableFact]
        public void AssertWaitUntilElementUnloadsAddsDefaultWaitTimeToImplicitWaitTest()
        {
            Assert.Throws<Exception>(() => Browser.Wait.UntilElementUnloads(SearchButton));

            AssertFluentWait();
        }

        private void AssertFluentWait(int timeToWait = 0)
        {
            timeToWait += Browser.Wait.ImplicitSecondsToWait;
            var currentDateTime = DateTime.Now;

            var minTime = new TimeSpan(0, 0, timeToWait);
            var maxDate = _startTime.AddSeconds(minTime.TotalSeconds).AddSeconds(1.5); // Wait the specified time to wait in seconds plus 1.5 second for additional overhead.

            Assert.True(currentDateTime < maxDate, $"Expected to wait between {_startTime} - {currentDateTime} ({timeToWait} seconds) but waited for {currentDateTime - _startTime} seconds");
        }
    }
}
