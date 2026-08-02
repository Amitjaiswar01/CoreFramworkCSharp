using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Framework.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Automation.Framework.Core
{
    class LocateIos : Locate
    {
        public LocateIos(Browser browser) : base(browser)
        {
        }

        private const string NativeContext = "NATIVE_APP";

        public void SwitchToNativeContext(AppiumDriver<AppiumWebElement> driver)
        {
            driver.Context = NativeContext;
            Log.Message($"Current context after switch is: {driver.Context}");
        }

        public override IElement ElementByXpath(string selector, bool nativeContext = false)
        {
            if (!nativeContext)
            {
                _selector = selector;
                _locatorStrategy = LocatorStrategy.Css;

                Log.Message($"Locate an element by selector {_selector}");

                var wait = Browser.Wait.GetDefaultWait(60);
                int tryCounter = 0;
                IElement element = null;
                for (; tryCounter < 2; tryCounter++)
                {
                    bool successWait;
                    try
                    {
                        element = new Element(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(_selector))), Log, _selector, _locatorStrategy);
                        successWait = true;
                    }
                    catch
                    {
                        Log.Message($"Element {_selector} not located, trying again");
                        successWait = false;
                        if (tryCounter > 0)
                        {
                            throw;
                        }
                    }
                    if (successWait)
                    {
                        break;
                    }
                }
                return element;
            }
            else //Locate Element at iOS Native context
            {
                SwitchToNativeContext((AppiumDriver<AppiumWebElement>) Browser.Driver);

                //get text
                var element = Browser.Locate.ElementByXpath(selector);

                return element;
            }
        }
    }

}
