using System.Linq;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using OpenQA.Selenium.Appium;

namespace Automation.Framework.Core
{
    public class IpadBrowser : Browser
    {
        private const string NativeContext = "NATIVE_APP";
        
        public IpadBrowser(MobileDevice device, Log log, string testName, string testTagName, int desiredViewPortWidth,
            SessionSettings settings, int implicitSecondsToWaitForElement, string requiredStringInSource,
            bool visualTest, bool disposeBrowserAfterTest = true) :
            base(device, log, testName, testTagName, desiredViewPortWidth, settings, implicitSecondsToWaitForElement,
                requiredStringInSource, visualTest, disposeBrowserAfterTest)
        {
            WebBrowser = WebBrowser.Safari;
        }

        public override IElement MouseOverOnElement(IElement element, IElement forcedElement = null)
        {
            element.Click(); //Click for iPad instead of actions class MoveToElement()
            return element;
        }

        public void SwitchToWebViewContext(AppiumDriver<AppiumWebElement> driver)
        {
            driver.Context = driver.Contexts.First(context => !context.Contains(NativeContext));
            Log.Message($"Current context after switch is: {driver.Context}");
        }

        public void SwitchToNativeContext(AppiumDriver<AppiumWebElement> driver)
        {
            driver.Context = NativeContext;
            Log.Message($"Current context after switch is: {driver.Context}");
        }
    }
}