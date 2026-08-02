using System;
using System.Collections.Generic;
using System.Linq;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Automation.Framework.Core
{
    public class IphoneBrowser : MobileBrowser, IBrowserIos
    {
        private readonly int _waitTime;

        public IphoneBrowser(MobileDevice device, Log log, string testName, string testTagName,
            int desiredViewPortWidth,
            SessionSettings settings, int implicitSecondsToWaitForElement, string requiredStringInSource,
            bool visualTest, bool disposeBrowserAfterTest = true) :
            base(device, log, testName, testTagName, desiredViewPortWidth, settings, implicitSecondsToWaitForElement,
                requiredStringInSource, visualTest, disposeBrowserAfterTest)
        {
            WebBrowser = WebBrowser.Safari;
            _waitTime = implicitSecondsToWaitForElement;
        }

        private void SetGeoLocation(AppiumDriver<AppiumWebElement> driver, double latitude, double longitude)
        {
            driver.Location = new Location
            {
                Latitude = latitude,
                Longitude = longitude
            };
        }

        public void EnableGeoLocation(double latitude, double longitude)
        {
            SetGeoLocation((AppiumDriver<AppiumWebElement>)Driver, latitude, longitude);
            AllowLocationOnce();
            RefreshPage();
        }

        public void SwitchToWebViewContext(AppiumDriver<AppiumWebElement> driver)
        {
            var currentContext = driver.Context;
            Log.Message($"Current context is: {currentContext}");
            foreach (var context in driver.Contexts)
            {
                if (!context.Contains("NATIVE_APP"))
                {
                    driver.Context = context;
                }
            }
            var currentContextAfter = driver.Context;
            Log.Message($"Current context after switch is: {currentContextAfter}");
        }

        public void SwitchToNativeContext(AppiumDriver<AppiumWebElement> driver)
        {
            driver.Context = "NATIVE_APP";
            var currentContextAfter = driver.Context;
            Log.Message($"Current context after switch is: {currentContextAfter}");
        }

        public void AllowLocationOnce()
        {
            var originalWaitTime = _waitTime;
            var reducedWaitTime = originalWaitTime / 10;

            try
            {
                //Reduce wait time to quickly detect Location modal presence and switch to iOS native context.
                Wait.EnableImplicitWait(reducedWaitTime);
                var locationModalLocator = "//XCUIElementTypeButton[@name='Allow Once']";
                SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Driver);
                Wait.IsVisibleElement(By.XPath(locationModalLocator), timeToWait: -(originalWaitTime - reducedWaitTime));
                Log.Message("Location alert is shown");

                //Accept Location alert ('Allow Once' option)
                var alowLocationOnceElement = Locate.ElementByXpath(locationModalLocator, nativeContext: true);
                alowLocationOnceElement.Click();
            }
            catch
            {
                Log.Message("Location alert was not shown");
            }
            finally
            {
                //Switch back to web context and revert wait time to standard.
                SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Driver);
                Wait.EnableImplicitWait(originalWaitTime);
            }
        }

        public override void CloseApp(string appName)
        {
            SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Driver);

            //Close app
            Dictionary<string, object> paramsApp = new Dictionary<string, object>
            {
                {"bundleId", appName}
            };
            ((IJavaScriptExecutor)(AppiumDriver<AppiumWebElement>)Driver).ExecuteScript("mobile: terminateApp", paramsApp);

            SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Driver);
        }

        public void SetSafariAddressBarAtTheBottom()
        {
            SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Driver);

            //Open Safari Page Settings Menu
            Locate.ElementByXpath("//*[@label='Page Settings']").Click();

            //Set implicit wait time to the minimum
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            //Change address bar location if it not at the bottom. 
            try
            {
                var waitReductionTime = -59;
                Wait.IsVisibleElement(By.XPath("//*[@label='Show Bottom Tab Bar']"), waitReductionTime);
                Locate.ElementByXpath("//*[@label='Show Bottom Tab Bar']").Click();
            }
            catch (Exception)
            {
                Locate.ElementByXpath("//*[@label='Page Settings']").Click();
                Log.Message("Safari address bar is located at the bottom");
            }

            //Restore original implicit wait time
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_waitTime);

            //Switch back to webview context
            SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Driver);
        }

        public override void CloseCurrentTab()
        {
            SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Driver);

            //open Safari browser tabs menu
            Locate.ElementByXpath("//*[@label='Tabs']").Click();

            //close all tabs but the first one.
            var tabs = Locate.ElementsByXpath("//XCUIElementTypeButton[@label='Close']");//get all tabs

            if (tabs.Count > 1)
            {
                var allTabsButFirst = tabs.Skip(1).ToList();//skip first tab

                foreach (var tab in allTabsButFirst)
                {
                    tab.Click();//close tab
                }
            }

            //confirm tab(s) removal
            Locate.ElementByXpath("//*[@label='Done']").Click();

            //switch back to webview context
            SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Driver);
        }

        public override void ClearBrowserHistoryAndWebsiteData()
        {
            //open and close Settings app, so that it comes to a default state.       
            Dictionary<string, object> paramsApp = new Dictionary<string, object>
            {
                {"bundleId", "com.apple.Preferences"}
            };
            ((IJavaScriptExecutor)(AppiumDriver<AppiumWebElement>)Driver).ExecuteScript("mobile: launchApp", paramsApp);
            ((IJavaScriptExecutor)(AppiumDriver<AppiumWebElement>)Driver).ExecuteScript("mobile: terminateApp", paramsApp);
            ((IJavaScriptExecutor)(AppiumDriver<AppiumWebElement>)Driver).ExecuteScript("mobile: launchApp", paramsApp);

            //search for safari app
            Dictionary<string, object> paramsSettings = new Dictionary<string, object>
            {
                {"test", "test"}
            };

            SwitchToNativeContext((AppiumDriver<AppiumWebElement>)Driver);

            //swipe to get search menu
            paramsSettings.Add("direction", "down");
            ((IJavaScriptExecutor)(AppiumDriver<AppiumWebElement>)Driver).ExecuteScript("mobile: swipe", paramsSettings);
            Locate.ElementByXpath("//*[@label='Search']").SendKeys("Safari");
            Locate.ElementByXpath("//XCUIElementTypeCell[2]//*[@name='Safari']").Click();

            //scroll to clear Safari data
            paramsSettings.Clear();
            paramsSettings.Add("direction", "up");
            ((IJavaScriptExecutor)(AppiumDriver<AppiumWebElement>)Driver).ExecuteScript("mobile: swipe", paramsSettings);

            //Clear Safari data
            Locate.ElementByXpath("//*[@value='Clear History and Website Data' and @visible='true']").Click();
            try
            {
                Driver.FindElement(By.XPath("//XCUIElementTypeButton[@name='Clear History and Data']")).Click();
            }
            catch (Exception ex)
            {
                Log.Message("'Clear History' element was closed " + ex.Message);
            }

            ((IJavaScriptExecutor)(AppiumDriver<AppiumWebElement>)Driver).ExecuteScript("mobile: terminateApp", paramsApp);

            //OPEN UP SAFARI BROWSER
            Dictionary<string, object> paramsSafariApp = new Dictionary<string, object>
            {
                {"bundleId", "com.apple.mobilesafari"}
            };
            ((IJavaScriptExecutor)(AppiumDriver<AppiumWebElement>)Driver).ExecuteScript("mobile: launchApp", paramsSafariApp);

            SwitchToWebViewContext((AppiumDriver<AppiumWebElement>)Driver);
        }
    }
}