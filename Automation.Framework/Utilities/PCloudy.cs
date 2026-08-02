//using System;
//using System.Linq;
//using Automation.Framework.Enums;
//using Automation.Framework.Utilities;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Appium;
//using OpenQA.Selenium.Appium.Android;
//using OpenQA.Selenium.Appium.Enums;
//using OpenQA.Selenium.Appium.iOS;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Remote;
//using OpenQA.Selenium.Safari;
////using ssts.util.pCloudy;
////using ssts.util.pCloudy.AppiumAPIs;

//namespace Automation.Framework.Utilities
//{

//	public static class PCloudy
//	{
//		private const string PCloudyApiEndpoint = "https://private-poc.pcloudy.com"; //"https://device.pcloudy.com";
//		private const string PCloudyEmailId = "yuz@lampsplus.com"; //"mdiamant@lampsplus.com";
//		private const string PCloudyApiKey = "bvhtyymss9mgthq2n4fkvbjt"; //"f9p943kdxdbch4j9kjmc6x8h";

//		private const string PCloudyPlatformIos = "ios";
//		private const string PCloudyPlatformAndroid = "android";

//		private const string PCloudyBrowserSafari = "Safari";
//		private const string PCloudyBrowserChrome = "Chrome";

//		private const int PCloudyBookingDuration = 10;
//		public static RemoteWebDriver GetRemoteWebDriver()
//		{

//			try
//			{
//				var capabilities = new DesiredCapabilities();
//				capabilities.SetCapability("pCloudy_Username", PCloudyEmailId);
//				capabilities.SetCapability("pCloudy_ApiKey", PCloudyApiKey);
//				capabilities.SetCapability("pCloudy_DurationInMinutes", PCloudyBookingDuration);
//				//capabilities.SetCapability("pCloudy_DeviceFullName", "Apple_iPhoneX_ios_11.1.1");
//				capabilities.SetCapability("pCloudy_DeviceFullName", "Apple_iPhone11Pro_ios_13.0.0");
//				capabilities.SetCapability("automationName", "XCUITest"); //This is needed for ios 9.3 and later
//				capabilities.SetCapability("browserName", PCloudyBrowserSafari);
//				capabilities.SetCapability("newCommandTimeout", 600);
//				capabilities.SetCapability("launchTimeout", 90000);
//				//capabilities.SetCapability("platformVersion", "11.1.1");
//				capabilities.SetCapability("platformVersion", "13.0.0"); //It's mandatory after appium v1.9.1
//				capabilities.SetCapability("webkitResponseTimeout", 90000);

//				//browserName cannot be set together with bundleId capability
//				//capabilities.SetCapability("bundleId", "com.pcloudy.TestmunkDemo");
//				//capabilities.SetCapability("pCloudy_ApplicationName", "TestmunkDemo.Resigned1574709352.ipa");

//				var driver = new IOSDriver<IOSElement>(new Uri(PCloudyApiEndpoint + "/appiumcloud/wd/hub"),
//					capabilities, TimeSpan.FromSeconds(180));

//				return driver;
//			}
//			catch (Exception ex)
//			{
//				Exception newEx = new Exception("Error creating pCloudy iOS driver.", ex);
//				throw newEx;
//			}

//		}

//		public static RemoteWebDriver GetRemoteAndroidWebDriver()
//		{
//			try
//			{
//				var capabilities = new DesiredCapabilities();
//				capabilities.SetCapability("pCloudy_Username", PCloudyEmailId);
//				capabilities.SetCapability("pCloudy_ApiKey", PCloudyApiKey);
//				capabilities.SetCapability("pCloudy_DurationInMinutes", PCloudyBookingDuration);
//				capabilities.SetCapability("pCloudy_DeviceFullName", "LG_V30_android_8.0.0");
//				//capabilities.SetCapability("pCloudy_DeviceFullName", "SAMSUNG_GalaxyS8Plus_android_7.0.0");
//				//capabilities.SetCapability("automationName", "XCUITest"); //This is needed for ios 9.3 and later
//				capabilities.SetCapability("browserName", PCloudyBrowserChrome);
//				capabilities.SetCapability("newCommandTimeout", 600);
//				capabilities.SetCapability("launchTimeout", 90000);
//				capabilities.SetCapability("platformVersion", "8.0.0");   //It's mandatory after appium v1.9.1


//				var driver = new AndroidDriver<AndroidElement>(new Uri(PCloudyApiEndpoint + "/appiumcloud/wd/hub"), capabilities);
//				return driver;
//			}
//			catch (Exception ex)
//			{
//				Exception newEx = new Exception("Error creating pCloudy Android driver.", ex);
//				throw newEx;
//			}
//		}
//	}
//}
