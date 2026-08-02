using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Automation.Framework.Enums;
using Automation.Framework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
//using WebDriverManager;
//using WebDriverManager.DriverConfigs.Impl;

namespace Automation.Framework.Core
{
    /// <summary>
    /// The Driver provides access to Selenium WebDriver for interacting with web browsers.
    /// </summary>
    public class Driver
    {
        private const string HeadlessArgumentString = "--headless";
        private string _proxyAddress { get; set; }

        /// <summary>
        /// Time to wait for the driver to wait before timing out.
        /// </summary>
        protected const int DriverTimeoutInMinutes = 9000;

        /// <summary>
        /// Should the tests be executed locally, or on a Selenium Grid?
        /// </summary>
        public bool RunLocalDriver { get; set; }

        /// <summary>
        /// Remote URI to connect to when running tests on the Selenium Grid.
        /// </summary>
        public string RemoteUri { get; set; }

        public IWebDriver CreateDriver(WebBrowser browser, SessionSettings settings)
        {
            InitializeSettings(settings);
            var cloudRunConfig = ConfigurationManager.AppSettings["DesktopGridCloud"];

            switch (browser)
            {
                case WebBrowser.Chrome:
                { 
                    //Note: if 'cloudRunConfig' set to 'true', the Desktop Windows Chrome test automation will run in the cloud.
                    return cloudRunConfig.Equals("false") ? new DriverCloud().CreateChromeCloudDriver(settings) : CreateChromeDriver(testName: settings.SettingsTestName, gridPort: settings.HubPort);

                }
                //case WebBrowser.ChromeHeadless: { return CreateChromeDriver(true); }
                case WebBrowser.ChromeMobileView: { return CreateChromeDriver(isMobileView: settings.IsMobileView, testName: settings.SettingsTestName, gridPort: settings.HubPort); }
                case WebBrowser.ChromeTabletView: { return CreateChromeDriver(isTabletEmulationView: settings.IsTabletEmulationView, testName: settings.SettingsTestName, gridPort: settings.HubPort); }
                case WebBrowser.Safari:
                {
                    return cloudRunConfig.Equals("true") ? new DriverCloud().CreateSafariCloudDriver(settings, "LambdaTest")
                            : new Driver().CreateSafariDriver(settings);
                }
                default: throw new ArgumentException($"The browser: {browser} is not a valid selection");
            }
        }

        public IWebDriver CreateMobileWebViewDriver(MobileDevice device, SessionSettings settings, Log log, bool isVisualMobileTest = false)
        {
            var options = new AppiumOptions();

            InitializeSettings(settings);

            if (!string.IsNullOrWhiteSpace(device.AutomationLibrary))
            {
                options.AddAdditionalCapability(MobileCapabilityType.AutomationName, device.AutomationLibrary);
            }

            if (!string.IsNullOrWhiteSpace(device.DeviceName))
            {
                options.AddAdditionalCapability(MobileCapabilityType.DeviceName, device.DeviceName);
            }

            if (!string.IsNullOrWhiteSpace(device.DeviceUuid))
            {
                options.AddAdditionalCapability(MobileCapabilityType.Udid, "auto");
            }

            if (!string.IsNullOrWhiteSpace(device.BrowserName))
            {
                options.AddAdditionalCapability(MobileCapabilityType.BrowserName, device.BrowserName);
            }

            if (!string.IsNullOrWhiteSpace(device.PlatformVersion))
            {
                options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, device.PlatformVersion);
            }

            if (device.AutomationLibrary == "XCUITest")
            {
                var applicationMobileGridSettings = ConfigurationManager.GetSection("GridGroup/MobileGrid") as NameValueCollection;

                if (isVisualMobileTest)//LP Baseline/Target mobile device switch for iOS visual test.
                {
                    if (settings.IsBaseLine)
                    {
                        options.AddAdditionalCapability("version", "baseline");
                    }
                    else if (!settings.IsBaseLine)
                    {
                        options.AddAdditionalCapability("version", "target");
                    }
                }

                options.AddAdditionalCapability("startIWDP", true); //This capability will let to start ios_webkit_debug_proxy programmatically on host Mac machine.
                options.AddAdditionalCapability("xcodeOrgId", applicationMobileGridSettings.GetValues("xcodeOrgId").FirstOrDefault());
                options.AddAdditionalCapability("xcodeSigningId", applicationMobileGridSettings.GetValues("xcodeSigningId").FirstOrDefault());

                //Device orientation options
                options.AddAdditionalCapability("orientation", device.IsPad ? "LANDSCAPE" : "PORTRAIT");

                //Specific tests options
                if (settings.SettingsTestName.Contains("T7234_iPad_VerifyTheLayoutOfCategoryMenus") && device.IsPad)
                {
                    options.AddAdditionalCapability("appium:nativeWebTap", true);
                }
                
                options.AddAdditionalCapability("autoAcceptAlerts", true);
                options.AddAdditionalCapability("safariAllowPopups", true);
                options.AddAdditionalCapability("safariInitialUrl", "https://www.lampsplus.com/denv.aspx?j=1");
                options.AddAdditionalCapability("newCommandTimeout", 120);

                if (settings.IsTabletView && device.IsPad)
                {
                    options.AddAdditionalCapability("newCommandTimeout", 300);

                    var tabletHubPort = settings.HubPort;

                    var macHubAddress = applicationMobileGridSettings.GetValues("SeleniumMobileHubHost").FirstOrDefault();
                    RemoteUri = $"http://{macHubAddress}:{tabletHubPort}/wd/hub"; //Re-assigning remote URI to Mobile Tablet Hub

                    return new IOSDriver<AppiumWebElement>(new Uri(RemoteUri), options,
                        TimeSpan.FromMinutes(DriverTimeoutInMinutes));
                }
                if (settings.IsMobileView)
                {
                    if (!settings.IsVisualTest)
                    {
                        log.Message($"isFunctionalIphoneDailyBuild: {ConfigurationManager.AppSettings["IphoneDailyBuild"]}");

                        if (ConfigurationManager.AppSettings["IphoneDailyBuild"].Contains("true"))
                        {
                            var functionalIphoneDailyBuildHubPort = applicationMobileGridSettings.GetValues("FunctionalIphoneBuildHubPort").FirstOrDefault();
                            var macHubAddress = applicationMobileGridSettings.GetValues("SeleniumMobileHubHost").FirstOrDefault(); 
                            RemoteUri = $"http://{macHubAddress}:{functionalIphoneDailyBuildHubPort}/wd/hub";

                            return new IOSDriver<AppiumWebElement>(new Uri(RemoteUri), options,
                                TimeSpan.FromMinutes(DriverTimeoutInMinutes));
                        }
                    }

                    return new IOSDriver<AppiumWebElement>(new Uri(RemoteUri), options,
                        TimeSpan.FromMinutes(DriverTimeoutInMinutes));
                }
            }

            return new AndroidDriver<AppiumWebElement>(new Uri(RemoteUri), options,
                TimeSpan.FromMinutes(DriverTimeoutInMinutes));
        }

        /// <summary>
        /// Initialize a Chrome browser for testing. Option parameter to use Mobile View by specifying the use of an iPhone X. This is only to enter mobile view mode -
        /// There is no significance attached to the device being used.
        /// </summary>
        /// <returns>Chrome WebDriver configured per the solution configuration and provided parameters.</returns>
        private IWebDriver CreateChromeDriver(bool isHeadless = false, bool isMobileView = false, bool isTabletEmulationView = false, string testName  = null, string gridPort = null)
        {
            var options = new ChromeOptions
            {
                PlatformName = "LINUX",
                Proxy = ConfigureProxy(),
                PageLoadStrategy = PageLoadStrategy.Eager,
                AcceptInsecureCertificates = true
            };

            var applicationDesktopGridSettings = ConfigurationManager.GetSection("GridGroup/DesktopGrid") as NameValueCollection;

            if (RunLocalDriver) options.PlatformName = "windows";

            if (isMobileView)
            {
                options.EnableMobileEmulation("iPhone 12 Pro");
            }

            options.AddArgument("--no-sandbox");

            if (isTabletEmulationView)
            {
                options.EnableMobileEmulation("iPad Pro");
            }

            if (isHeadless) { options.AddArgument(HeadlessArgumentString); }

            if (testName.Contains("T7229_Windows_VerifyEcommerceGoogleDataPopulatedWithNoActiveABTest") ||
                testName.Contains("T7465_Windows_VerifyDataCapturePixelAttributesCorrect")
                || testName.Contains("T7532_Window_VerifyLayoutOfClearanceSortPage")) //NOTE: Pixel GA tests are pointed to separate Grid. To remove test T7532 from condition once Selenium Grid timeouts will be bumped to 180s.
            {
                var hubAdress = applicationDesktopGridSettings.GetValues("DataCaptureBmpInstanceHost").First();
                RemoteUri = $"http://{hubAdress}:{gridPort}/wd/hub";
            }

            //new DriverManager().SetUpDriver(new ChromeConfig());//WebDriverManager to automatically update Chromedriver

            return RunLocalDriver ? new ChromeDriver(options) : new RemoteWebDriver(new Uri(RemoteUri), options.ToCapabilities(), TimeSpan.FromMinutes(DriverTimeoutInMinutes));
        }

        /// <summary>
        /// Create a remote WebDriver for Mac Safari testing.
        /// NOTE: Local testing is not supported for this driver type.
        /// </summary>
        /// <returns>Remote Safari WebDriver.</returns>
        private IWebDriver CreateSafariDriver(SessionSettings settings)
        {
            var options = new SafariOptions();
            var applicationMobileGridSettings = ConfigurationManager.GetSection("GridGroup/MobileGrid") as NameValueCollection;

            var macHubAdress = applicationMobileGridSettings.GetValues("SeleniumMobileHubHost").FirstOrDefault();
            RemoteUri = $"http://{macHubAdress}:{settings.HubPort}/wd/hub";
            options.AddAdditionalCapability("platformName", "MAC");//Local runs
            options.AddAdditionalCapability("browserName", "safari");

            return new RemoteWebDriver(new Uri(RemoteUri), options.ToCapabilities(), TimeSpan.FromMinutes(DriverTimeoutInMinutes));
        }

        private void InitializeSettings(SessionSettings settings)
        {
            RunLocalDriver = settings.IsLocalEnvironment; 

            RemoteUri = $"http://{settings.HubIpAddress}:{settings.HubPort}/wd/hub";
            _proxyAddress = settings.ProxyAddress;
        }

        private Proxy ConfigureProxy()
        {
            var proxy = new Proxy
            {
                Kind = ProxyKind.Manual,
                IsAutoDetect = false,
                HttpProxy = _proxyAddress,
                SslProxy = _proxyAddress
            };
            proxy.AddBypassAddresses(@"wss://*;ws://*");

            return proxy;
        }
    }
}
