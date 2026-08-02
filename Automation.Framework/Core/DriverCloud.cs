using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using Castle.Core.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using Automation.Framework.Utilities;

namespace Automation.Framework.Core
{
    public class DriverCloud : Driver
    {
        private string SauceUserName => !ConfigurationManager.AppSettings["SauceLabsBambooUserName"].IsNullOrEmpty() ? ConfigurationManager.AppSettings["SauceLabsBambooUserName"] : Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
        private string SauceAccessKey => !ConfigurationManager.AppSettings["SauceLabsBambooAccessKey"].IsNullOrEmpty() ? ConfigurationManager.AppSettings["SauceLabsBambooAccessKey"] : Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);

        public IWebDriver CreateMobileCloudDriver(MobileDevice device, SessionSettings settings, string vendorName, string testName)
        {
            var options = new AppiumOptions();
            var vendor = vendorName;
            var testingInstance = !settings.IsVisualTest ? settings.TargetInstance : settings.IsBaseLine ? settings.BaselineInstance : settings.TargetInstance;

            var applicationCloudSauceLabsSettings = ConfigurationManager.GetSection("CloudGroup/SauceLabs") as NameValueCollection;
            var applicationCloudPerfectoSettings = ConfigurationManager.GetSection("CloudGroup/Perfecto") as NameValueCollection;

            var bambooJobNumber = ConfigurationManager.AppSettings["BambooJobNumber"];
            var date = DateTime.Now.ToString("MM.dd.yyyy");
            var buildName = !settings.IsVisualTest ? $"Functional_Build_{bambooJobNumber}_{date}" : $"Visual_Build_{bambooJobNumber}_{date}";

            if (vendor.Equals("SauceLabs"))
            {
                if (device.AutomationLibrary == "XCUITest")
                {
                    options.AddAdditionalCapability("username", SauceUserName);
                    options.AddAdditionalCapability("accessKey", SauceAccessKey);

                    if (settings.IsVisualTest && !ConfigurationManager.AppSettings["VisualMobileAudit"].IsNullOrEmpty())
                    {
                        options.AddAdditionalCapability("appium:deviceName", ConfigurationManager.AppSettings["VisualMobileAudit"]);
                    }
                    else
                    {
                        options.AddAdditionalCapability("appium: deviceName", applicationCloudSauceLabsSettings.GetValues("deviceName").FirstOrDefault());
                    }

                    options.AddAdditionalCapability("platformName", applicationCloudSauceLabsSettings.GetValues("platformName").FirstOrDefault());

                    //Appium options
                    options.AddAdditionalCapability("appium:platformVersion", applicationCloudSauceLabsSettings.GetValues("platformVersion").FirstOrDefault());
                    options.AddAdditionalCapability("appium:newCommandTimeout", 180);
                    options.AddAdditionalCapability("appium:locationServicesEnabled", true);
                    options.AddAdditionalCapability("appium:locationServicesAuthorized", true);
                    options.AddAdditionalCapability("appium:autoAcceptAlerts", true);
                    options.AddAdditionalCapability("appium:safariInitialUrl", "https://www.lampsplus.com/denv.aspx?j=1");
                    options.AddAdditionalCapability("appium:automationName", "XCUITest");

                    //Sauce options
                    var sauceOptions = new Dictionary<string, object>();
                    sauceOptions.Add("appiumVersion", "2.0.0");
                    sauceOptions.Add("tunnelName", $"Lamps_Plus_tunnel_{testingInstance}");
                    sauceOptions.Add("tunnelOwner", "DBytsenko");
                    sauceOptions.Add("build", $"iOS_{buildName}");
                    sauceOptions.Add("phoneOnly", true);
                    sauceOptions.Add("name", testName.Substring(0, testName.LastIndexOf(".", StringComparison.Ordinal)));
                    options.AddAdditionalCapability("sauce:options", sauceOptions);

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, error) => true;

                    return new IOSDriver<AppiumWebElement>(new Uri($"https://{SauceUserName}:{SauceAccessKey}@ondemand.us-west-1.saucelabs.com:443/wd/hub"), options, TimeSpan.FromMinutes(DriverTimeoutInMinutes));
                }

                return new AndroidDriver<AppiumWebElement>(new Uri($"https://{SauceUserName}:{SauceAccessKey}@ondemand.us-west-1.saucelabs.com:443/wd/hub"), options, TimeSpan.FromMinutes(DriverTimeoutInMinutes));
            }
            if (vendor.Equals("LambdaTest"))
            {
                AppiumOptions caps = new AppiumOptions();

                //User settings
                caps.AddAdditionalCapability("user", "dbytsenko"); //Enter the Username here
                caps.AddAdditionalCapability("accessKey", "lpOgZ4RVk6HKUjyQAF0UphoXPBEtfbHboiw502k9aBiL7RqedR");  //Enter the Access key here

                // Specify device and os_version
                caps.AddAdditionalCapability("safariInitialUrl", "https://www.lampsplus.com/denv.aspx?j=1");
                caps.AddAdditionalCapability("automationName", "XCUITest");

                caps.AddAdditionalCapability("deviceName",
                    settings.IsVisualTest ? "iPhone 12$|iPhone 13$|iPhone 14$" : "iPhone.*");

                caps.AddAdditionalCapability("platformVersion", "17");
                caps.AddAdditionalCapability("platformName", "iOS");
                caps.AddAdditionalCapability("isRealMobile", true);
                caps.AddAdditionalCapability("visual", true);//Enables screenshots for tests

                //Tunnel settings
                caps.AddAdditionalCapability("tunnel", true);
                caps.AddAdditionalCapability("tunnelIdentifier", $"Lamps_Plus_tunnel_{testingInstance}");
                
                //Additional settings
                caps.AddAdditionalCapability("queueTimeout", 900);//LambdaTest Timeout for the devices allocation
                caps.AddAdditionalCapability("project", "Lamps Plus");
                caps.AddAdditionalCapability("build", $"iPhone_{buildName}");
                caps.AddAdditionalCapability("name", testName.Substring(0, testName.LastIndexOf(".", StringComparison.Ordinal)));

                return new IOSDriver<IOSElement>(new Uri("https://mobile-hub.lambdatest.com/wd/hub"), caps, TimeSpan.FromMinutes(3));
            }
            if (vendor.Equals("Perfecto"))
            {
                if (device.AutomationLibrary == "XCUITest")
                {
                    options.AddAdditionalCapability("platformName", applicationCloudPerfectoSettings.GetValues("platformName").FirstOrDefault());
                    options.AddAdditionalCapability("platformVersion", applicationCloudPerfectoSettings.GetValues("platformVersion").FirstOrDefault());
                    options.AddAdditionalCapability("location", applicationCloudPerfectoSettings.GetValues("location").FirstOrDefault());
                    options.AddAdditionalCapability("resolution", applicationCloudPerfectoSettings.GetValues("resolution").FirstOrDefault());
                    options.AddAdditionalCapability("manufacturer", applicationCloudPerfectoSettings.GetValues("manufacturer").FirstOrDefault());
                    options.AddAdditionalCapability("model", applicationCloudPerfectoSettings.GetValues("model").FirstOrDefault());

                    //options.AddAdditionalCapability("deviceName", applicationCloudPerfectoSettings.GetValues("deviceName").FirstOrDefault());//Alternative capability to use device unique id only instead of device attributes capabilities (Perfecto vendor only).
                    options.AddAdditionalCapability("tunnelId", applicationCloudPerfectoSettings.GetValues("tunnelId").FirstOrDefault());// Use mobile cloud vendor tunnel id, replace with the current one.
                    options.AddAdditionalCapability("securityToken", applicationCloudPerfectoSettings.GetValues("securityToken").FirstOrDefault());//mobile vendor security token for authorization.
                    options.AddAdditionalCapability("openDeviceTimeout", 5);//Wait for cloud device to become available.

                    return new IOSDriver<AppiumWebElement>(
                        new Uri("https://demo.perfectomobile.com/nexperience/perfectomobile/wd/hub"), options,
                        TimeSpan.FromMinutes(DriverTimeoutInMinutes));
                }

                return new AndroidDriver<AppiumWebElement>(
                    new Uri("https://testingcloud.perfectomobile.com/nexperience/perfectomobile/wd/hub"), options,
                    TimeSpan.FromMinutes(DriverTimeoutInMinutes));
            }

            throw new ArgumentException($"Error creating {vendor} vendor Appium driver.");
        }

        public IWebDriver CreateSafariCloudDriver(SessionSettings settings, string vendorName)
        {
            var testingInstance = !settings.IsVisualTest ? settings.TargetInstance :
                settings.IsBaseLine ? settings.BaselineInstance : settings.TargetInstance;

            var bambooJobNumber = ConfigurationManager.AppSettings["BambooJobNumber"];
            var date = DateTime.Now.ToString("MM.dd.yyyy");
            var buildName = !settings.IsVisualTest
                ? $"Mac_Functional_Build_{bambooJobNumber}_{date}"
                : $"Mac_Visual_Build_{bambooJobNumber}_{date}";

            if (vendorName.Equals("LambdaTest"))
            {
                SafariOptions options = new SafariOptions();
                options.BrowserVersion = "17.0";
                Dictionary<string, object> ltOptions = new Dictionary<string, object>();
                ltOptions.Add("username", "dbytsenko");
                ltOptions.Add("accessKey", "lpOgZ4RVk6HKUjyQAF0UphoXPBEtfbHboiw502k9aBiL7RqedR");
                ltOptions.Add("platformName", "MacOS Sonoma");
                ltOptions.Add("project", "Lamps Plus");
                ltOptions.Add("tunnel", true);
                ltOptions.Add("tunnelIdentifier", $"Lamps_Plus_tunnel_{testingInstance}");
                ltOptions.Add("build", buildName);
                ltOptions.Add("IdleTimeout", 240);
                ltOptions.Add("name", settings.SettingsTestName.Substring(0, settings.SettingsTestName.LastIndexOf(".", StringComparison.Ordinal)));
                options.AddAdditionalCapability("LT:Options", ltOptions);
                return new RemoteWebDriver(new Uri("https://hub.lambdatest.com/wd/hub/"), options.ToCapabilities(),
                    TimeSpan.FromMinutes(5));
            }
            if (vendorName.Equals("SauceLabs"))
            {
                var options = new SafariOptions();
                options.PlatformName = "macOS 12";
                options.BrowserVersion = "15";
                options.AddAdditionalCapability("username", SauceUserName);
                options.AddAdditionalCapability("accessKey", SauceAccessKey);
                options.AddAdditionalCapability("newCommandTimeout", 120);
                var sauceOptions = new Dictionary<string, object>();
                options.AddAdditionalCapability("sauce:options", sauceOptions);
                sauceOptions.Add("name", settings.SettingsTestName.Substring(0, settings.SettingsTestName.LastIndexOf(".", StringComparison.Ordinal)));
                sauceOptions.Add("tunnelName", $"Lamps_Plus_tunnel_{testingInstance}");
                var applicationCloudSauceLabsSettings = ConfigurationManager.GetSection("CloudGroup/SauceLabs") as NameValueCollection;
                sauceOptions.Add("tunnelOwner", applicationCloudSauceLabsSettings.GetValues("username").FirstOrDefault());//Defines the tunnel Admin owner, when use shared tunnels.
                sauceOptions.Add("screenResolution", "1920x1440");//Optional resolution parameter
                sauceOptions.Add("build", buildName); //Optional build parameter

                RemoteUri = $"https://{SauceUserName}:{SauceAccessKey}@ondemand.us-west-1.saucelabs.com:443/wd/hub";

                return new RemoteWebDriver(new Uri(RemoteUri), options.ToCapabilities(), TimeSpan.FromMinutes(DriverTimeoutInMinutes));
            }

            throw new ArgumentException($"Error creating {vendorName} vendor Selenium driver.");
        }

        public IWebDriver CreateChromeCloudDriver(SessionSettings settings) //Note: to be used for Desktop Chrome test automation in the cloud.
        {
            var SauceUserNameChrome = "";//Note: to be replaced with Bamboo build user credentials (Windows authentication).
            var SauceAccessKeyChrome = "";

            var testingInstance = !settings.IsVisualTest ? settings.TargetInstance : settings.IsBaseLine ? settings.BaselineInstance : settings.TargetInstance;

            var bambooJobNumber = ConfigurationManager.AppSettings["BambooJobNumber"];
            var date = DateTime.Now.ToString("MM.dd.yyyy");
            var buildName = !settings.IsVisualTest ? $"Chrome_Functional_Build_{bambooJobNumber}_{date}" : $"Chrome_Visual_Build_{bambooJobNumber}_{date}";

            var options = new ChromeOptions();
            options.PlatformName = "Windows 10";
            options.BrowserVersion = "latest";

            var sauceOptions = new Dictionary<string, object>();
            options.AddAdditionalCapability("sauce:options", sauceOptions, true);
            sauceOptions.Add("name", settings.SettingsTestName.Substring(0, settings.SettingsTestName.LastIndexOf(".", StringComparison.Ordinal)));
            sauceOptions.Add("tunnelName", $"Lamps_Plus_tunnel_{testingInstance}");
            var applicationCloudSauceLabsSettings = ConfigurationManager.GetSection("CloudGroup/SauceLabs") as NameValueCollection;
            sauceOptions.Add("tunnelOwner", applicationCloudSauceLabsSettings.GetValues("username").FirstOrDefault());//Defines the tunnel Admin owner, when use shared tunnels.
            sauceOptions.Add("build", buildName); ;//Optional build parameter
            sauceOptions.Add("screenResolution", "1920x1080");
            sauceOptions.Add("username", SauceUserNameChrome);
            sauceOptions.Add("accessKey", SauceAccessKeyChrome);

            RemoteUri = $"https://{SauceUserNameChrome}:{SauceAccessKeyChrome}@ondemand.us-west-1.saucelabs.com:443/wd/hub";

            return new RemoteWebDriver(new Uri(RemoteUri), options.ToCapabilities(), TimeSpan.FromMinutes(DriverTimeoutInMinutes));
        }
    }
}
