using System.Web.UI;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;

namespace Automation.Framework.Core
{
    public class MobileBrowser : Browser
    {

        public override void ScrollToBottomOfPage(string pageUrl)
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

        /// <inheritdoc />
        public override void MoveToElement(IElement element, int offsetX = 0, int offsetY = 0)
        {
            ScrollIntoView(element);
        }

        public MobileBrowser(WebBrowser browser, Log log, string testName, string testTagName, int desiredViewPortWidth, SessionSettings settings, int implicitSecondsToWaitForElement, string requiredStringInSource, bool visualTest, bool disposeBrowserAfterTest = true) : base(browser, log, testName, testTagName, desiredViewPortWidth, settings, implicitSecondsToWaitForElement, requiredStringInSource, visualTest, disposeBrowserAfterTest)
        {
        }

        public MobileBrowser(Log log, string testName) : base(log, testName)
        {
        }

        public MobileBrowser(MobileDevice device, Log log, string testName, string testTagName, int desiredViewPortWidth, SessionSettings settings, int implicitSecondsToWaitForElement, string requiredStringInSource, bool visualTest, bool disposeBrowserAfterTest = true) : base(device, log, testName, testTagName, desiredViewPortWidth, settings, implicitSecondsToWaitForElement, requiredStringInSource, visualTest, disposeBrowserAfterTest)
        {
        }
    }
}
