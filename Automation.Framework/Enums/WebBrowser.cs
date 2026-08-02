namespace Automation.Framework.Enums
{
    /// <summary>
    /// Supported drivers to launch a browser.
    /// </summary>
    public enum WebBrowser
    {
        /// <summary>
        /// Chrome Headless browser.
        /// </summary>
        ChromeHeadless = 1,

        /// <summary>
        /// User the Chrome browser to emulate a mobile view.
        /// </summary>
        ChromeMobileView,

        /// <summary>
        /// User the Chrome browser to emulate a tablet view.
        /// </summary>
        ChromeTabletView,

        /// <summary>
        /// Chrome browser.
        /// </summary>
        Chrome,

        /// <summary>
        /// Safari browser on Apple devices.
        /// </summary>
        Safari
    }
}
